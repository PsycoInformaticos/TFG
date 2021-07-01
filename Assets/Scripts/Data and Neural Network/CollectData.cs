using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectData : MonoBehaviour
{
    public GameObject sword;
    public Text dataText;

    Vector3 vData;
    int cont;
    int it;

    Data dataFile;

    float[] moveTransform = new float[150];
    bool nuevoMovimiento;
    int contTransform;
    int itTransform;

    private void Start()
    {
        vData = new Vector3(0, 0, 0);
        cont = 1;
        it = 0;
        dataFile = new Data();
        nuevoMovimiento = false;
        contTransform = 1;
        itTransform = 0;
    }

    private void FixedUpdate()
    {

        //Si JoyconMovement devuelve que se ha pulsado el botón b cuenta 1 segundo
        if (sword.GetComponent<JoyconMovement>().getPressed() && !nuevoMovimiento)
        {
            //Se guarda la aceleración en cada momento que se detecte como botón pulsado
            vData = sword.GetComponent<JoyconMovement>().getAccel();
            //vData = sword.GetComponent<JoyconMovement>().getGyro();

            //Suponemos que hay 50 fixedupdate por segundo
            //Si el contador llega a 50 (un segundo), le indicará a JoyconMovement
            //que ponga a false el pulsado y que no entre aquí
            //Además señala que es la última linea y manda true para que no escriba coma al final
            //Y el contador vuelve a su estado inicial
            if (cont >= 50)
            {
                sword.GetComponent<JoyconMovement>().setNotPressed();
                dataFile.Write(vData.x, vData.y, vData.z, true);

                //Se guarda cada valor de la acceleracion transformada
                int it = ((cont + 25) % 50);
                moveTransform[it++] = vData.x;
                moveTransform[it++] = vData.y;
                moveTransform[it] = vData.z;

                cont = 1;
                it = 0;
                nuevoMovimiento = true;
            }

            //Si no ha pasado un segundo, sigue escribiendo los distintos valores
            //de la aceleración
            else
            {
                dataText.text = vData.ToString();
                dataFile.Write(vData.x, vData.y, vData.z, false);

                //Se guarda cada valor de la acceleracion transformada
                
                moveTransform[((it + 50) % 150)] = vData.x; it++;
                moveTransform[((it + 50) % 150)] = vData.y; it++;
                moveTransform[((it + 50) % 150)] = vData.z; it++;

                cont++;

            }
        }
  
    }

    private void Update()
    {
        if (nuevoMovimiento)
        {
            dataFile.Write(moveTransform[itTransform++], moveTransform[itTransform++], moveTransform[itTransform++], false);
            contTransform++;
        }

        if (contTransform >= 50)
        {

            dataFile.Write(moveTransform[itTransform++], moveTransform[itTransform++], moveTransform[itTransform], true);
            itTransform = 0;
            contTransform = 1;
            nuevoMovimiento = false;
        }
    }

}
