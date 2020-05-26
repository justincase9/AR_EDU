using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Animation anim;
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
            //x = new List<Keyframe>();
            //y = new List<Keyframe>();
            //z = new List<Keyframe>();
        }

        public void SnapKey(int offset, Vector3 v3)
        {
            val[keys[offset]].Add(new Keyframe(timeStamp, v3.x));
            val[keys[offset + 1]].Add(new Keyframe(timeStamp, v3.y));
            val[keys[offset + 2]].Add(new Keyframe(timeStamp, v3.z));
        }
        public void SnapKey(int offset, Quaternion q3)
        {

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

                if(i<7)
                    clip.SetCurve("", typeof(Transform), keys[i], curve);
                else
                    clip.SetCurve("", typeof(AnimationData), keys[i], curve);




                curve.postWrapMode = WrapMode.ClampForever;
            }
            //curve = new AnimationCurve(X);
            //clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
            //curve = new AnimationCurve(Y);
            //clip.SetCurve("", typeof(Transform), "localPosition.y", curve);
            //curve = new AnimationCurve(Z);
            //clip.SetCurve("", typeof(Transform), "localPosition.z", curve);
            clip.EnsureQuaternionContinuity();
            //anim.clip.SetCurve("", typeof(AnimationData), "localV0.x", new AnimationCurve(new Keyframe(0.1f, 1)));
            anim.AddClip(clip, "replay");
        }
        public void Replay()
        {
            anim["replay"].speed = 0.2f;
            anim.Play("replay");
        }
    }


    KeyframeVec poskey;
    public bool recording;
    private AnimationData adata;

    void Start()
    {
        recording = true;
        poskey = new KeyframeVec(GetComponent<Animation>(),
            new string[] { "localPosition.x", "localPosition.y", "localPosition.z", "localRotation.x", "localRotation.y", "localRotation.z", "localRotation.w",
                "v0.x","v0.y","v0.z", "v1.x","v1.y","v1.z", "v2.x","v2.y","v2.z", "v3.x","v3.y","v3.z", "v4.x","v4.y","v4.z", "v5.x","v5.y","v5.z",   });
        adata = GetComponent<AnimationData>();
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
            poskey.SnapKey(16, adata.v3);
            poskey.SnapKey(19, adata.v4);
            poskey.SnapKey(22, adata.v5);
        }

    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(40, 40, 80, 30), "recording"))
        {
            adata.ChangeState();
            recording = false;
            Invoke("Playback", 1f);
        }
    }
    public void Playback()
    {
        poskey.SaveToAnim();
        poskey.Replay();
    }

}
