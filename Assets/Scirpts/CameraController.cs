using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 80.0f;
//    public float cameraDampValue = 0.2f;

    private IUserInput playerInput;
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject model;
    private Camera mainCamera;
    public Image lockDot;
    public bool lockState = false;
    public bool isAI = false;

    Dictionary<float, LockTarget> lockTargets = new Dictionary<float, LockTarget>();
    List<LockTarget> SortlockTargets = new List<LockTarget>();
    int targetNum=0;

    private float tempEulerX=20;
    [SerializeField]
    private LockTarget lockTarget;

//    private Vector3 cameraDampVelocity;

    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        ActorController actorController = playerHandle.GetComponent<ActorController>();
        playerInput = actorController.playerInput;
        model = actorController.model;

        if (!isAI)
        {
            mainCamera = Camera.main;
            lockDot.enabled = false;
//            Cursor.lockState = CursorLockMode.Locked;

        }

    }

    void LateUpdate()
    {
        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;
            //camera的左右旋转
            playerHandle.transform.Rotate(Vector3.up, playerInput.Jright * horizontalSpeed * Time.deltaTime);
            //camera的上下旋转
            tempEulerX -= playerInput.Jup * verticalSpeed * Time.deltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -30, 60);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            if (!isAI)
            {
                lockDot.transform.position = mainCamera.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));

            }
            cameraHandle.transform.LookAt(lockTarget.obj.transform.position + new Vector3(0,lockTarget.halfHeight/2.0f,0));

            if(Vector3.Distance(model.transform.position, lockTarget.obj.transform.position)>10.0f)
            {
                SortlockTargets.Remove(lockTarget);
            }

            if (lockTarget.actorManager!=null&& lockTarget.actorManager.stateManager.isDie)
            {
                SortlockTargets.Remove(lockTarget);

            }
        }

        if (!isAI)
        {
            mainCamera.transform.position = transform.position;
            //mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position,ref cameraDampVelocity,cameraDampValue);
            mainCamera.transform.LookAt(cameraHandle.transform);
        }
    }

    public void LockUnLock()
    {
        //try to lock
        if (lockTarget == null)
        {
            Vector3 modelOrigin1 = model.transform.position;
            Vector3 modelOrigin2 = modelOrigin1+new Vector3(0,1,0);
            Vector3 boxCenter = modelOrigin2 + model.transform.forward * 4.0f;
            Collider[] cols = Physics.OverlapSphere(boxCenter, 5f ,LayerMask.GetMask(isAI?"Player":"Enemy"));
            foreach (var col in cols)
            {
                lockTarget = new LockTarget(col.gameObject, col.bounds.extents.y);
                if (lockTarget.actorManager != null && lockTarget.actorManager.stateManager.isDie)
                {
                    lockTarget = null;
                    continue;
                }
                lockTargets.Add(Vector3.Distance(model.transform.position, col.gameObject.transform.position), lockTarget);
            }

            if (lockTargets.Count != 0)
            {
                lockTargets = lockTargets.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                SortlockTargets = lockTargets.Values.ToList<LockTarget>();
                lockTargets.Clear();
                lockTarget = SortlockTargets[targetNum];
                if (!isAI)
                {
                    lockDot.enabled = true;
                }
                lockState = true;
            }
        }
        else
        {
            LockProcessA(null, false, false, isAI);
            SortlockTargets.Clear();
            targetNum = 0;
        }
    }

    public void ChangeEnemys(float button)
    {
        if (SortlockTargets.Count <= 0)
        {
            LockUnLock();
            return;
        }
        if (button == 1)
        {
            targetNum++;

        }else if (button == -1)
        {
            targetNum--;
        }
        if(targetNum == SortlockTargets.Count)
            targetNum = 0;
        if (targetNum < 0)
            targetNum = SortlockTargets.Count - 1;
        lockTarget = SortlockTargets[targetNum];
    }

    private void LockProcessA(LockTarget lockTarget,bool lockDotEnabled,bool lockState,bool isAI)
    {
        this.lockTarget = lockTarget;
        this.lockState = lockState;
        if(!isAI)
        this.lockDot.enabled = lockDotEnabled;
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public ActorManager actorManager;
        public LockTarget(GameObject obj,float halfHeight)
        {
            this.obj = obj;
            this.halfHeight = halfHeight;
            this.actorManager = obj.GetComponent<ActorManager>();
        }
    }
    
}
