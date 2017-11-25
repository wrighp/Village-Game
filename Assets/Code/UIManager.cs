using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text[] children;
    private SupplyData data;
    public GameObject floatText;
    static public UIManager i;
    void Awake(){
        if(i == null) {
            i = this;
        }
    }


	// Update is called once per frame
	void Update () {
		if(data == null){
			data = GameObject.FindObjectOfType<SupplyData>();
			return;
		}
        children[0].text = data.food.ToString();
        children[1].text = data.wood.ToString();
        children[2].text = data.stone.ToString();
		children[3].text = data.gold.ToString();

		children[4].text = data.workers.ToString();
		children[5].text = data.fighters.ToString();
    }

	public void SpawnFloatingText(string text, Vector3 pos, float time = 1f, Color? color = null) {
        GameObject ft = Instantiate(floatText, pos, Quaternion.identity);
        ft.GetComponent<Text>().text = text;
        ft.transform.SetParent(this.transform);
        FloatingText ftS = ft.GetComponent<FloatingText>();
        ftS.start = Camera.main.WorldToScreenPoint(pos);
        ftS.end = ftS.start + Vector3.up * 50;
		color = color ?? Color.white;
		ftS.color = color.Value;
		ftS.time = time;
    }
}
