using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject normalButtons;
    public GameObject capsButton;
    private bool isCaps;


    void Start(){
        isCaps = false;
    }

    public void InsertChar(string c){
            inputField.text = c;
    }

    public void DeleteChar(){
        if(inputField.text.Length > 0){
            inputField.text = inputField.text.Substring(0, inputField.text.Length-1);
        }
    }

    public void InsertSpace(){
        inputField.text += " ";
    }

    public void CapsPressed(){
        if(!capsButton){
            normalButtons.SetActive(false);
            capsButton.SetActive(true);
            isCaps = true;
        }else{
            capsButton.SetActive(false);
            normalButtons.SetActive(true);
            isCaps = false;
        }
    }

    void Update()
    {
        
    }
}
