using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; //For controller rumble

public class GamepadRumble : MonoBehaviour
{
    /// <summary>
    /// This method causes gamepad to vibrate, will not stop until receives a false boolean
    /// </summary>
    public static void Rumble(bool rumble)
    {
        if (rumble)
        {
            GamePad.SetVibration(PlayerIndex.One, .75f, 1f);
        }
        else
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        }
    }
}