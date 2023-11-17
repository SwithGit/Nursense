using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWITHFACTORY.CYJ
{

    public class Syringe : MonoBehaviour
    {
        // Start is called before the first frame update
        Player_S2 _player;
        protected Transform underTR;
        protected Vector3 underPos;
        const float underLocalXMin = 0.0075f;
        const float underLocalXMax = 0.0866f;
        const float underLocalXMinDetect = 0.01f;
        const float underLocalXMaxDetect = 0.08f;
        public void Awake()
        {
            _player = FindObjectOfType<Player_S2>();
        }

        public virtual void Start()
        {
            Init();
        }


        protected void Init()
        {
            underTR = transform.GetChild(1);
            underPos = underTR.localPosition;
            //SetMin();
            //underPos.x = (underLocalXMin + underLocalXMax) / 2;
            //underTR.localPosition = underPos;

        }
        // Update is called once per frame
        void Update()
        {
            underTR.localPosition = new Vector3(Mathf.Clamp(underTR.localPosition.x, underLocalXMin, underLocalXMax), underPos.y, underPos.z);

            if (underTR.localPosition.x <= underLocalXMinDetect)
            {
                Min();
            }
            if (underTR.localPosition.x >= underLocalXMaxDetect)
            {
                Max();
            }
        }
          void Min()
        {

            //    underPos.x = underLocalXMin;
            //    underTR.localPosition = underPos;
            _player.SyringeMin();
        }

          void Max()
        {
            //underPos.x = underLocalXMax;
            //underTR.localPosition = underPos;
            _player.SyringeMax();

        }



        public string underName()
        {
            return underTR.name;
        }

        public void SetMax()
        {
            if(!underTR) Init();
            underPos.x = underLocalXMax - 0.001f;
            underTR.localPosition = underPos;
        }
        public void SetMin()
        {
            if (!underTR) Init();
            underPos.x = underLocalXMin + 0.001f;
            underTR.localPosition = underPos;
        }

        //public void SetUnderPos(Vector3 pos)
        //{
        //    underTR.position = pos;
        //    if (underTR.localPosition.x <= underLocalXMin)
        //    {
        //        underPos.x = underLocalXMin;
        //        underTR.localPosition = underPos;
        //        _player.SyringeMin();
        //    }
        //    if (underTR.localPosition.x >= underLocalXMax)
        //    {

        //        underPos.x = underLocalXMax;
        //        underTR.localPosition = underPos;
        //        _player.SyringeMax();
        //    }
        //    underTR.localPosition = new Vector3(Mathf.Clamp(underTR.localPosition.x, underLocalXMin, underLocalXMax), underPos.y, underPos.z);
        //}
    }



}