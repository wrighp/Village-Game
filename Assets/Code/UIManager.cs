using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text[] children;
    private SupplyData data;

    void Start(){
        
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
}
