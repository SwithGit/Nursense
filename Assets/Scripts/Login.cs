using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using ImaginationOverflow.UniversalDeepLinking;

public class Login : MonoBehaviour
{
    public RawImage ri; 
    public string parameter;
    public RectTransform contentTr;
    // Start is called before the first frame update
    void Start()
    {
        DeepLinkManager.Instance.LinkActivated += Instance_LinkActivated;
    }

    private void Instance_LinkActivated(LinkActivation s)
    {
        parameter = s.RawQueryString;
        if (string.IsNullOrEmpty(parameter))
            return;
        SceneManager.LoadScene(int.Parse(parameter));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeftButtonClick()
    {
        float x = contentTr.anchoredPosition.x + 400;        
        contentTr.anchoredPosition = new Vector2(x , 0);
    }

    public void OnRightButtonClick()
    {
        float x = contentTr.anchoredPosition.x - 400;
        contentTr.anchoredPosition = new Vector2(x , 0);
    }

    public void SceneSelect(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
