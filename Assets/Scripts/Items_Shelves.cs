using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Items_Shelves : MonoBehaviour
{
    Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Blink()
    {
        InvokeRepeating("Blink_Repeat" , 0f , 1f);
    }

    public void CancelBlink()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }
        CancelInvoke("Blink_Repeat");
    }
    

    void Blink_Repeat()
    {
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = !renderers[i].enabled;
        }
    }


    private void OnDisable()
    {
        CancelBlink();
    }

}
