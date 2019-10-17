using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
   Vector2 topLeft;
   Vector2 botRight;

   Node n;

   Quad topLeftTree;
   Quad topRightTree;
   Quad botLeftTree;
   Quad botRightTree;


    public Quad()
    {
        topLeft = new Vector2(0, 0);
        botRight = new Vector2(0, 0);

        n = null;

        topLeftTree = null;
        topRightTree = null;
        botLeftTree = null;
        botRightTree = null;
    }

    public Quad(Vector2 topL, Vector2 topR)
    {
	    topLeft = topL;
	    botRight = topR;

	    n = null;

	    topLeftTree = null;
	    topRightTree = null;
	    botLeftTree = null;
	    botRightTree = null;
    }

    private void Start()
    {
        topLeft = new Vector2(-10,10);
        botRight = new Vector2(10,-10);

        n = null;

        topLeftTree = null;
        topRightTree = null;
        botLeftTree = null;
        botRightTree = null;
    }



    // Insert a node into the quadtree
    public void insert(Node node)
    {
        if (node == null)
        {
            return;
        }

        // Current quad cannot contain it
        //if (!inBoundary(node.pos))
        //{
        //    Debug.Log("IN ehre for some reason");

        //    return;

        //}

        // We are at a quad of unit area

        // We cannot subdivide this quad further
        if (Mathf.Abs(topLeft.x - botRight.x) <= 1 &&
            Mathf.Abs(topLeft.y - botRight.y) <= 1)
        {
            if (n == null)
                n = node;

            return;
        }

        if ((topLeft.x + botRight.x) / 2 >= node.pos.x)
        {
            // Indicates topLeftTree 
            if ((topLeft.y + botRight.y) / 2 >= node.pos.y)
            {
                if (topLeftTree == null)
                {
                    topLeftTree = new Quad(new Vector2(topLeft.x, topLeft.y), new Vector2((topLeft.x + botRight.x) / 2, (topLeft.y + botRight.y) / 2));
                }
                topLeftTree.insert(node);
            }

            // Indicates botLeftTree 
            else
            {
                if (botLeftTree == null)
                    botLeftTree = new Quad(new Vector2(topLeft.x, (topLeft.y + botRight.y) / 2), new Vector2((topLeft.x + botRight.x) / 2, botRight.y));

                botLeftTree.insert(node);
            }
        }
        else
        {
            // Indicates topRightTree 
            if ((topLeft.y + botRight.y) / 2 >= node.pos.y)
            {
                if (topRightTree == null)
                    topRightTree = new Quad(new Vector2((topLeft.x + botRight.x) / 2, topLeft.y), new Vector2(botRight.x, (topLeft.y + botRight.y) / 2));

                topRightTree.insert(node);
            }

            // Indicates botRightTree 
            else
            {
                if (botRightTree == null)
                {
                    botRightTree = new Quad(new Vector2((topLeft.x + botRight.x) / 2, (topLeft.y + botRight.y) / 2), new Vector2(botRight.x, botRight.y));
                    Debug.DrawLine(new Vector2((topLeft.x + botRight.x) / 2, (topLeft.y + botRight.y) / 2), new Vector2(botRight.x, botRight.y));
                }
                botRightTree.insert(node);
            }
        }
    }

    public Node search(Vector2 p)
    {
        // Current quad cannot contain it
        if (!inBoundary(p))
            return null;

        // We are at quad of unit length

        // We cannot subdivide this quad further
        if (n != null)
            return n;

        if ((topLeft.x + botRight.x) / 2 >= p.x)
        {
            // Indicates topLeftTree 
            if ((topLeft.y + botRight.y) / 2 >= p.y)
            {
                if (topLeftTree == null)
                    return null;

                return topLeftTree.search(p);
            }

            // Indicates botLeftTree 
            else
            {
                if (botLeftTree == null)
                    return null;

                return botLeftTree.search(p);
            }
        }
        else
        {
            // Indicates topRightTree 
            if ((topLeft.y + botRight.y) / 2 >= p.y)
            {
                if (topRightTree == null)
                    return null;

                return topRightTree.search(p);
            }

            // Indicates botRightTree 
            else
            {
                if (botRightTree == null)
                    return null;

                return botRightTree.search(p);
            }
        }
    }

    // Check if the current quadtree contains to point
    public bool inBoundary(Vector2 p)
    {
        return (p.x >= topLeft.x &&
            p.x <= botRight.x &&
            p.y >= topLeft.y &&
            p.y <= botRight.y);
    }
}