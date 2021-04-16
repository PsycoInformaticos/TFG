using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private int pos;
    private int pocion;
    public GameObject[] posMaps;
    public GameObject[] pocionMaps;
    private float speed = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        pos = 0;
        pocion = 0;
        //Fijamos el primer punto para empezar a movernos hacia el
        transform.LookAt(GameObject.Find("punto0").transform);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = posMaps[pos].transform.position - transform.position;
        direction.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //actualizamos el contador de posicion y mientras haya posiciones a las que ir, cambiamos la direccion. Si no hay mas mensaje de que se acabo
        if (other.tag == "punto")
        {
            pos++;
        }

        else if (other.tag == "pocion")
        {
            //vida += 20;
            Debug.Log("Gano vida");
            //if (vida >= 100)
            //{
            //    vida = 100;
            //}
            Destroy(pocionMaps[pocion]);
            pocion++;
        }
        else if (other.tag == "piedra")
        {
            //vida -= 10;
            Debug.Log("Pierdo vida");
            //if (vida <= 0)
            //{
            //game over
            //}
            Destroy(other.gameObject);
        }
    }
}
