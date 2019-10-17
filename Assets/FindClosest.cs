using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{

    KdTree<RandomMovement> WhiteBalls= new KdTree<RandomMovement>();
    KdTree<RandomMovement> BlackBalls = new KdTree<RandomMovement>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var whiteBall in WhiteBalls)
        {

        }    

    
    }
}
