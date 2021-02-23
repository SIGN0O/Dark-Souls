using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager weaponManager;
    public WeaponData weaponData;
    void Awake()
    {
        weaponData = GetComponentInChildren<WeaponData>();
    }

    public float GetATK()
    {
        //if (weaponData == null)
        //    weaponData.ATK = 0;
        return weaponData.ATK+weaponManager.actorManager.stateManager.ATK;
    }
}
