using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEvent : MyEvent {

    GameObject processObj;
    public SmokeEvent(EventType type, float time) : base(type)
    {
        processObj = GameObject.Instantiate(Resources.Load("prefabs/processbottom")) as GameObject;
        processObj.transform.parent = GameObject.Find("balcony").transform;
        processObj.transform.localPosition = Vector3.zero;
        GameObject.Find("balcony").transform.GetChild(0).gameObject.SetActive(true);
        this.myTimer.StartTiming(time, OnComplete, OnUpdate);
    }
    void OnComplete()
    {
        GameObject.Find("Human").GetComponent<HumanSystem>().state = HumanState.isIdle;
        EventControl.instance.isSmoking = true;
        GameObject.Find("balcony").transform.GetChild(0).gameObject.SetActive(false);
        HumanSystem human = GameObject.Find("Human").GetComponent<HumanSystem>();
        human.patient -= 8;
        GameObject.Destroy(processObj);
    }
    void OnUpdate(float ratio)
    {
        processObj.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(ratio, 1, 1);
    }
}
