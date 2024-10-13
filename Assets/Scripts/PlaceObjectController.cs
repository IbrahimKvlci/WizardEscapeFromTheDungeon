using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectController : MonoBehaviour
{
    enum PlaceObjectTagEnum
    {
        Wood,
    }

    [SerializeField] private PlaceObjectTagEnum placeObjectTagEnum;
    [SerializeField] private GameObject placedObject;

    private bool isPlaced;

    private void Start()
    {
        isPlaced = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(placeObjectTagEnum.ToString())&&!isPlaced)
        {
            isPlaced=true;

            other.gameObject.SetActive(false);
            placedObject.SetActive(true);
        }    
    }
}
