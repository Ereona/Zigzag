﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObject : MonoBehaviour
{
    public int index;

    public abstract LevelObjectType Type { get; }

    public IEnumerator BeforeDestroySmoothly()
    {
        Rigidbody r = gameObject.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(3);
        Destroy(r);
    }

    public void BeforeDestroyStraight()
    {
        StopAllCoroutines();
        Rigidbody r = GetComponent<Rigidbody>();
        if (r != null)
        {
            Destroy(r);
        }
    }
}
