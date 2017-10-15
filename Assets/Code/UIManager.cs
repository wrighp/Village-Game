using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text[] children;
    private SupplyData data;

    void Start(){
        data = GameObject.FindObjectOfType<SupplyData>();
    }

	// Update is called once per frame
	void Update () {
        children[0].text = data.food.ToString();
        children[1].text = data.wood.ToString();
        children[2].text = data.stone.ToString();
    }
}
