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
    public GameObject Kurejai;
    private string[] lPav;

    void Start()
    {
        //Debug.Log(Lygiai);
        //Debug.Log(Lygiai.name);
        lPav = new string[12];
        lPav[0] = "Kamuolio metimas";
        lPav[1] = "Comming soon";
        lPav[2] = "Comming soon";
        lPav[3] = "Nuožulni plokštuma";
        lPav[4] = "Judėjimas apskritimu";
        lPav[5] = "Comming soon";
        lPav[6] = "Prizme";
        lPav[7] = "Lešis";
        lPav[8] = "Comming soon";
        lPav[9] = "Comming soon";
        lPav[10] = "Comming soon";
        lPav[11] = "Comming soon";

        Button[] buttons = Temos.GetComponentsInChildren<Button>();
        int i = 0;
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
            else if (GameObject.Find("Kurejai") != null)
            {
                Kurejai.SetActive(false);
                Main.SetActive(true);
            }
        }
    }

    void SetLygiai(string s)
    {
        int num = int.Parse(s.Substring(1, 1)) - 1;
        //Debug.Log(num);
        //Debug.Log(Lygiai.transform.childCount);
        for(int i=0;i < Lygiai.transform.childCount; i++)
            Lygiai.transform.GetChild(i).GetComponentInChildren<Text>().text = lPav[3 * num + i];
        if (num == 0)
        {
            Lygiai.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SceneLoad(2));
        }
        if(num == 1)
        {
            Lygiai.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SceneLoad(1));
            Lygiai.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SceneLoad(5));
        }
        if (num == 2)
        {
            Lygiai.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SceneLoad(3));
            Lygiai.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SceneLoad(4));
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
        else if(i == 3)
            SceneManager.LoadScene("Optics_1");
        else if(i == 4)
            SceneManager.LoadScene("Optics_2");
        else if(i == 5)
            SceneManager.LoadScene("Kinematics#3");

    }
}
