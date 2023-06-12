using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour {

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        DestroyUnitActionButtons();
        CreateUnitActionButtons();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, System.EventArgs e) {
        DestroyUnitActionButtons();
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons() {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray()) {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void DestroyUnitActionButtons() {
        foreach (Transform buttonTransform in actionButtonContainerTransform) {
            Destroy(buttonTransform.gameObject);
        }
    }
}
