using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HoldingObjectController : MonoBehaviour
{
    public event EventHandler OnHoldingObject;
    public event EventHandler OnDroppedObject;

    [field: Header("Settings")]
    [SerializeField] private LayerMask layer;
    [SerializeField] private float moveHoldingObjectSpeedForwardBack;
    [SerializeField] private float rayLength;
    private bool firstFrameAfterHold;

    [field: Header("References")]
    [SerializeField] private Transform holdPosTransform;
    public GameObject HoldingObject { get; private set; }
    private IHoldable lastCastedHoldableObject;
    private IInputService _inputService;

    [field:Header("VisualReferences")]
    [SerializeField] private Transform playerWithWandMagicStartingPos, playerWithoutWandMagicStartingPos;
    [SerializeField] private GameObject holdingMagicParticle;
    private GameObject magicTrailParticleObject;

    [field: Header("VisualSettings")]
    [SerializeField] private float visualTimerMax;
    private float visualTimer;

    private void Awake()
    {
        _inputService = InGameIoC.Instance.InputService;
    }

    private void Start()
    {
        visualTimer = 0;
    }

    private void Update()
    {
        firstFrameAfterHold = true;
        if (Physics.BoxCast(Camera.main.transform.position, new Vector3(1, 1, 1), Camera.main.transform.forward, out RaycastHit hitInfo, Camera.main.transform.rotation, rayLength, layer))
        {
            if (hitInfo.transform.TryGetComponent(out IHoldable holdable))
            {
                //Holdable Object Found
                if (HoldingObject != hitInfo.transform.gameObject)
                    holdable.SetColor(Color.green);
                if (_inputService.HoldObjectButtonPressed() && HoldingObject != hitInfo.transform.gameObject)
                {
                    if (HoldingObject != null)
                        HoldingObject.GetComponent<IHoldable>().Drop();
                    HoldingObject = hitInfo.transform.gameObject;
                    holdPosTransform.position = hitInfo.transform.position;
                    holdable.Hold(holdPosTransform);
                    firstFrameAfterHold = false;

                    OnHoldingObject?.Invoke(this, EventArgs.Empty);
                }
                lastCastedHoldableObject = holdable;
            }
            else if (lastCastedHoldableObject != null && lastCastedHoldableObject != HoldingObject.GetComponent<IHoldable>())
            {
                lastCastedHoldableObject.SetColor(Color.yellow);
                lastCastedHoldableObject = null;
            }
        }
        else if (lastCastedHoldableObject != null)
        {
            if (HoldingObject != null)
            {
                if (HoldingObject.GetComponent<IHoldable>() != lastCastedHoldableObject)
                {
                    lastCastedHoldableObject.SetColor(Color.yellow);
                    lastCastedHoldableObject = null;
                }
            }
            else
            {
                lastCastedHoldableObject.SetColor(Color.yellow);
                lastCastedHoldableObject = null;
            }
        }
        #region DropHoldingObject
        if (HoldingObject != null && firstFrameAfterHold)
        {
            if (_inputService.HoldObjectButtonPressed())
            {
                HoldingObject.GetComponent<IHoldable>().Drop();
                HoldingObject = null;

                OnDroppedObject?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
        #region MoveHoldingObjectForwardBack
        if (HoldingObject != null)
        {

            if (_inputService.GetScrollYValueSign() > 0 && Mathf.Abs(Player.Instance.transform.position.z - holdPosTransform.position.z) < 5)
            {
                holdPosTransform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * moveHoldingObjectSpeedForwardBack);
            }
            if (_inputService.GetScrollYValueSign() < 0 && Mathf.Abs(Player.Instance.transform.position.z - holdPosTransform.position.z) > 0.5f)
            {
                holdPosTransform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * moveHoldingObjectSpeedForwardBack);
            }
        }
        #endregion

        if (HoldingObject != null)
        {
            //Player is holding an object

            #region Visual
            if (magicTrailParticleObject == null)
            {
                magicTrailParticleObject = Instantiate(holdingMagicParticle);
            }
            visualTimer += Time.deltaTime;
            float percentage = visualTimer / visualTimerMax;
            if (Player.Instance.HasWand)
            {
                magicTrailParticleObject.transform.position = Vector3.Lerp(playerWithWandMagicStartingPos.position, HoldingObject.transform.position, percentage);
            }
            else
            {
                magicTrailParticleObject.transform.position = Vector3.Lerp(playerWithoutWandMagicStartingPos.position, HoldingObject.transform.position, percentage);
            }
            if (percentage >= 1)
            {
                visualTimer = 0;
                Destroy(magicTrailParticleObject);
                magicTrailParticleObject = null;
            }
            #endregion 
        }
        else
        {
            #region Visual
            if(magicTrailParticleObject != null)
            {
                Destroy(magicTrailParticleObject);
                magicTrailParticleObject = null;
            }
            #endregion
        }
    }


}
