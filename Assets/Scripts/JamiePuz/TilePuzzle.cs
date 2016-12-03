using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class TilePuzzle : MonoBehaviour {

    public List<GameObject> m_gameTiles = new List<GameObject>();

    public string[,] currentGame;
    bool first = true;

    [SerializeField]
    private bool Fun = false;

    AudioClip currentSound;
    public AudioSource soundSource;
    public Sprite[] mySprites;
    public AudioClip[] myAudio;
    public AudioClip correctTileDing;
    private AudioSource ding;

    float m_tileTimeStamp = 0;
    private int height = 8;
    private int width = 8;
    int index = 0;

    TileData currentTile;
    TileData tempTile;

    [SerializeField]
    private GameObject m_Tileprefab;
    [SerializeField]
    private Transform m_startPos;
    public Transform m_tempPos;

	void Start () {
        ding = GameObject.Find("Ding").GetComponent<AudioSource>();
        m_tempPos = m_startPos;
        createTiles();
	}

	void Update () {
        viewingPuzzle();
	}

    void createTiles()
    {
        currentGame = grabGame();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject obj = Instantiate<GameObject>(m_Tileprefab);
                m_tempPos.transform.position = new Vector3(i * 1.1f, j * 1.1f);
                obj.transform.position = m_tempPos.transform.position;
                m_gameTiles.Add(obj);
                PopulateTile(currentGame, i, j, index);
                index++;
            }
        }
    }

    public void flipTile(TileData td, bool flipped = true)
    {
        td.flipped = flipped;
    }

    public void viewingPuzzle()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10))
        {
            TileData tileItem = hit.collider.GetComponent<TileData>();
            currentSound = tileItem.GetComponent<TileData>().tilesound;
            if (!soundSource.isPlaying) {
               
               soundSource.pitch = tileItem.audioPitch;
               soundSource.PlayOneShot (currentSound);

        }
       //LOOKING AT A NEW TILE
            if (tileItem != currentTile)
            {
                if (tileItem != null)
                {
                    //this is the first loop thougth - found the new tile for the first time
                    m_tileTimeStamp = Time.time + 2;
                }
                else
                {
                    m_tileTimeStamp = 0;
                }
            }
            
       //LOOKING AT THE SAME TILE
            else if (currentTile != null)
            {
                if (Time.time > m_tileTimeStamp && m_tileTimeStamp != 0)
                {
                    if (tempTile != null)
                    {
                       

                        if (CheckMove())
                        {
                            Transform t = currentTile.transform.parent.Find("Marker");
                            //add ding
                            
                            if (t.gameObject && !ding.isPlaying)
                            {
                                ding.PlayOneShot(correctTileDing);
                            }
                            
                            t.gameObject.SetActive(true);
                            
                            // flipTile(currentTile);
                        }
                     

                        else if( currentTile.person && !first)
                        {
                            Debug.Log("TEST");
                            reSetTiles();
                        }
                        else if(Fun )
                        {
                            //StartCoroutine(wrongMove());
                            reSetTiles();
                        }

                    }
                    else
                    {
                        if (currentTile.person && first)
                        {
                            tempTile = currentTile;
                            Debug.Log("StartingPath");
                           // flipTile(currentTile);
                            first = false;
                        }
                    
                    }
                 
                }
            }
            currentTile = tileItem;
        }
        else
        {
            soundSource.Stop();
            //raycast return nothing 
            currentTile = null;
        }
        
    }

    public bool CheckMove()
    {
         if (tempTile.color == currentTile.color || tempTile.icon == currentTile.icon || tempTile.person == true)
        {
            
            //if the temp and the current share a property
            if (CheckDist())
            {
                
                //add ding
                //if the tile is next to or below
                {
                    tempTile = currentTile;
                    if (currentTile.pBag == true)
                    {
                        reSetTiles();
                        PlayerPrefs.SetInt("puzzleDone", 1);
                        SceneManager.LoadScene("Main_scene");
                        return false;
                    }
                    return true;
                }
            }
        }
       
            //StartCoroutine(wrongMove());
            return false;
         
    }

    public bool CheckDist()
    {
        int dx = Mathf.Abs(currentTile.col - tempTile.col);
        int dy = Mathf.Abs(currentTile.row - tempTile.row);

        return (dx + dy) <= 1;
    }

    IEnumerator wrongMove()
    {
        
            currentGame = grabGame();
            foreach (GameObject td in m_gameTiles)
            {
                td.GetComponentInChildren<TileData>().pBag = false;
            }
            PopulateTiles();
            foreach (GameObject td in m_gameTiles)
            {

                td.GetComponentInChildren<TileData>().flipped = true;
            }
            yield return new WaitForSeconds(2);
            reSetTiles();
        
    }

    public void reSetTiles()
    {
        foreach (GameObject td in m_gameTiles)
        {
            td.GetComponentInChildren<TileData>().flipped = false;
            Transform t = td.transform.Find("Marker");

            t.gameObject.SetActive(false);

        }
        tempTile = null;
        first = true;
    }

    public string[,] grabGame()
    {
        int num = UnityEngine.Random.Range(1, 5);
        switch (num)
        {
            case 1:
                return Games.game1;
                break;                  //Unreachable code detected
            case 2:
                return Games.game1;
                break;                  //Unreachable code detected
            case 3:
                return Games.game1;
                break;                  //Unreachable code detected
            case 4:
                return Games.game1;
                break;                  //Unreachable code detected
            case 5:
                return Games.game1;
                break;                  //Unreachable code detected
            default:
                return Games.game1;


        }
    } 

    void PopulateTiles()
    {
        index = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                PopulateTile(currentGame, i, j, index);
                index++;
            }
        }
    }

    void PopulateTile(string[,] data, int j, int i, int index)
    {
        string test = data[j, i].ToString();
        if (test.ToString() != null)
        {
            string color = test.Substring(0, 1);
            string icon = test.Substring(1, 1);

            m_gameTiles[index].GetComponentInChildren<TileData>().row = i;
            m_gameTiles[index].GetComponentInChildren<TileData>().col = j;

            switch (color)
            {
                case "G":
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    m_gameTiles[index].GetComponentInChildren<TileData>().audioPitch = 1;
                    break;
                case "W":
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    m_gameTiles[index].GetComponentInChildren<TileData>().audioPitch = 0.75f;

                    break;
                case "R":
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    m_gameTiles[index].GetComponentInChildren<TileData>().audioPitch = 0.5f;
                    break;
                case "B":
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                    m_gameTiles[index].GetComponentInChildren<TileData>().audioPitch = 1.5f;
                    break;
                case "X":
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().color = Color.black;
                    m_gameTiles[index].GetComponentInChildren<TileData>().audioPitch = 1.5f; // not used
                    break;
                case "P":
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                    m_gameTiles[index].GetComponentInChildren<TileData>().person = true;
                    m_gameTiles[index].GetComponentInChildren<TileData>().audioPitch = 1f;

                    break;
            }
            m_gameTiles[index].GetComponentInChildren<TileData>().color = color;

            switch (icon)
            {

                case "A":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio [13];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[13];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "A";
                    break;
                case "B":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[0];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[0];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "B";
                    break;
                case "C":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[2];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[2];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "C";
                    break;
                case "D":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[19];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[19];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "D";
                    break;
                case "E":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[4];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[4];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "E";
                    break;
                case "F":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[7];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[7];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "F";
                    break;
                case "G":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[8];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[8];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "G";
                    break;
                case "H":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[9];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[9];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "H";
                    break;
                case "I":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[3];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[3];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "I";
                    break;
                case "J":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[14];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[14];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "J";
                    m_gameTiles[index].GetComponentInChildren<TileData>().pBag = true;

                    break;
                case "K":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[15];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[15];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "K";
                    break;
                case "L":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[3];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[3];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "L";
                    break;
                case "M":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[10];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[10];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "M";
                    break;
                case "N":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[11];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[11];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "N";
                    break;
                case "O":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[16];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[16];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "O";
                    break;
               
                case "Q":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[12];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[12];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "Q";
                    break;
                case "R":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[1];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[1];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "R";
                    break;
                case "S":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[17];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[17];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "S";
                    break;
                case "T":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[6];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[6];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "T";
                    break;
                case "U":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[5];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[5];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "U";
                    break;
                case "V":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[3];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[3];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "V";
                    break;
                case "W":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[20];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[20];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "W";
                    break;
                case "X":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[3];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[3];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "X";
                    break;
                case "Y":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[3];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[3];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "Y";
                    break;
                case "Z":
                    m_gameTiles[index].GetComponentInChildren<TileData>().tilesound = (AudioClip)myAudio[18];
                    m_gameTiles[index].GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)mySprites[18];
                    m_gameTiles[index].GetComponentInChildren<TileData>().icon = "Z";
                    break;
            }

        }
    }

}
