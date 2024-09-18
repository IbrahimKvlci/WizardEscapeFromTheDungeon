using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameIoC : MonoBehaviour
{
    public static InGameIoC Instance { get; set; }

    public IInputService InputService { get; set; }
    public IPlayerMovementService PlayerMovementService { get; set; }
    public IObjectDetectService ObjectDetectService { get; set; }

    private void Awake()
    {
        Instance = this;
        InputService=InputManager.Instance;
        PlayerMovementService = new PlayerMovementManager(InputService);
        ObjectDetectService = new ObjectDetectManager();
    }
}
