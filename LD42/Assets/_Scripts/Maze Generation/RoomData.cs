using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RoomData : ScriptableObject
{
    public Vector2Int m_Size;
    public AnimationCurve m_FillCurve;
    public int m_SmoothIteration;
    public int m_PassagesWidth;
}
