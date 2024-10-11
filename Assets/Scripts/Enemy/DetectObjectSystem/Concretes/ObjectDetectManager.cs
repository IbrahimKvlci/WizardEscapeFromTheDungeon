using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetectManager : IObjectDetectService
{
    public bool IsVisibleInCamera(Camera camera, GameObject gameObject)
    {
        var planes=GeometryUtility.CalculateFrustumPlanes(camera);
        var point= gameObject.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }
}
