using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Pat : MonoBehaviour
{
    public GameObject goal;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().destination = goal.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position , goal.transform.position);
        if (dis < 3)
        {
            gameObject.SetActive(false);
            Player2 ps = FindObjectOfType<Player2>();
            ps._state = Player2.State.Beginning;
            ps._startstate = Player2.StartState.Computer;
            ps.noticeObj[1].SetActive(true);
            ps.missionText.transform.parent.gameObject.SetActive(true);
            ps.missionText.GetComponent<Text>().text = "컴퓨터 앞으로 이동하세요.";
            ps.startObj[1].GetComponent<Outline>().enabled = true;
        }
    }
}
