using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour
{
    Transform F1;
    Transform F2;
    Transform lens;
    Transform obj;
    List<LineRenderer> lr;
    Vector3 start, l1, end1, end2, intersection, intersection2;
    Material mat;
    Color bc;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform item in GetComponentsInChildren<Transform>())
        {
            if (item.name == "F1")
                F1 = item;
            else if (item.name == "F2")
                F2 = item;
            else if (item.name == "lens")
                lens = item;
            else if (item.name == "obj")
                obj = item;
        }
        lr = new List<LineRenderer>();
        foreach (LineRenderer l in GetComponentsInChildren<LineRenderer>())
            lr.Add(l);
        lr[0].positionCount = 2;
        lr[1].positionCount = 3;
        lr[2].positionCount = 2;


        start = obj.position + ((obj.lossyScale / 2).y * Vector3.up);
        l1 = lens.gameObject.GetComponent<Renderer>().bounds.center + ((obj.lossyScale).y * Vector3.up);
        end1 = F2.position - l1;
        end2 = lens.gameObject.GetComponent<Renderer>().bounds.center - start;

        ClosestPointsOnTwoLines(out intersection, out intersection2, lens.gameObject.GetComponent<Renderer>().bounds.center, end2, l1, end1);

        float scale = lens.gameObject.GetComponent<Renderer>().bounds.center.y - intersection.y;

        GameObject ga = Instantiate(obj.gameObject, intersection, obj.rotation);
        ga.transform.localScale = new Vector3(scale, scale, scale);
        ga.transform.position += Vector3.up * (scale / 2);
        mat = ga.GetComponent<Renderer>().material;
        Color bc = mat.color;
        bc.a = 0;
        mat.color = bc;

        lr[2].SetPositions(new[] { obj.position - ((obj.lossyScale / 2).y * Vector3.up),new Vector3(intersection.x, (obj.position - ((obj.lossyScale / 2).y * Vector3.up)).y,intersection.z) });
    }
    bool s = true, m = false, e = false;
    float t = 0;
    // Update is called once per frame
    void Update()
    {
        
        if (s)
        {
            t += 0.5f * Time.deltaTime;
            //lr[0].SetPositions(new[] { start, start + (end2 * 4) });
            //lr[1].SetPositions(new[] { start, l1, l1 + (end1 * 6) });
            lr[0].SetPositions(new[] { start, Vector3.Lerp(start, start + end2, t) });
            lr[1].SetPositions(new[] { start, Vector3.Lerp(start, l1, t), Vector3.Lerp(start, l1, t) });
            if(t >= 1)
            { 
                t = 0;
                m = true;
                s =false;
            }
        }
        else if (m)
        {
            t += 0.33f * Time.deltaTime;
            lr[0].SetPositions(new[] { start, start + end2 + Vector3.Lerp(Vector3.zero, (end2*3f), t) });
            lr[1].SetPositions(new[] { start, l1, l1 + Vector3.Lerp(Vector3.zero,(end1 * 3.5f), t) });
            //lr[0].SetPositions(new[] { start, start + (end2 * 4) });
            //lr[1].SetPositions(new[] { start, l1, l1 + (end1 * 3) });
            if (t >= 1)
            {
                t = 0;
                bc.r = 1;
                e = true;
                m = false;
            }
        }
        else if (e)
        {
            bc.a += 0.5f * Time.deltaTime;
            mat.color = bc;
            if (bc.a > 1)
                e = false;  
        }

    }

    public static bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {

        closestPointLine1 = Vector3.zero;
        closestPointLine2 = Vector3.zero;

        float a = Vector3.Dot(lineVec1, lineVec1);
        float b = Vector3.Dot(lineVec1, lineVec2);
        float e = Vector3.Dot(lineVec2, lineVec2);

        float d = a * e - b * b;

        //lines are not parallel
        if (d != 0.0f)
        {

            Vector3 r = linePoint1 - linePoint2;
            float c = Vector3.Dot(lineVec1, r);
            float f = Vector3.Dot(lineVec2, r);

            float s = (b * f - c * e) / d;
            float t = (a * f - c * b) / d;

            closestPointLine1 = linePoint1 + lineVec1 * s;
            closestPointLine2 = linePoint2 + lineVec2 * t;

            return true;
        }
        else
            return false;
    }
}
