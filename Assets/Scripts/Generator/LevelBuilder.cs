using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public BlockObject BlockPrefab;
    public BallObject Ball;
    public VoidEventChannelSO GameStartedEvent;
    public VoidEventChannelSO GameOverEvent;
    public GeneratorSettingsSO Settings;

    private List<BlockObject> Blocks = new List<BlockObject>();
    private List<BlockObject> DestroyingBlocks = new List<BlockObject>();
    private LevelGenerator Generator;
    private bool firstTime;

    private void Start()
    {
        firstTime = true;
        GameStartedEvent.OnEventRaised += OnGameStarted;
        GameOverEvent.OnEventRaised += OnGameOver;
        Generator = new LevelGenerator();
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
        List<BlockModel> blockModels = Generator.GenerateStartPlace();
        foreach (BlockModel model in blockModels)
        {
            AddBlock(model);
        }
        while (Blocks.Last().index < Settings.VisibleFieldLength)
        {
            GenerateAdditionalBlocks();
        }
    }

    private void AddBlock(BlockModel model)
    {
        BlockObject obj = SimplePool.Spawn(BlockPrefab.gameObject).GetComponent<BlockObject>();
        obj.transform.position = new Vector3(model.x, -0.5f, model.y);
        obj.transform.SetParent(this.transform);
        obj.index = model.index;
        Blocks.Add(obj);
    }

    private void RemoveAllBlocks()
    {
        foreach (BlockObject obj in Blocks)
        {
            SimplePool.Despawn(obj.gameObject);
        }
        Blocks.Clear();
        foreach (BlockObject obj in DestroyingBlocks)
        {
            obj.BeforeDestroyStraight();
            SimplePool.Despawn(obj.gameObject);
        }
        DestroyingBlocks.Clear();
    }

    private IEnumerator BuildRuntime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            BlockObject block = Blocks.Last();
            if (Vector3.Distance(block.transform.position, Ball.transform.position) < Settings.VisibleFieldLength)
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
            List<BlockObject> toRemove = new List<BlockObject>();
            foreach (BlockObject block in Blocks)
            {
                if (block.index + 1 < Ball.index)
                {
                    toRemove.Add(block);
                }
            }
            foreach (BlockObject b in toRemove)
            {
                StartCoroutine(DestroyBlock(b));
                Blocks.Remove(b);
            }
        }
    }

    private IEnumerator DestroyBlock(BlockObject obj)
    {
        DestroyingBlocks.Add(obj);
        yield return obj.BeforeDestroySmoothly();
        SimplePool.Despawn(obj.gameObject);
        DestroyingBlocks.Remove(obj);
    }

    private void GenerateAdditionalBlocks()
    {
        List<BlockModel> blockModels = Generator.GeneratePathSegment(Settings.MaxSegmentLength, Settings.PathWidth);
        foreach (BlockModel model in blockModels)
        {
            AddBlock(model);
        }
    }
}
