using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Quad quad;

    void Start()
    {
        quad.insert(new Node(new Vector2(1, 1), -1));
        Vector2 one = new Vector2(1, 1);
        Debug.Log(quad.search(one));
    }

    void Update()
    {




    }

}
