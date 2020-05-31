using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoYoScript : MonoBehaviour
{
    List<LineRenderer> lr;
    Transform yoyotf;
    Transform yoyoloc;
    Transform pivottf;
    Vector3 locoffset;
    float slider = 0;
    // Start is called before the first frame update
    void Start()
    {
        locoffset = new Vector3(1, -1.52f, 9.58f);
        foreach (Transform item in GetComponentsInChildren<Transform>())
        {
            if (item.name == "pivot")
                pivottf = item;
            else if (item.name == "yoyo")
                yoyotf = item;
            else if (item.name == "yoyoloc")
                yoyoloc = item;
        }
        lr = new List<LineRenderer>();
        foreach (LineRenderer item in GetComponentsInChildren<LineRenderer>())
            lr.Add(item);

        lr[0].positionCount = 2;
        lr[0].SetPosition(0, pivottf.position);

        lr[1].positionCount = 9;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yoyotf.RotateAround(pivottf.position, Vector3.up, Time.deltaTime * 360 * slider);
        lr[0].SetPosition(1, yoyotf.position);

        lr[1].SetPositions(new[] { locoffset,
            locoffset + new Vector3(0,40,0) * slider ,
            locoffset + new Vector3(0, 50, 0)* slider,
            locoffset,
            locoffset + new Vector3(0,-40,0)* slider,
            locoffset + new Vector3(0,-50,0)* slider,
            locoffset,
            locoffset + new Vector3(0,0,40)* slider,
            locoffset + new Vector3(0,0,50)* slider
        });
        lr[1].widthCurve = new AnimationCurve(
            new Keyframe(0, 1), new Keyframe(0.165f, 1), new Keyframe(0.1666f, 0),
            new Keyframe(0.333f, 0), new Keyframe(0.335f, 1), new Keyframe(0.571f, 1),
            new Keyframe(0.577f, 0), new Keyframe(0.691f, 0), new Keyframe(0.695f, 1),
            new Keyframe(0.990f, 1), new Keyframe(0.999f, 0));
    }
    private void OnGUI()
    {
        slider = GUI.HorizontalSlider(new Rect(40, 40, 400, 80), slider, 0, 1);
    }
}
