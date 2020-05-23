using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TemuScript : MonoBehaviour
{
    public GameObject Temos;
    public GameObject Lygiai;
    public GameObject Main;
    private string[] lPav;

    void Start()
    {
        //Debug.Log(Lygiai);
        //Debug.Log(Lygiai.name);
        lPav = new string[36];
        lPav[0] = "Nuozulni plokstuma";
        lPav[1] = "Kamuolio metimas";
        lPav[2] = "Comming soon";
        lPav[3] = "Comming soon";
        lPav[4] = "Comming soon";
        lPav[5] = "Comming soon";
        lPav[6] = "Prizme";
        lPav[7] = "Comming soon";
        lPav[8] = "Comming soon";
        lPav[9] = "Comming soon";
        lPav[10] = "Comming soon";
        lPav[11] = "Comming soon";
        lPav[12] = "Comming soon";
        lPav[13] = "Comming soon";
        lPav[14] = "Comming soon";
        lPav[15] = "Comming soon";
        lPav[16] = "Comming soon";
        lPav[17] = "Comming soon";
        lPav[18] = "Comming soon";
        lPav[19] = "Comming soon";
        lPav[20] = "Comming soon"; 
        lPav[21] = "Comming soon";
        lPav[22] = "Comming soon";
        lPav[23] = "Comming soon";
        lPav[24] = "Comming soon";
        lPav[25] = "Comming soon";
        lPav[26] = "Comming soon";
        lPav[27] = "Comming soon";
        lPav[28] = "Comming soon";
        lPav[29] = "Comming soon";

        Button[] buttons = Temos.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            //Debug.Log(b.name);
            b.onClick.AddListener(() => SetLygiai(b.name));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("Main") != null)
            Application.Quit();
            else if(GameObject.Find("Temos")!=null)
            {
                Temos.SetActive(false);
                Main.SetActive(true);
            }
            else if (GameObject.Find("Lygiai") != null)
            {
                Lygiai.SetActive(false);
                Temos.SetActive(true);
            }
        }
    }

    void SetLygiai(string s)
    {
        int num = int.Parse(s.Substring(1, 1)) - 1;
        //Debug.Log(num);
        //Debug.Log(Lygiai.transform.childCount);
        for(int i=0;i < Lygiai.transform.childCount; i++)
            Lygiai.transform.GetChild(i).GetComponentInChildren<Text>().text = lPav[6 * num + i];
        if (num == 0)
        {
            Lygiai.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SceneLoad(2));
            Lygiai.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SceneLoad(1));

        }
        if(num == 1)
        {
            Lygiai.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SceneLoad(7));

        }
        Lygiai.SetActive(true);
        Temos.SetActive(false);
    }

    void SceneLoad(int i)
    {
        if (i == 1)
            SceneManager.LoadScene("Kinematics_1");
        else if (i == 2)
            SceneManager.LoadScene("Kinematics#2");
        else if(i == 7)
            SceneManager.LoadScene("Optics_1");

    }
}
