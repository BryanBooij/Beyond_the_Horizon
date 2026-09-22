using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Chunk
    {
        public string chunkName;
        public GameObject chunkPrefab;
    }

    public Chunk[] chunks;

    private int currentChunk = -1;
    private GameObject activeChunk;

    void Start()
    {
        LoadNextChunk();
    }

    public void LoadNextChunk()
    {
        currentChunk++;

        if (currentChunk >= chunks.Length)
        {
            LevelComplete();
            return;
        }

        if (activeChunk != null)
        {
            Destroy(activeChunk);
        }

        activeChunk = Instantiate(chunks[currentChunk].chunkPrefab);

        Debug.Log("Started chunk: " + chunks[currentChunk].chunkName);
    }

    public void ChunkComplete()
    {
        LoadNextChunk();
    }

    void LevelComplete()
    {
        Debug.Log("LEVEL COMPLETE!");
    }
}