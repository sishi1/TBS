using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string SHOOT = "Shoot";

    [SerializeField] private Animator animator;

    private void Awake() {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction)) {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction)) {
            shootAction.OnShooting += ShootAction_OnShooting;
        }
    }

    private void ShootAction_OnShooting(object sender, System.EventArgs e) {
        animator.SetTrigger(SHOOT);
    }

    private void MoveAction_OnStopMoving(object sender, System.EventArgs e) {
        animator.SetBool(IS_WALKING, false);
    }

    private void MoveAction_OnStartMoving(object sender, System.EventArgs e) {
        animator.SetBool(IS_WALKING, true);
    }
}
