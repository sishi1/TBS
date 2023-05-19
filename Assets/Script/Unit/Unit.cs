using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private const string IS_WALKING = "IsWalking";

    [SerializeField] Animator unitAnimator;
    private Vector3 targetPosition;

    [SerializeField] private float unitMoveSpeed = 4f;
    [SerializeField] private float unitRotateSpeed = 10f;

    private void Awake() {
        targetPosition = transform.position;
    }

    private void Update() {
        float stopDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * unitMoveSpeed * Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, unitRotateSpeed * Time.deltaTime);

            unitAnimator.SetBool(IS_WALKING, true);
        } else {
            unitAnimator.SetBool(IS_WALKING, false);
        }
    }

    public void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }
}
