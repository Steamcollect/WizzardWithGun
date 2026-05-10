using MVsToolkit.Attributes;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_EntitySpawningWeapon", menuName = "SSO/Entity/SSO_EntitySpawningWeapon")]
public class SSO_EntitySpawningWeapon : ScriptableObject
{
    [Inline] public EntitySpawningWeapon[] Wepons;

    [System.Serializable]
    public struct EntitySpawningWeapon
    {
        public WeaponType Type;
        public float SpawningProbability;
    }

    [SerializeField, DrawInRect("DrawProbabilitySlider", 300)]
    int ProbabilityDraw;

    public EntitySpawningWeapon GetRandomWeapon()
    {
        if (Wepons == null || Wepons.Length == 0)
            return default;

        float total = 0f;
        for (int i = 0; i < Wepons.Length; i++)
            total += Mathf.Max(0f, Wepons[i].SpawningProbability);

        if (total <= 0f)
            return default;

        float r = Random.value * total;

        float cumulative = 0f;
        for (int i = 0; i < Wepons.Length; i++)
        {
            cumulative += Mathf.Max(0f, Wepons[i].SpawningProbability);
            if (r <= cumulative)
                return Wepons[i];
        }

        return Wepons[Wepons.Length - 1];
    }


#if UNITY_EDITOR
    void DrawProbabilitySlider(Rect r)
    {
        if (Wepons == null || Wepons.Length == 0)
            return;

        var list = new System.Collections.Generic.List<EntitySpawningWeapon>(Wepons);
        list.Sort((a, b) => b.SpawningProbability.CompareTo(a.SpawningProbability));

        float total = 0f;
        foreach (var w in list)
            total += Mathf.Max(0f, w.SpawningProbability);

        if (total <= 0f)
            return;

        const float sliderHeight = 12f;
        const float labelHeight = 16f;
        const float labelSpacing = 2f;

        Rect barRect = new Rect(
            r.x,
            r.y + (labelHeight + labelSpacing) * list.Count,
            r.width,
            sliderHeight
        );

        EditorGUI.DrawRect(barRect, new Color(0.15f, 0.15f, 0.15f));

        float x = barRect.x;
        int count = list.Count;

        for (int i = 0; i < count; i++)
        {
            var w = list[i];
            float normalized = Mathf.Max(0f, w.SpawningProbability) / total;
            float width = barRect.width * normalized;

            if (width <= 0.5f)
                continue;

            float hue = (float)i / Mathf.Max(1, count);
            Color c = Color.HSVToRGB(hue, 0.8f, 0.9f);

            Rect segRect = new Rect(x, barRect.y, width, barRect.height);
            EditorGUI.DrawRect(segRect, c);

            float percent = normalized * 100f;
            string label = $"{w.Type} ({percent:0.#}%)";

            // Centre du segment
            float midX = x + width * 0.5f;

            // Taille réelle du texte
            Vector2 textSize = EditorStyles.label.CalcSize(new GUIContent(label));
            float labelWidth = textSize.x;

            // Position idéale centrée
            float labelX = midX - labelWidth * 0.5f;

            // Clamp pour ne jamais dépasser
            if (labelX < r.x)
                labelX = r.x;

            if (labelX + labelWidth > r.x + r.width)
                labelX = r.x + r.width - labelWidth;

            Rect labelRect = new Rect(
                labelX,
                r.y + i * (labelHeight + labelSpacing),
                labelWidth,
                labelHeight
            );

            var style = EditorStyles.label;
            style.clipping = TextClipping.Overflow;
            style.alignment = TextAnchor.MiddleLeft;

            GUI.Label(labelRect, label, style);

            // Trait vertical label → segment
            Handles.color = c;
            Handles.DrawLine(
                new Vector3(midX, labelRect.yMax, 0),
                new Vector3(midX, barRect.y, 0)
            );

            x += width;
        }
    }
#endif
}