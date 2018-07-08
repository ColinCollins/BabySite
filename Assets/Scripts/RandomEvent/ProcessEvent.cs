using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessEvent : MyEvent {

    GameObject currentObject;
    GameObject processObj;
    public ProcessEvent(EventType type, float time, GameObject obj) : base(type)
    {
        currentObject = obj;
        processObj = GameObject.Instantiate(Resources.Load("prefabs/processbottom")) as GameObject;
        processObj.transform.parent = currentObject.transform;
        processObj.transform.localPosition = Vector3.zero;
        this.myTimer.StartTiming(time, OnComplete, OnUpdate);
    }
    void OnComplete()
    {
        GameObject.Destroy(processObj);
    }
    void OnUpdate(float ratio)
    {
        processObj.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(ratio, 1, 1);
    }
}
