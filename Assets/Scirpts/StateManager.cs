using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManagerInterface
{
    public float HPMAX = 100.0f;
    public float HP = 15.0f;
    public float ATK = 10.0f;

    [Header("1st order state flags")]
    public bool isGround;
    public bool isJump;
    public bool isRoll;
    public bool isFall;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack;
    public bool isCounterBackEnable;

    [Header("2nd order state flags")]
    public bool isAllowDefense;
    public bool isImmortal;
    public bool isCounterBackSuccess;
    public bool isCounterBackFailure;


    void Start()
    {
        HP = HPMAX;
    }

    void Update()
    {
        isGround = actorManager.actorController.CheckState("ground");
        isJump = actorManager.actorController.CheckState("jump");
        isFall = actorManager.actorController.CheckState("fall");
        isRoll = actorManager.actorController.CheckState("roll");
        isJab = actorManager.actorController.CheckState("jab");
        isAttack = actorManager.actorController.CheckStateTag("attackL")|| 
            actorManager.actorController.CheckStateTag("attackR");
        isHit = actorManager.actorController.CheckState("hit");
        isDie = actorManager.actorController.CheckState("die");
        isBlocked = actorManager.actorController.CheckState("blocked");
        //盾反
        isCounterBack = actorManager.actorController.CheckState("counterBack");
        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable;

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && actorManager.actorController.CheckState("defense1h", "defense");
        isImmortal = isRoll || isJab;
    }

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP,0,HPMAX);
    }

    //public void CounterBack()
    //{
    //    if (isCounterBackSuccess)
    //    {
    //        GameObject senser = actorManager.GetComponentInChildren<Transform>().gameObject;
    //        senser.SetActive(true);
    //    }
        
    //}
}
