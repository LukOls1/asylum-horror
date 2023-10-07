using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    private string validCode;
    private string typedCode;
    [SerializeField] private int codeLength = 4;
    [SerializeField] private RandomizeCode randomizeCode;
    [SerializeField] private Door controlledDoor;

    private void Start()
    {
        controlledDoor.doorState = Door.DoorState.Locked;
        validCode = randomizeCode.GenerateCode(codeLength);
    }

    public void GetNumber(string number)
    {
        typedCode += number;
        if(typedCode.Length == validCode.Length)
        {
            CheckCode();
        }
    }
    private void CheckCode()
    {
        if(typedCode == validCode)
        {
            controlledDoor.doorState = Door.DoorState.Closed;
        }
    }
}
