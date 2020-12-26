using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorWithCrystals : LevelGenerator
{
    private CrystalsGenerator CrystalsGen;

    public LevelGeneratorWithCrystals(CrystalsGenMode genMode, int bunchSize)
    {
        CrystalsGeneratorFabric fabric = new CrystalsGeneratorFabric();
        CrystalsGen = fabric.Create(genMode, bunchSize);
    }

    protected override List<LevelObjectModel> GenerateAdditionalObjects(List<LevelObjectModel> addedBlocks)
    {
        return CrystalsGen.AddCrystalsToBlocks(addedBlocks);
    }
}
