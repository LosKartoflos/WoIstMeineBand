using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //settings
    public float originalSpeed = 5f;
    private float speed;

    //allowed walking directions
    private bool left = false, right = false, forward = false, back = false, oldLeft = false, oldRight = false, oldForward = false, oldBack = false;

    List<GameObject> currentCollisions = new List<GameObject>();

    private void Start()
    {
        speed = originalSpeed;
    }

    void Update () {

        //input/movement part
        if (Input.GetKey("w") && forward == true )
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") && back == true )
        {

            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") && left == true )
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") && right == true)
        {

            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
    }

    
    public void OnTriggerEnter(Collider col)
    {
        SetupTile tileScript = col.gameObject.GetComponent<SetupTile>();
        
        setAllowedDirections(tileScript);
        Debug.Log("Tile: " + tileScript.name +"\nL: " + tileScript.left + "; R: " + tileScript.right + "; F: " + tileScript.forward + "; B: " + tileScript.back + ";    " + left + ", " + right + ", " + forward + ", " + back);

    }

    // set the allowed walking directions depending on the last tile entered und the one before;
    private void setAllowedDirections(SetupTile _tileScript)
    {
        //right
        if (_tileScript.right == true)
        {
            oldRight = right;
            right = true;
        }
        else
        {
            oldRight = right;
            right = false;
        }
        //left
        if (_tileScript.left == true)
        {
            oldLeft = left;
            left = true;
        }
        else
        {
            oldLeft = left;
            left = false;
        }
        //foward
        if (_tileScript.forward == true)
        {
            oldForward = forward;
            forward = true;
        }
        else
        {
            oldRight = right;
            right = false;
        }
        //back
        if (_tileScript.back == true)
        {
            oldBack = back;
            back = true;
        }
        else
        {
            oldBack = back;
            back = false;
        }

    }


   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "sidewalk")
        {
            foreach (GameObject gObject in currentCollisions)
            {
                print(gObject.name);
            }
        }
    }*/


    //check on which direction the player is allowed to walk depending on walkway part he is colliding

    void directionManager(string tag)
    {

    }
}
