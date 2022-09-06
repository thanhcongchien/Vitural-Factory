using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SocketEventDef
{
    public static string MOVE = "MOVE";

}

//Server Info
[System.Serializable]
public class ServerAPIJSON
{
    public string STATUS;
    public string MESSAGE;
    public ServerInfo DATA;

    public static ServerAPIJSON FromJSON(string data)
    {
        return JsonUtility.FromJson<ServerAPIJSON>(data);
    }
}

[System.Serializable]
public class ServerInfo
{
    public string version;
    public string message;
}


public class DirectionInfo
{
    public string direction;
    public float step;
}

[System.Serializable]

public class RobotMovement
{
    // public List<DirectionInfo> robotMovement;
    public string direction;
    public float step;
    public static RobotMovement FromJSON(string data)
    {
        return JsonUtility.FromJson<RobotMovement>(data);
    }
}


