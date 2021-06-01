using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectData : MonoBehaviour
{
    public GameObject sword;
    public Text dataText;

    Vector3 vData;
    float cont;

    Data dataFile;

    private void Start()
    {
        vData = new Vector3(0, 0, 0);
        cont = 1;
        dataFile = new Data();
    }

    private void FixedUpdate()
    {

        //Si JoyconMovement devuelve que se ha pulsado el botón b cuenta 1 segundo
        if (sword.GetComponent<JoyconMovement>().getPressed())
        {
            //Se guarda la aceleración en cada momento que se detecte como botón pulsado
            vData = sword.GetComponent<JoyconMovement>().getAccel();
            //vData = sword.GetComponent<JoyconMovement>().getGyro();

            //Suponemos que hay 50 fixedupdate por segundo
            //Si el contador llega a 50 (uno segundo), le indicará a JoyconMovement
            //que ponga a false el pulsado y que no entre aquí
            //Además señala que es la última linea y manda true para que no escriba coma al final
            //Y el contador vuelve a su estado inicial
            if (cont >= 50)
            {
                sword.GetComponent<JoyconMovement>().setNotPressed();
                dataFile.Write(vData.x, vData.y, vData.z, true);
                cont = 1;
            }

            //Si no ha pasado un segundo, sigue escribiendo los distintos valores
            //de la aceleración
            else
            {
                cont++;
                dataText.text = vData.ToString();
                dataFile.Write(vData.x, vData.y, vData.z, false);

            }
        }
        
    }

}
