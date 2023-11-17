using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWITHFACTORY.CYJ
{
    public class Syringe_5ml : Syringe
    {
        Player_S2_2 _player_S2_2;
        public float pre_X;
        const float underLocalXMin = -0.071f;
        const float underLocalXMax = -0.024f;
        const float underLocalXMinDetect = -0.067f;
        const float underLocalXMaxDetect = -0.03f;
        float timer;

        new void Awake()
        {
            _player_S2_2 = FindObjectOfType<Player_S2_2>();
        }


        public override void Start()
        {
            base.Start();
            pre_X = underTR.localPosition.x;
        }
        void Update()
        {
            underTR.localPosition = new Vector3(Mathf.Clamp(underTR.localPosition.x, underLocalXMin, underLocalXMax), underPos.y, underPos.z);

            if (underTR.localPosition.x <= underLocalXMinDetect)
            {
                Max();
            }
            if (underTR.localPosition.x >= underLocalXMaxDetect)
            {
                Min();
            }


            if (_player_S2_2._s2_3_State == Player_S2_2.S2_3_State.muscleinjection6)
            {
                timer += Time.deltaTime;
                if (timer < 0.1f) return;
                timer = 0;

                print(underTR.localPosition.x - pre_X);
                if (underTR.localPosition.x - pre_X > 0.001f)
                {
                    _player_S2_2.Syringe_Down();
                    Vector2 lp = underTR.localPosition;
                    lp.x = pre_X;
                    underTR.localPosition = lp;
                }
                else
                {
                    pre_X = underTR.localPosition.x;
                }

            }




        }



        void Min()
        {
            _player_S2_2.Syringe_Min();
        }

        void Max()
        {
            _player_S2_2.Syringe_Max();

        }


        public new void SetMax()
        {
            if (!underTR) Init();
            underPos.x = underLocalXMinDetect + 0.002f;
            underTR.localPosition = underPos; pre_X = underTR.localPosition.x;
        }
        public new void SetMin()
        {
            if (!underTR) Init();
            underPos.x = underLocalXMax - 0.001f;
            underTR.localPosition = underPos;
        }

    }
}

