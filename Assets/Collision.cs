using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    //public Polygon[] objs;

    public Polygon obj1, obj2;

    //public Sphere sp;

    void Update()
    {

    

                
	if (SAT(obj1, obj2))
        {
        
        	ElasticCollision(obj1, obj2);
        }

        }

    }


    void ElasticCollision(Polygon obj1, Polygon obj2)
    {
        if (obj1 == obj2)
            return;

        float mass1 = obj1.mass;//gets the masses
        float mass2 = obj2.mass;

        //calculates the velocity for object a 
        Vector3 aF = ((mass1 - mass2) / (mass1 + mass2) * obj1.velocity) + (2 * mass2) /
                                                    (mass1 + mass2) * obj2.velocity;

        Vector3 bF = ((2 * mass1) / (mass1 + mass2) * obj1.velocity) + (mass2 - mass1) /
                                                            (mass1 + mass2) * obj2.velocity;
        obj1.velocity = aF;
        obj2.velocity = bF;
        

    }

    bool polyCircle(List<Vector2> verticies, float cx, float cy, float radius)
    {
        int next;
        for (int current = 0; current < verticies.Count; current++)
        {
            next = current + 1;
            if (next == verticies.Count) next = 0;

            Vector2 vc = verticies[current];
            Vector2 vn = verticies[next];

            bool collision = lineCircle(vc.x, vc.y, vn.x, vn.y, cx, cy, radius);
            if (collision) return true;
        }


        return false;
    }

    bool lineCircle(float x1, float y1, float x2, float y2, float cx, float cy, float r)
    {
        bool inside1 = pointCircle(x1, y1, cx, cy, r);
        bool inside2 = pointCircle(x2, y2, cx, cy, r);

        if (inside1 || inside2) return true;

        float distX = x1 - x2;
        float distY = y1 - y2;
        float len = Mathf.Sqrt((distX * distX) + (distY * distY));
        // get dot product of the line and circle
        float dot = (((cx - x1) * (x2 - x1)) + ((cy - y1) * (y2 - y1))) / Mathf.Pow(len, 2);

        // find the closest point on the line
        float closestX = x1 + (dot * (x2 - x1));
        float closestY = y1 + (dot * (y2 - y1));

        // is this point actually on the line segment?
        // if so keep going, but if not, return false
        bool onSegment = linePoint(x1, y1, x2, y2, closestX, closestY);
        if (!onSegment) return false;

        // get distance to closest point
        distX = closestX - cx;
        distY = closestY - cy;
        float distance = Mathf.Sqrt((distX * distX) + (distY * distY));

        // is the circle on the line?
        if (distance <= r)
        {
            return true;
        }

        return false;
    }

    // LINE/POINT
    bool linePoint(float x1, float y1, float x2, float y2, float px, float py)
    {

        // get distance from the point to the two ends of the line
        float d1 = Vector2.Distance(new Vector2(px, py), new Vector2( x1, y1));
        float d2 = Vector2.Distance(new Vector2(px, py), new Vector2(x2, y2));
        
        // get the length of the line
        float lineLen = Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2));

        // since floats are so minutely accurate, add
        // a little buffer zone that will give collision
        float buffer = 0.1f;    // higher # = less accurate

        // if the two distances are equal to the line's
        // length, the point is on the line!
        // note we use the buffer here to give a range, rather
        // than one #
        if (d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer)
        {
            return true;
        }
        return false;
    }

    bool pointCircle(float px, float py, float cx, float cy, float r)
    {

        // get distance between the point and circle's center
        // using the Pythagorean Theorem
        float distX = px - cx;
        float distY = py - cy;
        float distance = Mathf.Sqrt((distX * distX) + (distY * distY));

        // if the distance is less than the circle's 
        // radius the point is inside!
        if (distance <= r)
        {
            return true;
        }
        return false;
    }



    /// <summary>
    /// Only for circle collisions only
    /// </summary>
    /// <returns></returns>
    //bool isColliding()
    //{
    //    float a = obj1.position.x - obj2.position.x;
    //    float b = obj1.position.y - obj2.position.y;

    //    a *= a;
    //    b *= b;

    //    float distance = Mathf.Sqrt(a + b);
    //   //Debug.Log(distance);
    //    if (distance < obj1.radius + obj2.radius)
    //        return true;
    //    else
    //        return false;


    //}

    //gets the normal faces
    List<Vector2> getEdgeNormals(Polygon pg)
    {
        List<Vector2> verticies = pg.childrenPosition;

        List<Vector2> edge_normals = new List<Vector2>();

        for (int i = 0; i < verticies.Count; i++)
        {
            Vector2 p1, p2;
            if ((i + 1) == verticies.Count)
            {
                p1 = verticies[i];
                p2 = verticies[0];

            }
            else {
                p1 = verticies[i];
                p2 = verticies[i+1];

            }

            Vector2 edge = p1 - p2;
            Vector2 normal = edge.normalized;

            normal = new Vector2(normal.y, -normal.x);

            edge_normals.Add(normal);
        }

        return edge_normals;
    }

    //normal vector is return (self.y, -self.x)

    //performs the separating axis theorem calculations on the given polygons,
    //returns the minimum seperation vector if colliding
    bool SAT(Polygon poly1, Polygon poly2)
    {
        List<Vector2> axis = getEdgeNormals(poly1);
        List<Vector2> axis2 = getEdgeNormals(poly2);
        float overlap = 999999;
        Vector2 min_pentraction_axis = Vector2.zero ;

        //concatenate axes2 into axes1
        for (int i = 0; i < axis2.Count; i++)
        {
            axis.Add(axis2[i]);
        }

        //loop over all the possible seperating axis and 
        //if we find even 1 that does not overlap, then we are done
        for (int i = 0; i < axis.Count; i++)
        {
            Vector2 ax = axis[i];
            //project both shapes onto the current axis
            float p1min = project_onto_axis(poly1.childrenPosition, ax)[0];
            float p1max = project_onto_axis(poly1.childrenPosition, ax)[1];
            

            float p2min = project_onto_axis(poly2.childrenPosition, ax)[0];
            float p2max = project_onto_axis(poly2.childrenPosition, ax)[1];

            //check from overlap     
            if (p1min > p2max || p2min > p1max)
            {
                return false;
            }
            
        }
        //return min_pentraction_axis * overlap;
        return true;
    }

    float[] project_onto_axis(List<Vector2> verticies, Vector2 axis)
    {
        float min = Vector2.Dot(axis, verticies[0]);
        float max = min;

        for (int i = 1; i < verticies.Count; i++)
        {
            float proj = Vector2.Dot(axis, verticies[i]);
            if (proj < min)
                min = proj;
            else if (proj > max)
                max = proj;
        }
        return new float[2] { min, max };
    }

}