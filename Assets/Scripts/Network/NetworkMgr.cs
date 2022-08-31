// using System.Collections;
// using System.Collections.Generic;
// using SocketIO;
// using UnityEngine;

// public class NetworkMgr :  Singleton<NetworkMgr>
// {

//     [SerializeField] SocketIOComponent _socket;
    
//     // Start is called before the first frame update
//     void Start()
//     {
//         _socket.On(SocketEventDef.CONNECTION_IS_AVLIE, OnConnectionIsAlive);
//         _socket.Connect();
        
//     }


//     void Update(){
//         if(_socket == null){
//             return;
//         }else{
//             _socket.SendMessage("ping");
//         }
//     }



//      public string CheckConnectionIsAlive(string message)
//     {
//         StartCoroutine(CheckConnectIsAliveRoutine(message));
//         return message;
//     }

//     IEnumerator CheckConnectIsAliveRoutine(string message)
//     {
//         yield return new WaitForSeconds(10.0f);
//         PINGJSON joinJSON = new PINGJSON(Time.realtimeSinceStartup);
//         string data = JsonUtility.ToJson(joinJSON);
//         _socket.Emit(SocketEventDef.CONNECTION_IS_AVLIE, new JSONObject(data));
//     }



//     private void OnConnectionIsAlive(SocketIOEvent socketEvent)
//     {
//         string data = socketEvent.data.ToString();
//         CheckConnectionIsAliveJSON resultJson = CheckConnectionIsAliveJSON.FromJSON(data);
//         Debug.Log("OnConnectionIsAlive : " + resultJson.message);
//         // if (DataManager.Instance.clientId == "" || DataManager.Instance.clientId == null || resultJson.clientId == DataManager.Instance.clientId) {
//         //     ConnectGameMgr.Instance.SetTimeBeginPing(Time.realtimeSinceStartup);
//         //     ConnectGameMgr.Instance.lagencyTimeout = Time.realtimeSinceStartup - resultJson.time;
//         // }

//     }
// }
