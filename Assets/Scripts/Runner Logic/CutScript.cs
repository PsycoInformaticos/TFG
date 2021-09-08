using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScript : MonoBehaviour
{
    private bool cutEnabled;
    public GameObject objCollider;
    public GameObject RightJoycon;

    private void Start()
    {
        cutEnabled = false;
    }

    private void Update()
    {
        if (cutEnabled)
        {
            if (RightJoycon.GetComponent<JoyconMovement>().getCut())
            {
                //Debug.Log("CORTAR: " + objCollider.name);
                RunnerSceneManager.Instance.punctuation++;

                SoundManager.Instance.Play(SoundManager.Instance.effects[0]);

                objCollider.gameObject.SetActive(false);

                RightJoycon.GetComponent<JoyconMovement>().setCut();
            }
            else 
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

            //Debug.Log("DETECTA: " + objCollider.name);
        }
        else
        {
            cutEnabled = false;
        }
    }
}
