using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    [Header("==== Keys settings ====")]
    //控制人物方向键
    public string LeftHorizontal = "LeftHorizontal";
    public string LeftVertical = "LeftVertical";
    //其他按键
    public string runOrder = "Run";
    public string jumpOrder = "Jump";
    public string attackOrder ="Attack";
    public string defenseOrder= "Defense";
    public string rollOrder = "Roll";
    public string LockOn = "LockOn";

    //控制镜头转向按键
    public string RightVertical = "RightVertical";
    public string RightHorizontal = "RightHorizontal";
    public string MouseX = "Mouse X";
    public string MouseY = "Mouse Y";

    [Header("==== Mouse settings ====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    public MyButton buttonJump = new MyButton();
    public MyButton buttonAttack = new MyButton();
    public MyButton buttonDefense = new MyButton();
    public MyButton buttonRun = new MyButton();
    public MyButton buttonJstick = new MyButton();
    public MyButton buttonChangeEnemys = new MyButton();
    public MyButton buttonRoll = new MyButton();
    public MyButton buttonLockOn = new MyButton();
    public MyButton buttonLRT = new MyButton();

    void Update()
    {
        buttonJump.Tick(Input.GetButton(jumpOrder));
        buttonAttack.Tick(Input.GetButton(attackOrder));
        buttonDefense.Tick(Input.GetButton(defenseOrder));
        buttonRun.Tick(Input.GetButton(runOrder));
        buttonLockOn.Tick(Input.GetButton(LockOn));
        buttonRoll.Tick(Input.GetButton(rollOrder));


        if (mouseEnable == true)
        {
            MouseCameraOrder(MouseX, MouseY);
        }
        else
        {
            CameraOrder(RightHorizontal, RightVertical);
        }
        MovementOrder(LeftHorizontal, LeftVertical);
        RunOrder();
        JumpOrder();
        DefenseOrder();
        RollOrder();
        LockonOrder();
    }

    protected void MouseCameraOrder(string RightStickHorizontal, string RightStickVertical)
    {
        Jup = Input.GetAxis(RightStickVertical)*3.0f*mouseSensitivityY;
        Jright = Input.GetAxis(RightStickHorizontal)*2.5f*mouseSensitivityX;
    }

    protected override Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    protected override void RunOrder()
    {
        run = (buttonRun.IsPressing && !buttonRun.IsDelaying) || buttonRun.IsExtending;
    }

    protected override void JumpOrder()
    {
        jump = buttonRun.IsExtending && buttonRun.OnPressed;
    }

    protected override void RollOrder()
    {
        roll = buttonRun.OnReleased && buttonRun.IsDelaying;
    }

    protected override void LockonOrder()
    {
        lockon = buttonLockOn.OnPressed;
    }
    protected override void DefenseOrder()
    {
        defense = buttonDefense.IsPressing;
    }
}
