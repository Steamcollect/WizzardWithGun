using MVsToolkit.Attributes;
using MVsToolkit.Utilities;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float chunkSize;
    [SerializeField] int chunksLoadedRadius;
    [SerializeField] float mapHeight;

    [Space(5)]
    [SerializeField, ReadOnly] Vector2Int playerCurrentChunk;

    [Header("References")]
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunksParent;

    Transform[] chunks;

    [Space]
    [SerializeField] RSO_PlayerTransform rsoPlayerTransform;

    //[Header("Input")]
    //[Header("Output")]

    private void Start()
    {
        playerCurrentChunk = Vector2Int.zero;
        Initialize();
    }

    private void FixedUpdate()
    {
        UpdateChunksPosition();
    }

    [Button]
    void Initialize()
    {
        ClearChunks();
        int[] rotations = new int[4] { 0, 90, 180, 270 };

        for (int x = -chunksLoadedRadius; x <= chunksLoadedRadius; x++)
        {
            for (int y = -chunksLoadedRadius; y <= chunksLoadedRadius; y++)
            {
                int size = chunksLoadedRadius * 2 + 1;
                int i = (x + chunksLoadedRadius) * size + (y + chunksLoadedRadius);

                chunks[i] = Instantiate(chunkPrefab, chunksParent).transform;
                chunks[i].position = new Vector3(x * chunkSize, mapHeight, y * chunkSize);
                chunks[i].rotation = Quaternion.Euler(0, rotations.GetRandom(), 0);
            }
        }
    }

    void UpdateChunksPosition()
    {
        Vector2 playerPos = rsoPlayerTransform.Get().position.ToVector2();
        Vector2 currentChunkPos = (Vector2)playerCurrentChunk * chunkSize;
        Vector2Int newPlayerCurrentChunk = playerCurrentChunk;

        //===Check new player position
        if (playerPos.x < currentChunkPos.x - chunkSize * .5f)
            newPlayerCurrentChunk.x--;
        else if (playerPos.x > currentChunkPos.x + chunkSize * .5f)
            newPlayerCurrentChunk.x++;

        if(playerPos.y < currentChunkPos.y - chunkSize * .5f)
            newPlayerCurrentChunk.y--;
        else if(playerPos.y > currentChunkPos.y + chunkSize * .5f)
            newPlayerCurrentChunk.y++;
        //===

        if(newPlayerCurrentChunk !=  playerCurrentChunk)
        {

            playerCurrentChunk = newPlayerCurrentChunk;
        }

        MVsDebug.DrawCircle(
                    new Vector3(playerCurrentChunk.x, mapHeight, playerCurrentChunk.y) * chunkSize,
                    1,
                    Vector3.up,
                    Color.red);
    }

    [Button]
    void ClearChunks()
    {
        if (chunks != null && chunks.Length > 0)
        {
            for (int i = 0; i < chunks.Length; i++)
            {
                if (chunks[i] != null)
                {
                    if(Application.isPlaying)
                        Destroy(chunks[i].gameObject);
                    else
                        DestroyImmediate(chunks[i].gameObject);
                }
            }
        }

        int size = chunksLoadedRadius * 2 + 1;
        chunks = new Transform[size * size];
    }

    private void OnDrawGizmos()
    {
        if (chunkSize <= 0 || chunksLoadedRadius <= 0)
            return;

        Gizmos.color = Color.green;

        int size = chunksLoadedRadius * 2 + 1;

        for (int x = -chunksLoadedRadius; x <= chunksLoadedRadius; x++)
        {
            for (int y = -chunksLoadedRadius; y <= chunksLoadedRadius; y++)
            {
                Vector3 center = new Vector3(x * chunkSize, mapHeight, y * chunkSize);

                // Coins du chunk
                Vector3 p1 = center + new Vector3(-chunkSize * 0.5f, 0, -chunkSize * 0.5f);
                Vector3 p2 = center + new Vector3(-chunkSize * 0.5f, 0, chunkSize * 0.5f);
                Vector3 p3 = center + new Vector3(chunkSize * 0.5f, 0, chunkSize * 0.5f);
                Vector3 p4 = center + new Vector3(chunkSize * 0.5f, 0, -chunkSize * 0.5f);

                // Lignes
                Gizmos.DrawLine(p1, p2);
                Gizmos.DrawLine(p2, p3);
                Gizmos.DrawLine(p3, p4);
                Gizmos.DrawLine(p4, p1);
            }
        }
    }
}