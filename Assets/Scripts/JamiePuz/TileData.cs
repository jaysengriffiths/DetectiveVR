using UnityEngine;
using System.Collections;


public class TileData : MonoBehaviour
{
    private bool startPoint;

    public string color;
    public string icon;
    public int row;
    public int col;

    public bool flipped = false;
    public bool person = false;
    public bool pBag = false;
    public AudioClip tilesound;
    public float audioPitch = 1; 

    void Update()
    {
        float desiredAngle = flipped ? 180 : 0;
        Vector3 angles = transform.parent.eulerAngles;
        angles.y = Mathf.Lerp(angles.y, desiredAngle, 0.05f);
        transform.parent.eulerAngles = angles;
    }
}
