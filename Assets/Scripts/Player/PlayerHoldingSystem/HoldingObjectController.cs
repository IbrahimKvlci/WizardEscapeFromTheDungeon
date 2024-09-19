using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HoldingObjectController : MonoBehaviour
{
    public GameObject HoldingObject { get; private set; }
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform holdPosTransform;
    [SerializeField] private float moveHoldingObjectSpeedForwardBack;
    private bool firstFrameAfterHold;

    private IInputService _inputService;

    private void Awake()
    {
        _inputService=InGameIoC.Instance.InputService;
    }

    private void Update()
    {
        firstFrameAfterHold = true;
        if (Physics.BoxCast(Camera.main.transform.position, new Vector3(1, 1, 1), Camera.main.transform.forward, out RaycastHit hitInfo, Camera.main.transform.rotation, 20f, layer))
        {
            if (hitInfo.transform.TryGetComponent(out IHoldable holdable))
            {
                //Holdable Object Found

                if (Input.GetMouseButtonDown(1) && HoldingObject != hitInfo.transform.gameObject)
                {
                    if (HoldingObject != null)
                        HoldingObject.GetComponent<IHoldable>().Drop();
                    HoldingObject = hitInfo.transform.gameObject;
                    holdPosTransform.position = hitInfo.transform.position;
                    holdable.Hold(holdPosTransform);
                    firstFrameAfterHold=false;
                }
            }
        }

        if(HoldingObject != null&&firstFrameAfterHold)
        {
            if (Input.GetMouseButtonDown(1))
            {
                HoldingObject.GetComponent<IHoldable>().Drop();
                HoldingObject = null;
            }
        }

        if(HoldingObject!= null)
        {

            if (_inputService.GetScrollYValueSign()>0&&Mathf.Abs(Player.Instance.transform.position.z-holdPosTransform.position.z)<5)
            {
                holdPosTransform.Translate(new Vector3(0, 0, 1)*Time.deltaTime*moveHoldingObjectSpeedForwardBack);
            }
            if (_inputService.GetScrollYValueSign() < 0 && Mathf.Abs(Player.Instance.transform.position.z - holdPosTransform.position.z) > 0.5f)
            {
                holdPosTransform.Translate(new Vector3(0, 0, -1)*Time.deltaTime*moveHoldingObjectSpeedForwardBack);
            }
        }

    }


}
