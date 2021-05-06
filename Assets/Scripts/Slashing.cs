using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slashing : MonoBehaviour
{

    public GameObject star;     //Esfera a cortar
    public GameObject rSword;   //Espada derecha
    public GameObject lSword;   //Espada izquierda
    public GameObject points;   //Texto de los puntos
    public GameObject slash;    //Sprite del corte

    public Material rMaterial;
    public Material lMaterial;

    bool sphereActive;          //Determina si hay una esfera que cortar
    int nextStar;               //Tiempo para la siguiente aparicion de una estrella
    int contNextStar;           //Contador de tiempo desde cero a nextStar

    int checkMove;              //Contador entre revisiones de movimientos nuevos

    private void Start()
    {
        sphereActive = false;
        nextStar = 100;
        contNextStar = 0;
        checkMove = 50;

        star.SetActive(false);
        slash.SetActive(false);
    }

    private void Update()
    {
        if (sphereActive)
        {
            if (checkMove >= 100)
            {
                if (isSlashing())
                {
                    star.SetActive(false);
                    sphereActive = false;
                    points.GetComponent<Puntos>().setPoints(5);

                }

                contNextStar = 0;
                checkMove = 0;
            }
            else checkMove++;
        }
        else
        {
        
            contNextStar++;

            if (contNextStar >= nextStar)
            {
                sphereActive = true;
                star.SetActive(true);

                Renderer render = star.GetComponent<Renderer>();
                int r = Random.Range(0, 2);
                if (r == 0) render.material = rMaterial;
                else if (r == 1) render.material = lMaterial;
            }
        }
    }

    bool isSlashing()
    {
     
        if (star.GetComponent<Renderer>().sharedMaterial == rSword.GetComponent<Renderer>().sharedMaterial && rSword.GetComponent<JoyconMovement>().moveType() != 4)
        {
            return true;
        }
        else if (star.GetComponent<Renderer>().sharedMaterial == lSword.GetComponent<Renderer>().sharedMaterial && lSword.GetComponent<JoyconMovement>().moveType() != 4)
        {
            return true;
        }
        else 
            return false;
    }

}
