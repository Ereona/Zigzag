using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalObject : LevelObject
{
    public override LevelObjectType Type => LevelObjectType.Crystal;

    public VoidEventChannelSO CrystalCollectedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnCollect();
        }
    }

    private void OnCollect()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        CrystalCollectedEvent.RaiseEvent();
    }

    private void OnEnable()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
    }
}
