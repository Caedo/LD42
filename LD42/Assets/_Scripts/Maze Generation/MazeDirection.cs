using UnityEngine;

public enum MazeDirection
{
    North,
    East,
    South,
    West
}

public static class MaveDirectionExtensions
{
    private static readonly Vector2Int[] m_Directions = {Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.down};

    public static Vector2Int ToVector2Int(this MazeDirection dir)
    {
        return m_Directions[(int) dir];
    }

    private static readonly MazeDirection[] opposites =
    {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return opposites[(int) direction];
    }
}