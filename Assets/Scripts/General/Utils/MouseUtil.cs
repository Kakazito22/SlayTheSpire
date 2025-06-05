using Unity.VisualScripting;
using UnityEngine;

public static class MouseUtil
{
    private static Camera mainCamera = Camera.main;
    
    public static Vector3 GetMouseWorldPosition(float zValue = 0f)
    {
        Plane groundPlane = new Plane(mainCamera.transform.forward, new Vector3(0,0,zValue));
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }

        return Vector3.zero;
    }
}
