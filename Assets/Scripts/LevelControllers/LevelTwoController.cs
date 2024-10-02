using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoController : MonoBehaviour
{

    [SerializeField] private List<Torch> torchList;
    [SerializeField] private List<int> torchNumberList;
    [SerializeField] private Door doorWithTorch;

    private bool isDoorOpened;

    private void Start()
    {
        isDoorOpened = false;

        foreach (Torch torch in torchList)
        {
            torch.ExtinguishTheTorch();
        }
    }

    private void Update()
    {
        if(CheckTorchesLight()&&!isDoorOpened)
        {
            doorWithTorch.OpenDoor();
            isDoorOpened=true;
        }
    }

    private bool CheckTorchesLight()
    {

        for (int i = 0; i < torchList.Count; i++)
        {
            bool willTorchLight=false;

            for (int j = 0; j < torchNumberList.Count; j++)
            {
                if (i == torchNumberList[j] - 1)
                {
                    if (!torchList[i].isTorchLighted)
                    {
                        return false;
                    }
                    willTorchLight=true;
                }
            }
            if (!willTorchLight)
            {
                if (torchList[i].isTorchLighted)
                {
                    return false;
                }
            }

        }

        return true;
    }




}
