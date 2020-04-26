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

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.useGravity = false;
        this.GetComponentInChildren<Rigidbody>().AddForce(rb.transform.up * force);
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
            
            //Vector2 v = rb.velocity;
            //float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            //rb.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
