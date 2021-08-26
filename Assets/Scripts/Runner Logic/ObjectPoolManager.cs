using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager SharedInstance;
    public List<GameObject> pooledObject;
    public GameObject[] prefabs;
    public int totalAmountToPool; //Total de objetos en la escena
    public int[] objectsAmounts;

    public GameObject[] spawnPositions;
    public GameObject spawnPoolPosition;
    public GameObject currentContainer;
    public GameObject previousContainer;
    public GameObject[] pooledObstacles;

    private float increment;

    public Vector3 woodOffset;
    public Vector3 rockOffset;
    public Vector3 potionOffset;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        increment = 0;
        pooledObject = new List<GameObject>();
        GameObject tmp;

        //Containers
        for (int i = 0; i < objectsAmounts[0]; i++)
        {
            tmp = Instantiate(prefabs[0]);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }

        //Potions
        for (int i = 0; i < objectsAmounts[1]; i++)
        {
            tmp = Instantiate(prefabs[1]);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }

        //Rocks
        for (int i = 0; i < objectsAmounts[2]; i++)
        {
            tmp = Instantiate(prefabs[2]);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }

        //Woods
        for (int i = 0; i < objectsAmounts[3]; i++)
        {
            tmp = Instantiate(prefabs[3]);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }
    }

    public GameObject GetPooledObject(string objectType)
    {
        for (int i = 0; i < totalAmountToPool; i++)
        {
            if (!pooledObject[i].activeInHierarchy && pooledObject[i].gameObject.name == objectType)
            {
                return pooledObject[i];
            }
        }
        return null;
    }

    //Esta funcion debe devolver los containers
    public void GeneratePoolObject()
    { 
        GameObject poolObject = GetPooledObject("PoolContainer(Clone)");

        previousContainer = currentContainer;
        currentContainer = poolObject;

        if (poolObject != null)
        {
            poolObject.transform.position = spawnPoolPosition.transform.position + new Vector3(0, 0, 180*increment);
            poolObject.SetActive(true);
            increment++;
        }

        GeneratePoolObjectObstacles();
    }

    //Esta funcion coloca los obstaculos
    public void GeneratePoolObjectObstacles()
    {
        string[] obs = GenerateRandomObstacles();

        for (int i = 0; i < obs.Length; i++)
        {
            GameObject poolObject = GetPooledObject(obs[i]);

            if (poolObject != null)
            {              
                
                if (poolObject.gameObject.GetComponent<ObstacleLogic>().obstacleType == ObstacleType.WOOD)
                {
                    //poolObject.transform.position = currentContainer.transform.GetChild(i + 1).gameObject.transform.position;
                    poolObject.transform.position = currentContainer.transform.GetChild(i + 1).gameObject.transform.position + woodOffset;
                }
                else if (poolObject.gameObject.GetComponent<ObstacleLogic>().obstacleType == ObstacleType.ROCK)
                {
                    //poolObject.transform.position = currentContainer.transform.GetChild(i + 1).gameObject.transform.position;
                    poolObject.transform.position = currentContainer.transform.GetChild(i + 1).gameObject.transform.position + rockOffset;
                }
                else
                {
                    //poolObject.transform.position = currentContainer.transform.GetChild(i + 1).gameObject.transform.position;
                    poolObject.transform.position = currentContainer.transform.GetChild(i + 1).gameObject.transform.position + potionOffset;
                }

                poolObject.SetActive(true);
                pooledObstacles[i] = poolObject;
                currentContainer.gameObject.GetComponent<ContainerController>().containerObstacles[i] = poolObject;
            }
        }
    }

    public string[] GenerateRandomObstacles()

    {

        int i;

        string[] obs = new string[5];



        for (int j = 0; j < 5; j++)

        {

            i = Random.Range(0, 6);



            if (i == 0)

            {

                obs[j] = "Potion(Clone)";

            }

            else if (i == 1 || i == 2)

            {

                obs[j] = "Rock(Clone)";

            }

            else

            {

                obs[j] = "Wood(Clone)";

            }

        }



        //for (int k = 0; k < 5; k++)

        //{

        //    debug.log(obs[k]);

        //}



        return obs;

    }

}
