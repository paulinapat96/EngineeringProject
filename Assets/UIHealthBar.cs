using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour {

    RectTransform _rectTransform;
    Vector3 rectStartPos;




	// Use this for initialization
	void Start ()
    {

        _rectTransform = GetComponent<RectTransform>();
        rectStartPos = _rectTransform.position;

	}
	
	// Update is called once per frame
	void Update () {

        _rectTransform.position = new Vector3(rectStartPos.x, rectStartPos.y + Mathf.Sin(Time.realtimeSinceStartup * 0.8f) * 1.3f, rectStartPos.z);

	}
}
