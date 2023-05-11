using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_GROUNDED = "IsGrounded";
    [SerializeField] private PlayerMovement player;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
        // animator.SetBool(IS_JUMPING, player.IsJumping());
        // animator.SetBool(IS_GROUNDED, player.IsGrounded());
    }
}
