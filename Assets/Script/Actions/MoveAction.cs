using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAction : BaseAction {

    private const string IS_WALKING = "IsWalking";

    [SerializeField] Animator unitAnimator;

    [SerializeField] private float unitMoveSpeed = 4f;
    [SerializeField] private float unitRotateSpeed = 10f;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;

    protected override void Awake() {
        base.Awake();

        targetPosition = transform.position;
    }

    private void Update() {
        if (!isActive) return;

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float stopDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance) {
            transform.position += moveDirection * unitMoveSpeed * Time.deltaTime;

            unitAnimator.SetBool(IS_WALKING, true);
        } else {
            unitAnimator.SetBool(IS_WALKING, false);
            isActive = false;
            onActionComplete();
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, unitRotateSpeed * Time.deltaTime);

    }

    public void Move(GridPosition gridPosition, Action onActionComplete) {
        this.onActionComplete = onActionComplete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }

    public List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        // Cycle through all the potential possible gridposition based on the max move range
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

                if (unitGridPosition == testGridPosition) continue;

                if (LevelGrid.Instance.HadAnyUnitOnGridPosition(testGridPosition)) continue;

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition) {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public override string GetActionName() => "Move";

}
