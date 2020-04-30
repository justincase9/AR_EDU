using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    [Range(0f, 200f)]
    public float force = 1000;

    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;
    bool animating = true;


    LineRenderer lr;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.useGravity = false;
        //this.GetComponentInChildren<Rigidbody>().AddForce(GameObject.Find("cannon").transform.up * force);
        //rb.gameObject.transform.Rotate(transform.rotation.eulerAngles - Quaternion.Euler(0, 90, 0).eulerAngles);
        this.GetComponentInChildren<Rigidbody>().AddForce(rb.transform.up * force);
        lr = this.GetComponentInChildren<LineRenderer>();
        this.GetComponentInChildren<LineRenderer>().positionCount = 6;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
        if (animating)
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
            if (rb.gameObject.transform.localPosition.y < 0)
            {
                gravityScale = 0;
                rb.velocity = new Vector3(0, 0, 0);
                animating = false;
                //rb.gameObject.GetComponent<LineRenderer>().enabled = false;
            }

            lr.SetPosition(0, rb.transform.position);
            lr.SetPosition(1, rb.transform.position + (rb.velocity * 0.15f));
            lr.SetPosition(2, rb.transform.position + (rb.velocity * 0.175f));
            lr.SetPosition(3, rb.transform.position);
            lr.SetPosition(4, rb.transform.position + (new Vector3(0, -.8f, 0) * 0.15f));
            lr.SetPosition(5, rb.transform.position + (new Vector3(0, -.8f, 0) * 0.175f));
            lr.widthCurve = new AnimationCurve(new Keyframe(0, 0.8f), new Keyframe(0.2f, .8f), new Keyframe(.4f, 0), new Keyframe(.531f, 0.8f), new Keyframe(.9f, 0.8f), new Keyframe(1f, 0f));
        }
    }
}
