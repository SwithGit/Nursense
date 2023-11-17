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
    public class Player_S2 : MonoBehaviour
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

        [Header("����")]
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

        [Header("Scenario1 - ��â - Scene1")]
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

        [Header("Scenario1 - ��â - Scene3")]
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

        [Space]
        [Space]
        [Space]
        [Space]
        [Header("Scenario2 - �ܼ�����(����)")]
        public GameObject s2StartPanal;
        public bool isSetBlink;
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
            Disinfect_1st,
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
            InsertCatheter7,
            InsertCatheter8,
            InsertCatheter9,
            InsertCatheter10,
            InsertCatheter11,
            InsertCatheter12,
            InsertCatheter13,
            InsertCatheter14,
            CleanUp1,
            CleanUp2,
            CleanUp3,
            CleanUp4,
            CleanUp5,
            CleanUp6,
            //CleanUp7,
            //CleanUp8,
            CleanUp9,
            CleanUp10,
            CleanUp11,
            CleanUp12,
            CleanUp13,
            CleanUp14,
            //CleanUp15,
            End,
        }

        public S2State _s2State;
        [SerializeField]
        S2PatientState _s2PatientState;


        enum DisinfectPubicState
        {
            �ܺ�,
            ������,
            ������,
            �䵵
        }
        [SerializeField]
        DisinfectPubicState _disinfectPubicState;


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
        public Items_Shelves P2_ShelvesProp;
        public Items_Shelves[] P2_ShelvesProps;
        public GameObject[] P2_OnCartProps;
        [Header("Scenario2 - �ܼ�����(����) - Patient")]
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

        [SerializeField]
        bool isHandGel;
        [SerializeField]
        bool isGloves;



        [Space]
        [Header("Scenario2 - �ܼ�����(����) - ħ�� �� �⹰")]
        public GameObject gloves;
        [Space]
        [Header("Scenario2 - �ܼ�����(����) - ħ�� �� �⹰")]
        public GameObject bagSet;
        public GameObject waterproof_onBad;
        public GameObject Burlapfold_onBad;
        [Space]
        [Header("Scenario2 - �ܼ�����(����) - ��ġ���� ��Ʈ �⹰")]
        public Camera bag_camera;
        public GameObject rope_orig;
        public GameObject Disinfection_water;
        public GameObject rope_Ballooning;
        [Space]
        [Header("Scenario2 - �ܼ�����(����) - ��ġ���� ��Ʈ �⹰ - �����")]
        public GameObject ballooningOBJ;
        public Syringe Syringe_orig;
        public Syringe Syringe_HL;
        public Syringe Syringe_Water;
        public Syringe Syringe_Ballooning;
        public Animator rope_Ballooning_Anim;
        [Space]
        [Header("Scenario2 - �ܼ�����(����) - ��ġ���� ��Ʈ �⹰ - ��Ȱ��")]
        public GameObject guase;
        public GameObject guase_HL;
        public GameObject guase_Gel;
        public Animator guase_spreadAnim;
        [Space]
        [Header("Scenario2 - �ܼ�����(����) - ��ġ���� ��Ʈ �⹰ - ��Ʈ ����")]
        public GameObject forceps;
        public GameObject forceps_HL;
        public GameObject forceps_Active;
        [Space]
        [Header("Scenario2 - �ܼ�����(����) - ������ ���� - �ҵ�")]
        public Camera pubic_Cam;
        public Camera pubic_Cam_overlayPubic;
        public GameObject pubic_parentTR;
        public GameObject pubicObjs;
        public GameObject nonpubicObjs;
        public GameObject Tweezers;
        public GameObject Tweezers_pointer;
        public float Tweezers_pointer_distance;
        public List<Collider> DisinfectPubicCollis;
        public List<string> completeSpotNameList;
        public string nowSpotName;
        //public int nowSpotIndex;
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
        [Header("Scenario2 - �ܼ�����(����) - ������ ���� - ����")]
        public GameObject insert_Obj;
        public Camera insert_Cam;
        public GameObject rope_insert;
        public Collider rope_insert_Colli;
        public Transform rope_peeOrig;
        public Vector3 rope_peeOrig_Pos;
        public Camera cam_Pee;
        public GameObject pee_Particle;
        public GameObject pee_pad;
        public GameObject pee_forceps;
        public GameObject pee_forceps_inactive;
        public Syringe Syringe_insert;
        Vector3 mouseStartPosition;
        public GameObject insertText;
        [Space]
        [Header("Scenario2 - �ܼ�����(����) - �ֺ�����")]
        public GameObject cleanUp_Obj;
        public GameObject rope_inserted;
        public GameObject Burlapfold_onBody;
        public GameObject urine;
        public GameObject urine_forceps;
        public GameObject urine_forceps1;
        public GameObject urine_forceps2;
        public Collider urine_joint_colli;
        public GameObject urine_joint_rope;
        public GameObject forceps_joint_urine;
        public GameObject sticker_onBody;
        public GameObject rope_Sticked;
        public GameObject SetBag_onbad;
        public Collider cart_collider;

        public GameObject urine_forceps_final;
        public Collider urine_bad_colli;
        public GameObject urine_bad;




        // Start is called before the first frame update
        void Start()
        {

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
                        zoomdis = -camZoomTr.transform.localPosition.z;
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
                        Zoom();
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
                            missionText.SetActive(false);
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
                //                SetNotice("�����Դϴ�. ������ ����\n������ �ҸӴ� ������ ���ּ���.");
                //            }
                //            else
                //            {
                //                SetNotice("Ʋ�Ƚ��ϴ�. �ٽ� Ǯ�����.");
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
                missionText.SetActive(true);
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
                SetNotice("�����Դϴ�. ������ ����\n������ �ҸӴ� ������ ���ּ���.");
                questionPanel.SetActive(false);
                answerNum = 0;
                myAnswer.Clear();
            }
            else
            {
                SetNotice("Ʋ�Ƚ��ϴ�. �ٽ� Ǯ�����.");
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
            dir.y = 0;
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
            Vector3 rot = lookCamera.transform.rotation.eulerAngles; // ���� ī�޶��� ������ Vector3�� ��ȯ
            rot.y += Input.GetAxis("Mouse X") * rotateSpeed; // ���콺 X ��ġ * ȸ�� ���ǵ�
            rot.x += -1 * Input.GetAxis("Mouse Y") * rotateSpeed; // ���콺 Y ��ġ * ȸ�� ���ǵ�
            rot.z = 0;

            //�ʹ� ���� ���ų� �ʹ� ���� ���� ������ ���� �� �� ����
            if (rot.x > 180)
                rot.x -= 360f;
            //print(rot.x);
            if (rot.x > 85)
                rot.x = 85;
            if (rot.x < -85)
                rot.x = -85;

            lookCamera.transform.rotation = Quaternion.Slerp(lookCamera.transform.rotation, Quaternion.Euler(rot), 2f); // �ڿ������� ȸ��  
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
                                if (hit.collider.CompareTag("RaycastTarget"))
                                {
                                    if (hit.collider.name == "01")
                                    {
                                        S2_Patient();
                                    }

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
                    else if (_s2PatientState == S2PatientState.Disinfect_1st)
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
                                    if (hit.collider.name == "HandGEL" )
                                    {
                                        hit.collider.GetComponent<Animator>().SetTrigger("trigger");
                                        SetNotice("�ռҵ��� �Ϸ�Ǿ����ϴ�.", S2_Patient);
                                        return;
                                    }
                                }
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
                                if (hit.collider.CompareTag("RaycastTarget"))
                                {
                                    if (hit.collider.name == "02")
                                    {
                                        S2_Patient();
                                    }
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
                                        SetNotice("�ռҵ��� �Ϸ�Ǿ����ϴ�.", S2_Patient);
                                        return;
                                    }
                                    if (hit.collider.name == "gloves" && !isGloves)
                                    {
                                        isGloves = true;
                                        gloves.SetActive(true);
                                        hit.collider.gameObject.SetActive(false);
                                        SetNotice("����尩�� ���� �Ǿ����ϴ�.", S2_Patient);
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
                                if (hit.collider.CompareTag("RaycastTarget"))
                                {
                                    if (hit.collider.name == "02")
                                    {
                                        myProp.transform.position = myProp_OrigPos;
                                        myProp.SetActive(false);
                                        myProp_OrigPos = Vector3.zero;
                                        myProp = null;
                                        S2_Patient();
                                        return;
                                    }
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
                                                else if (hit.collider.transform.parent.name == pubic_parentTR.name)
                                                {
                                                    SetNotice("�ҵ����� ���� �ҵ��� �������ּ���!");
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
                                                else if (hit.collider.transform.parent.name == pubic_parentTR.name)
                                                {
                                                    if (isCottonUse)
                                                    {
                                                        SetNotice("�ѹ��̶� ���� �ҵ����� ������ �������ּ���!");
                                                        return;
                                                    }
                                                    string tmp = hit.collider.name;

                                                    if (completeSpotNameList.Contains(tmp))
                                                    {

                                                        SetNotice("�ҵ��� �Ϸ�� �����Դϴ�! Ÿ ������ �ҵ����ּ���!");
                                                        return;
                                                    }

                                                    if(tmp == DisinfectPubicCollis[6].name)
                                                    {
                                                        if(_disinfectPubicState == DisinfectPubicState.�ܺ� || _disinfectPubicState == DisinfectPubicState.������ || _disinfectPubicState == DisinfectPubicState.������)
                                                            SetNotice("�ҵ� ������ �߸��Ǿ����ϴ�! �ٽ� �ҵ����ּ���!");
                                                    }
                                                    else if (tmp == DisinfectPubicCollis[5].name || tmp == DisinfectPubicCollis[4].name)
                                                    {

                                                        if (_disinfectPubicState == DisinfectPubicState.�ܺ� || _disinfectPubicState == DisinfectPubicState.������)
                                                            SetNotice("�ҵ� ������ �߸��Ǿ����ϴ�! �ٽ� �ҵ����ּ���!");
                                                    }
                                                    //else if (tmp == DisinfectPubicCollis[3].name || tmp == DisinfectPubicCollis[2].name)
                                                    //{

                                                    //    if (_disinfectPubicState == DisinfectPubicState.�ܺ�)
                                                    //        SetNotice("�ҵ� ������ �߸��Ǿ����ϴ�! �ٽ� �ҵ����ּ���!");
                                                    //}
                                                    nowSpotName = tmp;
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
                                        if(hit.collider.transform.parent.name == pubic_parentTR.name)
                                        {
                                            target.z = Vector3.Distance(pubic_Cam.transform.position, hit.point) - 0.05f;
                                            isCottonUse = true;
                                            if (Mathf.Abs(Input.mousePosition.y - swipeStartPosition.y) > swipeThreshold)
                                            {
                                                if (Input.mousePosition.y > swipeStartPosition.y)
                                                {
                                                    SetNotice("�ҵ��� ������ �Ʒ��� �������ּ���!");
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


                            if (swipeNum >= 2)
                            {
                                completeSpotNameList.Add(nowSpotName);

                                foreach(var DisinfectPubicColli in DisinfectPubicCollis)
                                {
                                    if(DisinfectPubicColli.name == nowSpotName)
                                    {
                                        DisinfectPubicColli.GetComponent<Renderer>().enabled = false;
                                    }
                                }

                                switch (_disinfectPubicState)
                                {
                                    //case DisinfectPubicState.�ܺ�:
                                    //    {
                                    //        if (completeSpotNameList.Contains(DisinfectPubicCollis[0].name) && completeSpotNameList.Contains(DisinfectPubicCollis[1].name))
                                    //            _disinfectPubicState = DisinfectPubicState.������;
                                    //        break;
                                    //    }
                                    case DisinfectPubicState.������:
                                        {
                                            if (completeSpotNameList.Contains(DisinfectPubicCollis[2].name) && completeSpotNameList.Contains(DisinfectPubicCollis[3].name))
                                                _disinfectPubicState = DisinfectPubicState.������;
                                            break;
                                        }
                                    case DisinfectPubicState.������:
                                        {
                                            if (completeSpotNameList.Contains(DisinfectPubicCollis[4].name) && completeSpotNameList.Contains(DisinfectPubicCollis[5].name))
                                                _disinfectPubicState = DisinfectPubicState.�䵵;
                                            break;
                                        }
                                    case DisinfectPubicState.�䵵:
                                        {
                                            if (completeSpotNameList.Contains(DisinfectPubicCollis[6].name))
                                            {
                                                _s2PatientState = S2PatientState.InsertCatheter1;
                                                S2_Patient();
                                            }
                                            break;
                                        }
                                }

                            }
                            swipeNum = 0;
                            nowSpotName = "";
                        }

                        //if(swipeNum > 20)
                        //{
                        //    _s2PatientState = S2PatientState.InsertCatheter1;
                        //    S2_Patient();
                        //}

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
                                        insertText.SetActive(true);
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
                            insertText.SetActive(false);
                            myProp = null;
                        }
                    }
                    else if (_s2PatientState == S2PatientState.InsertCatheter6)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = cam_Pee.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == pee_forceps.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.InsertCatheter7)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = cam_Pee.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == pee_forceps_inactive.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.InsertCatheter9)
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
                                        insertText.SetActive(true);
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
                            insertText.SetActive(false);
                            myProp = null;
                        }
                    }
                    else if (_s2PatientState == S2PatientState.InsertCatheter12)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = cam_Pee.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == Syringe_insert.underName())
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
                                target.z = Vector3.Distance(myProp.transform.position, cam_Pee.transform.position);
                                //print(target.z);
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, cam_Pee.ScreenToWorldPoint(target), 0.5f);
                            }
                        }
                        else
                        {

                            myProp = null;
                        }

                    }
                    else if (_s2PatientState == S2PatientState.InsertCatheter14)
                    {

                        if (Input.GetMouseButtonDown(0))
                        {
                            mouseStartPosition = Input.mousePosition;
                            Ray ray = insert_Cam.ScreenPointToRay(mouseStartPosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == rope_insert_Colli.name)
                                    {
                                        myProp = hit.transform.parent.gameObject;
                                        //insertText.SetActive(true);
                                        //myProp_OrigPos = hit.transform.position;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {

                            if (myProp)
                            {

                                if ((Input.mousePosition - mouseStartPosition).sqrMagnitude > Mathf.Pow(swipeThreshold,2))
                                {
                                    S2_Patient();
                                }


                                //Vector3 target = Input.mousePosition;
                                //target.z = Vector3.Distance(myProp.transform.position, insert_Cam.transform.position);
                                ////print(target.z);
                                ////myProp.transform.position = bag_camera.ScreenToWorldPoint(target);
                                //myProp.transform.position = Vector3.Lerp(myProp.transform.position, insert_Cam.ScreenToWorldPoint(target), Time.deltaTime);
                            }
                        }
                        else
                        {

                            myProp = null;
                        }
                    }
                    else if (_s2PatientState == S2PatientState.CleanUp3)
                    {

                        if (Input.GetMouseButtonDown(0))
                        {
                            mouseStartPosition = Input.mousePosition;
                            Ray ray = Camera.main.ScreenPointToRay(mouseStartPosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                            for (int i = 0; i < hits.Length; i++)
                            {
                                RaycastHit hit = hits[i];
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == Burlapfold_onBody.name)
                                    {
                                        myProp_OrigPos = hit.transform.position;
                                        myProp = hit.collider.gameObject;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {

                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = 3f;
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, Camera.main.ScreenToWorldPoint(target), 0.5f);
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            if (!myProp) return;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.CompareTag("RaycastTarget"))
                                {
                                    if (hit.collider.name == "02")
                                    {
                                        myProp.transform.position = myProp_OrigPos;
                                        myProp_OrigPos = Vector3.zero;
                                        myProp = null;
                                        return;
                                    }
                                }
                            }


                            S2_Patient();
                        }
                        else
                        {
                            if (myProp)
                            {
                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }

                    }
                    else if (_s2PatientState == S2PatientState.CleanUp4)
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
                                    if (hit.collider.name == "hand")
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }

                    }
                    else if (_s2PatientState == S2PatientState.CleanUp5)
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
                                    if (hit.collider.name == "HandGEL")
                                    {
                                        hit.collider.GetComponent<Animator>().SetTrigger("trigger");
                                        SetNotice("�ռҵ��� �Ϸ�Ǿ����ϴ�.", S2_Patient);
                                        return;
                                    }
                                }
                            }
                        }

                    }
                    else if (_s2PatientState == S2PatientState.CleanUp6)
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
                                    if (hit.collider.name == P2_OnCartProps_Patient[(int)S2OnCartEnum.Urinbag].name)
                                    {
                                        myProp_OrigPos = hit.transform.position;
                                        myProp = hit.collider.gameObject;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = Vector3.Distance(myProp_OrigPos, Camera.main.transform.position) / 2;
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, Camera.main.ScreenToWorldPoint(target) , 0.5f);
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            if (!myProp) return;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.CompareTag("RaycastTarget"))
                                {

                                    if(hit.collider.name == "01")
                                    {
                                        S2_Patient();
                                    }
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

                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }
                    }
                    //else if (_s2PatientState == S2PatientState.CleanUp7)
                    //{
                    //    if (Input.GetMouseButtonDown(0))
                    //    {
                    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                    //        for (int i = 0; i < hits.Length; i++)
                    //        {
                    //            RaycastHit hit = hits[i];
                    //            if (hit.collider.tag.Equals("RaycastTarget"))
                    //            {
                    //                if (hit.collider.name == urine_forceps.name)
                    //                {
                    //                    S2_Patient();
                    //                }
                    //            }
                    //        }
                    //    }

                    //}
                    //else if (_s2PatientState == S2PatientState.CleanUp8)
                    //{
                    //    if (Input.GetMouseButtonDown(0))
                    //    {
                    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                    //        for (int i = 0; i < hits.Length; i++)
                    //        {
                    //            RaycastHit hit = hits[i];
                    //            if (hit.collider.tag.Equals("RaycastTarget"))
                    //            {
                    //                if (hit.collider.name == urine.name)
                    //                {
                    //                    myProp_OrigPos = hit.transform.position;
                    //                    myProp = hit.collider.gameObject;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else if (Input.GetMouseButton(0))
                    //    {
                    //        if (myProp)
                    //        {
                    //            Vector3 target = Input.mousePosition;
                    //            target.z = Vector3.Distance(myProp_OrigPos, Camera.main.transform.position) / 2;
                    //            myProp.transform.position = Vector3.Lerp(myProp.transform.position, Camera.main.ScreenToWorldPoint(target), 0.5f);
                    //        }
                    //    }
                    //    else if (Input.GetMouseButtonUp(0))
                    //    {
                    //        if (!myProp) return;
                    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //        RaycastHit[] hits = Physics.RaycastAll(ray);
                    //        foreach (var hit in hits)
                    //        {
                    //            if (hit.collider.CompareTag("RaycastTarget"))
                    //            {

                    //                if (hit.collider.name == urine_joint_colli.name)
                    //                {
                    //                    myProp.transform.position = myProp_OrigPos;
                    //                    myProp_OrigPos = Vector3.zero;
                    //                    myProp = null;
                    //                    S2_Patient();
                    //                    return;
                    //                }
                    //            }
                    //        }
                    //        myProp.transform.position = myProp_OrigPos;
                    //        myProp_OrigPos = Vector3.zero;
                    //        myProp = null;
                    //    }
                    //    else
                    //    {
                    //        if (myProp)
                    //        {

                    //            myProp.transform.position = myProp_OrigPos;
                    //            myProp_OrigPos = Vector3.zero;
                    //            myProp = null;
                    //        }

                    //    }
                    //}
                    else if (_s2PatientState == S2PatientState.CleanUp9)
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
                                    if (hit.collider.name == forceps_joint_urine.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.CleanUp10)
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
                                    if (hit.collider.name == P2_OnCartProps_Patient[(int)S2OnCartEnum.Band].name)
                                    {
                                        myProp_OrigPos = hit.transform.position;
                                        myProp = hit.collider.gameObject;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = Vector3.Distance(myProp_OrigPos, Camera.main.transform.position) / 2;
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, Camera.main.ScreenToWorldPoint(target), 0.5f);
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            if (!myProp) return;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.CompareTag("RaycastTarget"))
                                {

                                    if (hit.collider.name == "02")
                                    {
                                        myProp.transform.position = myProp_OrigPos;
                                        myProp_OrigPos = Vector3.zero;
                                        myProp = null;
                                        S2_Patient();
                                        return;
                                    }
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

                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }
                    }
                    else if (_s2PatientState == S2PatientState.CleanUp11)
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
                                    if (hit.collider.name == "01")
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    else if (_s2PatientState == S2PatientState.CleanUp12)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.tag.Equals("RaycastTarget"))
                                {
                                    if (hit.collider.name == SetBag_onbad.name)
                                    {
                                        myProp_OrigPos = hit.transform.position;
                                        myProp = hit.collider.gameObject;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = Vector3.Distance(myProp_OrigPos, Camera.main.transform.position) / 2;
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, Camera.main.ScreenToWorldPoint(target), 0.5f);
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

                                    if (hit.collider.name == cart_collider.name)
                                    {
                                        S2_Patient();
                                        return;
                                    }
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

                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }
                    }
                    else if (_s2PatientState == S2PatientState.CleanUp13)
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
                                    if (hit.collider.name == waterproof_onBad.name)
                                    {
                                        myProp_OrigPos = hit.transform.position;
                                        myProp = hit.collider.gameObject;
                                    }
                                }
                            }
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            if (myProp)
                            {
                                Vector3 target = Input.mousePosition;
                                target.z = 3f;
                                myProp.transform.position = Vector3.Lerp(myProp.transform.position, Camera.main.ScreenToWorldPoint(target), 0.5f);
                            }
                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            if (!myProp) return;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit[] hits = Physics.RaycastAll(ray);
                            foreach (var hit in hits)
                            {
                                if (hit.collider.CompareTag("RaycastTarget"))
                                {

                                    if (hit.collider.name == "02")
                                    {
                                        myProp.transform.position = myProp_OrigPos;
                                        myProp_OrigPos = Vector3.zero;
                                        myProp = null;
                                        return;
                                    }
                                }
                            }
                            S2_Patient();
                        }
                        else
                        {
                            if (myProp)
                            {

                                myProp.transform.position = myProp_OrigPos;
                                myProp_OrigPos = Vector3.zero;
                                myProp = null;
                            }

                        }
                    }
                    else if (_s2PatientState == S2PatientState.CleanUp14)
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
                                    if (hit.collider.name == urine_forceps_final.name)
                                    {
                                        S2_Patient();
                                    }
                                }
                            }
                        }
                    }
                    //else if (_s2PatientState == S2PatientState.CleanUp15)
                    //{
                    //    if (Input.GetMouseButtonDown(0))
                    //    {
                    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                    //        for (int i = 0; i < hits.Length; i++)
                    //        {
                    //            RaycastHit hit = hits[i];
                    //            if (hit.collider.tag.Equals("RaycastTarget"))
                    //            {
                    //                if (hit.collider.name == urine_bad_colli.name)
                    //                {
                    //                    S2_Patient();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
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
            isSetBlink = false;
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
            Invoke("SetBlinkTimer", 5f);
        }

        void SetBlinkTimer()
        {
            isSetBlink = true;
            SetBlink();
        }


        void SetBlink()
        {
            if (!isSetBlink) return;
            if (P2_ShelvesProp) P2_ShelvesProp.CancelBlink();
            while (true)
            {
                int index = UnityEngine.Random.Range(0, P2_ShelvesProps.Length);
                if(P2_ShelvesProps[index].gameObject.activeInHierarchy)
                {
                    P2_ShelvesProps[index].Blink();
                    P2_ShelvesProp = P2_ShelvesProps[index];
                    break;
                }
            }
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
            else
            {
                SetBlink();
            }
        }
        public void S2_AfterQuiz1()
        {
            question.SetActive(false);
            SetNotice("�����Դϴ�!\n���� ���� ��ġ������ �ǽ��غ��ô�.\n���������� ���� ����ġ��Ƿ� �̵����ּ���.");
            _s2State = S2State.patient;
            C1cart.SetActive(false);
            C2cart_orig.SetActive(false);
            C2cart.SetActive(true);
        }
        public void S2_Patient()
        {
            if (_s2State != S2State.patient) return;

            


            switch (_s2PatientState)
            {
                case S2PatientState.Prologue:
                    {
                        SetNotice("���� ȯ�ڿ��� ���� �Ұ��� ȯ�� ����Ȯ�� ���� Ŀư�� ġ�� ��Ⱦ������ ���� �����Դϴ�.\n�ռҵ� ���� ���� ��Ʈ�� ������ ���� ��ġ���� ������ �̾������ ����.");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.Disinfect_1st:
                    {
                        //�ռҵ�

                        //SetNotice("���� ȯ�ڿ��� ���� �Ұ��� ȯ�� ����Ȯ�� ���� Ŀư�� ġ�� ��Ⱦ������ ���� �����Դϴ�.\n���� ��Ʈ�� ������ ���� ��ġ���� ������ �̾������ ����.");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.Start:
                    {
                        //������Ʈ ����
                        ///var e = (S2OnCartEnum)Enum.Parse(typeof(S2OnCartEnum), myProp.name);
                        P2_OnCartProps_Patient[(int)S2OnCartEnum.bag].SetActive(false);
                        bagSet.SetActive(true);
                        SetNotice("���� īƮ���� �巡�� �� ����� ���Ͽ�\nȯ���� ��� �Ʒ��� ������� ��ġ���ּ���.");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.WaterProof:
                    {
                        waterproof_onBad.SetActive(true);
                        P2_OnCartProps_Patient[(int)S2OnCartEnum.WaterproofClothfold].SetActive(false);
                        SetNotice("�ռҵ��� �����ϰ� ��� �尩�� �����ϵ��� ����.");
                        _s2PatientState = S2PatientState.Disinfect;
                        break;
                    }
                case S2PatientState.Disinfect:
                    {
                        if (isGloves && isHandGel)
                        {
                            SetNotice("���� ȯ�ںп��� �Ұ����� �����ּ���.");
                            _s2PatientState = S2PatientState.Burlapfold;
                        }

                        //waterproof_onBad.SetActive(true);
                        //P2_OnCartProps_Patient[(int)S2OnCartEnum.WaterproofClothfold].SetActive(false);
                        //SetNotice("�ռҵ��� �����ϰ� ��� �尩�� �����ϵ��� ����.");
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
                        SetNotice("���� ��ġ�������� ������� üũ�غ��ô�.");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.Ballooning2:
                    {
                        Syringe_HL.SetMin();
                        Syringe_orig.gameObject.SetActive(false);
                        Syringe_HL.gameObject.SetActive(true);
                        _s2PatientState = S2PatientState.Ballooning3;
                        break;
                    }
                case S2PatientState.Ballooning3:
                    {
                        Syringe_Water.SetMin();
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
                        SetNotice("������ �غ� ��ģ ���� ���θ� �ҵ��ϰ� �������� �����ϼ���");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectRope2:
                    {
                        //���� Ŭ��
                        //SetNotice("������ �غ� ��ģ ���� ���θ� �ҵ��ϰ� �������� �����ϼ���", S2_Patient);
                        guase.SetActive(false);
                        guase_HL.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectRope3:
                    {
                        //�� Ŭ��                   
                        guase_HL.SetActive(false);
                        guase_Gel.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectRope4:
                    {
                        //������ �� Ŭ��
                        guase_Gel.SetActive(false);
                        guase_spreadAnim.gameObject.SetActive(true);
                        guase_spreadAnim.SetTrigger("trigger");
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.forceps1:
                    {
                        //���� Ŭ��
                        forceps.SetActive(false);
                        forceps_HL.gameObject.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.forceps2:
                    {
                        //���ںκ� Ŭ��
                        forceps_HL.SetActive(false);
                        forceps_Active.SetActive(true);
                        NextPatientStep();
                        FadeOut(S2_Patient);
                        break;
                    }
                case S2PatientState.DisinfectPubic1:
                    {
                        bag_camera.gameObject.SetActive(false);
                        ballooningOBJ.SetActive(false);
                        pubic_Cam.gameObject.SetActive(true);
                        pubicObjs.SetActive(true);
                        nonpubicObjs.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.DisinfectPubic2:
                    {
                        //�ɼ�Ŭ��
                        Tweezers.gameObject.SetActive(false);
                        Tweezers_pointer.gameObject.SetActive(true);
                        //Tweezers_pointer_distance = Vector3.Distance(Tweezers_pointer.transform.position, pubic_Cam.transform.position);
                        Tweezers_pointer_distance = 0.2f;
                        _disinfectPubicState = DisinfectPubicState.������;
                        foreach (var DisinfectPubicColli in DisinfectPubicCollis)
                        {
                            DisinfectPubicColli.GetComponent<Renderer>().enabled = true;

                        }
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter1:
                    {
                        //ī���� ���Ծ� ����
                        NextPatientStep();
                        FadeOut(S2_Patient);
                        break;
                    }
                case S2PatientState.InsertCatheter2:
                    {
                        //ī���� ���Ծ� ī�޶� �����ֱ�
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
                        //�巡�� �� ���� �Ϸ�������

                        insertText.SetActive(false);
                        SetNotice("������ �� 5-8cm ������ �Ϸ�Ǿ����ϴ�.\n���� ���ڸ� Ǯ�� �Һ��� ���������� Ȯ���غ��ڽ��ϴ�.", S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter4:
                    {
                        //ī�޶� ��ȯ ����
                        FadeOut(S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter5:
                    {
                        //ī�޶� ��ȯ ��
                        insert_Cam.gameObject.SetActive(false);
                        cam_Pee.gameObject.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter6:
                    {
                        //���� Ǯ������
                        pee_forceps.SetActive(false);
                        pee_forceps_inactive.SetActive(true);
                        pee_Particle.SetActive(true);
                        Invoke("SetPee", 3f);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter7:
                    {
                        //���� �ᰬ����
                        pee_forceps.SetActive(true);
                        pee_forceps_inactive.SetActive(false);
                        pee_Particle.SetActive(false);
                        //CancelInvoke("SetPee");
                        //ī�޶� ��ȯ ����
                        FadeOut(S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter8:
                    {
                        //ī�޶� ��ȯ ��true
                        insert_Cam.gameObject.SetActive(true);
                        cam_Pee.gameObject.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter9:
                    {
                        //�巡�� �� ���� �Ϸ�������

                        insertText.SetActive(false);
                        SetNotice("������ �� 2-4cm ������ �Ϸ�Ǿ����ϴ�.\n���� ������� �����ρٽ��ϴ�.", S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter10:
                    {
                        //ī�޶� ��ȯ ����
                        FadeOut(S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter11:
                    {
                        //ī�޶� ��ȯ ��
                        insert_Cam.gameObject.SetActive(false);
                        cam_Pee.gameObject.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter12:
                    {
                        //�ֻ�� ������ ��â
                        //ī�޶� ��ȯ ����
                        FadeOut(S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter13:
                    {
                        //ī�޶� ��ȯ ��  true
                        Syringe_insert.gameObject.SetActive(false);
                        insert_Cam.gameObject.SetActive(true);
                        cam_Pee.gameObject.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.InsertCatheter14:
                    {
                        //����� üũ �Ϸ�
                        SetNotice("���������� ������� �Ȱ��� Ȯ���߽��ϴ�.", S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp1:
                    {
                        //ī�޶� ��ȯ ����
                        FadeOut(S2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp2:
                    {
                        //ī�޶� ��ȯ �Ϸ�
                        insert_Cam.gameObject.SetActive(false);
                        insert_Obj.SetActive(false);
                        cleanUp_Obj.SetActive(true);
                        SetNotice("�Ұ����� ������ ���� �����ϰ� �ִ� �尩�� Ŭ���Ͽ� �尩�� ���� �Һ� �ָӴϸ� �����ϰ� �ֺ� ������ �������ּ���.", NextPatientStep);
                        isMove = true;
                        break;
                    }
                case S2PatientState.CleanUp3:
                    {
                        //�Ұ��� ���� �Ϸ�
                        Burlapfold_onBad.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp4:
                    {
                        //�尩 ���� �Ϸ�
                        gloves.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp5:
                    {
                        //������   
                        SetNotice("���� �Һ��ָӴϸ� �������ּ���.");  
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp6:
                    {
                        //īƮ �� �Һ��ָӴ� �巡�� �� ��� 

                        P2_OnCartProps_Patient[(int)S2OnCartEnum.Urinbag].SetActive(false);
                        //urine.SetActive(true);
                        urine_joint_rope.SetActive(true);
                        NextPatientStep();
                        break;
                    }

                //case S2PatientState.CleanUp7:
                //    {
                //        //�Һ��ָӴ� ���� Ŭ�� 
                //        urine_forceps.SetActive(false);
                //        urine_forceps1.SetActive(true);
                //        urine_forceps2.SetActive(true);
                //        NextPatientStep();
                //        break;
                //    }

                //case S2PatientState.CleanUp8:
                //    {
                //        //�Һ��ָӴ� ���� 
                //        urine.SetActive(false);
                //        urine_joint_rope.SetActive(true);
                //        NextPatientStep();
                //        break;
                //    }

                case S2PatientState.CleanUp9:
                    {
                        //���� ����
                        forceps_joint_urine.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp10:
                    {
                        //��ƼĿ �巡�׾ص�� - ������ ����
                        P2_OnCartProps_Patient[(int)S2OnCartEnum.Band].SetActive(false);
                        sticker_onBody.SetActive(true);
                        rope_inserted.SetActive(false);
                        //rope_Sticked.SetActive(true);
                        urine_bad.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp11:
                    {
                        //������Ʈ Ŭ�� - ������Ʈ ������
                        bagSet.SetActive(false);
                        SetBag_onbad.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp12:
                    {
                        //������Ʈ īƮ�� ����
                        SetBag_onbad.SetActive(false);
                        P2_OnCartProps_Patient[(int)S2OnCartEnum.bag].SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp13:
                    {
                        //����� ����
                        waterproof_onBad.SetActive(false);
                        NextPatientStep();
                        break;
                    }
                case S2PatientState.CleanUp14:
                    {
                        //�Һ��� ����
                        urine_forceps_final.SetActive(false);
                        NextPatientStep();

                        Invoke("S2_Patient", 1f);
                        break;
                    }
                //case S2PatientState.CleanUp15:
                //    {
                //        //�����ָӴ� ���� 
                //        rope_Sticked.SetActive(false);
                //        urine_bad.SetActive(true);
                //        NextPatientStep();
                //        Invoke("S2_Patient", 1f);
                //        break;
                //    }
                case S2PatientState.End:
                    {
                        //��
                        question.SetActive(true);
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
            if (_s2PatientState == S2PatientState.InsertCatheter12)
            {
                S2_Patient();
            }

        }

        void SetPee()
        {
            pee_pad.SetActive(true);
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
