using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Camera playerCam;
    public float speed;

    private GameObject target;
    public float targetDistance;

    void FixedUpdate()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {
            Debug.Log("I'm looking at " + hit.transform.name);


            if (hit.transform.tag == "mapStar")
                    {
                        SceneManager.LoadScene("LostCat");
                    }
            else if
                    (hit.transform.tag == "Object")
                    {
                       //identify the object hit
                       target = hit.transform.gameObject;
                       //find the object's position
                       Vector3 targetPosition = target.transform.position;
                       //change the object's position so y is level with player's middle
                       targetPosition = new Vector3(targetPosition.x, 0.9f, targetPosition.z);
                        
                        //only move if still outside object
                        if (Vector3.Distance(transform.position, targetPosition) > targetDistance)
                            {//set the speed for the player to travel to the object
                                float step = speed * Time.deltaTime;
                                //make the player travel to the object
                                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                            }
                     }
          }
     }
}
