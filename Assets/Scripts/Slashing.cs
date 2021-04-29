using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slashing : MonoBehaviour
{

    public GameObject star;     //Esfera a cortar
    public GameObject rSword;   //Espada derecha
    public GameObject lSword;   //Espada izquierda
    public GameObject points;   //Texto de los puntos

    public Material rMaterial;
    public Material lMaterial;

    bool sphereActive;          //Determina si hay una esfera que cortar
    int nextStar;               //Tiempo para la siguiente aparicion de una estrella
    int contNextStar;           //Contador de tiempo desde cero a nextStar

    private void Start()
    {
        sphereActive = false;
        nextStar = 100;
        contNextStar = 0;

        star.SetActive(false);
    }

    private void Update()
    {
        if (sphereActive)
        {
            isSlashing();
            contNextStar = 0;
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
                Debug.Log("Random " + r);
                if (r == 0) render.material = rMaterial;
                else if (r == 1) render.material = lMaterial;
            }
        }
    }

    void isSlashing()
    {
        if(rSword.GetComponent<JoyconMovement>().moveType() == 1)
        {
            sphereActive = false;
            star.SetActive(false);
            points.GetComponent<Puntos>().setPoints(5);
        }
    }
}
