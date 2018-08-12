using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Maze))]
public class MazeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            var maze = (Maze) target;
            maze.DestroyMaze();
            maze.GenerateMap();
        }

        if (GUILayout.Button("Destroy"))
        {
            var maze = (Maze) target;
            maze.DestroyMaze();
        }
    }
}