using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput
{
    IEnumerator Start()
    {
        while (true)
        {
            rb = true;
            yield return 0;
        }
    }
    void Update()
    {
 //       MovementOrder(Dright,Dup);

        RunOrder();
        JumpOrder();
        DefenseOrder();
        RollOrder();
        LockonOrder();
    }
    protected override void DefenseOrder()
    {
        defense = false;
    }

    protected override void JumpOrder()
    {
        jump = false;
    }

    protected override void LockonOrder()
    {
        lockon = false;
    }

    protected override void RollOrder()
    {
        roll = false;
    }

    protected override void RunOrder()
    {
        run = false;
    }

    protected override Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    protected void MovementOrder(float _Dright,float _Dup)
    {
        Vector2 tempDAxis = SquareToCircle(new Vector2(_Dright, _Dup));
        Dright = tempDAxis.x;
        Dup = tempDAxis.y;

        Dmag = Mathf.Sqrt(Mathf.Pow(Dup, 2) + Mathf.Pow(Dright, 2));
        Dvec = Dright * transform.right + Dup * transform.forward;

    }
}
