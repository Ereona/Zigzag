using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGeneratorHelper : GeneratorHelper
{
    public override Direction Direction => Direction.Right;

    public override void FillBlockCoords(BlockModel block, Vector2Int currentPos, int widthCoord)
    {
        block.x = currentPos.x;
        block.y = widthCoord;
    }

    public override int GetMaxWidthCoordForPath(Vector2Int currentPos, int pathWidth)
    {
        return currentPos.y + pathWidth / 2;
    }

    public override int GetMinWidthCoordForPath(Vector2Int currentPos, int pathWidth)
    {
        return currentPos.y - (pathWidth - 1) / 2;
    }

    public override Vector2Int GetShiftForCurrentPosAfterTurn(int pathWidth)
    {
        return new Vector2Int(1, -1) * GetIndexShiftAfterTurn(pathWidth);
    }

    public override Vector2Int GetShiftForCurrentPosInPath()
    {
        return new Vector2Int(1, 0);
    }
}
