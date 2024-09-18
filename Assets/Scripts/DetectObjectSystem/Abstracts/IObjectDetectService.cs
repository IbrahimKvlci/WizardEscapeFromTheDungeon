using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectDetectService
{
    bool IsVisibleInCamera(Camera camera,GameObject gameObject);
}
