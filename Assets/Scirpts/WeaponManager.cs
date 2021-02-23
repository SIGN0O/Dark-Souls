using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    private Collider weaponColL;
    private Collider weaponColR;

    public GameObject weaponHandleLeft;
    public GameObject weaponHandleRight;

    public WeaponController weaponControllerLeft;
    public WeaponController weaponControllerRight;

    private void Start()
    {
        try
        {
            weaponHandleLeft = transform.DeepFind("weaponHandleL").gameObject;
            weaponControllerLeft = BindWeaponController(weaponHandleLeft);
            weaponColL = weaponHandleLeft.GetComponentInChildren<Collider>();
            weaponColL.enabled = false;

        }
        catch (System.Exception)
        {
            //
            //it there is no "weaponHandleLeft" or related objects.
            //

        }
        try
        {
            weaponHandleRight = transform.DeepFind("weaponHandleR").gameObject;
            weaponControllerRight = BindWeaponController(weaponHandleRight);
            weaponColR = weaponHandleRight.GetComponentInChildren<Collider>();
            weaponColR.enabled = false;
        }
        catch (System.Exception)
        {
            //
            //it there is no "weaponHandleRight" or related objects.
            //

        }
    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if (tempWc == null)
        {
            tempWc = targetObj.AddComponent<WeaponController>();
        }
        tempWc.weaponManager = this;
        return tempWc;
    }

    public void UpdateWeaponCollider(string side,Collider col)
    {
        if (side == "L")
        {
            weaponColL = col;
        }
        else if (side == "R")
        {
            weaponColR = col;
        }

    }

    public void UnloadWeapon(string side)
    {
        if (side == "L")
        {
            foreach (Transform weapon in weaponHandleLeft.transform)
            {
                weaponColL = null;
                weaponControllerLeft.weaponData = null;
                Destroy(weapon.gameObject);
            }
            
        }
        else if (side == "R")
        {
            foreach (Transform weapon in weaponHandleRight.transform)
            {
                weaponColR = null;
                weaponControllerLeft.weaponData = null;
                Destroy(weapon.gameObject);
            }
        }
    }

    public void WeaponEnable()
    {
        if (actorManager.actorController.CheckStateTag("attackL"))
        {
            weaponColL.enabled = true;
        }else if(actorManager.actorController.CheckStateTag("attackR"))
        {
            weaponColR.enabled = true;
        }
    }

    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        weaponColL.enabled = false;
    }

    public void CounterBackEnable()
    {
        actorManager.SetIsCounterBack(true);
    }
    
    public void CounterBackDisable()
    {
        actorManager.SetIsCounterBack(false);

    }

    public void ChangeDualHands(bool dualOn)
    {
        actorManager.ChangeDualHands(dualOn);
    }
}
