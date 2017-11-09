using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Vector3 end;
    public Vector3 start;
    float time;
    Text text;

	// Use this for initialization
	void Start () {
        time = 0;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(time > 1)
        {
            Destroy(gameObject);
        }
        this.transform.position = Vector3.Lerp(start, end, time);
        time += Time.deltaTime;
        text.color = new Color(1, 1, 1, 1 - time);
	}
}
