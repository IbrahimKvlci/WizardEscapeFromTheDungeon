using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    event EventHandler<OnSwitchMagicPressedEventArgs> OnSwitchMagicPressed;
    public class OnSwitchMagicPressedEventArgs : EventArgs
    {
        public int magicIndex;
    }

    Vector2 GetMovementVector();
    bool CheckJumpButton();
    bool FireButtonPressed();
    bool DashButtonPressed();
    bool HoldObjectButtonPressed();
    int GetMagicIndex();
    /// <summary>
    /// It gives one of the three value (-1,0,1)
    /// </summary>
    /// <returns></returns>
    int GetScrollYValueSign();
}
