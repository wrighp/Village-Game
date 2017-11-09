using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Vector3 end;
    public Vector3 start;
	public Color color;
	public float time;
	float timer;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(timer >= time)
        {
            Destroy(gameObject);
        }
		float lerp = timer / time;
		this.transform.position = Vector3.Lerp(start, end, lerp);
		color.a = 1 - lerp;
		text.color = color;
		timer += Time.deltaTime;
	}
}
