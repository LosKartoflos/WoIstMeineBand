using UnityEngine;


public class Camera : MonoBehaviour {

    //for the position of the player
    public GameObject player;

    //set angle in beginning
    private void Start()
    {
        
    }

    // Set Camera on player
    void Update () {
        transform.position = player.transform.position + new Vector3(0, 14, -13); // maybe change if player pivot is on  the ground
        
    }
}
