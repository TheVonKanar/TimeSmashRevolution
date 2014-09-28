using UnityEngine;
using System.Collections;

[System.Serializable]
public class PoolItem
{
    public string name;
    public GameObject prefab;
    public int count = 1;
}

public class PoolManager : MonoBehaviour {

    public PoolItem[] poolItems;

    GameObject[] poolObjects;
    GameObject pool;
    Transform myTransform;

    void Awake()
    {
        pool = GameObject.FindGameObjectWithTag("Pool");
        myTransform = transform;
        
        // initialize poolObjects array, set his size
        int totalCount = 0;
        for (int i = 0; i < poolItems.Length; i++)
        {
            totalCount += poolItems[i].count;
        }
        poolObjects = new GameObject[totalCount];

        // instantiate each object and fill the poolObjects array
        int counter = 0;
        for (int i = 0; i < poolItems.Length; i++)
        {
            for (int j = 0; j < poolItems[i].count; j++)
            {
                GameObject obj = Instantiate(poolItems[i].prefab) as GameObject;
                obj.name = poolItems[i].name;
                obj.transform.parent = myTransform;
                obj.SetActive(false);
                poolObjects[counter] = obj;
                counter++;
            }
            counter++;
        }
    }

    // Activate a pool object and give it a position
    public void Create(string name, Vector3 position)
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            if (poolObjects[i].name == name && !poolObjects[i].activeSelf)
            {
                poolObjects[i].SetActive(true);
                poolObjects[i].transform.position = position;
                return;
            }

        }
    }
}
