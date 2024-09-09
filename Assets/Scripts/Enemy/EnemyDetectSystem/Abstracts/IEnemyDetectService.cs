using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDetectService
{
    bool IsVisibleInCamera(Camera camera,Enemy enemy);
}
