using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDiaperEvent : MyEvent {

    GameObject processObj;
    public ChangeDiaperEvent(EventType type, float time) : base(type)
    {
        processObj = GameObject.Instantiate(Resources.Load("prefabs/processbottom")) as GameObject;
        processObj.transform.parent = GameObject.Find("babyevents").transform;
        processObj.transform.localPosition = Vector3.zero;
        this.myTimer.StartTiming(time, OnComplete, OnUpdate);
    }
    void OnComplete()
    {
        HumanSystem human = GameObject.Find("Human").GetComponent<HumanSystem>();
        human.state = HumanState.isIdle;
        EventControl.instance.isChangeDiaper = true;
        GameObject.Find("babyevents").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Destroy(processObj);
    }
    void OnUpdate(float ratio)
    {
        processObj.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(ratio, 1, 1);
    }
}
