using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private Vector3 targetPosition;
    [SerializeField] private float moveSpeed = 4f;

    private void Update() {
        float stopDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0)) {
            Move(MouseWorld.GetPosition());
        }
    }

    private void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }
}
