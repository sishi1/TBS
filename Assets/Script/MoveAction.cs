using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour {

    private const string IS_WALKING = "IsWalking";

    [SerializeField] Animator unitAnimator;

    [SerializeField] private float unitMoveSpeed = 4f;
    [SerializeField] private float unitRotateSpeed = 10f;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;

    private void Awake() {
        unit = GetComponent<Unit>();

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

    public List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
