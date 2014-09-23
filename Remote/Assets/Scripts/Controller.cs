using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public void StartInfoUpdate()
    {
        InvokeRepeating("UpdateInfos", 1f, 1f);
    }

    public void CancelInfoUpdate()
    {
        CancelInvoke("UpdateInfos");
    }

    void UpdateInfos()
    {
        if (Network.connections.Length > 0)
        {
            UpdateAcceleration(Input.acceleration);
        }
    }

    [RPC]
    void UpdateAcceleration(Vector3 acceleration)
    {
        networkView.RPC("UpdateAcceleration", RPCMode.Server, acceleration);
    }
}
