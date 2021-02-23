using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator anim;
    private ActorController actorController;
    public Vector3 a;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        actorController = GetComponentInParent<ActorController>();
    }
    private void OnAnimatorIK()
    {
        if (actorController.leftIsShield)
        {
            if (anim.GetBool("defense") == false)
            {
                Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowerArm.localEulerAngles += a;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm,Quaternion.Euler(leftLowerArm.localEulerAngles));
            }
        }
    }
}
