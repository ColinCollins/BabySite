using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapStuff : MonoBehaviour {
    private EventControl _instance;

    void Start() {
       _instance = EventControl.instance;
    }

    public void CreateEventByName(HumanSystem person) {
        string name = this.gameObject.name;
        Debug.Log(name);

        if (_instance == null) {
            Debug.LogWarning("There is still not generate machine exist!");
            _instance = EventControl.instance;
        }

        switch (name) { 
            case "bed":
                checkBabyNeed(person);
                break;
            case "garbage":
                DropToGrabage(person);
                break;
            case "nappy":
                if (person.hands != HandsState.none) {
                    return;
                }
                _instance.TakeDiaper(person);
                break;
            case "boiled water":
                boildWaterFeature(person);
                break;
            case "balcony":
                if (person.hands != HandsState.none) {
                    return;
                }
                _instance.Smokeing(4);
                break;
            case "tel":
                if (person.hands == HandsState.none && _instance.isCallPhone)
                {
                    _instance.PhoneTrig();
                    var manager = GameManager.getInstance();
                    manager.turnTable.gameObject.transform.localPosition = new Vector3(-401, -255, 0);
                    person.isTurnGame = true;
                    person.isRotate = true;
                }  
                break;
            default:
                Debug.LogWarning("There is a did not login event!");
                break;
        }
    }
    private void checkBabyNeed(HumanSystem person) {
        List<MyEvent> eventlist = EventControl.eventList;
        if (eventlist == null) {
            Debug.Log("There still not have any event generate!");
            return;
        }
        if (eventlist.Count > 0)
        {
            for (int i = 0; i < eventlist.Count; i++)
            {
                if (eventlist[i].type == EventType.play)
                {
                    Debug.Log("play");
                    _instance.Amuse(3);
                }
                if (eventlist[i].type == EventType.diaper && !_instance.isChangeDiaper && person.hands == HandsState.keepDiaper)
                {
                    Debug.Log("disaper");
                    _instance.ChangeDiaper(2);
                }
                if (eventlist[i].type == EventType.hungry && !_instance.isDrink && person.hands == HandsState.keepMilk)
                {
                    Debug.Log("milk");
                    _instance.DrinkMilk(2);
                }
            }
        }
    }
    private void boildWaterFeature(HumanSystem person) {
        if (!_instance.isWaterFinish)
        {
            _instance.WaterTri(5);
            _instance.isWaterFinish = true;
        }
        else if (_instance.isDrink && person.hands == HandsState.keepMilk) {
            person.hands = HandsState.none;
            _instance.isDrink = false;
        }
        else
        {
            if (!_instance.isMilk)
            {
                _instance.isMilk = true;
                _instance.Milk(1);
                _instance.isWaterFinish = false;
            }
        }
        
    }
    private void DropToGrabage(HumanSystem person) {
        if (person.hands == HandsState.keepDiaper)
            {
                person.hands = HandsState.none;
                _instance.isChangeDiaper = false;
                _instance.DropDiaper();
            }
    }
}
