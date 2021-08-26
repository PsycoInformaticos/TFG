using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScript : MonoBehaviour
{
    private bool cutEnabled;
    public GameObject objCollider;

    private void Start()
    {
        cutEnabled = false;
    }

    private void Update()
    {
        if (cutEnabled)
        {
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("CORTAR: " + objCollider.name);
                RunnerSceneManager.Instance.punctuation++;

                SoundManager.Instance.Play(SoundManager.Instance.effects[0]);

                objCollider.gameObject.SetActive(false);

            }
            else if (Input.GetKeyUp("e"))
            {
                cutEnabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" && (other.GetComponent<ObstacleLogic>().obstacleType == ObstacleType.WOOD))
        {
            cutEnabled = true;
            objCollider = other.gameObject;

            Debug.Log("DETECTA: " + objCollider.name);
        }
        else
        {
            cutEnabled = false;
        }
    }
}
