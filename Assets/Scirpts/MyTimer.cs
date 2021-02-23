using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public enum STATE
    {
        IDLE,
        RUN,
        FINISED
    }
    public STATE state;
    public float duration = 1.0f;            //设定记录的时间
    private float elapsedTime = 0f;           //流逝的时间

    public void TimeTick()
    {
        switch (state)
        {
            case STATE.IDLE:

                break;

            case STATE.RUN:
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= duration)
                {
                    state = STATE.FINISED;
                }
                break;

            case STATE.FINISED:

                break;

            default:
                Debug.LogError("MyTimer error");
                break;
        }
    }

    public void Go()
    {
        elapsedTime = 0;
        state = STATE.RUN;
    }

}
