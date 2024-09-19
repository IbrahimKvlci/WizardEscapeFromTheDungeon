using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject :MonoBehaviour, IHoldable
{
    [SerializeField] private Rigidbody rb;
    private Transform holdingPos;
    [SerializeField] private Outline outline;


    private void Start()
    {
        SetColor(Color.yellow);
    }

    public void Drop()
    {
        holdingPos= null;
        rb.useGravity = true;
        SetColor(Color.yellow);
    }

    public void Hold(Transform point)
    {
        holdingPos = point;
        rb.isKinematic = false;
        rb.useGravity = false;
        transform.SetParent(null);
        SetColor(Color.blue);
    }

    public void SetColor(Color color)
    {
        outline.OutlineColor= color;
    }

    private void FixedUpdate()
    {
        if (holdingPos != null)
        {
            float lerpSpeed = 10f;
            //Vector3 newPos = Vector3.Lerp(transform.position, holdingPos.position, Time.deltaTime * lerpSpeed);

            if (Vector3.Distance(transform.position,holdingPos.position)<=0.5f)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.AddForce((holdingPos.position - transform.position).normalized);

            }
        }
    }


}
