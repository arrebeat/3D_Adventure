using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public enum AnimationType
    {
        None,
        Idle,
        Run,
        Attack,
        Death
    }

    [System.Serializable]
    public class AnimationSetup
    {
        public AnimationType animationType;
        public string trigger;
    }

    public class AnimationBase : MonoBehaviour
    {
        public Animator animator;
        public List<AnimationSetup> animationSetups;

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            var setup = animationSetups.Find(x => x.animationType == animationType);
            if (setup != null)
            {
                animator.SetTrigger(setup.trigger);
            }
        }   

    }
}
