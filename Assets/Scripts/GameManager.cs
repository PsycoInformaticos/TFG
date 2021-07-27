using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    public void LoadTargetScene(int index)
    {
        Debug.Log("Loading scene");
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void UpdatePoints()
    {
        if (punctuationText != null)
        {
            punctuationText.text = "Puntuacion: " + punctuation.ToString();
        }
    }
}
