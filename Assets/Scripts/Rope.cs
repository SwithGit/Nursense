using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWITHFACTORY.CYJ
{

    public class Rope : MonoBehaviour
    {
        Player_S2 _player;
        Player_S2_2 _player_S2_2;

        public int phase;
        public Vector3 myPos;


        const float underLocalXMax = 0.03f;        
        const float underLocalXMin1 = 0.03f -( 0.08f / 1.5f);
        const float underLocalXMin2 = 0.03f - (0.08f / 1.5f) - (0.04f / 1.5f);

        private void Awake()
        {
            phase = 1;
            _player = FindObjectOfType<Player_S2>();
            _player_S2_2 = FindObjectOfType<Player_S2_2>();
        }

        private void Start()
        {
            myPos = transform.localPosition;
        }

        private void Update()
        {
            if(phase == 1 )
            {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, underLocalXMin1, underLocalXMax), myPos.y, myPos.z);
                if (transform.localPosition.x  == underLocalXMin1)
                {
                    _player.S2_Patient();
                    phase++;
                }
            }
            if(phase == 2)
            {
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, underLocalXMin2, underLocalXMin1), myPos.y, myPos.z);
                if (transform.localPosition.x == underLocalXMin2)
                {
                    _player.S2_Patient(); 
                    phase++;
                }

            }
            if (phase == 3)
            {
                transform.localPosition = new Vector3(underLocalXMin2, myPos.y, myPos.z);

            }
            //transform.localPosition = new Vector3(Mathf.Clamp(underTR.localPosition.x, underLocalXMin, underLocalXMax), underPos.y, underPos.z);

        }


        public void EndAnim()
        {
            if(_player) _player.AnimEnd();
            if(_player_S2_2) _player_S2_2.P2_Patient();
        }
    }



}

