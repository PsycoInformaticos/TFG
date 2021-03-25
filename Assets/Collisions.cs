using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private bool colision;
    private int obstaculo;
    public GameObject[] troncos;

    private void Start()
    {
        colision = false;
        obstaculo = 0;
    }

    private void Update()
    {
        if (colision && Input.GetKeyDown("e"))
        {
            Destroy(troncos[obstaculo]);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colision = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        obstaculo++;
        colision = false;
    }
    

}
