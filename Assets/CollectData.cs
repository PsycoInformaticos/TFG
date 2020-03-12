using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectData : MonoBehaviour
{
    public GameObject sword;
    public Text accelText;

    Vector3 accel;

    float cont;

    private void Start()
    {
        accel = new Vector3(0, 0, 0);
        cont = 0;
    }

    private void FixedUpdate()
    {
        if (sword.GetComponent<JoyconMovement>().getPressed())
        {
            cont++;
            accelText.text = sword.GetComponent<JoyconMovement>().getAccel().ToString();
        }

        //Suponemos que hay 50 fixedupdate por segundo
        if(cont >= 100)
        {
            sword.GetComponent<JoyconMovement>().setNotPressed();
        }
    }
}
