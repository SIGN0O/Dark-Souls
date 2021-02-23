using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private DataBase weaponDB;
    public WeaponFactory(DataBase _weaponDataBase)
    {
        weaponDB = _weaponDataBase;
    }

    public GameObject CreateWeapon(string weaponName,Vector3 pos,Quaternion rot)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(prefab, pos, rot);
        WeaponData weaponData = obj.AddComponent<WeaponData>();
        weaponData.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;
        return obj;
    }
    public GameObject CreateWeapon(string weaponName,string side,WeaponManager wm)
    {
        WeaponController wc;
        if (side == "L")
        {
            wc = wm.weaponControllerLeft;
        }else if (side == "R")
        {
            wc = wm.weaponControllerRight;
        }
        else
        {
            return null;
        }
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(prefab);
        obj.transform.parent = wc.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        WeaponData weaponData = obj.AddComponent<WeaponData>();
        weaponData.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;
        wc.weaponData = weaponData;
        return obj;
    }
}
