using Spine.Unity;
using UnityEngine;

public class SpineAnimator : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;

    [SerializeField, SpineSkin(dataField = "skeletonAnimation")]
    private string[] skinNames;

    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        if (skinNames.Length > 0)
        {
            var randomSkin = skinNames[Random.Range(0, skinNames.Length)];
            Debug.Log($"Setting skin to {randomSkin}");
            Debug.Log(skeletonAnimation.skeleton);
            skeletonAnimation.skeleton.SetSkin(randomSkin);
        }
    }
    

    public void PlayAnimation(string animationName) => skeletonAnimation.AnimationState.SetAnimation(0, animationName, false);
    public void PlayAnimationLoop(string animationName) => skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);

    public void PlayOneShot(string animationName)
    {
        var state = skeletonAnimation.AnimationState;

        state.SetAnimation(1, animationName, false);
        state.AddEmptyAnimation(1, 0, 0f);
    }

    public void PlayOneShotTrack0(string animationName)
    {
        var state = skeletonAnimation.AnimationState; 
        state.SetAnimation(1, animationName, false); 
        state.AddEmptyAnimation(1, 0.4f, 0f);
    }

    
}
