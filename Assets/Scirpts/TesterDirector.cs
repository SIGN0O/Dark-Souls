using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TesterDirector : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Animator attacker;
    public Animator victim;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var track in playableDirector.playableAsset.outputs)
            {
                if(track.streamName== "Attacker Animation")
                {
                    playableDirector.SetGenericBinding(track.sourceObject, attacker);

                }else if(track.streamName == "Victim Animation")
                {
                    playableDirector.SetGenericBinding(track.sourceObject, victim);

                }
            }
            playableDirector.Play();
            
        }
    }
}
