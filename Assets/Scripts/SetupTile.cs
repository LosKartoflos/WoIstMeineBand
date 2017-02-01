using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class SetupTile : MonoBehaviour {
    //this class sets up the tiles in the begining:
    //the allowed walking directions and the texture,
    //depending on walking directions

    //allowed walking directions
    public bool left = false, right = false, forward = false, back = false;

    void Start () {
        left = false;
        right = false;
        forward = false;
        back = false;
        //checks for colliders of other sidwalks to set the allowed walking directions
        //(**to do: check if there is a baking option to save the level an do it before)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.1f);
        int i = 0;
        while (i < hitColliders.Length)
        {   
            //sort the sidewalks out by tag and check if it is the same
            if(hitColliders[i].tag == "sidewalk" && hitColliders[i].transform.position != transform.position)
            {
                
                SetAllowedDirections(hitColliders[i].gameObject);
            }

            i += 2;
            // because it detects the mesh and the box collider of the same 
            //(**to do: if there are  detections problems change it to i++)
        }
    }

    

    
    private void SetAllowedDirections(GameObject collided)
    {
        //sets the allowed walking directions depending
        //on the position of the detected neighbours

        if (collided.transform.position - Vector3.left == transform.position)
            left = true;
        if (collided.transform.position - Vector3.right == transform.position)
            right = true;
        if (collided.transform.position - Vector3.forward == transform.position)
            forward = true;
        if (collided.transform.position - Vector3.back == transform.position)
            back = true;

        //check the directions in console
        // Debug.Log(this.name + ": left: " + left + ", right: " + right + ", forward: " + forward + ", back: " + back );

    }
	
}
