using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private List<Unit> unitList;
    private List<Unit> friendlyUnitList;
    private List<Unit> enemyUnitList;

    private void Awake() {
        unitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
    }

    private void Start() {
        if (Instance != null) {
            Debug.LogError("More than one UnitManager!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitDead(object sender, System.EventArgs e) {
        Unit unit = sender as Unit;

        unitList.Remove(unit);

        if (unit.IsEnemy()) {
            enemyUnitList.Remove(unit);
        } else {
            friendlyUnitList.Remove(unit);
        }
    }

    private void Unit_OnAnyUnitSpawned(object sender, System.EventArgs e) {
        Unit unit = sender as Unit;

        unitList.Add(unit);

        if (unit.IsEnemy()) {
            enemyUnitList.Add(unit);
        } else {
            friendlyUnitList.Add(unit);
        }
    }

    public List<Unit> GetUnitList() => unitList;
    public List<Unit> GetFriendlyUnitList() => friendlyUnitList;
    public List<Unit> GetEnemyUnitList() => enemyUnitList;
}
