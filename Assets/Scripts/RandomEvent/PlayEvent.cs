using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEvent : MyEvent {

    PressEvent pressEvent = null;
    public PlayEvent(EventType type, float time) : base(type)
    {
        Debug.Log("playtime=" + time);
        this.myTimer.StartTiming(time, CountDownEnd);
    }
    public override void CountDownEnd()
    {
        base.CountDownEnd();
        pressEvent = new PressEvent(EventType.play, 1);
        Debug.Log("孩子开始哭,压力值增加");
        //时间结束，还未处理完的,将事件删除，同时增加压力值
    }

    public override void HandleEvent()
    {
        base.HandleEvent();
       // GameObject.Find("baby").transform.GetChild(2).gameObject.SetActive(true);
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
