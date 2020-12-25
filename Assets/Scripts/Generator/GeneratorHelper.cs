using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneratorHelper
{
    public abstract Direction Direction { get; }

    public abstract int GetMinWidthCoordForPath(Vector2Int currentPos, int pathWidth);
    public abstract int GetMaxWidthCoordForPath(Vector2Int currentPos, int pathWidth);
    public abstract void FillBlockCoords(BlockModel block, Vector2Int currentPos, int widthCoord);
    public abstract Vector2Int GetShiftForCurrentPosInPath();
    public abstract Vector2Int GetShiftForCurrentPosAfterTurn(int pathWidth);

    protected int GetIndexShiftAfterTurn(int pathWidth)
    {
        return (pathWidth + 1) / 2 - 1;
    }
}
