using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackState : MonoBehaviour
{
    public Image attackArrow;
    public Canvas canvas;

    public GameObject sword;

    Vector3 accel;

    Queue arrowQueue;

    private void Start()
    {
        arrowQueue = new Queue();
    }

    //enum direction { up, down, left, right}

    //Principal run of the attack state in the combats
    public void attack()
    {
        //Se generará nueva flecha por tiempo y no pulsando un botón

        //Recogemos la aceleración que guarda la espada
        accel = sword.GetComponent<JoyconMovement>().getAccel();

       Image arrow = (Image)arrowQueue.Peek();

        //Arriba
        if (arrow.transform.rotation.z == 0 && (accel.x > 0 && accel.y > 0 && accel.y > 0))
        {
            //Revisar como destruir una imagen de una cola
            Destroy((Image)arrowQueue.Peek());
            arrowQueue.Dequeue();
        }

    }

    public void nextArrow()
    {
        Vector3 posArrow = new Vector3(canvas.transform.position.x + 100, canvas.transform.position.y, 0);

        //Depends on the number, will apear an arrrow in diferent direction. Will be some direction more, but first only 4 for trying
        int randomDirection = /*Random.Range(1, 4);*/ 0;
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

        arrowQueue.Enqueue(attackArrow);

    }
}
