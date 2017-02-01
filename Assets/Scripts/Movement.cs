using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //settings
    public float originalSpeed = 5f;
    public string direction; //to do: make this better(protected)
    private float speed;


    //save the current and the last tile standing on
    SetupTile tileScriptCurrent;
    SetupTile tileScriptOld;
    SetupTile activeTile;

    private void Start()
    {
        speed = originalSpeed;
    }

    void Update () {

        //movementChecker returns if its possible the wished move direction. Makes little Corrections at corners
        moveFigure(movementChecker(direction, tileScriptCurrent, tileScriptOld));
       
    }


    //moves the Gameobject
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


        Debug.Log(tileScriptNext.name);
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
        
        //Delete Debug.Log("Tile: " + tileScript.name +"\nL: " + tileScript.left + "; R: " + tileScript.right + "; F: " + tileScript.forward + "; B: " + tileScript.back + ";    " + left + ", " + right + ", " + forward + ", " + back);

    }


    //Enter thw wished directions and the current and old tile, a bool value will be given if direction is allowed (at corners it will guide the figure to the right z or x position (the middle of the tile)
    private string movementChecker( string _direction ,SetupTile _tileScriptCurrent, SetupTile _tileSriptOld)
    {
        
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

        return null;

    } 

}
