using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager actorManager;
    public float myFloat;
    PlayableDirector playableDirector;
    public bool lockState = false;


    public override void OnPlayableCreate (Playable playable)
    {
        
    }
    public override void OnGraphStart (Playable playable)
    {

    }
    public override void OnGraphStop (Playable playable)
    {
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
    }
    public override void OnBehaviourPause(Playable playable,FrameData frameData)
    {
        actorManager.LockActorController(false);

    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        actorManager.LockActorController(true);
    }
}
