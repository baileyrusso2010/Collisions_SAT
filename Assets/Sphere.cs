using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Vector3 position, velocity, accelleration;
    public float friction = .54f;
    
    public float mass = 10;
    public float radius = 1;

    public KeyCode k;

    // Start is called before the first frame update
    void Start()
    {
        position = this.transform.position;
        velocity = accelleration = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(k))
            ApplyForce(Vector3.right);
        else
            velocity *= 979f / 1000f;
        Move();


    }

    void ApplyForce(Vector3 force)
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

}
