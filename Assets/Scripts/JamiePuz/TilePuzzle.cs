using UnityEngine;
using System.Collections;

public class TilePuzzle : MonoBehaviour {

    private TileData[,] gameTiles;

    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;

    float m_tileTimeStamp = 0;
    private int width = 7;
    private int height = 8;
    TileData currentTile;

    [SerializeField]
    private GameObject m_Tileprefab;
    [SerializeField]
    private Transform m_startPos;

    public Transform m_tempPos;




	// Use this for initialization
	void Start () {
        m_tempPos = m_startPos;
        createTiles();

	}
	
	// Update is called once per frame
	void Update () {
        viewingPuzzle();
	}

    void createTiles()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject obj = Instantiate<GameObject>(m_Tileprefab);
                m_tempPos.transform.position = new Vector3(i * 1.1f, j * 1.1f);
                obj.transform.position = m_tempPos.transform.position;
            }
        }

    }

    public void flipTile()
    {
        this.transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void viewingPuzzle()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
        {
            TileData tileItem = hit.collider.GetComponent<TileData>();
            //if looking at new tile
            if(tileItem != currentTile)
            {
                if(tileItem != null)
                {
                    //this is the first loop thougth - found the new tile foir the first time
                    m_tileTimeStamp = Time.time + 2;
                  
                }
                else
                {
                    m_tileTimeStamp = 0;
                }

            }

            
            //looking at the same item
            else
            {
               if(Time.time >  m_tileTimeStamp &&  m_tileTimeStamp != 0)
                {
                    System.Console.WriteLine("tile");
                    currentTile.GetComponent<MeshRenderer>().material.color = colorEnd;

                }
            }
            currentTile = tileItem;

        }
        else
        {
            //raycast return nothing 
            currentTile = null;
        }

    }

}
