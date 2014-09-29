using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public float speed = 1;
    public GameObject[] patterns;

    PoolManager pool;

    GameObject prevPattern;
    GameObject activePattern;
    GameObject nextPattern;

    void Awake()
    {
        pool = GameObject.FindGameObjectWithTag("Pool").GetComponent<PoolManager>();
    }

    void Start()
    {
        GameObject randomPattern = patterns[Random.Range(0, patterns.Length)];
        activePattern = pool.Create(randomPattern.name, Vector3.zero);

        randomPattern = patterns[Random.Range(0, patterns.Length)];
        Vector3 nextPatternPos = new Vector3(activePattern.transform.position.x + activePattern.collider2D.bounds.size.x, 0, 0);
        nextPattern = pool.Create(randomPattern.name, nextPatternPos);   
    }

    void Update()
    {

    }

    public void CreateNextPattern()
    {
        if (prevPattern != null)
            pool.Destroy(prevPattern);

        prevPattern = activePattern;
        activePattern = nextPattern;

        Vector3 nextPatternPos = new Vector3(activePattern.transform.position.x + activePattern.collider2D.bounds.size.x, 0, 0);
        GameObject randomPattern = patterns[Random.Range(0, patterns.Length)];
        nextPattern = pool.Create(randomPattern.name, nextPatternPos);    
    }
}
