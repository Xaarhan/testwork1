using UnityEngine;
using System.Collections;

public class AutoRotateObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		_object = gameObject;
        Quaternion q = new Quaternion();
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 euler = gameObject.transform.localEulerAngles;
		gameObject.transform.localEulerAngles = new Vector3 (euler.x, euler.y + rotateSpeed * Time.deltaTime, euler.z);
	}

	public float rotateSpeed;
	private GameObject _object;
}
