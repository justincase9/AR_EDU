using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData : MonoBehaviour
{
    private bool rec = true;
    private LineRenderer lr;
    public Vector3 v0;
    public Vector3 v1;
    public Vector3 v2;
    public Vector3 v3;
    public Vector3 v4;
    public Vector3 v5;
    private float slider;
    private bool stop;

    public void ChangeState()
    {
        rec = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        slider = 0;

        v0 = new Vector3();
        v1 = new Vector3();
        v2 = new Vector3();
        v3 = new Vector3();
        v4 = new Vector3();
        v5 = new Vector3();
        lr = GetComponentInChildren<LineRenderer>();
        //if ()
        //{
        //    rec = false;
        //    lr.positionCount = 6;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (rec)
        {
            v0 = lr.GetPosition(0);
            v1 = lr.GetPosition(1);
            v2 = lr.GetPosition(2);
            v3 = lr.GetPosition(3);
            v4 = lr.GetPosition(4);
            v5 = lr.GetPosition(5);
        }
        else
        {
            lr.SetPositions(new[] { v0, v1, v2, v3, v4, v5 });
            lr.widthCurve = new AnimationCurve(new Keyframe(0, 0.8f), new Keyframe(0.2f, .8f), new Keyframe(.4f, 0), new Keyframe(.531f, 0.8f), new Keyframe(.9f, 0.8f), new Keyframe(1f, 0f));
        }
    }
    
   
}
