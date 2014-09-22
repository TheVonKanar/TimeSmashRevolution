using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    public PlayerShip playerShip;

	[RPC]
    void UpdateAcceleration(Vector3 acceleration)
    {
        
            playerShip.Acceleration = acceleration;
    }
}
