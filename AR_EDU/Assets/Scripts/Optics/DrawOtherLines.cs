using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOtherLines : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float counter;
    private float counter2;
    private float dist;
    private float dist2;
    public Transform origin;
    public Transform destination;
    public Transform destination2;
    public bool isReady = false;
    int t = 0;
    public bool isDoneFirst = false;


    public float lineDrawSpeed = 20f;
    public float lineDrawSpeed2 = 30f;

    void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = 0.010f;

        dist = Vector3.Distance(origin.localPosition, destination.localPosition);
        dist2 = Vector3.Distance(destination.localPosition, destination2.localPosition);
        lineRenderer.startWidth = 0f;
        lineRenderer.endWidth = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject previousLine = GameObject.Find("line1");
        DrawLineScript drawLineScript = previousLine.GetComponent<DrawLineScript>();
        isReady = drawLineScript.isAtEnd;
        if (isReady && t == 0)
        {
            lineRenderer.SetPosition(0, origin.localPosition);
            if (counter < dist)
            {
                counter += .1f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, counter);

                Vector3 pointA = origin.localPosition;
                Vector3 pointB = destination.localPosition;

                Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
                lineRenderer.SetPosition(1, pointAlongLine);
                lineRenderer.SetPosition(2, pointAlongLine);

            }
            
        }
        if (counter > 1f)
        {
            isDoneFirst = true;
            
        }
        else lineRenderer.endWidth += 0.00001f;

        if (isDoneFirst)
        {

          //  lineRenderer.SetPosition(1, destination.localPosition);
            if (counter2 < dist2)
            {
                counter2 += .1f / 40;

                float x = Mathf.Lerp(0, dist2, counter2);

                Vector3 pointA = destination.localPosition;
                Vector3 pointB = destination2.localPosition;

                Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
                lineRenderer.SetPosition(2, pointAlongLine);
            }
            if(counter2 < 1)
                lineRenderer.endWidth += 0.00002f;

        }

    }

}
