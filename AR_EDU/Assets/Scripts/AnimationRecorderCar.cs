using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRecorderCar : MonoBehaviour
{
    public class KeyframeVec : UnityEngine.Object
    {

        public Dictionary<string, List<Keyframe>> val;
        public List<string> keys;
        //List<Keyframe> x { get; set; }
        //List<Keyframe> y { get; set; }
        //List<Keyframe> z { get; set; }

        //public Keyframe[] X => x.ToArray();
        //public Keyframe[] Y => y.ToArray();
        //public Keyframe[] Z => z.ToArray();

        private float tinst;
        private float timeStamp => Time.time - tinst;
        public Animation anim;
        public KeyframeVec(Animation iAnim, string[] names)
        {
            val = new Dictionary<string, List<Keyframe>>();
            keys = new List<string>();
            for (int i = 0; i < names.Length; i++)
            {
                val.Add(names[i], new List<Keyframe>());
                keys.Add(names[i]);
            }
            anim = iAnim;
            tinst = Time.time;
        }

        public void SnapKey(int offset, Vector3 v3)
        {
            //Keyframe a, b, c;
            val[keys[offset]].Add(new Keyframe(timeStamp, v3.x));
            val[keys[offset + 1]].Add(new Keyframe(timeStamp, v3.y));
            val[keys[offset + 2]].Add(new Keyframe(timeStamp, v3.z));

        }
        public void SnapKey(int offset, Quaternion q3)
        {
            //Keyframe a, b, c, d;
            val[keys[offset]].Add(new Keyframe(timeStamp, q3.x));
            val[keys[offset + 1]].Add(new Keyframe(timeStamp, q3.y));
            val[keys[offset + 2]].Add(new Keyframe(timeStamp, q3.z));
            val[keys[offset + 3]].Add(new Keyframe(timeStamp, q3.w));

        }
        public void SnapKey(int index, float f)
        {
            val[keys[index]].Add(new Keyframe(timeStamp, f));
        }

        /// <summary>
        /// Pakeist kad typeof(transform...etc.) butu dinamiskas, tai padaryt su pridetu BindComponent() scriptu ir listu tipu
        /// kintamieji gali buti paimti per typeof(klase) meetodo grazinamus rezultatus
        /// </summary>
        public void SaveToAnim()
        {
            AnimationCurve curve;
            AnimationClip clip = new AnimationClip();
            clip.legacy = true;

            for (int i = 0; i < keys.Count; i++)
            {

                curve = new AnimationCurve(val[keys[i]].ToArray());
                SetCurveLinear(curve);

                if (i < 7)
                    clip.SetCurve("", typeof(Transform), keys[i], curve);
                else
                    clip.SetCurve("default", typeof(AnimationData), keys[i], curve);

                curve.preWrapMode = WrapMode.ClampForever;
                curve.postWrapMode = WrapMode.ClampForever;
            }
            clip.EnsureQuaternionContinuity();


            anim.AddClip(clip, "replay");
        }
        public void Replay()
        {
            anim["replay"].speed = 0.2f;
            anim.Play("replay");
        }
        private static void SetCurveLinear(AnimationCurve curve)
        {
            for (int i = 0; i < curve.keys.Length; ++i)
            {
                Vector2 deltapoint;
                Keyframe key = curve[i];

                if (i == 0 || i == curve.keys.Length - 1) ;
                else
                {
                    Vector2 p1 = new Vector2(curve.keys[i - 1].time, curve.keys[i - 1].value);
                    Vector2 p2 = new Vector2(curve.keys[i].time, curve.keys[i].value);
                    deltapoint = p2 - p1;

                    float intangent = deltapoint.y / deltapoint.x;

                    p1 = new Vector2(curve.keys[i].time, curve.keys[i].value);
                    p2 = new Vector2(curve.keys[i + 1].time, curve.keys[i + 1].value);
                    deltapoint = p2 - p1;

                    float outtangent = deltapoint.y / deltapoint.x;

                    key.inTangent = intangent;
                    key.outTangent = outtangent;
                    curve.MoveKey(i, key);

                }

            }

        }
    }

    public bool recording;
    KeyframeVec poskey;
    AnimationData adata;
    float slider;
    Transform tf;
    void Start()
    {
        tf = GetComponentInParent<Transform>();
        recording = true;
        poskey = new KeyframeVec(GetComponentInParent<Animation>(),
            new string[] { "localPosition.x", "localPosition.y", "localPosition.z", "localRotation.x", "localRotation.y", "localRotation.z", "localRotation.w",
                "v0.x","v0.y","v0.z", "v1.x","v1.y","v1.z", "v2.x","v2.y","v2.z", "v3.x","v3.y","v3.z", "v4.x","v4.y","v4.z", "v5.x","v5.y","v5.z",
            "v6.x","v6.y","v6.z", "v7.x","v7.y","v7.z", "v8.x","v8.y","v8.z",});
        adata = GetComponent<AnimationData>();
        slider = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (recording)
        {
            //Debug.Log(adata);
            Debug.Log(transform.parent.localPosition);
            Debug.Log(transform.parent.localRotation);
            poskey.SnapKey(0, transform.parent.localPosition);
            poskey.SnapKey(3, Quaternion.Euler(transform.parent.localRotation.eulerAngles.x, transform.parent.localRotation.eulerAngles.y, transform.parent.localRotation.eulerAngles.z));
            poskey.SnapKey(7, adata.v0);
            poskey.SnapKey(10, adata.v1);
            poskey.SnapKey(13, adata.v2);
            poskey.SnapKey(16, adata.v3);
            poskey.SnapKey(19, adata.v4);
            poskey.SnapKey(22, adata.v5);
            poskey.SnapKey(25, adata.v6);
            poskey.SnapKey(28, adata.v7);
            poskey.SnapKey(31, adata.v8);
        }
        else
        {
            if (poskey.anim.isPlaying)
                poskey.anim["replay"].speed = slider;
        }

    }

    private void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        GUIStyle mySliderStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fixedWidth = 40;
        myButtonStyle.fixedHeight = 85;
        mySliderStyle.fixedWidth = 810;
        mySliderStyle.fixedHeight = 100;

        if (GUI.Button(new Rect(40, 40, 250, 200), "recording"))
        {
            GetComponentInParent<Rigidbody>().isKinematic = true;
            adata.ChangeState();
            recording = false;
            //GetComponentInParent<cannon>().enabled = false;
            Invoke("Playback", 1f);
        }
        slider = GUI.HorizontalSlider(new Rect(300, 40, 800, 200), slider, -1, 1,mySliderStyle,myButtonStyle);
    }
    public void Playback()
    {
        poskey.SaveToAnim();
        poskey.Replay();
    }
}
