using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour
{
    public static MovementButton Instance;
    public XRButton xrButton;
    public ControlRobotButton controlRobotButton;
    public float x, y, z;
    bool isCollision = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<MovementButton>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (xrButton.currentPress)
        {
            controlRobotButton.ControlRobotMovement(x, y, z);
        }
    }
}
