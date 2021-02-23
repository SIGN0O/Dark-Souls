using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterJoystick : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
        //print("LSH : "+Input.GetAxis("LSH"));
        //print("LSV : "+Input.GetAxis("LSV"));
        print("RSH : "+Input.GetAxis("RSH"));
        print("RSV : "+Input.GetAxis("RSV"));
        print("DPadH : " + Input.GetAxis("DPadH"));
        print("DPadV : " + Input.GetAxis("DPadV"));
        //print("A : " + Input.GetButton("A"));
        //print("B : " + Input.GetButton("B"));
        //print("X : " + Input.GetButton("X"));
        //print("Y : " + Input.GetButton("Y"));
        //print("LB : " + Input.GetButton("LB"));
        //print("RB : " + Input.GetButton("RB"));
        //print("View : " + Input.GetButton("View"));
        //print("Menu : " + Input.GetButton("Menu"));
        //print("LS : " + Input.GetButton("LS"));
        //print("RS : " + Input.GetButton("RS"));
        //print("LRT : " + Input.GetAxis("LRT"));

    }
}
