using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Utils
{   
    public IEnumerator GamepadVibration(float lowFrequencyForce, float highFrequencyForce, float time)
    {
        Gamepad.current.SetMotorSpeeds(lowFrequencyForce, highFrequencyForce);
        yield return new WaitForSeconds(time);
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
