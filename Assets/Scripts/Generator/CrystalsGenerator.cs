using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CrystalsGenerator
{
    public CrystalsGenerator(int bunchSize)
    {
        this.bunchSize = bunchSize;
    }

    private List<LevelObjectModel> remainedBlocks = new List<LevelObjectModel>();
    protected int bunchSize;

    public List<LevelObjectModel> AddCrystalsToBlocks(List<LevelObjectModel> blocks)
    {
        remainedBlocks.AddRange(blocks);
        List<LevelObjectModel> result = new List<LevelObjectModel>();
        while (remainedBlocks.Count >= bunchSize)
        {
            List<LevelObjectModel> currentBunch = remainedBlocks.GetRange(0, bunchSize);
            remainedBlocks.RemoveRange(0, bunchSize);
            int crystalIndex = GetCrystalIndex();
            LevelObjectModel crystal = new LevelObjectModel();
            crystal.x = currentBunch[crystalIndex].x;
            crystal.y = currentBunch[crystalIndex].y;
            crystal.index = currentBunch[crystalIndex].index;
            crystal.type = LevelObjectType.Crystal;
            result.Add(crystal);
        }
        return result;
    }

    protected abstract int GetCrystalIndex();
}
