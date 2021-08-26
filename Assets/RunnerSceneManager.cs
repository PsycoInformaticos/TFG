using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunnerSceneManager : MonoBehaviour
{
    public static RunnerSceneManager Instance { get; private set; }

    public TextMeshProUGUI punctuationText;
    public int punctuation = 0;

    public GameObject characterLogic;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();

    }

    public void UpdatePoints()
    {
        if (punctuationText != null)
        {
            punctuationText.text = "Puntuacion: " + punctuation.ToString();
        }
    }
}
