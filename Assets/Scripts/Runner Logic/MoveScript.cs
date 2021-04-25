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
        GameObject.FindWithTag("Player").transform.LookAt(GameObject.Find("Point0").transform);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = posMaps[pos].transform.position - GameObject.FindWithTag("Player").transform.position;
        direction.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject.FindWithTag("Player").transform.rotation = Quaternion.Lerp(GameObject.FindWithTag("Player").transform.rotation, rotation, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //actualizamos el contador de posicion y mientras haya posiciones a las que ir, cambiamos la direccion. Si no hay mas mensaje de que se acabo
        if (other.tag == "Point")
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
        else if (other.tag == "Obstacle")
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
