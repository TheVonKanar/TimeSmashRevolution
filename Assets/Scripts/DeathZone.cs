using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

    PoolManager pool;

    void Awake()
    {
        pool = GameObject.FindGameObjectWithTag("Pool").GetComponent<PoolManager>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
