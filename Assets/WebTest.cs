using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{
    const string url = "http://kloud.lk/nursense/login.php";
    // Start is called before the first frame update
    void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("id" , "abc");
        form.AddField("pw" , "123");
        StartCoroutine(TestTest(form));
    }

    IEnumerator TestTest(WWWForm form)
    {
        UnityWebRequest request = UnityWebRequest.Post(url , form);
        yield return request.SendWebRequest();
        print(request.downloadHandler.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
