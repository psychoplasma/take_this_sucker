using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR

		if (Input.GetMouseButtonDown(0)) {
			Debug.Log ("Pointer is " + (EventSystem.current.IsPointerOverGameObject(-1)? "not " : "") + "on a gameobject.");
		}

		#endif
	}
}
