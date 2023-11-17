using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace SWITHFACTORY.CYJ
{
    public class Player : MonoBehaviour
    {
        public enum State
        {
            Start,
            Beginning,
            Town,
            Patients
        }
        public State _state;

        private void Awake()
        {
            //_state = (State)SceneManager.GetActiveScene().buildIndex;
        }

        public enum ScenarioState
        {
            S1,
            S2

        }

        public ScenarioState _scenarioState;

        enum StartState
        {
            Cloth,
            Computer,
            Phone,
            Bag,
            Item,
            End
        }

        enum PatientState
        {
            Prologue,
            Start,
            Pat,
            Daewha,
            Question,
            End,
        }

        [Header("공통")]
        StartState _startstate;
        public int moveSpeed;
        public GameObject fadeOut;
        public GameObject notice;
        public Action noticeCallback;
        Animator anim;
        public GameObject lookCamera;
        public float rotateSpeed;
        Quaternion cameraFixRotation;
        Vector3 dir;
        float zoomdis;
        public Transform camZoomRayTr;
        public Transform camZoomTr;
        bool isMove;

        [Header("Scenario1 - 욕창 - Scene1")]
        public GameObject questionPanel;
        public GameObject chartPanel;
        public GameObject mainQuestPanel;
        public GameObject missionText;
        public GameObject cloth;
        public GameObject callPanel;
        public BoxCollider[] door;
        public GameObject[] noticeObj;
        public GameObject[] startObj;
        public List<GameObject> myItems = new List<GameObject>();
        public Toggle[] questionToggle;
        public List<bool> myAnswer;

        [Header("Scenario1 - 욕창 - Scene3")]
        public GameObject patient;
        public GameObject patientMale;
        NavMeshAgent maleAgent;
        Animator patientAnim;
        Animator patientMaleAnim;
        public GameObject question;
        public AnswerButton[] answerButtons;
        bool isStart;
        bool isPat;



        [Space]
        [Header("Chat")]
        PatientState _patientState;
        public GameObject conver;
        public string[] daehwa;
        int converNum = -1;
        int answerNum;
        public bool isDaewha;
        public Text nickname;
        public Text daehwaName;
        ChatController chatManager;

        [Header("Scenario2 - 단순도뇨(여성)")]
        public GameObject s2StartPanal;
        public enum S2State
        {
            start,
            cart,
            quiz1,
            patient
        }

        enum S2PatientState
        {
            Prologue,
            Start,
            WaterProof,
            Disinfect,
            Burlapfold,
            Ballooning1,
            Ballooning2,
            Ballooning3,
            Ballooning4,
            Ballooning5,
            Ballooning6,
            Ballooning7,
            Ballooning8,
            Ballooning9,
            //Ballooning10,
            DisinfectRope1,
            DisinfectRope2,
            DisinfectRope3,
            DisinfectRope4,
            DisinfectRope5,
            forceps1,
            forceps2,
            DisinfectPubic1,
            DisinfectPubic2,
            DisinfectPubic_off,
            DisinfectPubic_on,
            InsertCatheter1,
            InsertCatheter2,
            InsertCatheter3,
            InsertCatheter4,
            InsertCatheter5,
            InsertCatheter6,
            End,
        }

        public S2State _s2State;
        [SerializeField]
        S2PatientState _s2PatientState;


        enum S2OnCartEnum
        {
            HandGEL,
            gloves,
            Band,
            Urinbag,
            WaterproofClothfold,
            bag,
            Burlapfold
        }
        [SerializeField]
        GameObject myProp;
        Vector3 myProp_OrigPos;
        public GameObject[] P2_OnCartProps;
        [Header("Scenario2 - 단순도뇨(여성) - Patient")]
        public Animator FadeAnim;
        Action fadeCallBack;
        public GameObject patient_S2;
        public Animator patientBottomAnim;
        public GameObject C1cart;
        public GameObject C2cart_orig;
        public GameObject C2cart;

        public GameObject[] P2_OnCartProps_Patient;



        enum TargetColliderEnum
        { }


        bool isHandGel;
        bool isGloves;



        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 침대 밖 기물")]
        public GameObject gloves;
        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 침대 위 기물")]
        public GameObject bagSet;
        public GameObject waterproof_onBad;
        public GameObject Burlapfold_onBad;
        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 유치도뇨 세트 기물")]
        public Camera bag_camera;
        public GameObject rope_orig;
        public GameObject Disinfection_water;
        public GameObject rope_Ballooning;
        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 유치도뇨 세트 기물 - 벌루닝")]
        public Syringe Syringe_orig;
        public Syringe Syringe_HL;
        public Syringe Syringe_Water;
        public Syringe Syringe_Ballooning;
        public Animator rope_Ballooning_Anim;
        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 유치도뇨 세트 기물 - 윤활제")]
        public GameObject guase;
        public GameObject guase_HL;
        public GameObject guase_Gel;
        public Animator guase_spreadAnim;
        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 유치도뇨 세트 기물 - 세트 겸자")]
        public GameObject forceps;
        public GameObject forceps_HL;
        public GameObject forceps_Active;
        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 도뇨관 삽입 - 소독")]
        public Camera pubic_Cam;
        public GameObject pubic_body;
        public GameObject pubicObjs;
        public GameObject nonpubicObjs;
        public GameObject Tweezers;
        public GameObject Tweezers_pointer;
        float Tweezers_pointer_distance;
        public GameObject cottonPad;
        public GameObject cotton;
        public GameObject puspan;
        public int cottonNum;
        public GameObject[] cottons_on_puspan;
        public bool isCottonUse;
        private float swipeThreshold = 50f;
        Vector3 swipeStartPosition;
        public int swipeNum;
        [Space]
        [Header("Scenario2 - 단순도뇨(여성) - 도뇨관 삽입 - 삽입")]
        public GameObject insert_Obj;
        public Camera insert_Cam;
        public GameObject rope_insert;
        public Collider rope_insert_Colli;
        public Transform rope_peeOrig;
        public Vector3 rope_peeOrig_Pos;
        public GameObject pee_Particle;
        public GameObject pee_pad;

        // Start is called before the first frame update
        void Start()
        {

            zoomdis = -camZoomTr.transform.localPosition.z;
            isMove = true;
            switch (_scenarioState)
            {
                case ScenarioState.S1:
                    {


                        if (_state == State.Start)
                        {
                            _startstate = StartState.Cloth;
                            mainQuestPanel.SetActive(true);
                        }

                        else if (_state == State.Patients)
                        {
                            patientAnim = patient.GetComponent<Animator>();
                            patientMaleAnim = patientMale.GetComponent<Animator>();
                            maleAgent = patientMale.GetComponent<NavMeshAgent>();
                            _patientState = PatientState.Prologue;
                        }
                        break;
                    }
                case ScenarioState.S2:
                    {
                        S2Init();
                        break;
                    }


            }






            chatManager = GetComponent<ChatController>();
            anim = transform.GetChild(0).GetComponent<Animator>();
            cameraFixRotation = lookCamera.transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {

            Zoom();

            switch (_scenarioState)
            {

                case ScenarioState.S1:
                    {
                        if ((int)_state > 0 || isStart)
                        {
                            Move();
                            RayPoint();
                        }
                        switch ((int)_state)
                        {
                            case 1:
                                Beginning();
                                break;

                            case 2:
                                Town();
                                break;

                            case 3:
                                Patients();
                                break;
                        }
                        break;

                    }
                case ScenarioState.S2:
                    {
                        if (_s2State == S2State.start || _s2State == S2State.quiz1) return;
                        Move();
                        RayPoint();
                        break;
                    }

            }





        }

        #region S1

        void Beginning()
        {
            if (_startstate == StartState.Cloth)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, 1 << 0))
                    {
                        if (hit.collider.tag.Equals("Cloth"))
                        {
                            _startstate = StartState.Computer;
                            noticeObj[0].SetActive(false);
                            noticeObj[1].SetActive(true);
                            cloth.SetActive(true);
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                }
            }

            else if (_startstate == StartState.Computer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, 1 << 0))
                    {
                        if (hit.collider.tag.Equals("Computer"))
                        {
                            chartPanel.SetActive(true);
                            _startstate = StartState.Phone;
                            noticeObj[1].SetActive(false);
                            noticeObj[2].SetActive(true);
                        }
                    }
                }
            }

            else if (_startstate == StartState.Phone)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, 1 << 0))
                    {
                        if (hit.collider.tag.Equals("Phone"))
                        {
                            callPanel.SetActive(true);
                            isDaewha = false;
                            converNum++;
                            string[] test = daehwa[converNum].Split('_');
                            StartCoroutine(chatManager.NormalChat(test[0], test[1]));
                        }
                    }
                }
            }

            else if (_startstate == StartState.Bag)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, 1 << 0))
                    {
                        if (hit.collider.tag.Equals("Bag"))
                        {
                            //for (int i = 0; i < startObj.Length; i++)
                            //{
                            //    startObj[i].gameObject.SetActive(true);
                            //}
                            startObj[0].SetActive(true);
                            missionText.transform.parent.gameObject.SetActive(false);
                            noticeObj[3].SetActive(false);
                            _startstate = StartState.Item;
                        }
                    }
                }
            }

            else if (_startstate == StartState.Item)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, 1 << 0))
                    {
                        if (hit.collider.tag.Equals("Item"))
                        {
                            questionPanel.SetActive(true);
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                }
                //if (Input.GetMouseButtonDown(0))
                //{
                //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //    RaycastHit hit;
                //    if (Physics.Raycast(ray , out hit , 100 , 1 << 0))
                //    {
                //        if (hit.collider.tag.Equals("Item"))
                //        {
                //            myItems.Add(hit.collider.gameObject);
                //        }
                //        if (myItems.Count == 3)
                //        {
                //            for (int i = 0; i < myItems.Count; i++)
                //            {
                //                if (myItems[i].GetComponent<Items>().answerNum < 3)
                //                {
                //                    continue;
                //                }
                //                answerNum++;
                //            }
                //            if (answerNum == 3)
                //            {
                //                _startstate = StartState.End;
                //                for (int i = 0; i < door.Length; i++)
                //                {
                //                    door[i].enabled = true;
                //                }
                //                SetNotice("정답입니다. 밖으로 나가\n문정례 할머니 집으로 가주세요.");
                //            }
                //            else
                //            {
                //                SetNotice("틀렸습니다. 다시 풀어보세요.");
                //                myItems.Clear();
                //                answerNum = 0;
                //            }
                //        }
                //    }
                //}
                //


            }
        }

        void Town()
        {

        }

        void Patients()
        {
            if (_patientState == PatientState.Prologue)
            {
                float dis = Vector3.Distance(transform.position, patientMale.transform.position);
                if (dis < 2)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isDaewha = false;
                        _patientState = PatientState.Start;
                        conver.SetActive(true);
                        converNum++;
                        string[] test = daehwa[converNum].Split('_');
                        StartCoroutine(chatManager.NormalChat(test[0], test[1]));
                    }
                }
            }

            else if (_patientState == PatientState.Start)
            {
                if (!isStart)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (converNum == 1)
                        {
                            conver.SetActive(false);
                            chatManager.audio2.Stop();
                            maleAgent.destination = patient.transform.position;
                            patientMaleAnim.SetInteger("Move", 1);
                            isStart = true;
                            return;
                        }

                        if (isDaewha)
                        {
                            isDaewha = false;
                            conver.SetActive(true);
                            converNum++;
                            chatManager.waitTime = 0.1f;
                            string[] test = daehwa[converNum].Split('_');
                            StartCoroutine(chatManager.NormalChat(test[0], test[1]));
                        }

                        else
                        {
                            chatManager.waitTime = 0;
                        }
                    }
                }

                else
                {
                    float disMale = Vector3.Distance(patientMale.transform.position, patient.transform.position);
                    if (disMale < 3)
                    {
                        patientMaleAnim.SetInteger("Move", 0);
                        anim.SetInteger("Move", 0);
                        isStart = false;
                        _patientState = PatientState.Pat;
                    }

                    else
                    {
                        anim.SetInteger("Move", 1);
                        GetComponent<NavMeshAgent>().destination = patientMale.transform.position;
                    }
                }
            }

            else if (_patientState == PatientState.Pat)
            {
                float dis = Vector3.Distance(transform.position, patient.transform.position);
                if (dis < 4)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isDaewha = false;
                        _patientState = PatientState.Daewha;
                        conver.SetActive(true);
                        converNum++;
                        string[] test = daehwa[converNum].Split('_');
                        StartCoroutine(chatManager.NormalChat(test[0], test[1]));
                    }
                }
            }

            else if (_patientState == PatientState.Daewha)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (converNum == 20)
                    {
                        conver.SetActive(false);
                        converNum = 0;
                        _patientState = PatientState.Question;
                        chatManager.audio2.Stop();
                        question.SetActive(true);
                        return;
                    }

                    if (isDaewha)
                    {
                        isDaewha = false;
                        conver.SetActive(true);
                        converNum++;
                        chatManager.waitTime = 0.1f;
                        string[] test = daehwa[converNum].Split('_');
                        StartCoroutine(chatManager.NormalChat(test[0], test[1]));
                        if (converNum == 7)
                        {
                            patientAnim.SetTrigger("Left");
                        }
                    }

                    else
                    {
                        chatManager.waitTime = 0;
                    }
                }
            }

            else if (_patientState == PatientState.Question)
            {

            }
        }

        public void SetNotice(string t, Action callback = null)
        {
            notice.SetActive(true);
            notice.transform.GetChild(0).GetComponent<Text>().text = t;
            noticeCallback = callback;
        }
        public void OnClickNoticeOK()
        {
            notice.SetActive(false);
            if (noticeCallback != null) noticeCallback.Invoke();
        }
        public void MainCloseButtonClick()
        {
            mainQuestPanel.SetActive(false);
            _state = State.Beginning;
        }
        public void DaeHwaNextButtonClick()
        {
            if (converNum == 5)
            {
                _startstate = StartState.Bag;
                converNum = 0;
                callPanel.SetActive(false);
                noticeObj[2].SetActive(false);
                noticeObj[3].SetActive(true);
                missionText.transform.parent.gameObject.SetActive(true);
                return;
            }
            if (isDaewha)
            {
                isDaewha = false;
                converNum++;
                chatManager.waitTime = 0.1f;
                string[] test = daehwa[converNum].Split('_');
                if (converNum != 4)
                    StartCoroutine(chatManager.NormalChat(test[0], test[1]));
                else
                    StartCoroutine(chatManager.NormalChat2(test[0], test[1]));
            }

            else
            {
                chatManager.waitTime = 0;
            }
        }
        public void QuestionButtonClick(bool tf)
        {
            myAnswer.Add(tf);
        }
        public void ApplyButtonClick()
        {
            for (int i = 0; i < myAnswer.Count; i++)
            {
                if (!myAnswer[i])
                    break;
                answerNum++;
            }
            if (answerNum == 5)
            {
                _startstate = StartState.End;
                for (int i = 0; i < door.Length; i++)
                {
                    door[i].enabled = true;
                }
                SetNotice("정답입니다. 밖으로 나가\n이영숙 할머니 집으로 가주세요.");
                questionPanel.SetActive(false);
                answerNum = 0;
                myAnswer.Clear();
            }
            else
            {
                SetNotice("틀렸습니다. 다시 풀어보세요.");
                for (int i = 0; i < questionToggle.Length; i++)
                {
                    questionToggle[i].isOn = false;
                }
                myAnswer.Clear();
                answerNum = 0;
            }
        }
        #endregion
        void Move()
        {
            if (!isMove) return;
            dir = lookCamera.transform.forward * Input.GetAxis("Vertical") + lookCamera.transform.right * Input.GetAxis("Horizontal");

            if (dir.sqrMagnitude != 0)
            {
                transform.position += dir * moveSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0f)), 0.1f);
                anim.SetInteger("Move", 1);
                moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 5 : 2;
            }

            else
                anim.SetInteger("Move", 0);

            lookCamera.transform.rotation = cameraFixRotation;

            if (Input.GetMouseButton(1)) CameraRotate();
        }
        void CameraRotate()
        {
            Vector3 rot = lookCamera.transform.rotation.eulerAngles; // 현재 카메라의 각도를 Vector3로 반환
            rot.y += Input.GetAxis("Mouse X") * rotateSpeed; // 마우스 X 위치 * 회전 스피드
            rot.x += -1 * Input.GetAxis("Mouse Y") * rotateSpeed; // 마우스 Y 위치 * 회전 스피드
            rot.z = 0;

            //너무 높게 보거나 너무 낮게 보면 오류가 생겨 그 점 방지
            if (rot.x > 180)
                rot.x -= 360f;
            //print(rot.x);
            if (rot.x > 85)
                rot.x = 85;
            if (rot.x < -85)
                rot.x = -85;

            lookCamera.transform.rotation = Quaternion.Slerp(lookCamera.transform.rotation, Quaternion.Euler(rot), 2f); // 자연스럽게 회전  
            cameraFixRotation = lookCamera.transform.rotation;
        }
        void Zoom()
        {
            Ray ray = new Ray(camZoomRayTr.position, -camZoomRayTr.forward * zoomdis);
            Debug.DrawRay(ray.origin, ray.direction * 2);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, zoomdis))
            {
                camZoomTr.localPosition = new Vector3(0, 0, -Vector3.Distance(camZoomRayTr.position, hit.point) + 0.01f);
            }
            else
            {
                camZoomTr.localPosition = new Vector3(0, 0, -zoomdis);
            }



        }
        void RayPoint()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (_scenarioState == ScenarioState.S1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, 1 << 0))
                    {
                        if (hit.collider.tag.Equals("Open") && hit.collider.gameObject.GetComponent<Animator>())
                        {
                            Animator anim = hit.collider.gameObject.GetComponent<Animator>();

                            anim.SetTrigger("Open");
                        }
                    }
                }
            }
            else if (_scenarioState == ScenarioState.S2)
            {
                if (_s2State == S2State.cart)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 100, 1 << 0))
                        {
                            if (hit.collider.tag.Equals("RaycastTarget"))
                            {
                                myProp_OrigPos = hit.transform.position;
                                myProp = hit.collider.gameObject;
                            }
                        }
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        if (myProp)
                        {
                            Vector3 target = Input.mousePosition;
                            target.z = 3f;
                            myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
                        }
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        if (!myProp) return;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit[] hits = Physics.RaycastAll(ray);
                        foreach (var hit in hits)
                        {
                            if (hit.collider.CompareTag("Cart"))
                            {
                                print("Cart!");
                                try
                                {
                                    var e = (S2OnCartEnum)Enum.Parse(typeof(S2OnCartEnum), myProp.name);
                                    P2_OnCartProps[(int)e].SetActive(true);


                                }
                                catch (Exception e)
                                {
                                    print(e);
                                    myProp.transform.position = myProp_OrigPos;
                                    myProp_OrigPos = Vector3.zero;
                                    myProp = null;
                                    return;
                                }




                                myProp.transform.position = myProp_OrigPos;
                                myProp.SetActive(false);
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                                CheckShelfObj();
                                return;
                            }
                        }
                        print("No Cart!");
                        myProp.transform.position = myProp_OrigPos;
                        myProp_OrigPos = Vector3.zero;
                        myProp = null;
                    }
                    else
                    {
                        if (myProp)
                        {

                            print("No!!!");
                            myProp.transform.position = myProp_OrigPos;
                            myProp_OrigPos = Vector3.zero;
                            myProp = null;
                        }

                    }
                }
                else if (_s2State == S2State.patient)
                {
                    if (_s2PatientState == S2PatientState.Start)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hits[i].collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name != "bag") return;
                                    myProp_OrigPos = hit.transform.position;
                                    myProp = hit.collider.gameObject;
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = 2f;
                                myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            if (!myProp) return;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.CompareTag("Target"))
                                {
                                    if (hit.collider.name != "01") return;
                                    S2_Patient();
                                    return;
                                }
                            }

                            myProp.transform.position = myProp_OrigPos;
                            myProp_OrigPos = Vector3.zero;
                            myProp = null;
                        }
                        else
                        {
                            if (myProp)
                            {

                                print("No!!!");
                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }
                    }
                    else if (_s2PatientState == S2PatientState.WaterProof)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name != "WaterproofCloth01") return;
                                    myProp_OrigPos = hit.transform.position;
                                    myProp = hit.collider.gameObject;
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = 3f;
                                myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            if (!myProp) return;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.CompareTag("Target"))
                                {
                                    if (hit.collider.name != "02") return;
                                    S2_Patient();
                                    return;
                                }
                            }

                            myProp.transform.position = myProp_OrigPos;
                            myProp_OrigPos = Vector3.zero;
                            myProp = null;
                        }
                        else
                        {
                            if (myProp)
                            {

                                print("No!!!");
                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }
                    }
                    else if (_s2PatientState == S2PatientState.Disinfect)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("Disinfect"))
                                {
                                    if (hit.collider.name == "HandGEL" && !isHandGel)
                                    {
                                        isHandGel = true;
                                        hit.collider.GetComponent<Animator>().SetTrigger("trigger");
                                        SetNotice("손소독이 완료되었습니다.", S2_Patient);
                                        return;
                                    }
                                    if (hit.collider.name == "gloves" && !isGloves)
                                    {
                                        isGloves = true;
                                        gloves.SetActive(true);
                                        hit.collider.gameObject.SetActive(false);
                                        SetNotice("멸균장갑이 착용 되었습니다.", S2_Patient);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.Burlapfold)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name != "Burlapfold") return;
                                    myProp_OrigPos = hit.transform.position;
                                    myProp = hit.collider.gameObject;
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = 3f;
                                myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            if (!myProp) return;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.CompareTag("Target"))
                                {
                                    if (hit.collider.name != "02") return;
                                    myProp.transform.position = myProp_OrigPos;
                                    myProp.SetActive(false);
                                    myProp_OrigPos = Vector3.zero;
                                    myProp = null;
                                    S2_Patient();
                                    return;
                                }
                            }
                            myProp.transform.position = myProp_OrigPos;
                            myProp_OrigPos = Vector3.zero;
                            myProp = null;
                        }
                        else
                        {
                            if (myProp)
                            {

                                print("No!!!");
                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }
                    }
                    else if (_s2PatientState == S2PatientState.Ballooning2)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    //print($"hit.collider.name : {hit.collider.name} , Syringe_orig.name : {Syringe_orig.name} ");
                                    if (hit.collider.name == Syringe_orig.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.Ballooning3)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == Disinfection_water.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.Ballooning4)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == Syringe_Water.underName())
                                    {
                                        myProp = hit.transform.gameObject;
                                        //myProp_OrigPos = hit.transform.position;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = Vector3.Distance(myProp.transform.position, bag_camera.transform.position);
                                //print(target.z);
                                //myProp.transform.position = bag_camera.ScreenToWorldPoint(target);
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, bag_camera.ScreenToWorldPoint(target), 0.5f);
                            }
                        }
                        else
                        {

                            myProp = null;
                        }

                    }
                    else if (_s2PatientState == S2PatientState.Ballooning5)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == rope_orig.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }

                    }
                    else if (_s2PatientState == S2PatientState.Ballooning6)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == "ballooningPos")
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.Ballooning7 || _s2PatientState == S2PatientState.Ballooning9)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == Syringe_Ballooning.underName())
                                    {
                                        myProp = hit.transform.gameObject;
                                        //myProp_OrigPos = hit.transform.position;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = Vector3.Distance(myProp.transform.position, bag_camera.transform.position);
                                //print(target.z);
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, bag_camera.ScreenToWorldPoint(target), 0.5f);
                            }
                        }
                        else
                        {

                            myProp = null;
                        }

                    }
                    else if (_s2PatientState == S2PatientState.DisinfectRope2)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == guase.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.DisinfectRope3)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == "Gel")
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.DisinfectRope4)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == "gauzePos")
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.forceps1)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == forceps.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.forceps2)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = bag_camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == "forcepsPos")
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.DisinfectPubic2)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = pubic_Cam.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == Tweezers.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.DisinfectPubic_off || _s2PatientState == S2PatientState.DisinfectPubic_on)
                    {
                        Vector3 target = Input.mousePosition;

                        if(Input.GetMouseButtonDown(0))
                        {
                            Ray ray = pubic_Cam.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            switch (_s2PatientState)
                            {
                                case S2PatientState.DisinfectPubic_off:
                                    {
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.tag.Equals("RaycastTarget"))
                                            {
                                                if (hit.collider.name == cottonPad.name)
                                                {
                                                    cotton.SetActive(true);
                                                    isCottonUse = false;
                                                    _s2PatientState = S2PatientState.DisinfectPubic_on;
                                                }
                                                else if (hit.collider.name == pubic_body.name)
                                                {
                                                    SetNotice("소독솜을 집고 소독을 진행해주세요!");
                                                }
                                            }
                                        }


                                        break;
                                    }
                                case S2PatientState.DisinfectPubic_on:
                                    {
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.tag.Equals("RaycastTarget"))
                                            {
                                                if (hit.collider.name == puspan.name)
                                                {
                                                    cotton.SetActive(false);
                                                    
                                                    try
                                                    {
                                                        cottons_on_puspan[cottonNum].SetActive(true);
                                                        cottonNum++;
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    _s2PatientState = S2PatientState.DisinfectPubic_off;
                                                }
                                                else if (hit.collider.name == pubic_body.name)
                                                {
                                                    if(isCottonUse)
                                                    {
                                                        SetNotice("한번이라도 사용된 소독솜은 버리고 진행해주세요!");
                                                        return;
                                                    }
                                                    swipeStartPosition = Input.mousePosition;
                                                }
                                            }
                                        }
                                        break;
                                    }


                            }

                        }
                        if (Input.GetMouseButton(0))
                        {
                            if (swipeStartPosition == Vector3.zero) return;
                            Ray ray = pubic_Cam.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            if(hits.Length != 0)
                            {
                                for (int i = 0; i < hits.Length; i++)
                                {
                                    RaycastHit hit = hits[i];
                                    if (hit.collider.tag.Equals("RaycastTarget"))
                                    {
                                        target.z = Vector3.Distance(pubic_Cam.transform.position, hit.point) - 0.05f;

                                        if(hit.collider.name == pubic_body.name)
                                        {

                                            isCottonUse = true;
                                            if (Mathf.Abs(Input.mousePosition.y - swipeStartPosition.y) > swipeThreshold)
                                            {
                                                if (Input.mousePosition.y > swipeStartPosition.y)
                                                {
                                                    SetNotice("소독은 위에서 아래로 진행해주세요!");
                                                    swipeStartPosition = Input.mousePosition;
                                                    return;
                                                }
                                                else
                                                {
                                                    swipeStartPosition = Input.mousePosition;
                                                    swipeNum++;
                                                }
                                                //Mathf.Abs(Input.mousePosition.y - swipeStartPosition.y)
                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                target.z = Tweezers_pointer_distance;
                            }
                        }
                        else
                        {
                            target.z = Tweezers_pointer_distance;
                        }

                        //print(target.z);
                        Tweezers_pointer.transform.position = pubic_Cam.ScreenToWorldPoint(target);

                        if(Input.GetMouseButtonUp(0))
                        {
                            swipeStartPosition = Vector3.zero;
                        }

                        if(swipeNum > 20)
                        {
                            _s2PatientState = S2PatientState.InsertCatheter1;
                            S2_Patient();
                        }

                        //if (Input.GetMouseButton(0))
                        //{
                        //    Ray ray = pubic_Cam.ScreenPointToRay(Input.mousePosition);
                        //    RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                        //    for (int i = 0; i < hits.Length; i++)
                        //    {
                        //        RaycastHit hit = hits[i];
                        //        if (hit.collider.tag.Equals("RaycastTarget"))
                        //        {
                        //            Vector3 target = Input.mousePosition;
                        //            target.z = Vector3.Distance(pubic_Cam.transform.position, hit.point) - 0.05f;
                        //            //print(target.z);Tweezers_pointer.transform.position = pubic_Cam.ScreenToWorldPoint(target);

                        //            //if (hit.collider.name == Tweezers.name)
                        //            //{
                        //            //    S2_Patient();
                        //            //}
                        //        }
                        //    }
                        //}
                        //else
                        //{

                        //}

                        //Ray ray = pubic_Cam.ScreenPointToRay(Input.mousePosition);
                        //RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                        //for (int i = 0; i < hits.Length; i++)
                        //{
                        //    RaycastHit hit = hits[i];
                        //    if (hit.collider.tag.Equals("RaycastTarget"))
                        //    {
                        //        if (hit.collider.name == Tweezers.name)
                        //        {
                        //            S2_Patient();
                        //        }
                        //    }
                        //}


                    }
                    else if(_s2PatientState == S2PatientState.InsertCatheter3)
                    {
                        rope_peeOrig.position = rope_peeOrig_Pos;
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = insert_Cam.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == rope_insert_Colli.name)
                                    {
                                        myProp = hit.transform.parent.gameObject;
                                        //myProp_OrigPos = hit.transform.position;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = Vector3.Distance(myProp.transform.position, insert_Cam.transform.position);
                                //print(target.z);
                                //myProp.transform.position = bag_camera.ScreenToWorldPoint(target);
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, insert_Cam.ScreenToWorldPoint(target), Time.deltaTime);
                            }
                        }
                        else
                        {

                            myProp = null;
                        }
                    }
                }
            }
        }
        #region ETC
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Door")
            {
                fadeOut.SetActive(true);
                conver.SetActive(false);
                Invoke("Fade", 2);
            }
            if (other.tag == "PatientRoom")
            {
                if (_s2State == S2State.patient && _s2PatientState == S2PatientState.Prologue) S2_Patient();
            }
        }
        void Fade()
        {
            fadeOut.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void SetDaeHwa(string charName, string daehwaText)
        {
            nickname.text = charName;
            daehwaName.text = daehwaText;
        }

        #endregion
        #region S2
        void S2Init()
        {

            s2StartPanal.SetActive(true);
            _s2State = S2State.start;
            _s2PatientState = S2PatientState.Prologue;
            CartInit();
        }
        void CartInit()
        {
            foreach (GameObject obj in P2_OnCartProps)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in P2_OnCartProps_Patient)
            {
                obj.SetActive(true);
            }


            bag_camera.gameObject.SetActive(false);
            pubic_Cam.gameObject.SetActive(false);


            C1cart.SetActive(true);
            C2cart_orig.SetActive(true);
            C2cart.SetActive(false);
            bagSet.SetActive(false);
            waterproof_onBad.SetActive(false);
            Burlapfold_onBad.SetActive(false);

        }
        public void OnClickS2StartButton()
        {
            s2StartPanal.SetActive(false);
            _s2State = S2State.cart;
        }
        void CheckShelfObj()
        {
            bool b = true;

            foreach (GameObject obj in P2_OnCartProps)
            {
                b = b && obj.activeSelf;
            }
            if (b)
            {
                print("Complete!");
                _s2State = S2State.quiz1;
                question.SetActive(true);
                //Complete!
            }
        }
        public void S2_AfterQuiz1()
        {
            question.SetActive(false);
            SetNotice("정답입니다!\n이제 실제 유치도뇨를 실습해봅시다.\n진료소장실을 나가 물리치료실로 이동해주세요.");
            _s2State = S2State.patient;
            C1cart.SetActive(false);
            C2cart_orig.SetActive(false);
            C2cart.SetActive(true);
        }
        void S2_Patient()
        {
            if (_s2State != S2State.patient) return;
            switch (_s2PatientState)
            {
                case S2PatientState.Prologue:
                    {
                        SetNotice("현재 환자에게 본인 소개와 환자 본인확인 이후 커튼을 치고 배횡와위를 취한 상태입니다.\n먼저 세트를 세팅한 이후 유치도뇨 과정을 이어나가도록 하자.");
                        _s2PatientState = S2PatientState.Start;
                        break;
                    }
                case S2PatientState.Start:
                    {
                        ///var e = (S2OnCartEnum)Enum.Parse(typeof(S2OnCartEnum), myProp.name);
                        P2_OnCartProps_Patient[(int)S2OnCartEnum.bag].SetActive(false);
                        bagSet.SetActive(true);
                        SetNotice("이제 카트에서 드래그 앤 드랍을 통하여\n환자의 골반 아래에 방수포를 설치해주세요.");
                        _s2PatientState = S2PatientState.WaterProof;
                        break;
                    }
                case S2PatientState.WaterProof:
                    {
                        waterproof_onBad.SetActive(true);
                        P2_OnCartProps_Patient[(int)S2OnCartEnum.WaterproofClothfold].SetActive(false);
                        SetNotice("손소독을 시행하고 멸균 장갑을 착용하도록 하자.");
                        _s2PatientState = S2PatientState.Disinfect;
                        break;
                    }
                case S2PatientState.Disinfect:
                    {
                        if (isGloves && isHandGel)
                        {
                            SetNotice("이제 환자분에게 소공포를 덮어주세요.");
                            _s2PatientState = S2PatientState.Burlapfold;
                        }

                        //waterproof_onBad.SetActive(true);
                        //P2_OnCartProps_Patient[(int)S2OnCartEnum.WaterproofClothfold].SetActive(false);
                        //SetNotice("손소독을 시행하고 멸균 장갑을 착용하도록 하자.");
                        //_s2PatientState = S2PatientState.Disinfect;
                        break;
                    }
                case S2PatientState.Burlapfold:
                    {
                        Burlapfold_onBad.SetActive(true);
                        _s2PatientState = S2PatientState.Ballooning1;
                        FadeOut(S2_Patient);
                        break;
                    }
                case S2PatientState.Ballooning1:
                    {
                        isMove = false;
                        bag_camera.gameObject.SetActive(true);
                        SetNotice("이제 유치도뇨관의 벌루닝을 체크해봅시다.");
                        _s2PatientState = S2PatientState.Ballooning2;
                        break;
                    }
                case S2PatientState.Ballooning2:
                    {
                        Syringe_orig.gameObject.SetActive(false);
                        Syringe_HL.gameObject.SetActive(true);
                        _s2PatientState = S2PatientState.Ballooning3;
                        break;
                    }
                case S2PatientState.Ballooning3:
                    {
                        Syringe_HL.gameObject.SetActive(false);
                        Syringe_Water.gameObject.SetActive(true);
                        _s2PatientState = S2PatientState.Ballooning4;
                        break;
                    }
                case S2PatientState.Ballooning4:
                    {
                        Syringe_HL.SetMax();
                        Syringe_Water.gameObject.SetActive(false);
                        Syringe_HL.gameObject.SetActive(true);
                        //_s2PatientState = S2PatientState.Ballooning5;
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.Ballooning5:
                    {
                        rope_orig.SetActive(false);
                        rope_Ballooning.SetActive(true);
                        //Syringe_HL.gameObject.SetActive(false);
                        //Syringe_Ballooning.gameObject.SetActive(true);
                        //_s2PatientState = S2PatientState.Ballooning6;
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.Ballooning6:
                    {
                        Syringe_HL.gameObject.SetActive(false);
                        Syringe_Ballooning.gameObject.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectRope1:
                    {
                        SetNotice("도뇨관 준비를 마친 이후 음부를 소독하고 도뇨관을 삽입하세요");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectRope2:
                    {
                        //거즈 클릭
                        //SetNotice("도뇨관 준비를 마친 이후 음부를 소독하고 도뇨관을 삽입하세요", S2_Patient);
                        guase.SetActive(false);
                        guase_HL.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectRope3:
                    {
                        //젤 클릭                   
                        guase_HL.SetActive(false);
                        guase_Gel.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectRope4:
                    {
                        //도뇨관 끝 클릭
                        guase_Gel.SetActive(false);
                        guase_spreadAnim.gameObject.SetActive(true);
                        guase_spreadAnim.SetTrigger("trigger");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.forceps1:
                    {
                        //겸자 클릭
                        forceps.SetActive(false);
                        forceps_HL.gameObject.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.forceps2:
                    {
                        //겸자부분 클릭
                        forceps_HL.SetActive(false);
                        forceps_Active.SetActive(true);
                        NextPatientStep();
                        FadeOut(S2_Patient);
                        break;
                    }
                case S2PatientState.DisinfectPubic1:
                    {
                        bag_camera.gameObject.SetActive(false);
                        pubic_Cam.gameObject.SetActive(true);
                        pubicObjs.SetActive(true);
                        nonpubicObjs.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectPubic2:
                    {
                        //핀셋클릭
                        Tweezers.gameObject.SetActive(false);
                        Tweezers_pointer.gameObject.SetActive(true);
                        //Tweezers_pointer_distance = Vector3.Distance(Tweezers_pointer.transform.position, pubic_Cam.transform.position);
                        Tweezers_pointer_distance = 0.2f;
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter1:
                    {
                        //카테터 삽입 시작
                        NextPatientStep();
                        FadeOut(S2_Patient);
                        break;
                    }
                case S2PatientState.InsertCatheter2:
                    {
                        pubic_Cam.gameObject.SetActive(false);
                        pubicObjs.gameObject.SetActive(false);
                        insert_Obj.SetActive(true);
                        insert_Cam.gameObject.SetActive(true);
                        rope_peeOrig_Pos = rope_peeOrig.position;
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter3:
                    {
                        SetNotice("8cm 도뇨관 삽입이 완료되었습니다.\n이제 겸자를 풀고 소변이 나오는지를 확인해보겠습니다.\n(유치도뇨 중간결과본 완료되었습니다.)");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter4:
                    {
                        FadeOut(S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter5:
                    {
                        FadeOut(S2_Patient);
                        NextPatientStep();
                        break;
                    }


            }


        }

        public void SyringeMax()
        {
            if (_s2PatientState == S2PatientState.Ballooning4)
            {
                S2_Patient();
            }
            if (_s2PatientState == S2PatientState.Ballooning9)
            {
                rope_Ballooning_Anim.SetTrigger("min");
                NextPatientStep();
            }
        }

        public void SyringeMin()
        {
            if (_s2PatientState == S2PatientState.Ballooning7)
            {
                rope_Ballooning_Anim.SetTrigger("max");
                NextPatientStep();
            }

        }

        public void InsertC1()
        {
            S2_Patient();
        }

        public void InsertC2()
        {

        }


        public void AnimEnd()
        {
            //if (_s2PatientState == S2PatientState.Ballooning9)
            //{
            //    _s2PatientState = S2PatientState.Ballooning10;

            //}
            if (_s2PatientState == S2PatientState.DisinfectRope1)
            {
                S2_Patient();
                return;

            }
            NextPatientStep();
        }

        void NextPatientStep()
        {
            int i = (int)_s2PatientState;
            i++;
            _s2PatientState = (S2PatientState)i;
        }

        void FadeOut(Action callback = null)
        {
            fadeCallBack = callback;
            FadeAnim.gameObject.SetActive(true);
            FadeAnim.SetTrigger("trigger");

        }

        public void FadeOutEnd()
        {
            if (fadeCallBack != null) fadeCallBack.Invoke();
            fadeCallBack = null;
            FadeAnim.gameObject.SetActive(false);
        }

        #endregion
    }


}
