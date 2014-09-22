using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    void Update()
    {
        if (GetComponent<NetworkManager>().IsConnected)
            UpdateAcceleration(Input.acceleration);
    }

    [RPC]
    void UpdateAcceleration(Vector3 acceleration)
    {
        networkView.RPC("UpdateAcceleration", RPCMode.Server, acceleration);
    }
}
