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
        mLines.positionCount = 6;
        mLines.useWorldSpace = true;

       
        //mLines.SetPosition(3, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        mLines.SetPosition(0, transform.position);
        mLines.SetPosition(1, transform.position + (rb.velocity * 0.15f));
        mLines.SetPosition(2, transform.position + (rb.velocity * 0.175f));
        mLines.SetPosition(3, transform.position );
        mLines.SetPosition(4, transform.position + (Vector3.down*0.02f));
        mLines.SetPosition(5, transform.position + (Vector3.down*0.025f));
        mLines.widthCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.345f, 1), new Keyframe(0.349f, 0), new Keyframe(0.95f, 1), new Keyframe(1f, 0));
        //mLines.SetPosition(3, Vector3.zero);
        //Debug.Log(this.GetComponent<Rigidbody>().velocity);
    }
}
