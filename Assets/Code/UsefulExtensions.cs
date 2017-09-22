using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UsefulExtensions {

	/// <summary>
	/// Pick and return random element in specified list.
	/// Does not check if list is empty before accessing
	/// </summary>
	/// <param name="list">List.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T Pick<T>(this IList<T> list){
		int count = list.Count;
		int random = Random.Range(0 , list.Count);
		return list[random];
	}
		
}
