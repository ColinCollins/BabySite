using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEvent : MyEvent {
    
    public PressEvent(EventType type, float time) : base(type)
    {
        //Debug.Log("111111playtime=" + time);
        this.myTimer.StartTiming(time, OnComplete, null, true, true, false, 0, 0, true);
    }
    void OnComplete()
    {
        HumanSystem human = GameObject.Find("Human").GetComponent<HumanSystem>();
        //压力值增加
        //Test.press += 2;
        human.patient += 0.5f;
    }
}
