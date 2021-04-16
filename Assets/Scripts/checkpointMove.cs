using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class checkpointMove : MonoBehaviour
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
        GameObject.FindWithTag("Player").transform.LookAt(GameObject.Find("punto0").transform);
    }

    private void Update()
    {
        Vector3 direction = posMaps[pos].transform.position - transform.position;
        direction.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

    }
}
