using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWITHFACTORY.CYJ
{

    public class Rope2 : MonoBehaviour
    {
        Player_S2_2 _player_S2_2;

        public int phase;
        public Vector3 myPos;
        Vector3 stickedPos;
        Vector3 trainedPos;


        const float underLocalXMax = 0;        
        const float underLocalXMin1 = 0 - 0.18f;
        const float underLocalXMin2 = 0 - 0.18f - 0.04f;


        public Transform stickedTr;
        public Transform trainedTr;
        private void Awake()
        {
            phase = 1;
            _player_S2_2 = FindObjectOfType<Player_S2_2>();
        }

        private void Start()
        {
            myPos = transform.localPosition;
            stickedPos = stickedTr.position;
            trainedPos = transform.localPosition;
        }

        private void Update()
        {
            if (phase == 1)
            {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, underLocalXMin1, underLocalXMax), myPos.y, myPos.z);
                if (transform.localPosition.x == underLocalXMin1)
                {
                    _player_S2_2.P2_Patient();
                    phase++;
                }
            }
            if(phase == 2)
            {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, underLocalXMin2, underLocalXMin1), myPos.y, myPos.z);
                if (transform.localPosition.x == underLocalXMin2)
                {
                    _player_S2_2.P2_Patient();
                    phase++;
                }

            }
            if (phase == 3)
            {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, underLocalXMin2, underLocalXMax), myPos.y, myPos.z);
                if (transform.localPosition.x == underLocalXMax)
                {
                    phase = 4;
                }

            }
            Stick();
            if (trainedTr)
            {
                if (phase == 4) return;
                trainedTr.position += trainedTr.right * (trainedPos.x - transform.localPosition.x) * 0.7f;
                trainedPos = transform.localPosition;
            }

            //transform.localPosition = new Vector3(Mathf.Clamp(underTR.localPosition.x, underLocalXMin, underLocalXMax), underPos.y, underPos.z);

        }

        public void Stick()
        {
            if (phase == 4) return;
            if (stickedTr) stickedTr.position = stickedPos;
        }


        public void EndAnim()
        {
            if(_player_S2_2) _player_S2_2.P2_Patient();
        }
    }



}

