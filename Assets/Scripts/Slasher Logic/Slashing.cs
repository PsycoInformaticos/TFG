using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slashing : MonoBehaviour
{

    public GameObject pinkBot;      //Esfera rosa a cortar
    public GameObject greenBot;     //Esfera verde a cortar
    public GameObject rSword;       //Espada derecha
    public GameObject lSword;       //Espada izquierda
    public GameObject points;       //Texto de los puntos

    //public Text direccionR; //Texto para indicar hacia donde se mueve el mando
    //public Text direccionL; //Texto para indicar hacia donde se mueve el mando

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

        pinkBot.SetActive(false);
        greenBot.SetActive(false);
    }

    private void Update()
    {
        //int moveTypeR = rSword.GetComponent<JoyconMovement>().moveType();
        //int moveTypeL = lSword.GetComponent<JoyconMovement>().moveType();

        //switch (moveTypeR)
        //{
        //    case 0:
        //        direccionR.text = "UP";
        //        break;
        //    case 1:
        //        direccionR.text = "DOWN";
        //        break;
        //    case 2:
        //        direccionR.text = "RIGHT";
        //        break;
        //    case 3:
        //        direccionR.text = "LEFT";
        //        break;
        //    case 4:
        //        direccionR.text = "NONE";
        //        break;
        //}

        //direccionR.text = moveTypeR.ToString();
        //direccionL.text = moveTypeL.ToString();


        if (sphereActive)
        {
            if (checkMove >= 100)
            {
                if (isSlashing())
                {
                    if(pinkBot.activeSelf) pinkBot.SetActive(false);
                    else if (greenBot.activeSelf) greenBot.SetActive(false);
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
                randomBot();
            }
        }
    }

    void randomBot() 
    {
        sphereActive = true;

        int r = Random.Range(0, 4);
        if (r == 0)
        {
            Vector3 angles = pinkBot.transform.rotation.eulerAngles;
            angles.z = 0;
            pinkBot.transform.rotation = Quaternion.Euler(angles);
            pinkBot.SetActive(true);
        }
        else if (r == 1)
        {
            Vector3 angles = greenBot.transform.rotation.eulerAngles;
            angles.z = 0;
            greenBot.transform.rotation = Quaternion.Euler(angles);
            greenBot.SetActive(true);
        }
        else if (r == 2)
        {
            Vector3 angles = pinkBot.transform.rotation.eulerAngles;
            angles.z = 90;
            pinkBot.transform.rotation = Quaternion.Euler(angles);
            pinkBot.SetActive(true);
        }
        else if (r == 3)
        {
            Vector3 angles = greenBot.transform.rotation.eulerAngles;
            angles.z = 90;
            greenBot.transform.rotation = Quaternion.Euler(angles);
            greenBot.SetActive(true);
        }

    }


    bool isSlashing()
    {
        int moveTypeR = rSword.GetComponent<JoyconMovement>().moveType();
        int moveTypeL = lSword.GetComponent<JoyconMovement>().moveType();

        //direccionR.text = moveTypeR.ToString();
        //direccionL.text = moveTypeL.ToString();

        //Si la esfera es rosa y se mueve la espada rosa en horizontal (2 = der, 3 = izq)
        if ((pinkBot.activeSelf && pinkBot.transform.rotation.eulerAngles.z == 0) && (moveTypeR == 2 || moveTypeR == 3))
        {
            return true;
        }
        //Si la esfera es rosa y se mueve la espada rosa en vertical (0 = arriba, 1 = abajo)
        else if ((pinkBot.activeSelf && pinkBot.transform.rotation.eulerAngles.z == 90) && (moveTypeR == 0 || moveTypeR == 1))
        {
            return true;
        }
        //Si la esfera es verde y se mueve la espada verde en horizontal (2 = der, 3 = izq)
        if ((greenBot.activeSelf && greenBot.transform.rotation.eulerAngles.z == 0) && (moveTypeL == 2 || moveTypeL == 3))
        {
            return true;
        }
        //Si la esfera es verde y se mueve la espada verde en vertical (0 = arriba, 1 = abajo)
        else if ((greenBot.activeSelf && greenBot.transform.rotation.eulerAngles.z == 90) && (moveTypeL == 0 || moveTypeL == 1))
        {
            return true;
        }
        else 
            return false;
    }

}
