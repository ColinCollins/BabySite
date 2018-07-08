using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    public Text processText;
    public Text clockText;
    Timer timer;
    public  Text pressValue;
    public static int press = 0;
    // Use this for initialization
    void Start () {
        //eventList = new List<MyEvent>();
        //// 创建计时器
        //timer = Timer.CreateTimer("Timer");
        ////开始计时
        //timer.StartTiming(10,null,null, ShowCurrentTime
    }

    private void ShowCurrentTime(float process)
    {
        processText.text = "" + process;
    }
    public GameObject baby;
	// Update is called once per frame
	/*void Update () {
        pressValue.text = "" + press;
        if(Input.GetKeyDown(KeyCode.A))
        {
            //饿了
            EventControl.instance.PressEvent(0);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //换尿布
            EventControl.instance.PressEvent(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            //逗玩
            EventControl.instance.PressEvent(2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //烧开水
            EventControl.instance.WaterTri(5);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //冲奶粉
            EventControl.instance.Milk(1);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            //喂奶粉
            EventControl.instance.DrinkMilk(1);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            //放奶瓶
            EventControl.instance.PlaceMilk();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            //拿尿布
            EventControl.instance.TakeDiaper();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            //换尿布
            EventControl.instance.ChangeDiaper(2);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            //扔尿布
            EventControl.instance.DropDiaper();
        }
        
        else if (Input.GetKeyDown(KeyCode.K))
        {
            //抽烟
            EventControl.instance.Smokeing(4);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            //逗玩
            EventControl.instance.Amuse(3);
        }
    }*/
}
