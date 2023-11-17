using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pincet : MonoBehaviour
{
    public int num;
    public Player2 ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (num > 30)
        {
            ps.pincet.transform.GetChild(0).GetComponent<MeshRenderer>().material = ps.cottonMt[1];
            ps.SetAnim(12);
            num = 0;
            GetComponent<BoxCollider>().enabled = false;
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Hip"))
        {
            if (ps._sgstate == Player2.SGState.SG20)
            {
                num++;
            }

            else if (ps._sgstate == Player2.SGState.SG22)
            {                
                ps.hand[1].GetComponent<Animator>().enabled = true;
                ps.hand[1].GetComponent<Animator>().SetTrigger("Gauze");
                ps.isSG = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {        
        if (other.gameObject.tag.Equals("Hip"))
        {
            if (ps._sgstate == Player2.SGState.SG20)
            {
                num++;
            }
        }
    }
}
