using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrrigerTest : MonoBehaviour {
    public string name;
	// Use this for initialization
	void Start () {
        Debug.Log("Start:" + name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter:"+name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter:" + name);
    }
}
