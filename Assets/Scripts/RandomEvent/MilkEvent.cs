using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkEvent : MyEvent {

    GameObject processObj;
    public MilkEvent(EventType type, float time) : base(type)
    {
        processObj = GameObject.Instantiate(Resources.Load("prefabs/processbottom")) as GameObject;
        processObj.transform.parent = GameObject.Find("boiled water").transform;
        processObj.transform.localPosition = Vector3.zero;
        this.myTimer.StartTiming(time, OnComplete, OnUpdate);
    }
    void OnComplete()
    {
        GameObject.Find("Human").GetComponent<HumanSystem>().state = HumanState.isIdle;
        EventControl.instance.isMilk = true;
        GameObject.Destroy(processObj);
    }
    void OnUpdate(float ratio)
    {
        processObj.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(ratio, 1, 1);
    }
}
