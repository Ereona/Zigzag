using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollow : MonoBehaviour
{
    public Transform ObjectToFollow;
    public bool PosX;
    public bool PosY;
    public bool PosZ;
    public bool RotX;
    public bool RotY;
    public bool RotZ;

    public Vector3 posOff;
    public Vector3 rotOff;

    protected virtual void LateUpdate()
    {
        if (ObjectToFollow == null) return;
        transform.position = new Vector3(
            PosX ? ObjectToFollow.position.x + posOff.x : transform.position.x,
            PosY ? ObjectToFollow.position.y + posOff.y : transform.position.y,
            PosZ ? ObjectToFollow.position.z + posOff.z : transform.position.z);
        transform.eulerAngles = new Vector3(
            RotX ? ObjectToFollow.eulerAngles.x + rotOff.x : transform.eulerAngles.x,
            RotY ? ObjectToFollow.eulerAngles.y + rotOff.y : transform.eulerAngles.y,
            RotZ ? ObjectToFollow.eulerAngles.z + rotOff.z : transform.eulerAngles.z);
    }
}
