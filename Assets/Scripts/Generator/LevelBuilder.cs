using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public ObjectsStorage Storage;
    public BallObject Ball;
    public VoidEventChannelSO GameStartedEvent;
    public VoidEventChannelSO GameOverEvent;
    public GeneratorSettingsSO LevelSettings;
    public CrystalGeneratorSettingsSO CrystalSettings;

    private List<LevelObject> Objects = new List<LevelObject>();
    private List<LevelObject> DestroyingObjects = new List<LevelObject>();
    private LevelGenerator Generator;
    private bool firstTime;

    private void Start()
    {
        firstTime = true;
        GameStartedEvent.OnEventRaised += OnGameStarted;
        GameOverEvent.OnEventRaised += OnGameOver;
        Generator = new LevelGeneratorWithCrystals(CrystalSettings.GenMode, CrystalSettings.BunchSize);
        GenerateStartPlace();
    }

    private void OnGameStarted()
    {
        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            RemoveAllBlocks();
            GenerateStartPlace();
        }
        StartCoroutine(BuildRuntime());
        StartCoroutine(DestroyRuntime());
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
    }

    private void GenerateStartPlace()
    {
        List<LevelObjectModel> objectModels = Generator.GenerateStartPlace();
        foreach (LevelObjectModel model in objectModels)
        {
            AddObject(model);
        }
        while (Objects.Last().index < LevelSettings.VisibleFieldLength)
        {
            GenerateAdditionalBlocks();
        }
    }

    private void AddObject(LevelObjectModel model)
    {
        LevelObject prefab = Storage.GetObjectPrefab(model.type);
        LevelObject obj = SimplePool.Spawn(prefab.gameObject).GetComponent<LevelObject>();
        obj.transform.position = new Vector3(model.x, 0, model.y);
        obj.transform.SetParent(this.transform);
        obj.index = model.index;
        Objects.Add(obj);
    }

    private void RemoveAllBlocks()
    {
        foreach (LevelObject obj in Objects)
        {
            SimplePool.Despawn(obj.gameObject);
        }
        Objects.Clear();
        foreach (LevelObject obj in DestroyingObjects)
        {
            obj.BeforeDestroyStraight();
            SimplePool.Despawn(obj.gameObject);
        }
        DestroyingObjects.Clear();
    }

    private IEnumerator BuildRuntime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            BlockObject block = Objects.OfType<BlockObject>().Last();
            if (Vector3.Distance(block.transform.position, Ball.transform.position) < LevelSettings.VisibleFieldLength)
            {
                GenerateAdditionalBlocks();
            }
        }
    }

    private IEnumerator DestroyRuntime()
    {
        while (true)
        {
            yield return null;
            List<LevelObject> toRemove = new List<LevelObject>();
            foreach (LevelObject block in Objects)
            {
                if (block.index + 1 < Ball.index)
                {
                    toRemove.Add(block);
                }
            }
            foreach (LevelObject b in toRemove)
            {
                StartCoroutine(DestroyObject(b));
                Objects.Remove(b);
            }
        }
    }

    private IEnumerator DestroyObject(LevelObject obj)
    {
        DestroyingObjects.Add(obj);
        yield return obj.BeforeDestroySmoothly();
        SimplePool.Despawn(obj.gameObject);
        DestroyingObjects.Remove(obj);
    }

    private void GenerateAdditionalBlocks()
    {
        List<LevelObjectModel> models = Generator.GeneratePathSegment(LevelSettings.MaxSegmentLength, LevelSettings.PathWidth);
        foreach (LevelObjectModel model in models)
        {
            AddObject(model);
        }
    }
}
