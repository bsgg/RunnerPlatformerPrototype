using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject m_TilePrefab;
    [SerializeField] private GameObject m_PlayerPrefab;

    private Queue<TileChunk> m_TileChunksQueue = new Queue<TileChunk>();
    private TileChunk m_NextTileChunk;

    private  GameObject m_Player;

    private int m_Diff = 0;
    

    

    // Number of tile chunks visibles by the player
    private const int NTILECHUNKVISIBLES = 5;

    // Each tile chunk is 9 units long.
    // It makes of 9 tiles
    private const float LEAP_CHUNCK = 9.0f;
    private float m_TileChunkZPos = 0.0f;

    [SerializeField] private int m_stx;
    [SerializeField] private int m_stz;


    void Start()
    {
        Init();
    }

    private void Init()
    {        
        m_Player = Instantiate(m_PlayerPrefab) as GameObject;
        m_TileChunkZPos = 0.0f;
        for (int i = 0; i < NTILECHUNKVISIBLES; ++i)
        {
            AddTile();
        }

        // Init camera
        Camera.main.gameObject.GetComponent<CameraControl>().Init(m_Player.transform);
        StartCoroutine(UpdateRoutine());
    }

    private void AddTile()
    {
        TileChunk tc = (Instantiate( m_TilePrefab,new Vector3(0, 0, m_TileChunkZPos), Quaternion.identity) as GameObject).GetComponent<TileChunk>();
        // Update 
        m_TileChunkZPos += LEAP_CHUNCK;
        tc.Init(m_Diff++);
        m_TileChunksQueue.Enqueue(tc);
    }

    private IEnumerator UpdateRoutine()
    {
        while (true)
        {
            // No next tile, take the first tile from the queue
            if (m_NextTileChunk == null)
            {
                m_NextTileChunk = m_TileChunksQueue.Dequeue();
            }

            // Check position player
            if (((m_NextTileChunk.transform.position.z) + 13.5f) < m_Player.transform.position.z)
            {
                Destroy(m_NextTileChunk.gameObject);
                AddTile();
                m_NextTileChunk = null;
            }

            // Get tile at the beginning of the queue
            TileChunk tc = m_TileChunksQueue.Peek();

            // ??
            m_stx = (int)m_Player.transform.position.x;
            m_stx = (m_stx + 3) / 3;

            m_stz = (int)m_Player.transform.position.z;
            m_stz = (m_stz + 3) / 3;
            m_stz = m_stz % 3;
            m_stz = 2 - m_stz;

            /*if (tc.Collision(m_stx, m_stz) != TileControl.Tipos.Libre)
            {
                Debug.Log("!!!!!");
            }*/


            yield return new WaitForEndOfFrame();
        }

    }
}
