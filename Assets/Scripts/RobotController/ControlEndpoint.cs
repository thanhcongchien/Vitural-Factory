using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlEndpoint : MonoBehaviour
{
    public GameObject endPoint, LT;
    public int speed = 1000;

// Use this for initialization
void Start () {

    }

    void Update()
    {
      
    }

    public void MoveUp()
    {
      
        endPoint.transform.Translate(Vector3.up * Time.deltaTime * speed);
      
    }
    public void MoveDown()
    {

        endPoint.transform.Translate(Vector3.down * Time.deltaTime * speed);
      
    }
    public void MoveRight()
    {

        endPoint.transform.Translate(Vector3.right * Time.deltaTime * speed);
      
    }
    public void MoveLeft()
    {

        endPoint.transform.Translate(Vector3.left * Time.deltaTime * speed);

    }
    public void MoveForward()
    {

        endPoint.transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
    public void MoveBack()
    {

        endPoint.transform.Translate(Vector3.back * Time.deltaTime * speed);

    }
    public void resetOrigin()
    {
        endPoint.transform.localPosition = new Vector3(-115.0f, 0, 0);
        endPoint.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void xaxiscw()
    {
        endPoint.transform.Rotate(Vector3.left * Time.deltaTime * speed);
    }
    public void xaxisccw()
    {
        endPoint.transform.Rotate(Vector3.right * Time.deltaTime * speed);
    }
    public void yaxiscw()
    {
        endPoint.transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
    public void yaxisccw()
    {
        endPoint.transform.Rotate(Vector3.down * Time.deltaTime * speed);
    }
    public void zaxiscw()
    {
        endPoint.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
    public void zaxisccw()
    {
        endPoint.transform.Rotate(Vector3.back * Time.deltaTime * speed);
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (Input.GetKey(KeyCode.LeftArrow)) { MoveForward();  }
        if (Input.GetKey(KeyCode.RightArrow)) { MoveBack(); }
        if (Input.GetKey(KeyCode.UpArrow)) { MoveRight(); }
        if (Input.GetKey(KeyCode.DownArrow)) { MoveLeft(); }
        if (Input.GetKey(KeyCode.RightShift)) { MoveUp(); }
        if (Input.GetKey(KeyCode.RightControl)) { MoveDown(); }

        if (Input.GetKey(KeyCode.Keypad1)) { xaxiscw(); }
        if (Input.GetKey(KeyCode.Keypad4)) { xaxisccw(); }
        if (Input.GetKey(KeyCode.Keypad2)) { yaxiscw(); }
        if (Input.GetKey(KeyCode.Keypad5)) { yaxisccw(); }
        if (Input.GetKey(KeyCode.Keypad3)) { zaxiscw(); }
        if (Input.GetKey(KeyCode.Keypad6)) { zaxisccw(); }

        if (Input.GetKey(KeyCode.A)) { LT.transform.Translate(Vector3.back * Time.deltaTime * speed); }
        if (Input.GetKey(KeyCode.Z)) { LT.transform.Translate(Vector3.forward * Time.deltaTime * speed); }

        //if (Input.GetKey(KeyCode.Space)) { resetOrigin(); }
    }
}
