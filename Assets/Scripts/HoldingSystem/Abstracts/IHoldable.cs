using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoldable
{
    void Hold(Transform point);
    void Drop();
    void SetColor(Color color);

}
