using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackState : MonoBehaviour
{
    public Image attackArrow;
    public Canvas canvas;

    //enum direction { up, down, left, right}

    //Principal run of the attack state in the combats
    void attack()
    {
        


    }

    public void nextArrow()
    {
        Vector3 posArrow = new Vector3(canvas.transform.position.x + 100, canvas.transform.position.y, 0);

        //Depends on the number, will apear an arrrow in diferent direction. Will be some direction more, but first only 4 for trying
        int randomDirection = Random.Range(1, 4);
        int rotationZ = 0;
        //Select the direction depends on the random number
        switch(randomDirection){

            //UP
            //case 0: the original rotation of the sprite
                
            //DOWN
            case 1:
                rotationZ = 180;
                break;
            //LEFT
            case 2:
                rotationZ = 90;
                break;
            //RIGHT
            case 3:
                rotationZ = -90;
                break;
        }
       
        Instantiate(attackArrow, posArrow, transform.rotation, canvas.transform);
        
        transform.Rotate(0, 0, rotationZ);

    }
}
