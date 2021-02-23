using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WeaponManager testwm; 

    private static GameManager instance;
    private DataBase dataBase;
    private WeaponFactory weaponFactory;

    private void Awake()
    {
        CheckGameObject();
        CheckSingle();
    }

    private void Start()
    {
        InitWeaponDataBase();
        InitWeaponFactory();

        GameObject weapon = weaponFactory.CreateWeapon("Sword", "R", testwm);
        testwm.UpdateWeaponCollider("R", weapon.GetComponent<Collider>());
        testwm.ChangeDualHands(false);

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 10, 150, 30), "R: Sword"))
        {
            testwm.UnloadWeapon("R");
            GameObject weapon = weaponFactory.CreateWeapon("Sword", "R",testwm);
            testwm.UpdateWeaponCollider("R", weapon.GetComponent<Collider>());
        testwm.ChangeDualHands(false);
        }
        if (GUI.Button(new Rect(0, 50, 150, 30), "R: Falchion"))
        {
            testwm.UnloadWeapon("R");
            GameObject weapon = weaponFactory.CreateWeapon("Falchion", "R", testwm);
            testwm.UpdateWeaponCollider("R", weapon.GetComponent<Collider>());
        testwm.ChangeDualHands(true);
        }
        if (GUI.Button(new Rect(0, 90, 150, 30), "R: Mace"))
        {
            testwm.UnloadWeapon("R");
            GameObject weapon = weaponFactory.CreateWeapon("Mace", "R", testwm);
            testwm.UpdateWeaponCollider("R", weapon.GetComponent<Collider>());
        testwm.ChangeDualHands(false);
        }
        if (GUI.Button(new Rect(0, 130, 150, 30), "R: Clear All Weapons"))
        {
            testwm.UnloadWeapon("R");
        testwm.ChangeDualHands(false);
        }
        if (GUI.Button(new Rect(200, 10, 150, 30), "L: Sword"))
        {
            testwm.UnloadWeapon("L");
            GameObject weapon = weaponFactory.CreateWeapon("Sword", "L", testwm);
            testwm.UpdateWeaponCollider("L", weapon.GetComponent<Collider>());
            testwm.ChangeDualHands(false);
        }
    }

    /// 
    /// 
    /// 

    private void CheckSingle()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void CheckGameObject()
    {
        if (tag == "GM")
        {
            return;
        }
        Destroy(this);
    }

    private void InitWeaponDataBase()
    {
        dataBase = new DataBase();
    }

    private void InitWeaponFactory()
    {
        weaponFactory = new WeaponFactory(dataBase);
    }
}
