using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    private LineRenderer currentLine;
    public Material material;

    private int lineID = 0;
    private int lineObjID = 0;

    public GameObject DrawingHead;
    public GameObject Ground;
    public Vector3 Target;

    void Start()
    {
        createLine();

        //StartPoint(0, 0);
        //NextPoint(0, 100);
        //NextPoint(10, 100);
        //NextPoint(10, 0);
        //NextPoint(20, 0);
        //NextPoint(20, 100);
        //EndPoint();
    }

    void Update()
    {
        //Vector3 relative = Controller.GetInstance().TriangleRelativePosition;
        //Target = new Vector3(relative.x/100, 0, relative.y/100);
        //currentLine.SetPosition(1, Target);
    }

    void createLine()
    {
        GameObject lineObj = new GameObject("Line" + lineObjID);
        lineObj.transform.SetParent(this.transform);
        lineObj.transform.localPosition = new Vector3(0, 0, 0);

        lineObjID++;

        currentLine = lineObj.AddComponent<LineRenderer>();

        currentLine.material = material;
        currentLine.positionCount = 1;
        currentLine.startWidth = 0.01f;
        currentLine.endWidth = 0.01f;
        currentLine.useWorldSpace = false;
        //currentLine.numCapVertices = 2; 

        //currentLine.SetPosition(0, new Vector3(100, 0, 100));
        //currentLine.SetPosition(1, new Vector3(100, 0, -100));
        //currentLine.SetPosition(2, new Vector3(-100, 0, -100));
        //currentLine.SetPosition(3, new Vector3(-100, 0, 100));
        //currentLine.SetPosition(4, new Vector3(50, 0, 50));
    }

    public void StartPoint(float x, float y)
    {
        lineID = 0;
        currentLine.SetPosition(lineID, new Vector3(x/100, 0, y/100));
    }

    public void NextPoint(float x, float y)
    {        
        lineID += 1;
        currentLine.positionCount = lineID + 1;
        Debug.Log(currentLine.positionCount);

        currentLine.SetPosition(lineID, new Vector3(x/100, 0, y/100));
        //Debug.Log(lineID);
    }

    public void EndPoint()
    {
        createLine();
    }

    public void DeleteAll()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}