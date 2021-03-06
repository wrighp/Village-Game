﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponCollision : MonoBehaviour
{

    public string type;
    public HashSet<GameObject> currentCollisions = new HashSet<GameObject>();

    void OnCollisionEnter2D(Collision2D col)
    {
        if (currentCollisions.Contains(col.gameObject)) return;
        currentCollisions.Add(col.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (currentCollisions.Contains(col.gameObject)) return;
        currentCollisions.Add(col.gameObject);
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (currentCollisions.Contains(col.gameObject)) return;
        currentCollisions.Add(col.gameObject);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (currentCollisions.Contains(col.gameObject)) return;
        currentCollisions.Add(col.gameObject);
    }
}