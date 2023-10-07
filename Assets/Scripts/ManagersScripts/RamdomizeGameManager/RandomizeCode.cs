using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeCode : MonoBehaviour
{
    private string randomCode;
    public string RandomCode
    {
        get         { return randomCode; }
        private set { randomCode = value; }
    }

    public string GenerateCode(int codeLength)
    {
        int randomNumber;
        for (int i = 0; i < codeLength; i++)
        {
            randomNumber = Random.Range(0, 10);
            randomCode += randomNumber.ToString();
        }
        Debug.Log(randomCode);
        return randomCode;
    }
    
}
