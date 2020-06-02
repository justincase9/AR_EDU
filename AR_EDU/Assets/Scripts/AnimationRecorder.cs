using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationRecorder : MonoBehaviour
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
                    clip.SetCurve("", typeof(AnimationData), keys[i], curve);

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



    KeyframeVec poskey;
    bool recording;
    private AnimationData adata;
    private float slider;

    void Start()
    {
        recording = true;
        poskey = new KeyframeVec(GetComponent<Animation>(),
            new string[] { "localPosition.x", "localPosition.y", "localPosition.z", "localRotation.x", "localRotation.y", "localRotation.z", "localRotation.w",
                "v0.x","v0.y","v0.z", "v1.x","v1.y","v1.z", "v2.x","v2.y","v2.z"  });
        adata = GetComponent<AnimationData>();
        slider = 0;
    }
    void FixedUpdate()
    {
        //Debug.Log(transform.localRotation.x + "      " + transform.localRotation.y + "         " + transform.localRotation.z + "\n" + transform.localRotation.eulerAngles.x + "      " + transform.localRotation.eulerAngles.y + "         " + transform.localRotation.eulerAngles.z + "\n" + Quaternion.Euler(transform.localRotation.eulerAngles).x + "      " + Quaternion.Euler(transform.localRotation.eulerAngles).y + "         " + Quaternion.Euler(transform.localRotation.eulerAngles).z + "\n" + Quaternion.Euler(transform.localRotation.eulerAngles).eulerAngles.x + "      " + Quaternion.Euler(transform.localRotation.eulerAngles).eulerAngles.y + "         " + Quaternion.Euler(transform.localRotation.eulerAngles).eulerAngles.z);
        //Debug.Log(transform.rotation.x+ "      " + transform.rotation.y + "         " + transform.rotation.z + "\n" + transform.rotation.eulerAngles.x + "      " + transform.rotation.eulerAngles.y + "         " + transform.rotation.eulerAngles.z + "\n" + Quaternion.Euler(transform.rotation.eulerAngles).x + "      " + Quaternion.Euler(transform.rotation.eulerAngles).y + "         " + Quaternion.Euler(transform.rotation.eulerAngles).z+ "\n" + Quaternion.Euler(transform.rotation.eulerAngles).eulerAngles.x + "      " + Quaternion.Euler(transform.rotation.eulerAngles).eulerAngles.y + "         " + Quaternion.Euler(transform.rotation.eulerAngles).eulerAngles.z);
        if (recording)
        {
            //Debug.Log(adata);
            poskey.SnapKey(0, transform.localPosition);
            poskey.SnapKey(3, Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
            poskey.SnapKey(7, adata.v0);
            poskey.SnapKey(10, adata.v1);
            poskey.SnapKey(13, adata.v2);

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

        if (GUI.Button(new Rect(40, 40, 250, 200), "Pakartoti"))
        {
            adata.ChangeState();
            recording = false;
            GetComponentInParent<cannon>().enabled = false;
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



