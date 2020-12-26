using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Crystal Generator Settings")]
public class CrystalGeneratorSettingsSO : ScriptableObject
{
    public CrystalsGenMode GenMode;
    public int BunchSize;
}
