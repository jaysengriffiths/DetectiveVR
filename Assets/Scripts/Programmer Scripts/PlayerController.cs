using UnityEngine;
using System.Collections;

//JB to work on
public class PlayerController : MonoBehaviour
{
    //enum statemachine
    //walking
        //-special power
        //-interaction
        //-interogate
            //-arrest
            //-talk
            //-warn

    void Start()
    {

    }
    void Update()
    {

    }

    void Movement()
    {
        //Player movement
    }

    void CollideWithObject(GameObject obj)
    {
        //sound
        //nothing
        //NCP
        //object
    }

    void UseDetectivePower()
    {
        //lookat sky activates power
    }

    void InteractWithNPC()
    {
        //inital lookat & dialogue || 2nd time talking within mission
        //question/interview
        //arrest/warn
    }

    void InteractWithClue()
    {
        //lookat sound
        //pickup obj rotate
    }

    void ReturnHome()
    {
        //once gazed at building
    }

    //public class Movement : MonoBehaviour
    //{

    //    public Transform trans;
    //    private Rigidbody player;
    //    public float minBounds = 10;
    //    public float maxBounds = 20;
    //    int counter;
    //    public int moveTime = 20;
    //    public float speed = 0.04f;
    //    // Use this for initialization
    //    void Start()
    //    {
    //        float cameraAngle = Camera.main.transform.rotation.x;
    //        trans = gameObject.GetComponent<Transform>();
    //        player = gameObject.GetComponent<Rigidbody>();
    //        counter = 0;
    //    }

    //    // Update is called once per frame
    //    private void Update()
    //    {
    //        float cameraAngle = Camera.main.transform.eulerAngles.x;

    //        if (cameraAngle > minBounds && cameraAngle < maxBounds)
    //            counter++;
    //        else
    //            counter = 0;
    //        if (counter > moveTime)
    //            isMoving();
    //    }

    //    void isMoving()
    //    {
    //        Vector3 fwd = Camera.main.transform.forward;
    //        fwd.y = 0;
    //        player.transform.position = player.transform.position + speed * fwd;
    //    }

    //}
}
