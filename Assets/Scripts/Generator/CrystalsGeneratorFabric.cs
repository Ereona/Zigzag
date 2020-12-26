using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsGeneratorFabric
{
    public CrystalsGenerator Create(CrystalsGenMode generationMode, int bunchSize)
    {
        switch (generationMode)
        {
            case CrystalsGenMode.Random:
                return new RandomCrystalsGenerator(bunchSize);
            case CrystalsGenMode.Ordered:
                return new OrderedCrystalsGenerator(bunchSize);
            default:
                throw new System.NotImplementedException("Unknown generation mode");
        }
    }
}
