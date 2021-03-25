using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class checkpointMove : MonoBehaviour
{
    private int pos;
    private int obstaculo;
    private int pocion;
    private bool cortar;
    public GameObject[] posMaps;
    public GameObject[] obstaculoMaps;
    public GameObject[] pocionMaps;

    private float speed = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        //Leemos el archivo para saber cuantos puntos hay en cada mapa
        pos = 0;
        obstaculo = 0;
        pocion = 0;
        cortar = false;
        //Fijamos el primer punto para empezar a movernos hacia el
        transform.LookAt(GameObject.Find("punto0").transform);
    }

    private void Update()
    {
        if (cortar && Input.GetKeyDown("e"))
        {
            Destroy(obstaculoMaps[obstaculo]);
            obstaculo++;
            cortar = false;
        }
        Vector3 direction = posMaps[pos].transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    // Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter (Collider other)
    {
        //actualizamos el contador de posicion y mientras haya posiciones a las que ir, cambiamos la direccion. Si no hay mas mensaje de que se acabo
        if (other.tag == "punto") {
            pos++;
            //if (pos < posMaps.Length)
            //{
            //    transform.LookAt(posMaps[pos].transform);

            //}
            //else
            //{
            //    Debug.Log("Fin del viaje");
            //}
        }
        else if (other.tag == "tronco")
        {
            cortar = true;
        }
        else if (other.tag == "pocion")
        {
            Destroy(pocionMaps[pocion]);
            pocion++;
        }

        //Vector3 direction = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
