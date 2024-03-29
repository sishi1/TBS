using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem {

    private int width, height;
    private float cellSize;

    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float cellSize) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
            );
    }

    public void CreateDebugObjects(Transform debugPrefab) {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition) =>
        gridPosition.x >= 0 &&
        gridPosition.z >= 0 &&
        gridPosition.x < width &&
        gridPosition.z < height;

    public int GetWidth() => width;
    public int GetHeight() => height;
}

public struct GridPosition : IEquatable<GridPosition> {
    public int x, z;

    public GridPosition(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public override string ToString() {
        return $"x: {x}; z: {z}";
    }

    public static bool operator ==(GridPosition a, GridPosition b) => a.x == b.x && a.z == b.z;

    public static bool operator !=(GridPosition a, GridPosition b) => !(a == b);

    public override bool Equals(object obj) {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public override int GetHashCode() {
        return HashCode.Combine(x, z);
    }

    public bool Equals(GridPosition other) => this == other;

    public static GridPosition operator +(GridPosition a, GridPosition b) => new GridPosition(a.x + b.x, a.z + b.z);
    public static GridPosition operator -(GridPosition a, GridPosition b) => new GridPosition(a.x - b.x, a.z - b.z);
}