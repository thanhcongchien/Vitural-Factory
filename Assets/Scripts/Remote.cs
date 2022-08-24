using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remote : MonoBehaviour
{
    public Button rightBtn;
    public Button leftBtn;
    public Button upBtn;
    public Button downBtn;
    public Button forwardBtn;
    public Button backwardBtn;
    public float speed;
    public Transform worldTransform;
    public Vector3 movementVector;
    public float x, y, z;
    public float stepMovement;
    public bool isClicking = true;
    // Start is called before the first frame update
    void Start()
    {
        rightBtn = GetComponent<Button>();
        leftBtn = GetComponent<Button>();
        upBtn = GetComponent<Button>();
        downBtn = GetComponent<Button>();
        forwardBtn = GetComponent<Button>();
        backwardBtn = GetComponent<Button>();
    }

    public void MoveRight()
    {
        if (isClicking)
        {

            worldTransform.position += new Vector3(stepMovement, 0, 0);
            Debug.Log("Moving");
        }

    }
    public void MoveLeft()
    {
        worldTransform.position -= new Vector3(stepMovement, 0, 0);
        Debug.Log("Moving");
    }
    public void MoveUp()
    {
        
        worldTransform.position += new Vector3( 0,stepMovement, 0);
        Debug.Log("Moving");
    }
    public void MoveDown()
    {
        worldTransform.position -= new Vector3( 0,stepMovement, 0);
        Debug.Log("Moving");
    }
    public void MoveForward()
    {
        worldTransform.position += new Vector3( 0, 0,stepMovement);
        Debug.Log("Moving");
    }
    public void MoveBackward()
    {
        worldTransform.position -= new Vector3( 0, 0,stepMovement);
        Debug.Log("Moving");
    }


    void Update()
    {
        // if (worldTransform.position.x >= 0.54f)
        // {
        //     isClicking = false;
        // }else{
        //     isClicking = true;
        // }
    }
}
