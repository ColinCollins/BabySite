using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 用于控制相应时间生成相应事件，不同事件的生成时间间隔不一样
/// 
/// </summary>
public class EventControl : MonoBehaviour {
    static public List<MyEvent> eventList;
    public int minTimeDelta;
    public int maxTimeDelta;
    public float hungryHandleTimeDelta;
    public float diaperHandleTimeDelta;
    public float playHandleTimeDelta;

    CallPhoneEvent myPhoneEvent;
    HungryEvent hungryEvent;
    DiaperEvent diaperEvent;
    PlayEvent playEvent;
    Timer myTimer;
    int allType = 3;
    [HideInInspector]
    public bool isWaterFinish;//水烧开了
    [HideInInspector]
    public bool isMilk;//冲好奶粉
    public bool isDrink;
    public bool isCallPhone;
    [HideInInspector]
    public bool isSmoking;//抽烟结束
    [HideInInspector]
    public bool isChangeDiaper;//换好尿布
    public static EventControl instance;

    // Use this for initialization
    void Start () {
        if (instance == null)
            instance = this;
        int timeDelta = UnityEngine.Random.Range(minTimeDelta, maxTimeDelta);
        myTimer = Timer.CreateTimer("myTimer");
        int time = UnityEngine.Random.Range(minTimeDelta, maxTimeDelta + 1);
        myTimer.StartTiming(time, GenerateEvent, null, true, true, true, minTimeDelta, maxTimeDelta);
        Timer callTimer = Timer.CreateTimer("callTimer");
        int callDeltaTime = UnityEngine.Random.Range(25, 30 + 1);
        callTimer.StartTiming(time, GenerateCallEvent, null, true, true, true, 30, 40);
    }
    // when the timing finished will be execute.
	void GenerateEvent() {
        if (eventList == null)
            eventList = new List<MyEvent>();
        if (eventList.Count == allType)
            return;
        EventType type = 0;
        // generate the random event.
        while(true)
        {
            int count = 0;
            type = (EventType)UnityEngine.Random.Range(0, 3);
            Debug.Log(type);
            foreach(MyEvent myEvent in eventList)
            {
                if (myEvent.type == type)
                    break;
                count++;
            }
            if (count == eventList.Count)
                break;
        }

        if(type == EventType.hungry)
        { 
            hungryEvent = new HungryEvent(EventType.hungry, hungryHandleTimeDelta);
            eventList.Add(hungryEvent);
            GameObject.Find("babyevents").transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (type == EventType.diaper)
        {
            diaperEvent = new DiaperEvent(EventType.diaper, diaperHandleTimeDelta);
            eventList.Add(diaperEvent);
            // switch the state
            isChangeDiaper = false;
            //Debug.Log("换尿布");
            GameObject.Find("babyevents").transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (type == EventType.play)
        {
            playEvent = new PlayEvent(EventType.play, playHandleTimeDelta);
            eventList.Add(playEvent);
            //Debug.Log("逗孩子玩");
            GameObject.Find("babyevents").transform.GetChild(2).gameObject.SetActive(true);
        }
        //Debug.Log("count=" + eventList.Count);
    }

    public void RemoveEvent(GameObject obj,EventType type,bool isHideRemind,int index,bool isStopPressValue)
    {
        //处理事件
        if(isHideRemind)
            obj.transform.GetChild(index).gameObject.SetActive(false);
        //Debug.Log("length=" + eventList.Count);
        
        foreach (MyEvent myEvent in eventList)
        {
            Debug.Log("type=" + myEvent.type+",type="+type);
            if (myEvent.type == type)
            {
                if(isStopPressValue)
                    myEvent.StopPressAdd();
                eventList.Remove(myEvent);
                break;
            }
        }
    }
    public void GenerateProcess(GameObject obj,float times)
    {
        ProcessEvent processEvent = new ProcessEvent(EventType.processEvent, times,obj);
    }
    public void WaterTri(float times)
    {
        waterEvent processEvent = new waterEvent(EventType.water, times);
    }
    public void Milk(float times)
    {
        //冲奶
        MilkEvent processEvent = new MilkEvent(EventType.milk, times);
    }
    public void DrinkMilk(float times)
    {
        //喂奶
        DrinkMilkEvent processEvent = new DrinkMilkEvent(EventType.drikMilk, 2);
    }
    public void PlaceMilk()
    {
        //奶瓶放回去
        //看动画是直接处理，还是放到这里面处理
    }
    public void TakeDiaper(HumanSystem person)
    {
        //拿尿布
        //看动画是直接处理，还是放到这里面处理
        person.state = HumanState.isIdle;
        person.hands = HandsState.keepDiaper;
    }
    public void ChangeDiaper(float times)
    {
        ChangeDiaperEvent processEvent = new ChangeDiaperEvent(EventType.changDiaper, times);
    }
    public void DropDiaper()
    {
        //只是一个扔的动作，并且暂停压力值的增加
        RemoveEvent(null, EventType.diaper, false, 0, true);
        GameObject.Find("babyevents").transform.GetChild(1).gameObject.SetActive(false);
    }
    public void Amuse(float times)
    {
        AmuseBabyEvent amuseBabyEvent = new AmuseBabyEvent(EventType.amuse, times);
    }
    public void Smokeing(float times)
    {
        //只是一个扔的动作，并且暂停压力值的增加
        SmokeEvent processEvent = new SmokeEvent(EventType.smokeEvent, times);
    }
    public void PhoneTrig()
    {
        myPhoneEvent.StopPhone();
        myPhoneEvent = null;
    }
    public void GenerateCallEvent()
    {
        EventControl.instance.isCallPhone = true;
        myPhoneEvent = new CallPhoneEvent(EventType.callPhone, 5);
    }
    /*
    public void PressEvent(EventType type)
    {
        if (eventList == null)
            eventList = new List<MyEvent>();
        if (type == 0)
        {
            hungryEvent = new HungryEvent(0, hungryHandleTimeDelta);
            eventList.Add(hungryEvent);
            //饿了
            GameObject.Find("babyevents").transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (type == 1)
        {
            diaperEvent = new DiaperEvent(, diaperHandleTimeDelta);
            eventList.Add(diaperEvent);
            Debug.Log("换尿布");
            GameObject.Find("babyevents").transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (type == 2)
        {
            playEvent = new PlayEvent(2, playHandleTimeDelta);
            eventList.Add(playEvent);
            Debug.Log("逗孩子玩");
            GameObject.Find("babyevents").transform.GetChild(2).gameObject.SetActive(true);
        }
    }*/
}
