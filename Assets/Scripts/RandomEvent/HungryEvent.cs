using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 时间计时器
/// 触发饿了事件，将饿了事件添加到事件链表中，弹出提示框，创建一个计时器(可以进度条的形式显示)
/// 时间结束的处理
/// public void StartTiming(float time, CompleteEvent onCompleted_, UpdateEvent update = null, bool isIgnoreTimeScale_ = true, bool isRepeate_ = false, bool isDestory_ = true)
/// </summary>
public class HungryEvent : MyEvent {
    PressEvent pressEvent = null;
    static public List<MyEvent> childEventList;//子事件，烧开水、冲奶粉、喂奶
    public HungryEvent(EventType type, float time):base(type)
    {
        this.myTimer.StartTiming(time, CountDownEnd);
    }
    public override void CountDownEnd()
    {
        base.CountDownEnd();
        pressEvent = new PressEvent(EventType.hungry, 1);
        //时间结束，还未处理完的,将事件删除，同时增加压力值
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
