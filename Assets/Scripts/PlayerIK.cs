using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class PlayerIK : MonoBehaviour
{
    [SerializeField]
    private Transform LeftHandIKTarget;
    [SerializeField]
    private Transform RightHandIKTarget;
    [SerializeField]
    private Transform LeftElbowIKTarget;
    [SerializeField]
    private Transform RightElbowIKTarget;

    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
        Animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
        Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        
        Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        Animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIKTarget.position);
        Animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandIKTarget.position);
        Animator.SetIKHintPosition(AvatarIKHint.LeftElbow, LeftElbowIKTarget.position);
        Animator.SetIKHintPosition(AvatarIKHint.RightElbow, RightElbowIKTarget.position);

        Animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandIKTarget.rotation);
        Animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandIKTarget.rotation);
    }
}
