using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AutoSort : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	SortingGroup sortingGroup;
	public float offset = 0;
	const float OFFSET_AMOUNT = -100f;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		sortingGroup = GetComponent<SortingGroup>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		if(spriteRenderer != null){
			//spriteRenderer.sortingOrder =(int)((spriteRenderer.bounds.min.y + offset) * OFFSET_AMOUNT );
			spriteRenderer.sortingOrder =(int)((transform.position.y + offset) * OFFSET_AMOUNT );
		}
		else if(sortingGroup != null){
			sortingGroup.sortingOrder = (int)((transform.position.y + offset) * OFFSET_AMOUNT);
		}
	}
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Vector3 point = transform.position + Vector3.up * offset;
		Gizmos.DrawLine(point + Vector3.left, point + Vector3.right);
	}
}
