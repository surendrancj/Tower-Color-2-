using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public Animation anim;

    public AnimationClip[] allAnimationClips;

    string currentAnimationName = "";

    public void PlayAnimation(string _animationName){

        if(currentAnimationName == _animationName){
            anim[currentAnimationName].speed = 1f;
        }

        AnimationClip clip = null;
        foreach(AnimationClip c in allAnimationClips){
            if(c.name == _animationName)
                clip = c;
        }
        if(clip != null){
            anim.clip = clip;
            currentAnimationName = clip.name;
            anim.Play();
        }
    }

    public void StopAnimation(){
        anim.Stop();
    }

    public void PauseAnimation(){
        anim[currentAnimationName].speed = 0;
    }
}
