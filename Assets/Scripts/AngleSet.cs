using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SWITHFACTORY.CYJ
{

    public class AngleSet : MonoBehaviour
    {
        bool angleSet;
        Transform childTr;
        Vector3 local;
        const float outPosX = -0.12f;
        private void Start()
        {
            angleSet = true;
            childTr = transform.GetChild(0);
            local = childTr.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (angleSet) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.Clamp(transform.localEulerAngles.y, 210f, 330f), transform.localEulerAngles.z);
            else
            {
                if (childTr.localPosition.x < outPosX)
                {
                    FindObjectOfType<Player_S2_2>().C3_Patient();
                }
                else
                {
                    childTr.localPosition = new Vector3(Mathf.Clamp(childTr.localPosition.x, outPosX - 0.01f, -0.06783997f), local.y, local.z);
                }

            }
        }


        public void SetMove()
        {
            angleSet = false;
        }
        public int GetAngle()
        {
            return (int)(transform.localEulerAngles.y - 180f);
        }

        public void SetAngle(float delta)
        {
            Vector3 vector = transform.localEulerAngles;
            vector.y += delta;
            vector.y = Mathf.Clamp(vector.y, 210f, 330f);
            transform.localEulerAngles = vector;
        }



        public bool IsAngle()
        {
            //if (transform.localEulerAngles.x < 270f + angleHold || transform.localEulerAngles.x > 270f - angleHold)
            if (GetAngle() == 90)
            {
                Vector3 vector = transform.localEulerAngles;
                vector.y = 270f;
                transform.localEulerAngles = vector;
                return true;
            }
            else return false;
        }
    }


}