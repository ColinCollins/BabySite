using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemindHandle : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
		
	}
    public Sprite[] remindSprite;//喂奶、玩、换尿布、抽烟、箭头
    public void ShowRemind(int type)
    {
        GameObject remindObj = GameObject.Instantiate(Resources.Load("prefabs/Remind")) as GameObject;
        remindObj.transform.parent = gameObject.transform;
        remindObj.transform.GetChild(0).GetComponent<Image>().sprite = remindSprite[type];
    }
    public void HideRemind(int type)
    {
        Destroy(gameObject.transform.GetChild(1));
    }
}
