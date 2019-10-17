using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    protected Vector2 position;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual Vector3 GetPosition()
    {
        return this.transform.position;
    }

}
