using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackState : MonoBehaviour
{
    public GameObject attackArrow;
    public Canvas canvas;

    public GameObject sword;
    public GameObject PlayerHealthbar;
    public GameObject EnemyHealthbar;

    Vector3 accel;

    Queue arrowQueue;

    bool flechaDestruida;

    private void Start()
    {
        arrowQueue = new Queue();

        flechaDestruida = true;
    }

    //Principal run of the attack state in the combats
    public void attack()
    {
        //Se generará nueva flecha por tiempo

       GameObject arrow = (GameObject)arrowQueue.Peek();

        //Revisa si hay una flecha en una dirección y se mueve el mando en la misma
        if ((arrow.transform.rotation.z == 0 && sword.GetComponent<JoyconMovement>().moveType() == 0)        //Arriba
            || (arrow.transform.rotation.z == 180 && sword.GetComponent<JoyconMovement>().moveType() == 1)   //Abajo
            || (arrow.transform.rotation.z == -90 && sword.GetComponent<JoyconMovement>().moveType() == 2)   //Derecha
            || (arrow.transform.rotation.z == 90 && sword.GetComponent<JoyconMovement>().moveType() == 3))   //Izquierda
        {
            //Destruye el objeto y lo quita de la cola
            Destroy(arrow);
            arrowQueue.Dequeue();
            flechaDestruida = true;

            EnemyHealthbar.GetComponent<Healthbar>().DecreaseHealth();
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
       
        GameObject auxArrow = Instantiate(attackArrow, posArrow, transform.rotation, canvas.transform);
        
        transform.Rotate(0, 0, rotationZ);

        arrowQueue.Enqueue(auxArrow);

        flechaDestruida = false;

    }

    public bool GetFlechaDestruida ()
    {
        return flechaDestruida;
    }
}
