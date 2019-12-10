using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject PlayerHealthbar;
    public GameObject EnemyHealthbar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Esto es para debugear que la vida de cada personaje baja y salen flechas aleatorias
        if (Input.GetButtonDown("Fire1")) //E
        {
            PlayerHealthbar.GetComponent<Healthbar>().DecreaseHealth();

        }

        if (Input.GetButtonDown("Fire2")) //R
        {
            EnemyHealthbar.GetComponent<Healthbar>().DecreaseHealth();
        }

        bool flecha = false;
        if (Input.GetButtonDown("Fire3")) //T
        {
            GetComponent<AttackState>().nextArrow();
            flecha = true; 

        }
        if (flecha)
        {
            GetComponent<AttackState>().attack();
            flecha = false;
        }

    }
}
