using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tets : MonoBehaviour {

    public GameObject test1;
    public GameObject test2;
	// Use this for initialization
	void Start () {
        Image test = test1.GetComponentInChildren<Image>();
        Debug.Log(test);
	}
	
	// Update is called once per frame
	void Update () {
        float test = UnityEngine.Random.Range(0, 3);
	}
}
