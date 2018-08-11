using UnityEngine;
using System.Collections.Generic;

public static class MazeHelper
{
    public static List<MazeDirection> GetRandomDirectionsQueue()
    {
        var queue = new List<MazeDirection>();
        List<int> list =
            new List<int>() {0, 1, 2, 3};

        for (int i = list.Count; i >= 0; i--)
        {
            int index = Random.Range(0, list.Count);
            queue.Add((MazeDirection)list[index]);
        }

        return queue;
    }
}