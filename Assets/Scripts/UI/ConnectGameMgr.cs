using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectGameMgr : Singleton<ConnectGameMgr>
{
    public static ConnectGameMgr connectGameMgrInstance;
    private float timeBeginPaused = 0f;
    const float TIME_DISCONNECT = 10f;
    private string message = "checkConnectionIsAlive";
    private float timeBeginPing = 0f;
    private float timeSendRequest = 0f;
    public float lagencyTimeout = 0f;
    public float maxLagencyTime = 2f;



    private void Awake()
    {
        if (Instance == null)
        {
            connectGameMgrInstance = GetComponent<ConnectGameMgr>();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        timeBeginPaused = Time.realtimeSinceStartup;
    }

    void Update()
    {

        // if (NetworkMgr.Instance != null)
        // {
        //     if (Time.realtimeSinceStartup - timeSendRequest > 5f)
        //     {
        //         NetworkMgr.Instance.CheckConnectionIsAlive(message);
        //         timeSendRequest = Time.realtimeSinceStartup;
        //     }
        //     if (((Time.realtimeSinceStartup - timeBeginPing > 10f && timeBeginPing != 0) || (timeBeginPing == 0 && Time.realtimeSinceStartup - timeSendRequest > 10f)))
        //     {
        //         Debug.Log("timeBeginPing : " + timeBeginPing);
        //     }
        // } 
        // string data = "hello";
        // ws.Send(data.ToString());
    }
}
