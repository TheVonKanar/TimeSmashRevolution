using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public int infoUpdateRate = 30; // 30 update per second by default
    public UILabel labelAcceleration;

    void Start()
    {
        // Prevent application from going to sleep
        Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
    }

    public void StartInfoUpdate()
    {
        InvokeRepeating("UpdateInfos", 1f / infoUpdateRate, 1f / infoUpdateRate);
    }

    public void CancelInfoUpdate()
    {
        CancelInvoke("UpdateInfos");
    }

    void UpdateInfos()
    {
        if (Network.connections.Length > 0)
        {
            labelAcceleration.text = Input.acceleration.x.ToString();
            UpdateAcceleration(Input.acceleration);
        }
    }

    [RPC]
    void UpdateAcceleration(Vector3 acceleration)
    {
        networkView.RPC("UpdateAcceleration", RPCMode.Server, acceleration);
    }
}
