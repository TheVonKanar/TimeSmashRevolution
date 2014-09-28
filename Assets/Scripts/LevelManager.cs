using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject[] patterns;

    PoolManager pool;

    void Awake()
    {
        pool = GameObject.FindGameObjectWithTag("Pool").GetComponent<PoolManager>();
    }

    void Start()
    {
        pool.Create("Pattern_1", Vector3.zero);
    }
}
