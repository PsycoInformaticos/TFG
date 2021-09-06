using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    
    public void LoadTargetScene(int index)
    {
        Debug.Log("Loading scene");
        
        if (index == 1)
        {
            SoundManager.Instance.PlayMusic(SoundManager.Instance.songs[0]);
        }
        else if (index == 2)
        {
            SoundManager.Instance.PlayMusic(SoundManager.Instance.songs[1]);
        }
        
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    

}
