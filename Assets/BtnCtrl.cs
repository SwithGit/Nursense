using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCtrl : MonoBehaviour
{
    Button button;
    public int i;
    Player_IVShot _player_IVShot;

    private void Awake()
    {
        _player_IVShot = FindObjectOfType<Player_IVShot>();
    }
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        if (FindObjectOfType<Player2>())
        {
            if (name.Contains("Sodoc"))
            {
                button.onClick.AddListener(() => OnSodocButtonClick());
            }

            else if (name.Contains("Som"))
            {
                if(FindObjectOfType<Player2>()._state == Player2.State.SG_Diabates_Insulin)
                button.onClick.AddListener(() => OnSomButtonClick2());
                else button.onClick.AddListener(() => OnSomButtonClick());
            }
            else if (name.Contains("Garbage"))
            {
                button.onClick.AddListener(() => OnGarbageButtonClick());
            }

            else if (name.Contains("Som"))
            {
                button.onClick.AddListener(() => OnSomButtonClick());
            }

            else if (name.Contains("Gauze"))
            {
                button.onClick.AddListener(() => OnGauzeButtonClick());
            }

            else if (name.Contains("Pose"))
            {
                button.onClick.AddListener(() => OnPoseButtonClick());
            }

            else if (name.Contains("Curtain"))
            {
                button.onClick.AddListener(() => OnCurtainButtonClick());
            }

            else if (name.Contains("Confirm"))
            {
                button.onClick.AddListener(() => OnBaseButtonClick());
            }

            else if (name.Contains("Lancet"))
            {
                button.onClick.AddListener(() => OnBaseButtonClick());
            }

            else
            {
                button.onClick.AddListener(() => OnButtonClick());
            }
        }
        else if(FindObjectOfType<Player_IVShot>())
        {
            button.onClick.AddListener(() => OnClickIVButton());
        }

    }

    public void OnButtonClick()
    {
        FindObjectOfType<Login>().SceneSelect(i);
    }

    public void OnCurtainButtonClick()
    {
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }

        FindObjectOfType<Player2>().OnButtonClick(i);
        if (i == 8)
            i = 27;
    }

    public void OnBaseButtonClick()
    {
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }
        FindObjectOfType<Player2>().OnDiabateButtonClick(i);
    }

    public void OnGauzeButtonClick()
    {
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }
        FindObjectOfType<Player2>().pincet.GetComponent<BoxCollider>().enabled = true;
        FindObjectOfType<Player2>().OnButtonClick(i);
        if (i == 2)
            i = 20;
    }

    public void OnGarbageButtonClick()
    {
        if (FindObjectOfType<Player2>()._sgstate == Player2.SGState.SG23)
            i = 22;
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }

        FindObjectOfType<Player2>().OnButtonClick(i);
        if (i == 14)
            i = 19;
        else if (i == 19)
            i = 19;        
    }

    public void OnPoseButtonClick()
    {
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }

        FindObjectOfType<Player2>().OnButtonClick(i);
        if (i == 9)
            i = 25;        
    }

    public void OnSomButtonClick()
    {
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }

        FindObjectOfType<Player2>().OnButtonClick(i);
        if (i == 3) i = 18;        
    }

    public void OnSodocButtonClick()
    {
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }
        
        FindObjectOfType<Player2>().OnButtonClick(i);
        if (FindObjectOfType<Player2>()._state == Player2.State.SG_Diabates)
        {
            i = 13;
            return;
        }

        else if (FindObjectOfType<Player2>()._state == Player2.State.SG_Diabates_Insulin)
        {
            if (i == 1)
                i = 3;
            else if (i == 3)
                i = 10;
            else if (i == 10)
                i = 16;
            else if (i == 16)
                i = 299;
            return;
        }

        if (i == 0)
            i = 5;
        else if (i == 5)
            i = 11;
        else if (i == 11)
            i = 15;
        else if (i == 15)
            i = 29;
    }

    public void OnSomButtonClick2()
    {
        if (i != (int)FindObjectOfType<Player2>()._sgstate)
        {
            FindObjectOfType<Player2>().notice1_fail.SetActive(true);
            return;
        }

        FindObjectOfType<Player2>().OnButtonClick(i);


        if (FindObjectOfType<Player2>()._state == Player2.State.SG_Diabates_Insulin)
        {

            if (i == 11)
                i = 13;
            return;
        }
        //if (i == 0)
        //    i = 5;
        //else if (i == 5)
        //    i = 11;
        //else if (i == 11)
        //    i = 15;
        //else if (i == 15)
        //    i = 29;
    }

    public void OnClickIVButton()
    {
        if(_player_IVShot._rayState == Player_IVShot.RayState.button)
        {
            if(_player_IVShot.btnNum == i)
            {
                _player_IVShot.IV_Patient();
                return;
            }
        }

        _player_IVShot.SetNotice("올바른 행동을 취해주세요!");
    }
}
