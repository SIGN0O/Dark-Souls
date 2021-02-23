using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    private CapsuleCollider defCol;
    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = new Vector3(0, 1.0f, 0);
        defCol.height = 2.0f;
        defCol.radius = 0.25f;
        defCol.isTrigger = true;
    }
    private void OnTriggerEnter(Collider col)
    {
        WeaponController targetWeaponController = col.GetComponentInParent<WeaponController>();
        if (targetWeaponController == null)
        {
            return;
        }

        GameObject attacker = targetWeaponController.weaponManager.actorManager.gameObject;
        GameObject receiver = actorManager.actorController.model;

        if (col.tag == "Weapon")
        {
            actorManager.TryDoDamage(targetWeaponController, CheckAngleTarget(receiver,attacker,70), CheckAngleExecutor(receiver,attacker,30));
        }
    }

    public static bool CheckAngleExecutor(GameObject executor,GameObject target,float executorAngleLimit)
    {
        Vector3 counterDir = target.transform.position - executor.transform.position;

        float counterAngel1 = Vector3.Angle(executor.transform.forward, counterDir);
        float counterAngel2 = Vector3.Angle(target.transform.forward, executor.transform.forward);

        bool counterVaild = ((counterAngel1 <= executorAngleLimit) && (Mathf.Abs(counterAngel2 - 180) < executorAngleLimit));
        return counterVaild;
    }
    public static bool CheckAngleTarget(GameObject executor, GameObject target, float targrtAngleLimit)
    {
        Vector3 attackingDir = executor.transform.position - target.transform.position;

        float attackingAngle1 = Vector3.Angle(target.transform.forward, attackingDir);

        bool attackValid = (attackingAngle1 <= targrtAngleLimit);
        return attackValid;
    }
}
