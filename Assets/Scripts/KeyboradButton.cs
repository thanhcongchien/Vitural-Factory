using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KeyboradButton : MonoBehaviour
{

    Keyboard keyboard;
    TextMeshProUGUI buttonTextMesh;



    // Start is called before the first frame update
    void Start()
    {
        keyboard = GetComponent<Keyboard>();
        buttonTextMesh = GetComponent<TextMeshProUGUI>();

        if(buttonTextMesh.text.Length == 1){
            NameToButtonText();
            GetComponentInChildren<XRButton>().OnRelease.AddListener(delegate{keyboard.InsertChar(buttonTextMesh.text);});
        }
    }


    public void NameToButtonText(){
        buttonTextMesh.text = gameObject.name;
    }
}
