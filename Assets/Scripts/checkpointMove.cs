using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class checkpointMove : MonoBehaviour
{
    private int pos;
    public GameObject[] posMaps;

    //private Transform target;
    //private float speed = 20f;

    // Start is called before the first frame update
    private void Start()
    {
        //Leemos el archivo para saber cuantos puntos hay en cada mapa
        pos = 0;
        //Fijamos el primer punto para empezar a movernos hacia el
        transform.LookAt(GameObject.Find("punto0").transform);
    }

    // Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter (Collider other)
    {
        //actualizamos el contador de posicion y mientras haya posiciones a las que ir, cambiamos la direccion. Si no hay mas mensaje de que se acabo
        pos++;
        if (pos < posMaps.Length)
        {
            transform.LookAt(posMaps[pos].transform);

        }
        else
        {
            Debug.Log("Fin del viaje");
        }

        //target = GameObject.Find(nextpos).transform;
        //Vector3 direction = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        //transform.LookAt(GameObject.Find(nextpos).transform);


        // Determine which direction to rotate towards
        //Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        //float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
