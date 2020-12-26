using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedCrystalsGenerator : CrystalsGenerator
{
    private int lastIndex = -1;

    public OrderedCrystalsGenerator(int bunchSize)
        : base(bunchSize)
    { }

    protected override int GetCrystalIndex()
    {
        lastIndex++;
        return lastIndex % bunchSize;
    }
}
