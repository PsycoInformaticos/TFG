using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //public GameObject PlayerHealthbar;
    public GameObject EnemyHealthbar;

    bool flecha;

    public TextMeshProUGUI punctuationText;
    public int punctuation = 0;

    public GameObject characterLogic;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        flecha = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();
        //Esto es para debugear que la vida de cada personaje baja y salen flechas aleatorias
        if (Input.GetButtonDown("Fire1")) //E
        {
            //PlayerHealthbar.GetComponent<Healthbar>().DecreaseHealth();

        }

        if (Input.GetButtonDown("Fire2")) //R
        {
            EnemyHealthbar.GetComponent<Healthbar>().DecreaseHealth();
        }

        if (Input.GetButtonDown("Fire3")) //T
        {
            GetComponent<AttackState>().nextArrow();
            flecha = true; 

        }
        if (flecha)
        {
            GetComponent<AttackState>().attack();

            if (GetComponent<AttackState>().GetFlechaDestruida())
                flecha = false;
        }

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
