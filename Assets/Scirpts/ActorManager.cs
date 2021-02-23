using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController actorController;

    [Header("=== Auto Generate if Null ===")]
    public BattleManager battleManager;
    public WeaponManager weaponManager;
    public StateManager stateManager;
    public DirectorManager directorManager;
    public InteractionManager interactionManager;

    [Header("=== Override Animators ===")]
    public AnimatorOverrideController oneHandAnim;
    public AnimatorOverrideController twoHandAnim;

    void Awake()
    {
        actorController = GetComponent<ActorController>();
        GameObject model = actorController.model;
        GameObject sensor=null;
        try
        {
            sensor = transform.Find("sensor").gameObject;

        }
        catch (System.Exception)
        {
            //
            //it there is no "sensor" object.
            //
        }

        battleManager = Bind<BattleManager>(sensor);
        weaponManager = Bind<WeaponManager>(model);
        stateManager = Bind<StateManager>(gameObject);
        directorManager = Bind<DirectorManager>(gameObject);
        interactionManager = Bind<InteractionManager>(sensor);

        actorController.OnAction += DoAction;
    }

    public void DoAction()
    {
        if (interactionManager.overlapEcastms.Count != 0)
        {
            EventCasterManager thisEvenCastManger = interactionManager.overlapEcastms[0];
            if (thisEvenCastManger.active == true && directorManager.JudgeStateIsPlaying() == false)
            {
                //I should play corresponding(eventName) timeline here.
                if (thisEvenCastManger.eventName == "frontStab")
                {
                    transform.position = thisEvenCastManger.actorManager.transform.position + thisEvenCastManger.actorManager.transform.TransformVector(thisEvenCastManger.offset);
                    actorController.model.transform.LookAt(thisEvenCastManger.actorManager.transform, Vector3.up);
                    directorManager.PlayTimeline("frontStab",this,thisEvenCastManger.actorManager);
                }
                else if (thisEvenCastManger.eventName == "openBox")
                {
                    if (BattleManager.CheckAngleExecutor(actorController.model,thisEvenCastManger.actorManager.gameObject,45))
                    {
                        transform.position = thisEvenCastManger.actorManager.transform.position + thisEvenCastManger.actorManager.transform.TransformVector(thisEvenCastManger.offset);
                        actorController.model.transform.LookAt(thisEvenCastManger.actorManager.transform,Vector3.up);
                        thisEvenCastManger.active = false;
                        directorManager.PlayTimeline("openBox", this, thisEvenCastManger.actorManager);

                    }
                }
                else if (thisEvenCastManger.eventName == "leverUp")
                {
                    if (BattleManager.CheckAngleExecutor(actorController.model, thisEvenCastManger.actorManager.gameObject, 45))
                    {
                        transform.position = thisEvenCastManger.actorManager.transform.position + thisEvenCastManger.actorManager.transform.TransformVector(thisEvenCastManger.offset);
                        actorController.model.transform.LookAt(thisEvenCastManger.actorManager.transform, Vector3.up);
                     //   thisEvenCastManger.active = false;
                        directorManager.PlayTimeline("leverUp", this, thisEvenCastManger.actorManager);

                    }
                }

            }
        }
    }

    private T Bind<T>(GameObject gameObject) where T: IActorManagerInterface
    {
        T tempInstance;
        if (gameObject == null)
        {
            return null;
        }
        tempInstance = gameObject.GetComponent<T>();
        if (tempInstance == null)
        {
            tempInstance = gameObject.AddComponent<T>();
        }
        tempInstance.actorManager = this;
        return tempInstance;
    }

    public void TryDoDamage(WeaponController targetWeaponController,bool attackValid,bool counterVaild)
    {
        if (stateManager.isCounterBackSuccess && counterVaild)
        {
             targetWeaponController.weaponManager.actorManager.Stunned();
        }
        else if (stateManager.isImmortal)
        {
            //Do nothing
        }
        else if (attackValid)
        {
            if (stateManager.isCounterBackFailure&& attackValid)
            {
                HitOrDie(targetWeaponController,false);
            }
            else if (stateManager.isDefense)
            {
                //attack should be blocked!
                Blocked();
            }
            else
            {
                HitOrDie(targetWeaponController,true);
            }
        }
    }


    public void SetIsCounterBack(bool value)
    {
       stateManager.isCounterBackEnable = value;
    }

    public void Stunned()
    {
        actorController.IssueTrigger("stunned");

    }
    public void Blocked()
    {
        actorController.IssueTrigger("blocked");
    }

    public void Hit()
    {
        actorController.IssueTrigger("hit");
    }

    public void Die()
    {
        actorController.IssueTrigger("die");
        actorController.playerInput.inputEnabled = false;
        if (actorController.camcon.lockState == true)
        {
            actorController.camcon.LockUnLock();
        }
        actorController.camcon.enabled = false;
    }

    public void HitOrDie(WeaponController targetWeaponController, bool doHitAnimation)
    {
        if (stateManager.HP > 0)
        {
            stateManager.AddHP(-1*targetWeaponController.GetATK());
            if (doHitAnimation)
            {
                Hit();
            }
            //do some VFX, like splatter blood...
        }
        if (stateManager.HP <= 0 && !stateManager.isDie)
        {
            //Already dead.
            Die();
        }
    }

    public void LockActorController(bool value)
    {
        actorController.SetBool("lock", value);
    }

    public void ChangeDualHands(bool dualOn)
    {
        if (dualOn)
        {
            actorController.animator.runtimeAnimatorController = twoHandAnim;

        }else
        {
            actorController.animator.runtimeAnimatorController = oneHandAnim;

        }
    }
}
