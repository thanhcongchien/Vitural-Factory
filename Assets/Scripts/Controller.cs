using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class Controller : MonoBehaviour {
    
    public GameObject MovingPlatform;
    public TCPClient Socket;
    public GameObject GripperL;
    public GameObject GripperR;
    public GameObject Gripper;
    public GameObject SuctionCup;
    public Camera MainCamera;

    public Vector3 InitTrianglePosition;
    public Vector3 TrianglePosition;
    public Vector3 TriangleRelativePosition;
    public DrawLines GroundDrawLines;
        
    MoveTriangle TriangleScriptObject;
    public float distanceToZHome = 0.065f;
    Vector3 TriangleHomePosition;

    float feedrate = 0.2f;
    float RealRobotZHome = -200.0f;

    string server = "localhost";
    int port = 8844;
    string ip;

    public static Controller instance = null;


    public bool isXFreeze = false;
    public bool isYFreeze = false;
    public bool isZFreeze = false;

    private bool isMoveTriangle = false;
    private bool isMoveFollowArc = false;
    private Vector3 targetPosition;

    public bool isDeltaX = false;

    string RunningGcodeType = "G28";

    bool IsDrawing = false;

    void Awake()
    {
        instance = this;
    }
    static public Controller GetInstance()
    {
        if (instance == null)
            instance = new Controller();

        return instance;
    }

    void Start () {
        TriangleScriptObject = MovingPlatform.GetComponent<MoveTriangle>();

        InitTrianglePosition = MovingPlatform.transform.localPosition;

        InitHome();

        SetupClient();


        //GroundDrawLines.StartPoint(0, 0);
        //GroundDrawLines.NextPoint(0, 100);
        //GroundDrawLines.NextPoint(10, 100);
        //GroundDrawLines.NextPoint(10, 0);
        //GroundDrawLines.NextPoint(20, 0);
        //GroundDrawLines.NextPoint(20, 100);
        //GroundDrawLines.EndPoint();
    }

    void Update()
    {
        string request = GetInput();

        string[] requests = request.Split('\n');
        for (int i = 0; i < requests.Length; i++)
        {
            ProcessRequest(requests[i]);
        }
        
        UpdateMovingPlatformPosition();
    }

    void DrawLine()
    {
        if (CurrentEndEffector > 1)
        {
            if (IsDrawing == true)
            {
                GroundDrawLines.NextPoint(TriangleRelativePosition.x, TriangleRelativePosition.y);
            }
        }
    }

    private void UpdateMovingPlatformPosition()
    {
        if (isMoveTriangle == true)
        {
            if (RunningGcodeType == "G01")
            {
                MovingPlatform.transform.localPosition = Vector3.MoveTowards(MovingPlatform.transform.localPosition, targetPosition, feedrate * Time.deltaTime);
                if (MovingPlatform.transform.localPosition == targetPosition)
                {
                    DrawLine();

                    isMoveTriangle = false;
                    SetOutput("Ok");
                }
            }

            if (RunningGcodeType == "G02" || RunningGcodeType == "G03")
            {
                if (RunningGcodeType == "G02")
                    CurrentPhi -= (2*Math.PI)/360;
                else
                    CurrentPhi += (2 * Math.PI) / 360;

                float targetX = (float) (RadiusArc * Math.Cos(CurrentPhi)) + ArcOrigin.x;
                float targetY = (float) (RadiusArc * Math.Sin(CurrentPhi)) + ArcOrigin.y;

                Debug.Log(CurrentPhi.ToString() + ", " + targetX.ToString() + ", " + targetY.ToString());

                TriangleRelativePosition = new Vector3(targetX, targetY, TriangleRelativePosition.z);

                DrawLine();

                TrianglePosition = MovingPlatform.transform.localPosition;

                TrianglePosition.x = TriangleHomePosition.x + targetX / 1000;
                TrianglePosition.y = TriangleHomePosition.y + -targetY / 1000;
                TrianglePosition.z = TriangleHomePosition.z + (TriangleRelativePosition.z - RealRobotZHome) / 1000;

                MovingPlatform.transform.localPosition = Vector3.MoveTowards(MovingPlatform.transform.localPosition, TrianglePosition, feedrate * Time.deltaTime);

                int curX = (int)(targetX);
                int curY = (int)(targetY);
                int tarX = (int)(targetPosition.x);
                int tarY = (int)(targetPosition.y);

                Debug.Log(curX.ToString() + ", " + curY.ToString() + ", " + tarX.ToString() + ", " + tarY.ToString());

                if (curX == tarX && curY == tarY)
                {
                    isMoveTriangle = false;
                    SetOutput("Ok");
                }
            }
        }        
    }

    private void SetupClient()
    {
        if (File.Exists("ip.txt"))
        {
            using (StreamReader reader = new StreamReader("ip.txt"))
            {
                string inputText = reader.ReadToEnd();
                reader.Close();

                Debug.Log(inputText);

                server = inputText.Substring(0, inputText.IndexOf(":"));
                port = int.Parse(inputText.Substring(inputText.IndexOf(":") + 1));

                Debug.Log(server);
                Debug.Log(port);
            }
        }        

        Socket.ConnectToTcpServer(server, port);
    }

    void InitHome()
    {
        TrianglePosition = MovingPlatform.transform.localPosition;

        TrianglePosition.z += distanceToZHome;
        TriangleHomePosition = TrianglePosition;

        TriangleRelativePosition = Vector3.zero;
        TriangleRelativePosition.z = RealRobotZHome - distanceToZHome * 1000;

        //isMoveTriangle = true;
        //targetPosition = TrianglePosition;
    }

    void ProcessRequest(string request)
    {
        if (request == "")
        {
            return;
        }

        //Debug.Log(request);

        string[] words = request.Split(' ');

        try
        {
            if (words[0] == "move")
            {
                if (words[1] == "triangle")
                {
                    float x = TriangleRelativePosition.x;
                    float y = TriangleRelativePosition.y;
                    float z = TriangleRelativePosition.z - RealRobotZHome;

                    for (int i = 2; i < words.Length; i++)
                    {
                        if (words[i] == "x")
                        {
                            x = float.Parse(words[i + 1]);
                            ++i;
                        }
                        if (words[i] == "y")
                        {
                            y = float.Parse(words[i + 1]);
                            ++i;
                        }
                        if (words[i] == "z")
                        {
                            z = float.Parse(words[i + 1]);
                            ++i;
                        }

                        if (words[i] == "f")
                        {
                            feedrate = float.Parse(words[i + 1]) / 1000;
                            Debug.Log(feedrate);
                            ++i;
                        }
                    }
                    
                    G01(x, y, RealRobotZHome + z);
                }
            }

            if (words[0] == "update")
            {
                if (words[1] == "object")
                {
                    Debug.Log(request);
                    ObjectManager.GetInstance().UpdateObject(words[2], new Vector3(float.Parse(words[3]), float.Parse(words[4]), float.Parse(words[5])), new Vector3(float.Parse(words[6]), float.Parse(words[7]), float.Parse(words[8])), float.Parse(words[9]));
                }
                if (words[1] == "end_effector")
                {
                    ChangeEndEffector(int.Parse(words[2]));
                }
                if (words[1] == "camera_position")
                {
                    ChangeCameraPosition(int.Parse(words[2]));
                }
                if (words[1] == "z_home")
                {
                    RealRobotZHome = float.Parse(words[2]);
                }
            }

            if (words[0] == "delete")
            {
                ObjectManager.GetInstance().DeleteObject(words[1]);
            }

            if (words[0] == "gcode")
            {
                if (words[1].ToLower() == "m03" || words[1].ToLower() == "m3")
                {
                    if (words.Length > 2)
                    {
                        float angle = int.Parse(words[2].Substring(1));

                        if (angle == 0)
                        {
                            M05();
                        }
                        else
                        {
                            M03(angle);
                        }                        
                    }
                    else
                    {
                        M03(255);
                    }         
                }
                else if (words[1].ToLower() == "m05" || words[1].ToLower() == "m5")
                {
                    Debug.Log(request);
                    M05();
                }
                else if (words[1].ToLower() == "m360")
                {
                    Debug.Log(request);
                    if (words[2][0] == 'E' || words[2][0] == 'e')
                    {
                        ChangeEndEffector(int.Parse(words[2].Substring(1)));
                    }

                    SetOutput("Ok");

                }
                else if (words[1].ToLower() == "g28")
                {
                    G28();
                }

                else if (words[1].ToLower() == "g01" || words[1].ToLower() == "g1" || words[1].ToLower() == "g0" || words[1].ToLower() == "g00")
                {
                    float x = TriangleRelativePosition.x;
                    float y = TriangleRelativePosition.y;
                    float z = TriangleRelativePosition.z;

                    for (int i = 2; i < words.Length; i++)
                    {
                        if (words[i][0] == 'X' || words[i][0] == 'x')
                        {
                            x = float.Parse(words[i].Substring(1));
                        }
                        if (words[i][0] == 'Y' || words[i][0] == 'y')
                        {
                            y = float.Parse(words[i].Substring(1));
                        }
                        if (words[i][0] == 'Z' || words[i][0] == 'z')
                        {
                            z = float.Parse(words[i].Substring(1));
                        }
                        if (words[i][0] == 'F' || words[i][0] == 'f')
                        {
                            feedrate = float.Parse(words[i].Substring(1)) / 1000;
                        }
                        
                    }
                    G01(x, y, z);
                }
                else if (words[1].ToLower() == "g02" || words[1].ToLower() == "g2" || words[1].ToLower() == "g03" || words[1].ToLower() == "g3")
                {
                    float I = 0, J = 0, X = 0, Y = 0;

                    for (int i = 2; i < words.Length; i++)
                    {
                        if (words[i][0] == 'I')
                        {
                            I = float.Parse(words[i].Substring(1));
                        }
                        if (words[i][0] == 'J')
                        {
                            J = float.Parse(words[i].Substring(1));
                        }
                        if (words[i][0] == 'X')
                        {
                            X = float.Parse(words[i].Substring(1));
                        }
                        if (words[i][0] == 'Y')
                        {
                            Y = float.Parse(words[i].Substring(1));
                        }
                        if (words[i][0] == 'F')
                        {
                            feedrate = float.Parse(words[i].Substring(1)) / 1000;
                        }

                    }

                    if (words[1].ToLower() == "g02" || words[1].ToLower() == "g2")
                        G02(I, J, X, Y);
                    else
                        G03(I, J, X, Y);
                }
                else
                {
                    SetOutput("Ok");
                    Debug.Log("No move Ok");
                }

            }
            else
            {
                
            }
            if (words[0] == "deltax")
            {
                isDeltaX = true;
                
                InvokeRepeating("CheckDeltaXIsAlive", 0, 5);
                Debug.Log("deltax");
            }
        }
        catch (Exception e)
        {

        }
    }

    void CheckDeltaXIsAlive()
    {
        try
        {
            if (!SetOutput("ros"))
            {
                CancelInvoke();
                Debug.Log("Exit");
                Application.Quit();
            }
        }
        catch (Exception e)
        {
            CancelInvoke();
            Debug.Log("Exit");
            Application.Quit();
        }
    }

    public void G01(float x, float y, float z)
    {        
        TriangleRelativePosition = new Vector3(x, y, z);
        TrianglePosition = MovingPlatform.transform.localPosition;

        TrianglePosition.x = TriangleHomePosition.x + x / 1000;
        TrianglePosition.y = TriangleHomePosition.y + -y / 1000;
        TrianglePosition.z = TriangleHomePosition.z + (z - RealRobotZHome) / 1000;

        isMoveTriangle = true;
        targetPosition = TrianglePosition;

        RunningGcodeType = "G01";
    }

    double CurrentPhi;
    double TargetPhi;
    Vector3 ArcOrigin;
    double RadiusArc;

    public void G02(float I, float J, float X, float Y)
    {
        ArcOrigin = new Vector3(I, J, TriangleRelativePosition.z);
        RadiusArc = Math.Sqrt(Math.Pow((I - TriangleRelativePosition.x), 2) + Math.Pow((J - TriangleRelativePosition.y), 2));
        double RadiusArc2 = Math.Sqrt(Math.Pow((I - X), 2) + Math.Pow((J - Y), 2));

        if (Math.Abs(RadiusArc2 - RadiusArc) > 0.5)
            return;

        CurrentPhi = Math.Atan2((TriangleRelativePosition.y - J), (TriangleRelativePosition.x - I));
        TargetPhi = Math.Atan2((Y - J), (X - I));

        if (CurrentPhi < 0)
        {
            CurrentPhi += 2 * Math.PI;
        }

        if (TargetPhi < 0)
        {
            TargetPhi += 2 * Math.PI;
        }


        Debug.Log(RadiusArc.ToString() + ", " + CurrentPhi.ToString() + ", " + TargetPhi.ToString());

        targetPosition.x = X;
        targetPosition.y = Y;
        
        Debug.Log("X:" + targetPosition.x);

        isMoveTriangle = true;

        RunningGcodeType = "G02";
    }

    public void G03(float I, float J, float X, float Y)
    {
        G02(I, J, X, Y);

        RunningGcodeType = "G03";
    }

    public void G28()
    {
        //TriangleRelativePosition = Vector3.zero;
        //TrianglePosition = TriangleHomePosition;

        //isMoveTriangle = true;
        //targetPosition = TrianglePosition;

        G01(0, 0, RealRobotZHome);
    }

    bool IsEndEffectorOpen = false;

    IEnumerator RespondOkAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        SetOutput("Ok");
    }

    public void M03(float a)
    {
        IsEndEffectorOpen = true;
        SetGripperAngle(a);

        if (CurrentEndEffector > 1)
        {
            IsDrawing = true;
            GroundDrawLines.StartPoint(TriangleRelativePosition.x, TriangleRelativePosition.y);
        }

        StartCoroutine(RespondOkAfterTime(0.3f));
    }

    public void M05()
    {
        IsEndEffectorOpen = false;
        SetGripperAngle(0);

        if (CurrentEndEffector > 1)
        {
            IsDrawing = false;
            GroundDrawLines.EndPoint();
        }

        StartCoroutine(RespondOkAfterTime(0.3f));
    }

    public void SetGripperAngle(float a)
    {
        if (a > 100)
        {
            a = 100;
        }

        a = 100 - a;
        GripperL.transform.localEulerAngles = new Vector3(-a, 0, 0);
        GripperR.transform.localEulerAngles = new Vector3(a, 0, 0);
    }

    public void LogRelativePosition(Vector3 pointPosition)
    {
        Vector3 relative;
        relative.x = (pointPosition.x - TriangleHomePosition.x) * 1000;
        relative.y = -(pointPosition.y - TriangleHomePosition.y) * 1000;
        relative.z = (pointPosition.z - TriangleHomePosition.z) * 1000;

        TriangleRelativePosition.x = relative.x;
        TriangleRelativePosition.y = relative.y;
        TriangleRelativePosition.z = RealRobotZHome - relative.z;

        SetOutput("x " + Math.Round(relative.x, 1) + " y " + Math.Round(relative.y, 1) + " z " + Math.Round(relative.z, 1));
    }

    bool SetOutput(string msg)
    {
        //Debug.Log("Send: " + msg);
        return Socket.SendMsg(msg + "\n");
    }

    string GetInput()
    {
        string msg = Socket.GetMessage();
        //Debug.Log("Receive: " + msg);
        return msg;
    }


    public void FreezeX(bool value)
    {
        isXFreeze = value;
    }

    public void FreezeY(bool value)
    {
        isYFreeze = value;
    }

    public void FreezeZ(bool value)
    {
        isZFreeze = value;
    }

    int CurrentEndEffector = 0;

    public void ChangeEndEffector(int value)
    {
        CurrentEndEffector = value;

        if (value == 0)
        {
            Gripper.SetActive(false);
            SuctionCup.SetActive(true);
        }
        if (value == 1)
        {
            Gripper.SetActive(true);
            SuctionCup.SetActive(false);
        }
        if (value == 2)
        {
            Gripper.SetActive(false);
            SuctionCup.SetActive(false);
        }
    }

    public void ChangeCameraPosition(int value)
    {
        if (value == 0)
        {
            Camera.main.transform.position = new Vector3(0, 1, -17);
            Camera.main.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (value == 1)
        {
            Camera.main.transform.position = new Vector3(5.5f, 5.5f, -15);
            Camera.main.transform.eulerAngles = new Vector3(18, -24, 0);
        }
        if (value == 2)
        {
            Camera.main.transform.position = new Vector3(18, 1, -1);
            Camera.main.transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }
}
