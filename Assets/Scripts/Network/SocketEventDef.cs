// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public static class SocketEventDef
// {



//  public static string CONNECTION_IS_AVLIE = "checkConnect";


// }

// //Server Info
// [System.Serializable]
// public class ServerAPIJSON
// {
//     public string STATUS;
//     public string MESSAGE;
//     public ServerInfo DATA;

//     public static ServerAPIJSON FromJSON(string data)
//     {
//         return JsonUtility.FromJson<ServerAPIJSON>(data);
//     }
// }

// [System.Serializable]
// public class ServerInfo
// {
//     public string version;
//     public string message;
// }



// [System.Serializable]
// public class PINGJSON
// {
//     public string time;
//     public PINGJSON(float _time) 
//     {
//         time = Utils.ToString(_time, CommonDef.JSON_FORMAT_FLOAT_STRING);
//     }
// }


// public class CheckConnectionIsAliveJSON
// {
//     public string message;
//     public string clientId;
//     public float time;
//     public static CheckConnectionIsAliveJSON FromJSON(string data)
//     {
//         return JsonUtility.FromJson<CheckConnectionIsAliveJSON>(data);
//     }
// }


