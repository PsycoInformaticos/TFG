using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vida;

    // Update is called once per frame
    public void BajarVida(int cantidad)
    {
        vida -= cantidad;
        if (vida < 0)
        {
            vida = 0;
        }
    }
    public void SubirVida(int cantidad)
    {
        vida += cantidad;
        if (vida > 100)
        {
            vida = 100;
        }
    }
}
