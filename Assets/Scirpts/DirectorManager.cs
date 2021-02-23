using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent (typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface
{
    public PlayableDirector playableDirector;

    [Header("=== Timeline assets ===")]
    public TimelineAsset frontStab;
    public TimelineAsset openBox;
    public TimelineAsset leverUp;

    [Header("=== Assets Settings ===")]
    public ActorManager attacker;
    public ActorManager victim;


    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.playOnAwake = false;

    }

    void Update()
    {

    }

    public void PlayTimeline(string timelineName,ActorManager attacker,ActorManager victim)
    {
        if (playableDirector.state == PlayState.Playing)
        {
            return;
        }
        if (timelineName == "frontStab")
        {
            playableDirector.playableAsset = Instantiate(frontStab);
            //找到相应的timeline给导演
            TimelineAsset timeline = (TimelineAsset)playableDirector.playableAsset;
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Attacker Script")
                {
                    playableDirector.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myBehav.myFloat = 111;
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        playableDirector.SetReferenceValue(myClip.actorManager.exposedName, attacker);
                    }
                }
                else if (track.name == "Victim Script")
                {
                    playableDirector.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myBehav.myFloat = 222;
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        playableDirector.SetReferenceValue(myClip.actorManager.exposedName, victim);
                    }
                }
                else if (track.name == "Attacker Animation")
                {
                    playableDirector.SetGenericBinding(track, attacker.actorController.animator);
                }
                else if (track.name == "Victim Animation")
                {
                    playableDirector.SetGenericBinding(track, victim.actorController.animator);
                }
            }

            playableDirector.Evaluate();

            playableDirector.Play();
        }
        else if (timelineName == "openBox")
        {
            playableDirector.playableAsset = Instantiate(openBox);
            //找到相应的timeline给导演
            TimelineAsset timeline = (TimelineAsset)playableDirector.playableAsset;
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Player Script")
                {
                    playableDirector.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        playableDirector.SetReferenceValue(myClip.actorManager.exposedName, attacker);
                    }
                }
                else if (track.name == "Box Script")
                {
                    playableDirector.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        playableDirector.SetReferenceValue(myClip.actorManager.exposedName, victim);
                    }
                }
                else if (track.name == "Player Animation")
                {
                    playableDirector.SetGenericBinding(track, attacker.actorController.animator);
                }
                else if (track.name == "Box Animation")
                {
                    playableDirector.SetGenericBinding(track, victim.actorController.animator);
                }
            }

            playableDirector.Evaluate();

            playableDirector.Play();
        }
        else if (timelineName == "leverUp")
        {
            playableDirector.playableAsset = Instantiate(leverUp);
            //找到相应的timeline给导演
            TimelineAsset timeline = (TimelineAsset)playableDirector.playableAsset;
            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Player Script")
                {
                    playableDirector.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        playableDirector.SetReferenceValue(myClip.actorManager.exposedName, attacker);
                    }
                }
                else if (track.name == "Lever Script")
                {
                    playableDirector.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        playableDirector.SetReferenceValue(myClip.actorManager.exposedName, victim);
                    }
                }
                else if (track.name == "Player Animation")
                {
                    playableDirector.SetGenericBinding(track, attacker.actorController.animator);
                }
                else if (track.name == "Lever Animation")
                {
                    playableDirector.SetGenericBinding(track, victim.actorController.animator);
                }
            }

            playableDirector.Evaluate();

            playableDirector.Play();
        }
    }

    public bool JudgeStateIsPlaying()
    {
        bool state = false;
        if (playableDirector.state == PlayState.Playing)
        {
            state = true;
        }
        else
        {
            state = false;

        }
            return state;
    }
}
