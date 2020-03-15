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

    Data dataFile;

    private void Start()
    {
        accel = new Vector3(0, 0, 0);
        cont = 0;
        dataFile = new Data();
    }

    private void FixedUpdate()
    {
        //Si JoyconMovement devuelve que se ha pulsado el botón b cuenta 2 segundos
        if (sword.GetComponent<JoyconMovement>().getPressed())
        {
            //Se guarda la aceleración en cada momento que se detecte como botón pulsado
            accel = sword.GetComponent<JoyconMovement>().getAccel();

            //Suponemos que hay 50 fixedupdate por segundo
            //Si el contador llega a 100 (unos 2 segundos), le indicará a JoyconMovement
            //que ponga a false el pulsado y que no entre aquí
            //Además señala que es la última linea y manda true para que no escriba coma al final
            //Y el contador vuelve a su estado inicial
            if (cont >= 100)
            {
                sword.GetComponent<JoyconMovement>().setNotPressed();
                dataFile.Write(accel.x, accel.y, accel.z, true);
                cont = 0;
            }

            //Si no han pasado los 2 segundos, sigue escribiendo los distintos valores
            //de la aceleración
            else
            {
                cont++;
                accelText.text = accel.ToString();
                dataFile.Write(accel.x, accel.y, accel.z, false);
            }
        }
  
    }
}
