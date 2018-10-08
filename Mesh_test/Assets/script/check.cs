using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int[] array = new int[] { 1, 2, 3, 4, 5 };
        for(int n = 0;n < 4;)
        {
            Debug.Log("array[" + n + "]" + array[n]);
            Debug.Log("array[" + n + "]"+array[++n]);
        }
	}
}
