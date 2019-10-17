using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2 pos;
    public int data;

    public Node(Vector2 _pos, int _data)
    {
        pos = _pos;
        data = _data;
    }

    public Node()
    {
        data = 0;
    }

}