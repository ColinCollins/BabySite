using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMilkEvent : MyEvent {

    GameObject processObj;
    public DrinkMilkEvent(EventType type, float time) : base(type)
    {
        processObj = GameObject.Instantiate(Resources.Load("prefabs/processbottom")) as GameObject;
        processObj.transform.parent = GameObject.Find("babyevents").transform;
        processObj.transform.localPosition = Vector3.zero;
        this.myTimer.StartTiming(time, OnComplete, OnUpdate);
    }
    void OnComplete()
    {
        EventControl.instance.isDrink = true;
        EventControl.instance.isMilk = false;
        GameObject.Find("Human").GetComponent<HumanSystem>().state = HumanState.isIdle;
        GameObject.Find("babyevents").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Destroy(processObj);
        EventControl.instance.RemoveEvent(null, 0, false, 0, true);
    }
    void OnUpdate(float ratio)
    {
        processObj.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(ratio, 1, 1);
    }
}
