using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour
{

    public Text t;
    int points;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        t.text = "Puntuación: " + points;
    }


    public void setPoints (int p)
    {
        points += p;
        t.text = "Puntuación: " + points;
    }

    public void reset()
    {
        points = 0;
        t.text = "Puntuación: " + points;
    }
}
