using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    hungry = 0,//饿了
    diaper,//尿布提示
    play,//需要逗玩
    amuse,//逗玩
    water,//烧开水
    milk,//冲奶粉
    drikMilk,//喂奶
    placeMilk,//放奶瓶
    dropDiaper,//扔尿布
    changDiaper,//换尿布
    takeDiaper,//取尿布
    smokeEvent,//抽烟
    callPhone,
    processEvent,
    pressEvent // 压力增长
}

public class MyEvent {
    public Timer myTimer;
    public bool isHandle;//是否处理
    public EventType type;
    
    public MyEvent(EventType type_) {
        type = type_;
        myTimer = Timer.CreateTimer("myTimer");
    }
    public virtual void ConditionTrigger() { }
    public virtual void CountDownEnd() {
        if (isHandle)
            return;
    }
    public virtual void HandleEvent(){}
    public virtual void StopPressAdd(){}
}
