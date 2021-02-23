using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBOXJoystickInput : IUserInput
{
    [Header("===== Joystick Settings =====")]
    public string LSH= "LSH";
    public string LSV= "LSV";
    public string RSH= "RSH";
    public string RSV= "RSV";
    public string DPadH= "DPadH";
    public string DPadV= "DPadV";
    public string LRT= "LRT";
    public string A="A";
    public string B="B";
    public string X="X";
    public string Y="Y";
    public string LB="LB";
    public string RB="RB";
    public string View= "View";
    public string Menu= "Menu";
    public string LS="LS";
    public string RS="RS";

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonX = new MyButton();
    public MyButton buttonY = new MyButton();
    public MyButton buttonLS = new MyButton();
    public MyButton buttonRS = new MyButton();
    public MyButton buttonRSH = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonLRT = new MyButton();


    void Update()
    {
        buttonA.Tick(Input.GetButton(A));
        buttonB.Tick(Input.GetButton(B));
        buttonX.Tick(Input.GetButton(X));
        buttonY.Tick(Input.GetButton(Y));
        buttonLS.Tick(Input.GetButton(LS));
        buttonRS.Tick(Input.GetButton(RS));
        buttonRSH.Tick(Input.GetAxisRaw(RSH));
        buttonLB.Tick(Input.GetButton(LB));
        buttonRB.Tick(Input.GetButton(RB));
        buttonLRT.Tick(Input.GetAxisRaw(LRT));

        CameraOrder(RSH, RSV);
        MovementOrder(LSH,LSV);
        RunOrder();
        JumpOrder();
        DefenseOrder();
        RollOrder();
        LockonOrder();
        ChangedEnemy();
        LBOrder();
        RBOrder();
        LRTOrder();
        ActionOrder();
        ChangeDualHands();

    }

    protected override Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = input;
        return output;
    }

    protected void ChangedEnemy()
    {
        changeEnemys = buttonRSH.TurnState;
    }

    protected override void JumpOrder()
    {
        jump = buttonLS.OnPressed && run;
    }

    protected override void RunOrder()
    {
        run = (buttonB.IsPressing && !buttonB.IsDelaying) || buttonB.IsExtending;
    }

    protected override void RollOrder()
    {
        roll = buttonB.OnReleased && buttonB.IsDelaying;
    }

    protected override void LockonOrder()
    {
        lockon = buttonRS.OnPressed;
    }

    protected void LBOrder()
    {
        lb = buttonLB.OnPressed;
    }
    protected void RBOrder()
    {
        rb = buttonRB.OnPressed;
    }
    protected void LRTOrder()
    {
        lrt = buttonLRT.TurnState;

    }

    protected override void DefenseOrder()
    {
        defense = buttonLB.IsPressing;
    }

    protected void ActionOrder()
    {
        action = buttonA.OnPressed;
    }

    protected void ChangeDualHands()
    {
        if(buttonY.OnPressed)
            dualHands = !dualHands;
        print(dualHands);
 
    }
}
