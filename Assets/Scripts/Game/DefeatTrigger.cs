using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTrigger : MonoBehaviour
{
    public VoidEventChannelSO GameOverEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameOverEvent.RaiseEvent();
        }
    }
}
