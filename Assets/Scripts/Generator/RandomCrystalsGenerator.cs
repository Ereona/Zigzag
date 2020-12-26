using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCrystalsGenerator : CrystalsGenerator
{
    public RandomCrystalsGenerator(int bunchSize)
        : base(bunchSize)
    { }

    protected override int GetCrystalIndex()
    {
        return Random.Range(0, bunchSize);
    }
}
