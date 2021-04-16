using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScript : MonoBehaviour
{
    private bool cortar;
    private int obstaculo;
    public GameObject[] obstaculoMaps;

    private void Start()
    {
        cortar = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            cortar = true;

        }
        else if (Input.GetKeyUp("e"))
        {
            cortar = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tronco")
        {
            if (cortar)
            {
                Destroy(obstaculoMaps[obstaculo]);
                obstaculo++;
            }
            else
            {
                //vida -= 10;
                Debug.Log("pierde vida");
                //if (vida <= 0)
                //{
                //    //game over
                //}
                Destroy(obstaculoMaps[obstaculo]);
                obstaculo++;
            }

        }
    }
}
