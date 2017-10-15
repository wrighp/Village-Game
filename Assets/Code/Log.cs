using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour {

    public List<GameObject> messageLog;

	// Use this for initialization
	void Start () {
        messageLog = new List<GameObject>();
	}

    public void AddMessage(string m) {
        if(messageLog.Count >= 7)
        {
            GameObject temp = messageLog[0];
            messageLog.RemoveAt(0);
            Destroy(temp);
        }
        GameObject message = new GameObject();
        Text text = message.AddComponent<Text>();
        text.text = m;
        text.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
        text.color = new Color(0, 0, 0);
        text.rectTransform.localScale = new Vector3(1, 1, 1);
        text.rectTransform.sizeDelta = new Vector2(160, 20);
        message.transform.SetParent(this.transform);
        messageLog.Add(message);
    }
}
