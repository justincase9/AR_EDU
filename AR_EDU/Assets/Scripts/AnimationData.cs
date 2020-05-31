using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData : MonoBehaviour
{
     
    public int type;
    private bool rec = true;
    private LineRenderer lr;
    public Vector3 v0;
    public Vector3 v1;
    public Vector3 v2;
    public Vector3 v3;
    public Vector3 v4;
    public Vector3 v5;
    public Vector3 v6;
    public Vector3 v7;
    public Vector3 v8;





    public void ChangeState()
    {
        rec = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        v0 = new Vector3();
        v1 = new Vector3();
        v2 = new Vector3();
        v3 = new Vector3();
        v4 = new Vector3();
        v5 = new Vector3();
        v6 = new Vector3();
        v7 = new Vector3();
        v8 = new Vector3();
        lr = GetComponentInChildren<LineRenderer>();
        if(type == 0)
        {
            lr.positionCount = 3;
        }
        //if ()
        //{
        //    rec = false;
        //    lr.positionCount = 6;
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rec)
        {
            if (type == 0)
            {
                v0 = lr.GetPosition(0);
                v1 = lr.GetPosition(1);
                v2 = lr.GetPosition(2);
            }
            if (type == 1)
            {
                v0 = lr.GetPosition(0);
                v1 = lr.GetPosition(1);
                v2 = lr.GetPosition(2);
                v3 = lr.GetPosition(3);
                v4 = lr.GetPosition(4);
                v5 = lr.GetPosition(5);
                v6 = lr.GetPosition(6);
                v7 = lr.GetPosition(7);
                v8 = lr.GetPosition(8);
            }
        }
        else
        {
            if (type == 0)
            {
                lr.SetPositions(new[] { v0, v1, v2, /*v3, v4, v5 */});
                lr.widthCurve = new AnimationCurve(new Keyframe(0, 0.8f), new Keyframe(0.2f, .8f), new Keyframe(.4f, 0)/*, new Keyframe(.531f, 0.8f), new Keyframe(.9f, 0.8f), new Keyframe(1f, 0f)*/);
            }
            if (type == 1)
            {
                lr.SetPositions(new[] { v0, v1, v2, v3, v4, v5, v6,v7,v8});
                lr.widthCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.210f, 1), new Keyframe(0.211f, 0), new Keyframe(0.393f, 1), new Keyframe(0.634f, 1), new Keyframe(0.635f, 0), new Keyframe(0.995f, 1), new Keyframe(1f, 0));
            }
        }
    }


}
