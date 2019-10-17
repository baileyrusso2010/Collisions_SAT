using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    public List<Vector2> childrenPosition;

    public Vector3 position, velocity, accelleration;
    public Vector3 angualVelocity;

    public float mass = 10;
    float orientation, angularVelocity, torque;
    public KeyCode Up, Down, Right, Left;

    public bool colliding = false;
    // Start is called before the first frame update
    void Start()
    {
        position = this.transform.position;
        velocity = accelleration = Vector3.zero;
        //Move(Vector3.left);
                
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Up))
            ApplyForce(Vector3.up);
        else if (Input.GetKey(Down))
            ApplyForce(Vector3.down);
        else if (Input.GetKey(Right))
            ApplyForce(Vector3.right);
        else if (Input.GetKey(Left))
            ApplyForce(Vector3.left);
        else
            velocity *= 979f / 1000f;
        Move();
        DrawLines();
    }


    public void ApplyForce(Vector3 force)
    {
        accelleration += force / mass;

    }

    void Move()
    {

        velocity += accelleration;
        position += velocity * Time.deltaTime;
        this.transform.position = position;
        accelleration = Vector3.zero;

        this.position.z = 0;//contraints to zero

    }

    /// <summary>
    /// Draws an outline of the shape
    /// </summary>
    void DrawLines()
    {
        childrenPosition.Clear();
        foreach (Transform child in transform)
        {
            childrenPosition.Add(child.transform.position);

        }

        for (int i = 0; i < childrenPosition.Count; i++)
        {
            if((i+1) == childrenPosition.Count)
                Debug.DrawLine(childrenPosition[i], childrenPosition[0], Color.red);
            else
                Debug.DrawLine( childrenPosition[i],  childrenPosition[i + 1], Color.red);
        }
    }
}