using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;

    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one MouseWorld!" + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Update() {
        transform.position = GetPosition();
    }

    public static Vector3 GetPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
