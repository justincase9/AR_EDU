using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceArrows : MonoBehaviour
{
   
    LineRenderer mLines;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        mLines = GetComponent<LineRenderer>();
        mLines.positionCount = 9;
        mLines.useWorldSpace = true;

       
        //mLines.SetPosition(3, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        mLines.SetPosition(0, transform.position + Vector3.up *0.005f);
        mLines.SetPosition(1, transform.position+ Vector3.up * 0.005f + (transform.up * 0.03f));
        mLines.SetPosition(2, transform.position + Vector3.up * 0.005f + (transform.up * 0.035f));
        mLines.SetPosition(3, transform.position + Vector3.up * 0.005f);
        mLines.SetPosition(4, transform.position + Vector3.up * 0.005f + (Vector3.down*0.03f));
        mLines.SetPosition(5, transform.position + Vector3.up * 0.005f + (Vector3.down*0.035f));
        mLines.SetPosition(6, transform.position + Vector3.up * 0.005f);
        mLines.SetPosition(7, transform.position + Vector3.up * 0.005f + (transform.forward * 0.015f * -1));
        mLines.SetPosition(8, transform.position + Vector3.up * 0.005f + (transform.forward *0.02f * -1));
        mLines.widthCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.210f, 1), new Keyframe(0.211f, 0), new Keyframe(0.393f, 1), new Keyframe(0.634f, 1), new Keyframe(0.635f, 0), new Keyframe(0.995f, 1), new Keyframe(1f, 0));
        //mLines.SetPosition(3, Vector3.zero);
        //Debug.Log(this.GetComponent<Rigidbody>().velocity);
    }
}
