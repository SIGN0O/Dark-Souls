using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton{
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;
    public float TurnState = 0;
    public float IsPressingNumState = 0;

    public float extendingDuration = 0.15f;
    public float delayingDuration = 0.5f;

    private bool curState = false;
    private bool lastState = false;
    private float nowStateNum = 0;
    private float lastStateNum = 0;

    private MyTimer extTimer = new MyTimer();         //按键释放后继续延时指定时间执行
    private MyTimer delayTimer = new MyTimer();       //按键按下后推迟指定时间再执行
    public void Tick(bool input)
    {
        extTimer.TimeTick();
        delayTimer.TimeTick();

        curState = input;
        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;

        if(curState != lastState)
        {
            if (curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }
        lastState = curState;

        IsExtending = (extTimer.state == MyTimer.STATE.RUN);
        IsDelaying = (delayTimer.state == MyTimer.STATE.RUN);
    }

    public void Tick(float input)
    {
        nowStateNum = input;
        IsPressingNumState = nowStateNum;

        TurnState = 0;

        if (nowStateNum != lastStateNum)
        {
            if (nowStateNum == 1||nowStateNum==-1)
            {
                TurnState = nowStateNum;
            }

        }
        lastStateNum = nowStateNum;


    }
    public void StartTimer(MyTimer timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
}
