using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Header("==== Output signals ====")]
    public float Dup;            //人物移动纵向轴
    public float Dright;         //人物移动横向轴
    public float Dmag;           //人物移动平方开根
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    //pressing signal
    public bool run;
    public bool defense;
    //trigger once signal
    public bool action;
    public bool roll;
    public bool jump;
    public bool lockon;
    public float changeEnemys;
    public bool lb;
    public bool rb;
    public float lrt;
    public bool dualHands;
    //double trigger


    [Header("==== Others ====")]
    public bool inputEnabled = true;


    /// <summary>
    /// 计算人物移动x轴与y轴向量
    /// </summary>
    /// <param name="input">摇杆或者键盘输入（x，y）的二维向量</param>
    /// <returns>计算得出的人物移动二维向量</returns>
    protected abstract Vector2 SquareToCircle(Vector2 input);

    /// <summary>
    /// 移动指令
    /// </summary>
    /// <param name="LeftStickHorizontal">人物移动左右方向</param>
    /// <param name="LeftStickVertical">人物移动垂直方向</param>
    protected void MovementOrder(string LeftStickHorizontal, string LeftStickVertical)
    {
        Dup = Input.GetAxis(LeftStickVertical);
        Dright = Input.GetAxis(LeftStickHorizontal);

        if (!inputEnabled)
        {
            Dup = 0;
            Dright = 0;
        }

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        Dright = tempDAxis.x;
        Dup = tempDAxis.y;

        Dmag = Mathf.Sqrt(Mathf.Pow(Dup, 2) + Mathf.Pow(Dright, 2));
        Dvec = Dright * transform.right + Dup * transform.forward;

    }

    /// <summary>
    /// 控制摄像机转视角指令
    /// </summary>
    /// <param name="RightStickHorizontal">视角左右移动</param>
    /// <param name="RightStickVertical">视角上下移动</param>
    protected void CameraOrder(string RightStickHorizontal, string RightStickVertical)
    {
        Jup = Input.GetAxis(RightStickVertical);
        Jright = Input.GetAxis(RightStickHorizontal);
    }

    /// <summary>
    /// 跳跃指令
    /// </summary>
    /// <param name="jumpButton">跳跃按键</param>
    protected abstract void JumpOrder();

    /// <summary>
    /// 跑步指令
    /// </summary>
    /// <param name="runButton">跑步按键</param>
    protected abstract void RunOrder();

    /// <summary>
    /// 翻滚指令
    /// </summary>
    /// <param name="rollButton">翻滚按键</param>
    protected abstract void RollOrder();

    protected abstract void LockonOrder();

    protected abstract void DefenseOrder();

}