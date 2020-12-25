using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    private Vector2Int CurrentPos;
    private Direction CurrentDirection;
    private List<GeneratorHelper> Helpers;

    public LevelGenerator()
    {
        Helpers = new List<GeneratorHelper>();
        Helpers.Add(new ForwardGeneratorHelper());
        Helpers.Add(new RightGeneratorHelper());
    }

    public List<BlockModel> GenerateStartPlace()
    {
        List<BlockModel> blocks = new List<BlockModel>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                BlockModel block = new BlockModel();
                block.x = i;
                block.y = j;
                block.index = j;
                blocks.Add(block);
            }
        }
        CurrentPos = new Vector2Int(0, 2);
        CurrentDirection = Direction.Forward;
        return blocks;
    }

    public List<BlockModel> GeneratePathSegment(int maxSegmentLength, int pathWidth)
    {
        GeneratorHelper currentHelper = Helpers.Find(c => c.Direction == CurrentDirection);
        if (currentHelper == null)
        {
            throw new System.NotImplementedException("Unknown direction");
        }
        int pathLength = Random.Range(pathWidth, maxSegmentLength + 1);
        List<BlockModel> blocks = new List<BlockModel>();
        for (int i = 0; i < pathLength; i++)
        {
            int minCoord = currentHelper.GetMinWidthCoordForPath(CurrentPos, pathWidth);
            int maxCoord = currentHelper.GetMaxWidthCoordForPath(CurrentPos, pathWidth);
            for (int j = minCoord; j <= maxCoord; j++)
            {
                BlockModel block = new BlockModel();
                currentHelper.FillBlockCoords(block, CurrentPos, j);
                block.index = CurrentPos.x + CurrentPos.y;
                blocks.Add(block);
            }
            CurrentPos += currentHelper.GetShiftForCurrentPosInPath();
        }
        CurrentPos += currentHelper.GetShiftForCurrentPosAfterTurn(pathWidth);
        CurrentDirection = CurrentDirection == Direction.Forward ? Direction.Right : Direction.Forward;
        int indexShift = (pathWidth + 1) / 2 - 1;
        return blocks;
    }
}
