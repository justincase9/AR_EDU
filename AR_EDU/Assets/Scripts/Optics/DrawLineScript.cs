using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    public float counter;
    public float dist;
    public Transform origin;
    public Transform destination;
    public bool isAtEnd = false;
    //public float counter;

    public float lineDrawSpeed = 6f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.localPosition);
        lineRenderer.SetWidth(.005f, .005f);

        dist = Vector3.Distance(origin.localPosition, destination.localPosition);

    }

    // Update is called once per frame
    void Update()
    {
        if(counter < dist)
        {
            counter += .1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.localPosition;
            Vector3 pointB = destination.localPosition;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            lineRenderer.SetPosition(1, pointAlongLine);
            if (counter >= 1)
                isAtEnd = true;
        }
        
    }
}
