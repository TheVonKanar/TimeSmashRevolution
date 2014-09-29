using UnityEngine;
using System.Collections;

public class Pattern : MonoBehaviour {

    LevelManager levelManager;
    Transform myTransform;
    Collider2D myCollider;

    bool used;

    void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        myTransform = transform;
        myCollider = collider2D;
    }

    void Update()
    {
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;

        if (!used && (myTransform.position.x + myCollider.bounds.size.x < cameraWidth))
        {
            levelManager.CreateNextPattern();
            used = true;
        }
    }

    void FixedUpdate()
    {
        myTransform.Translate(-levelManager.speed * Time.deltaTime, 0, 0);
    }

    void OnDisable()
    {
        used = false;
    }
}
