using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaperEvent : MyEvent {
    PressEvent pressEvent = null;
    public DiaperEvent(EventType type, float time) : base(type)
    {
        this.myTimer.StartTiming(time, CountDownEnd);
    }
    public override void CountDownEnd()
    {
        base.CountDownEnd();
        pressEvent = new PressEvent(EventType.pressEvent, 1);
        //时间结束，还未处理完的，同时增加压力值
    }
    public override void StopPressAdd()
    {
        if (pressEvent == null)
            return;
        if (pressEvent != null)
        {
            pressEvent.myTimer.Destory();
        }
        pressEvent = null;
    }
}
