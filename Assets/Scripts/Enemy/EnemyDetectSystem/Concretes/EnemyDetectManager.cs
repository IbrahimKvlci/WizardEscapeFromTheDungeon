using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectManager : IEnemyDetectService
{
    public bool IsVisibleInCamera(Camera camera, Enemy enemy)
    {
        var planes=GeometryUtility.CalculateFrustumPlanes(camera);
        var point=enemy.transform.position;

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
