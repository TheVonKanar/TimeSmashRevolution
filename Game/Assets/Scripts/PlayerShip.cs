using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {
    
    private Vector3 _acceleration;
    public Vector3 Acceleration
    {
        get { return _acceleration; }
        set { _acceleration = value; }
    }

    void FixedUpdate()
    {
        //Debug.Log(Acceleration);
        transform.Translate(Acceleration.x, 0, 0);
    }


}
