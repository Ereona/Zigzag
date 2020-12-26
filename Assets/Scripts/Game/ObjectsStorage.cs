using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Objects Storage")]
public class ObjectsStorage : ScriptableObject
{
    public List<LevelObject> ObjectPrefabs = new List<LevelObject>();

    public LevelObject GetObjectPrefab(LevelObjectType type)
    {
        return ObjectPrefabs.FirstOrDefault(c => c.Type == type);
    }
}
