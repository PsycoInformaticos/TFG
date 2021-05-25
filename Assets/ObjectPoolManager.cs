using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager SharedInstance;
    public List<GameObject> pooledObject;
    public GameObject objectToPool;
    public int amountToPool;

    public GameObject[] spawnPositions;
    public GameObject spawnPoolPosition;

    private float increment;

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
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }
    }

    //Cambiar funcion para que reciba cualquier lista de objetos a poolear.
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    }

    public void GeneratePoolObject()
    {
        GameObject poolObject = GetPooledObject();

        if (pooledObject != null)
        {
            poolObject.transform.position = spawnPoolPosition.transform.position + new Vector3(0, 0, 180*increment);
            poolObject.SetActive(true);
            increment++;
        }
    }

}
