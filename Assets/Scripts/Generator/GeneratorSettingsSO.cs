using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Generator Settings")]
public class GeneratorSettingsSO : ScriptableObject
{
    [Range(1, 3)]
    public int PathWidth;
    [Range(1, 20)]
    public int MaxSegmentLength;
    [Range(10, 100)]
    public int VisibleFieldLength;
}
