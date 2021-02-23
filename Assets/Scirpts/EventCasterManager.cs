using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManagerInterface
{
    public string eventName;
    public bool active;
    public Vector3 offset=new Vector3(0,0,0.5f);

    private void Start()
    {
        if (actorManager == null)
        {
            actorManager = GetComponentInParent<ActorManager>();
        }
    }

}
