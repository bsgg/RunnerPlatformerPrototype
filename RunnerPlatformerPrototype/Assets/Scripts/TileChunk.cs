using UnityEngine;
using System.Collections;

public class TileChunk : MonoBehaviour
{
    public enum TypeChunk : byte
    {
        FREE,
        TAKEN,
        HIGH,
        LOWER
    };

    enum State : byte
    {
        ADVANCE,
        SWAP,
        OBSTACLE
    };

    static State m_State = State.ADVANCE;
    static int m_FreeWay = 1;

    private const int m_NUMBERTILES = 9;
    private const int m_NTILESAROW = 3;
    private const int m_NTILESACOL = 3;
    TypeChunk[] m_TypeTiles = new TypeChunk[m_NUMBERTILES];

    public void Init(int difficulty = 100)
    {
        string[] nameTiles = new string[] { "11", "12", "13", "21", "22", "23", "31", "32", "33" };

        // Procedural tiles
        int val;
        for (int y = 0; y < m_NTILESAROW; ++y)
        {
            // Set taken tiles in this row
            for (int x = 0; x < m_NTILESACOL; ++x)
            {
                TypeChunk act = TypeChunk.FREE;

                // Random chance to put the tile in taken tile
                val = Random.Range(0, 100);
                if (val < difficulty) act = TypeChunk.TAKEN;
                m_TypeTiles[y * m_NTILESAROW + x] = act;
            }


            val = Random.Range(0, 100);
            m_TypeTiles[y * m_NTILESAROW + m_FreeWay] = TypeChunk.FREE;
            if (m_State == State.ADVANCE)
            {
                if (val > 25)
                {
                    // Swap direction
                    if (val > 50)
                    {
                        if (m_FreeWay > 0)
                        {
                            m_FreeWay--;
                        }
                    }
                    else if (m_FreeWay < 2)
                    {
                        m_FreeWay++;
                    }
                    m_TypeTiles[y * m_NTILESAROW + m_FreeWay] = TypeChunk.FREE;
                    m_State = State.SWAP;
                }
                else
                {
                    val = Random.Range(0, 100);
                    if (val > 50)
                    {
                        m_TypeTiles[y * m_NTILESAROW + m_FreeWay] = TypeChunk.LOWER;
                    }
                    else
                    {
                        m_TypeTiles[y * m_NTILESAROW + m_FreeWay] = TypeChunk.HIGH;
                    }
                    m_State = State.OBSTACLE;
                }
            }
            else
            {
                m_State = State.ADVANCE;
            }
        }

        // Set visual tiles
        for (int i = 0; i < m_NUMBERTILES; ++i)
        {
            Transform chl = transform.Find(nameTiles[i]);
            Vector3 pos = chl.position;
            Vector3 esc = chl.localScale;

            switch (m_TypeTiles[i])
            {
                case TypeChunk.FREE:
                    chl.gameObject.SetActive(false);
                    break;

                case TypeChunk.TAKEN:
                    chl.gameObject.SetActive(true);
                    esc.y = 30;
                    pos.y = 1.5f;
                    break;

                case TypeChunk.LOWER:
                    chl.gameObject.SetActive(true);
                    esc.y = 10;
                    pos.y = 0.5f;
                    break;

                case TypeChunk.HIGH:
                    chl.gameObject.SetActive(true);
                    esc.y = 10;
                    pos.y = 1.5f;
                    break;
            }

            chl.position = pos;
            chl.localScale = esc;
        }
    }
}
