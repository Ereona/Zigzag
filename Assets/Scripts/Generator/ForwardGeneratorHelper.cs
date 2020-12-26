using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardGeneratorHelper : GeneratorHelper
{
    public override Direction Direction => Direction.Forward;

    public override void FillBlockCoords(LevelObjectModel block, Vector2Int currentPos, int widthCoord)
    {
        block.x = widthCoord;
        block.y = currentPos.y;
    }

    public override int GetMaxWidthCoordForPath(Vector2Int currentPos, int pathWidth)
    {
        return currentPos.x + pathWidth / 2;
    }

    public override int GetMinWidthCoordForPath(Vector2Int currentPos, int pathWidth)
    {
        return currentPos.x - (pathWidth - 1) / 2;
    }

    public override Vector2Int GetShiftForCurrentPosAfterTurn(int pathWidth)
    {
        return new Vector2Int(-1, 1) * GetIndexShiftAfterTurn(pathWidth);
    }

    public override Vector2Int GetShiftForCurrentPosInPath()
    {
        return new Vector2Int(0, 1);
    }
}
