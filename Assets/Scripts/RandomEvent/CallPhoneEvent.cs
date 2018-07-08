using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPhoneEvent : MyEvent {

    GameObject processObj;
    public CallPhoneEvent(EventType type, float time) : base(type)
    {
        processObj = GameObject.Instantiate(Resources.Load("prefabs/processbottom")) as GameObject;
        processObj.transform.parent = GameObject.Find("desk").transform;
        processObj.transform.localPosition = Vector3.zero;
        this.myTimer.StartTiming(time, OnComplete,OnUpdate);
        //EventControl.instance.GenerateArrow(GameObject.Find("desk"),"desk");
        Camera.main.GetComponent<AudioSource>().Play();
        //播放音效
    }
    void OnComplete()
    {
       // GameObject.Find("babyevents").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Human").GetComponent<HumanSystem>().state = HumanState.isIdle;
        GameObject.Destroy(processObj);
        EventControl.instance.isCallPhone = false;
        if (GameObject.Find("desk/Arrow(Clone)"))
            GameObject.Destroy(GameObject.Find("desk/Arrow(Clone)"));
        //停止音效
        //Debug.Log("stop audioresource");
        Camera.main.GetComponent<AudioSource>().Stop();
    }
    void OnUpdate(float ratio)
    {
        processObj.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(ratio, 1, 1);
    }
    public void StopPhone()
    {
        myTimer.Destory();
        Camera.main.GetComponent<AudioSource>().Stop();
        GameObject.Destroy(processObj);
        if (GameObject.Find("desk/Arrow(Clone)"))
            GameObject.Destroy(GameObject.Find("desk/Arrow(Clone)"));
    }
}
