using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //settings
    public float originalSpeed = 5f;
    private float speed;

    //save the current and the last tile standing on
    SetupTile tileScriptCurrent;
    SetupTile tileScriptOld;
    SetupTile activeTile;

    //allowed walking directions
    //delete private bool left = false, right = false, forward = false, back = false, oldLeft = false, oldRight = false, oldForward = false, oldBack = false;


    private void Start()
    {
        speed = originalSpeed;
    }

    void Update () {

        //input/movement part
        if (Input.GetKey("w") )
        {
            moveFigure(checkIfWalkdirectionIsAllowed("forward", tileScriptCurrent, tileScriptOld));
        }
        if (Input.GetKey("s") )
        {
            moveFigure(checkIfWalkdirectionIsAllowed("back", tileScriptCurrent, tileScriptOld));
        }
        if (Input.GetKey("a"))
        {
            moveFigure(checkIfWalkdirectionIsAllowed("left", tileScriptCurrent, tileScriptOld));
        }
        if (Input.GetKey("d") )
        {
            moveFigure(checkIfWalkdirectionIsAllowed("right", tileScriptCurrent, tileScriptOld));
        }
    }

    private void moveFigure(string _direction)
    {
        if (_direction == "forward")
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        else if(_direction == "back")
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        else if(_direction == "left")
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        else if(_direction == "right")
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }
 
    public void OnTriggerEnter(Collider col)
    {
        SetupTile tileScriptNext = col.gameObject.GetComponent<SetupTile>();
        // the old and the current tile have always to be connected. if the Player moves in the oppisite direction you got to assure that the old tile stays the same.
        // the next current and old tile need to be in a line, this is checked by the leaste indented if clauses (or else it's a corner)
        // if the old and the next tile are 2 apart, then there is no direction change.

        //inital
        if(tileScriptCurrent == null)
        {
            tileScriptCurrent = tileScriptNext;
            tileScriptOld = tileScriptNext;
        }
            
        // to do: make fucntion to reduce redudancy 
        //horizontal 
       if (tileScriptNext.transform.position.z == tileScriptCurrent.transform.position.z && tileScriptCurrent.transform.position.z == tileScriptOld.transform.position.z)
        {
            if ((tileScriptNext.transform.position.x - tileScriptOld.transform.position.x) == 2)//no change
            {
                tileScriptOld = tileScriptCurrent;
                tileScriptCurrent = tileScriptNext;
            }
            else if ((tileScriptNext.transform.position.x - tileScriptOld.transform.position.x) <= 1)//change
            {
                //tileScriptOld stays the same
                tileScriptCurrent = tileScriptNext;
            }
        }
        //vertical
        else if (tileScriptNext.transform.position.x == tileScriptCurrent.transform.position.x && tileScriptCurrent.transform.position.x == tileScriptOld.transform.position.x)
        {
            if ((tileScriptNext.transform.position.z - tileScriptOld.transform.position.z) == 2)//no change
            {
                tileScriptOld = tileScriptCurrent;
                tileScriptCurrent = tileScriptNext;
            }
            else if ((tileScriptNext.transform.position.z - tileScriptOld.transform.position.z) <= 1)//change
            {
                //tileScriptOld stays the same
                tileScriptCurrent = tileScriptNext;
            }
        }
        else //corner
        {
            tileScriptOld = tileScriptCurrent;
            tileScriptCurrent = tileScriptNext;
        }
        

        //Delete setAllowedDirections(tileScript);
        //Delete Debug.Log("Tile: " + tileScript.name +"\nL: " + tileScript.left + "; R: " + tileScript.right + "; F: " + tileScript.forward + "; B: " + tileScript.back + ";    " + left + ", " + right + ", " + forward + ", " + back);

    }


    //Enter thw wished directions and the current and old tile, a bool value will be given if direction is allowed (at corners it will guide the figure to the right z or x position (the middle of the tile)
    private string checkIfWalkdirectionIsAllowed( string _direction ,SetupTile _tileScriptCurrent, SetupTile _tileSriptOld)
    {
        /*delete
        //straight vertical
        if(_tileScriptCurrent.straighthor && _tileSriptOld.straighthor)
        {          
            if (direction == "left")
                return true;
            else if (direction == "right")
                return true;

            return false;
        } //straight horizontal
        else if (_tileScriptCurrent.straightvert && _tileSriptOld.straightvert)
        {
            if (direction == "forward" )
                return true;
            else if (direction == "back" )
                return true;

            return false;
        }//corner to straight
        //if (!tileScriptCurrent.straight && )
        */

       // if the gameobject is at the same height for horizontal or same width for vertical movement and the direction is set true, then true is returned (direction is allowed)
       // if the new is a corner also the other axis have to be checker
       // to do: (think about the straight marking again)
       /*
        if(direction == "left")
        {
            //if current is corner
            if (tileScriptCurrent.transform.position.z == transform.position.z && tileScriptCurrent.transform.position.x < transform.position.x && !tileScriptCurrent.left && tileScriptOld.left)
                return true;
            //on current
            else if (tileScriptCurrent.transform.position.z == transform.position.z && tileScriptCurrent.left)
                return true;
            //on old
            else if (tileScriptOld.transform.position.z == transform.position.z && tileScriptOld.left);
            // return true;
            else
                return false;
        }

        else if (direction == "right")
        {
            //if current is corner
            if (tileScriptCurrent.transform.position.z == transform.position.z && tileScriptCurrent.transform.position.x > transform.position.x && !tileScriptCurrent.right && tileScriptOld.right)
                return true;
            //on current
            else if (tileScriptCurrent.transform.position.z == transform.position.z && tileScriptCurrent.right)
                return true;
            //on old
            else if (tileScriptOld.transform.position.z == transform.position.z && tileScriptOld.right);
            // return true;
            else
                return false;
        }

        else if (direction == "forward")
        {
            //if current is corner
            if (tileScriptCurrent.transform.position.x == transform.position.x && tileScriptCurrent.transform.position.z > transform.position.z && !tileScriptCurrent.forward && tileScriptOld.forward)
                return true;
            //on current
            else if (tileScriptCurrent.transform.position.x == transform.position.x && tileScriptCurrent.forward)
                return true;
            //on old
            else if (tileScriptOld.transform.position.x == transform.position.x && tileScriptOld.forward) ;
            // return true;
            else
                return false;
        }
        */


        //Select 
        if(tileScriptCurrent.transform.position.z == tileScriptOld.transform.position.z)
        {
            if (Math.Abs(tileScriptCurrent.transform.position.x - transform.position.x) <= 0.5f)
                activeTile = tileScriptCurrent;
            else
                activeTile = tileScriptOld;
        }
        else
        {
            if (Math.Abs(tileScriptCurrent.transform.position.z - transform.position.z) <= 0.5f)
                activeTile = tileScriptCurrent;
            else
                activeTile = tileScriptOld;
        }
        //Debug.Log(activeTile.name);

        float tolerance = 0.15f;

        //left
        //corner not in the middle (adjiust until it's centerd and can change direction)
        if(_direction == "left")
        {
            //not perfectlty centerd vertical but left is true
            if (activeTile.left && activeTile.transform.position.z - transform.position.z != 0)
            {
                //left and 99% centered
                if (Math.Abs(activeTile.transform.position.z - transform.position.z) < tolerance)
                    transform.position = new Vector3(transform.position.x, transform.position.y, activeTile.transform.position.z);
                //left too high
                else if (activeTile.transform.position.z < transform.position.z && activeTile.transform.position.z != transform.position.z)
                    return "back";
                //left too low
                else if (activeTile.transform.position.z > transform.position.z)
                    return "forward";
            }

            //centered vertical and no Corner
            else if (activeTile.left)
                return _direction;

            //centered vertical and a Corner (needs to be centered horizontal
            else if (!activeTile.left && activeTile.right && activeTile.transform.position.x < transform.position.x)
                return _direction;
            else if (!activeTile.left && activeTile.right && activeTile.transform.position.x > transform.position.x)
                transform.position = new Vector3(activeTile.transform.position.x, transform.position.y, transform.position.z);
        }

        //right
        //corner not in the middle (adjiust until it's centerd and can change direction)
        else if (_direction == "right")
        {
            //not perfectlty centerd vertical but right is true
            if (activeTile.right && activeTile.transform.position.z - transform.position.z != 0)
            {
                //right and 99% centered
                if (Math.Abs(activeTile.transform.position.z - transform.position.z) < tolerance)
                    transform.position = new Vector3(transform.position.x, transform.position.y, activeTile.transform.position.z);
                //right too high
                else if (activeTile.transform.position.z < transform.position.z && activeTile.transform.position.z != transform.position.z)
                    return "back";
                //right too low
                else if (activeTile.transform.position.z > transform.position.z)
                    return "forward";
            }

            //centered vertical and no Corner
            else if (activeTile.right)
                return _direction;

            //centered vertical and a Corner (needs to be centered horizontald
          else if (_direction == "right" && !activeTile.right && activeTile.left && activeTile.transform.position.x > transform.position.x)
                return _direction;
            else if (_direction == "right" && !activeTile.right && activeTile.left && activeTile.transform.position.x < transform.position.x)
                transform.position = new Vector3(activeTile.transform.position.x, transform.position.y, transform.position.z);
        }

        //forward
        //corner not in the middle (adjiust until it's centerd and can change direction)
        else if (_direction == "forward")
        {
            //not perfectlty centerd horizontal but foward is true
            if (activeTile.forward && activeTile.transform.position.x - transform.position.x != 0)
            {
                //foward and 99% centered
                if (Math.Abs(activeTile.transform.position.x - transform.position.x) < tolerance)
                    transform.position = new Vector3(activeTile.transform.position.x, transform.position.y, transform.position.z);
                //foward too right
                else if (activeTile.transform.position.x < transform.position.x)
                    return "left";
                //foward too left
                else if (activeTile.transform.position.x > transform.position.x)
                    return "right";
            }

            //centered horizonatal and no Corner
            else if (activeTile.forward)
                return _direction;

            //centered horizontal and a Corner (needs to be centered vertical)
            else if (_direction == "forward" && !activeTile.forward && activeTile.back && activeTile.transform.position.z > transform.position.z)
                return _direction;
            else if (_direction == "forward" && !activeTile.forward && activeTile.back && activeTile.transform.position.z < transform.position.z)
                transform.position = new Vector3(transform.position.x, transform.position.y, activeTile.transform.position.z);
        }
        //back
        //corner not in the middle (adjiust until it's centerd and can change direction)
        else if (_direction == "back")
        {
            //not perfectlty centerd horizontal but back is true
            if (activeTile.back && activeTile.transform.position.x - transform.position.x != 0)
            {
                //back and 99% centered
                if (Math.Abs(activeTile.transform.position.x - transform.position.x) < tolerance)
                    transform.position = new Vector3(activeTile.transform.position.x, transform.position.y, transform.position.z);
                //back too high
                else if (activeTile.transform.position.x < transform.position.x)
                    return "left";
                //back too low
                else if (activeTile.transform.position.x > transform.position.x)
                    return "right";
            }

            //centered horizonatal and no Corner
            else if (activeTile.back)
                return _direction;

            //centered horizontal and a Corner (needs to be centered vertical)
            else if (!activeTile.back && activeTile.forward && activeTile.transform.position.z < transform.position.z)
                return _direction;
            else if (!activeTile.back && activeTile.forward && activeTile.transform.position.z > transform.position.z)
                transform.position = new Vector3(transform.position.x, transform.position.y, activeTile.transform.position.z);
        }




        //straight
        /*

        else if (_direction == "forward" && activeTile.forward)
            return _direction;
        else if (_direction == "back" && activeTile.back)
            return _direction;

        */

        
        //right
       
        //forward
        
        //back
        




        return null;


    } 

    // set the allowed walking directions depending on the last tile entered und the one before;
    /*deleteprivate void setAllowedDirections(SetupTile _tileScript)
    {


       
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


   private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "sidewalk")
        {
            foreach (GameObject gObject in currentCollisions)
            {
                print(gObject.name);
            }
        }
    }*/

}
