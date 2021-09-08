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
    public Text points;             //Texto de los puntos
    int nPoints;                    //Número de puntos
    public Text time;               //Texto del tiempo transcurrido
    public float timer;                    //Contador del tiempo

    public GameObject gameOverMenu;
    public Text finalScore;

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
        nPoints = 0;
        //timer = 121.0f;

        points.text = "Score\n" + nPoints;

        pinkBot.SetActive(false);
        greenBot.SetActive(false);

        gameOverMenu.SetActive(false);
    }

    private void Update()
    {

        //direccionR.text = moveTypeR.ToString();
        //direccionL.text = moveTypeL.ToString();

        if (timer > 0)
        {
            decreaseTime();

            if (sphereActive)
            {
                if (checkMove >= 100)
                {
                    if (isSlashing())
                    {
                        if (pinkBot.activeSelf) pinkBot.SetActive(false);
                        else if (greenBot.activeSelf) greenBot.SetActive(false);
                        sphereActive = false;
                        nPoints += 5;
                        points.text = "Score\n" + nPoints;

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

        else if (timer <= 0)
        {
            gameOverMenu.SetActive(true);
            finalScore.text = "Score\n" + nPoints;
        }

    }

    void decreaseTime()
    {
        timer -= Time.deltaTime;
        int t = (int)timer;
        time.text = "Time\n" + t;
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

        //switch (moveTypeR)
        //{
        //    case 0:
        //        Debug.Log("JoyconR: UP");
        //        break;
        //    case 1:
        //        Debug.Log("JoyconR: DOWN");
        //        break;
        //    case 2:
        //        Debug.Log("JoyconR: RIGHT");
        //        break;
        //    case 3:
        //        Debug.Log("JoyconR: LEFT");
        //        break;
        //    case 4:
        //        Debug.Log("JoyconR: NONE");
        //        break;
        //}

        //switch (moveTypeL)
        //{
        //    case 0:
        //        Debug.Log("JoyconL: UP");
        //        break;
        //    case 1:
        //        Debug.Log("JoyconL: DOWN");
        //        break;
        //    case 2:
        //        Debug.Log("JoyconL: RIGHT");
        //        break;
        //    case 3:
        //        Debug.Log("JoyconL: LEFT");
        //        break;
        //    case 4:
        //        Debug.Log("JoyconL: NONE");
        //        break;
        //}

        //direccionR.text = moveTypeR.ToString();
        //direccionL.text = moveTypeL.ToString();

        //Debug.Log(pinkBot.transform.rotation.eulerAngles.z);
        //Debug.Log(greenBot.transform.rotation.eulerAngles.z);

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

    public void ResetGame()
    {
        nextStar = 100;
        contNextStar = 0;
        checkMove = 50;

        timer = 121.0f;
        nPoints = 0;
        points.text = "Score\n" + nPoints;

        pinkBot.SetActive(false);
        greenBot.SetActive(false);
        sphereActive = false;

        gameOverMenu.SetActive(false);
    }

}
