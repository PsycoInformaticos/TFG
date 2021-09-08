using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterLogic : MonoBehaviour
{
    //private int pos;
    //public GameObject[] posMaps;
    //private float speed = 5f;

    public int healthCounter;
    public int maxHealth = 5;
    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite fullHeart;

    public GameObject deathUI;
    public Button restartButton;



    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
        //pos = 0;
        //Fijamos el primer punto para empezar a movernos hacia el
       // GameObject.FindWithTag("Player").transform.LookAt(GameObject.Find("Point0").transform);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLife();
        /*Vector3 direction = posMaps[pos].transform.position - GameObject.FindWithTag("Player").transform.position;
        direction.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject.FindWithTag("Player").transform.rotation = Quaternion.Lerp(GameObject.FindWithTag("Player").transform.rotation, rotation, speed * Time.deltaTime);*/
    }
    private void OnTriggerEnter(Collider other)
    {
        //actualizamos el contador de posicion y mientras haya posiciones a las que ir, cambiamos la direccion. Si no hay mas mensaje de que se acabo
        /*if (other.tag == "Point")
        {
            pos++;
        }*/

        /*if (other.tag == "Potion")
        {
            healthCounter++;
            SoundManager.Instance.Play(SoundManager.Instance.effects[1]);
            Debug.Log("Gano vida");

            other.gameObject.SetActive(false);
        }*/
        if (other.tag == "Obstacle")
        {
            if(other.GetComponent<ObstacleLogic>().obstacleType == ObstacleType.WOOD)
            {
                //Debug.Log("Pierdo vida: choque contra tronco");
                healthCounter--;
                SoundManager.Instance.Play(SoundManager.Instance.effects[2]);
            }
            else if (other.GetComponent<ObstacleLogic>().obstacleType == ObstacleType.ROCK)
            {
                //Debug.Log("Pierdo vida: choque contra piedra");
                healthCounter = healthCounter - 2;
                SoundManager.Instance.Play(SoundManager.Instance.effects[2]);
            }
            else if (other.GetComponent<ObstacleLogic>().obstacleType == ObstacleType.POTION)
            {
                healthCounter++;
                SoundManager.Instance.Play(SoundManager.Instance.effects[1]);
                //Debug.Log("Gano vida");

                other.gameObject.SetActive(false);
            }

            if (healthCounter <= 0)
            {
                SoundManager.Instance.Play(SoundManager.Instance.effects[3]);
            }
            other.gameObject.SetActive(false);
        }

        if (other.tag == "PunctuationTrigger")
        {
            //Debug.Log("Aumenta puntuacion");
            RunnerSceneManager.Instance.punctuation += 2;
        }

        if (other.tag == "PoolTrigger")
        {
            //Debug.Log("PoolTrigger");

            ObjectPoolManager.SharedInstance.GeneratePoolObject();
            
        }

        if (other.tag == "SpawnPoolPosition")
        {
            //Debug.Log("SpawnPoolPosition");

            //Aqui deberiamos desactivar contenedor y obstaculos
            //Necesitamos tener una referencia a los obstaculos de ESE contenedor

            

            other.gameObject.transform.parent.gameObject.SetActive(false);
            for (int i = 0; 
                  i < ObjectPoolManager.SharedInstance.previousContainer.gameObject.
                  GetComponent<ContainerController>().containerObstacles.Length; i++)
            {
                //Ojito aqui migente
                //ObjectPoolManager.SharedInstance.pooledObstacles[i].SetActive(false);
                ObjectPoolManager.SharedInstance.previousContainer.gameObject.
                    GetComponent<ContainerController>().containerObstacles[i].SetActive(false);
            }
        }
    }

    private void UpdateLife()
    {
        if (healthCounter > maxHealth)
        {
            healthCounter = maxHealth;
        }

        if (healthCounter <= 0)
        {
            Time.timeScale = 0;
            deathUI.SetActive(true);
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < healthCounter)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
