using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{

    SpriteRenderer sp;


    void Start()
    {
        sp = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(sp.bounds);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, sp.bounds.extents);
    }

}
