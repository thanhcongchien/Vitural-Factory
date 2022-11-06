using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;




public class LiveCamera : MonoBehaviour
{
    public static LiveCamera instance;
    public Encoding encoding = Encoding.JPG;
    public string imageData;
    public Camera Cam;
    public bool imageDataReady;
    Texture2D source;
    [Header("Must be the same in sender and receiver")]
    public int messageByteLength = 24;

    public byte[] bytesToSendCount;
    public byte[] bytesToSend;
    public enum Encoding
    {
        JPG = 0,
        PNG = 1
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<LiveCamera>();
        }
    }
    void Start()
    {
        Cam = GetComponent<Camera>();
        // imageDataReady = false;
        StartCoroutine(initAndWaitForTexture());
    }


    IEnumerator initAndWaitForTexture()
    {
        while (source == null)
        {
            yield return null;
        }

    }
    void Update()
    {
        RTImage(Cam);
    }



    // Take a "screenshot" of a camera's Render Texture.
    Texture2D RTImage(Camera camera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        byte[] frameBytesLength = new byte[messageByteLength];
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        // Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        // image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        // image.Apply();
        source = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        source.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        source.Apply();

        byte[] imageBytes = EncodeImage();
        // bytesToSend = imageBytes;
        // Debug.Log("Sending image of size: " + imageBytes.Length);
        byteLengthToFrameByteArray(imageBytes.Length, frameBytesLength);
        string encodedText = System.Convert.ToBase64String(imageBytes);
        imageData = encodedText;
        // System.IO.File.WriteAllBytes("D:/UnityProjects/SavedScreen.png", Bytes);
        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        // imageDataReady = true;
        return source;
    }

    //Converts the data size to byte array and put result to the fullBytes array
    void byteLengthToFrameByteArray(int byteLength, byte[] fullBytes)
    {
        //Clear old data
        Array.Clear(fullBytes, 0, fullBytes.Length);
        //Convert int to bytes
        bytesToSendCount = BitConverter.GetBytes(byteLength);
        //Copy result to fullBytes
        bytesToSendCount.CopyTo(fullBytes, 0);
    }


    private byte[] EncodeImage()
    {
        if (encoding == Encoding.PNG) return source.EncodeToPNG();
        Debug.Log("Encoding JPG " + source.width + " " + source.height);
        return source.EncodeToJPG();
    }

}
