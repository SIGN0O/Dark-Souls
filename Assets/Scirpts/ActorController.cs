using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public CameraController camcon;
    public IUserInput playerInput;
    public float walkSpeed= 2.4f;
    public float runMultiplier = 2.7f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 2.0f;
    private Vector3 deltaPos;

    [Space (10)]
    [Header("===== Friction Settings =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionzero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;              //冲量向量
    private bool canAttack;
    private CapsuleCollider col;
    private bool trackDirection=false;
    [Space(10)]
    public bool leftIsShield = true;

    public delegate void OnActionDelegate();
    public event OnActionDelegate OnAction;

    [Space(10)]
    [SerializeField]
    private bool lockPlanar;

    void Awake()
    {
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled)
            {
                playerInput = input;
                break;
            }
        }
        rigid = GetComponent<Rigidbody>();
        anim = model.GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (playerInput.lockon)
        {
            camcon.LockUnLock();
        }

        if (camcon.lockState)
        {
            camcon.ChangeEnemys(playerInput.changeEnemys);
        }

        if (camcon.lockState == false)
        {
            anim.SetFloat("forward",playerInput.Dmag * Mathf.Lerp(anim.GetFloat("forward"), JudgeRun(),0.5f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDVec = transform.InverseTransformVector(playerInput.Dvec);
            anim.SetFloat("forward", localDVec.z * JudgeRun());
            anim.SetFloat("right", localDVec.x * JudgeRun());
        }

 //       anim.SetBool("defense", playerInput.defense);

        if (playerInput.roll|| anim.GetBool("isGround") == false && rigid.velocity.magnitude > 7f)      //roll or jab
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (playerInput.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if ((playerInput.rb||playerInput.lb)&&(CheckState("ground")||CheckStateTag("attackR")|| CheckStateTag("attackL")) &&canAttack)
        {
            if (playerInput.rb)
            {
                anim.SetBool("R0L1", false);
                anim.SetTrigger("attack");
            }else if (playerInput.lb&&!leftIsShield)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");
            }
        }

        if ((playerInput.lrt != 0) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (playerInput.lrt == -1)
            {
                //do right heavy attack
                print(playerInput.lrt);
            }
            else if (playerInput.lrt == 1)
            {
                if (!leftIsShield)
                {
                    //do left heavy attack
                }
                else
                {
                    //shield defense
                    anim.SetTrigger("counterBack");
                }

            }
        }


        if ((CheckState("ground")||CheckState("blocked")))
        {
            if (leftIsShield)
            {
                anim.SetBool("defense", playerInput.defense);
                if (playerInput.defense)
                {
                    anim.SetLayerWeight(anim.GetLayerIndex("defense"),1);
                }
                else
                {
                    anim.SetLayerWeight(anim.GetLayerIndex("defense"),0);
                }
            }
            else
            {
                    anim.SetLayerWeight(anim.GetLayerIndex("defense"),0);

            }

        }

        //如果没锁定
        if (!camcon.lockState)
        {
            if (playerInput.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dvec, 0.3f);
            }
            //lockPlanar是锁刚体速度向量
            if (lockPlanar==false)
            {
                planarVec = playerInput.Dmag * model.transform.forward * walkSpeed* ((playerInput.run) ? runMultiplier : 1.0f);

            }
        }
        //如果锁定了
        else
        {
            if (trackDirection == false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized*0.99f+ model.transform.forward*0.01f;
            }

            if (lockPlanar == false)
            {
                planarVec = playerInput.Dvec * walkSpeed * ((playerInput.run) ? runMultiplier : 1.0f);
            }
        }

        if (playerInput.action)
        {
            OnAction.Invoke();
        }

        leftIsShield = !playerInput.dualHands;
      
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z)+thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    public bool CheckState(string stateName,string layerName="Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }
    
    public bool CheckStateTag(string tagName,string layerName="Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return result;
    }

    float JudgeRun()
    {
        return (playerInput.run) ? 2.0f : 1.6f;
    }

    ///
    /// Message processing block
    ///
    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        playerInput.inputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
        
    }

    public void OnGroundEnter()
    {
        playerInput.inputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }

    public void OnGroundExit()
    {
        col.material = frictionzero;
    }

    public void OnFallEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        playerInput.inputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }

    public void OnJabEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
    //    thrustVec = model.transform.forward * -jabVelocity;
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");

    }

    public void OnAttack1hAEnter()
    {
        playerInput.inputEnabled = false;

    }
    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }

    public void OnHitEnter()
    {
        playerInput.inputEnabled = false;
        model.SendMessage("WeaponDisable");
//        planarVec = Vector3.zero;
        
    }

    public void OnUpdateRM (object _deltaPos)
    {
        if(CheckState("attack1handC"))
        {
            deltaPos += (deltaPos + (Vector3)_deltaPos)/2.0f;

        }
    }

    public void OnBlockedEnter()
    {
        playerInput.inputEnabled = false;

    }

    public void OnDieEnter()
    {
        playerInput.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");

    }

    public void OnstunnedEnter()
    {
        playerInput.inputEnabled = false;
        planarVec = Vector3.zero;

    }

    public void OnCounterBackEnter()
    {
        playerInput.inputEnabled = false;
        planarVec = Vector3.zero;

    }

    public void OnCounterBackExit()
    {
        model.SendMessage("CounterBackDisable");
    }

    public void OnLockEnter()
    {
        playerInput.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");

    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetBool(string boolName,bool value)
    {
        anim.SetBool(boolName, value);
    }

    public Animator animator
    {
        get { return this.anim; }
        set { anim = animator; }
    }
}
