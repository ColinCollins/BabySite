using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEventTimer
{
    Timer myTimer;
    public HandleEventTimer(float times, CompleteEvent onCompleted)
    {
        myTimer = Timer.CreateTimer("myTimer");
        myTimer.StartTiming(times, onCompleted);
    }
}
