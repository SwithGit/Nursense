using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace SWITHFACTORY.CYJ
{
    public enum GameStateEnum
    {
        �ܼ�����,
        �����ֻ�
    }
    public class Player_S2_2 : MonoBehaviour
    {

        public GameStateEnum _gameStateEnum;
        [Header("�÷��̾� ����")]
        public Camera cam_main;
        public Transform camZoomTr;
        public Transform camZoomRayTr;
        public GameObject lookCamera;
        public Animator anim;
        Quaternion cameraFixRotation;
        bool isMove;
        float zoomdis;
        float moveSpeed;
        float rotateSpeed = 5f;
        Vector3 dir;

        [Header("����")]
        public Animator FadeAnim;
        Action fadeCallBack;
        public GameObject question;
        public GameObject notice;
        public Action noticeCallback;

        [Header("Ray")]
        public GameObject myProp;
        Vector3 myProp_OrigPos;
        public Camera myCam;
        public string myName;
        public string myTargetName;
        public enum RayState
        {
            none,
            click,
            drag,
            dragdrop,
            cart,
            disinfect,
            tweezers,
            insert,
            extract,
            guase,
            syringe,
            alcoholcotton,
            injectionarea,
            rotateSyringe
        }
        public RayState _rayState;

        #region �ܼ����� ����


        public enum S2_2_State
        {
            start,
            cart,
            quiz,
            prologue,
            Disinfect_1st,
            Bag,
            WaterProof,
            Disinfect,
            Burlapfold,
            GelRope1,
            GelRope2,
            GelRope3,
            GelRope4,
            GelRope5,
            GelRope6,
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
            //InsertCatheter9,
            ExtractCatheter1,
            ExtractCatheter2,
            ExtractCatheter3,
            ExtractCatheter4,
            CleanUp1,
            CleanUp2,
            CleanUp3,
            CleanUp4,
            CleanUp5,
            CleanUp6,
            End

        }

        [Header("�ܼ����� ����")]
        [Header("�ܼ����� ����")]
        [Header("�ܼ����� ����")]
        [Header("�ܼ����� ���� - ����")]
        public S2_2_State _s2_2_State;



        [Header("�ܼ����� ���� - ����")]
        public GameObject s2StartPanal;

        public enum OnCartProbEnum
        {
            HandGEL,
            gloves,
            WaterproofClothfold,
            bag,
            Burlapfold,
            Disinfection01,
            Disinfection04
        }

        public GameObject[] OnCartProbs_C1;
        public GameObject[] OnCartProbs_C2;

        public Items_Shelves P2_ShelvesProp;
        public Items_Shelves[] P2_ShelvesProps;
        bool isSetBlink;

        public GameObject C1cart;
        public GameObject C2cart_orig;
        public GameObject C2cart;


        [Header("�ܼ����� ���� - ���ѷα�")]
        //public GameObject handGel;
        public GameObject bag;
        public GameObject waterproof;
        public GameObject Burlapfold;
        public Collider colli_onBad;
        public Collider colli_onPatient;
        public bool isHandGel;
        public bool isGloves;
        public GameObject gloves_doctor;


        [Header("�ܼ����� ���� - ������ ��Ȱ")]
        public Camera cam_rope;
        public GameObject rope_orig;
        public GameObject rope_HL;
        public Collider colli_ropeGel;
        public GameObject guase;
        public GameObject guase_HL;
        public GameObject guase_Gel;
        public Animator guase_spreadAnim;
        public GameObject Gel;


        [Header("�ܼ����� ���� - �ҵ�")]
        public Camera cam_pubic;
        //public Camera pubic_Cam_overlayPubic;
        //public GameObject pubic_parentTR;
        public GameObject pubicObjs;
        public GameObject nonpubicObjs;
        public float Tweezers_pointer_distance = 0.5f;
        //public List<Collider> DisinfectPubicCollis;
        //public List<string> completeSpotNameList;
        //public string nowSpotName;
        //public int nowSpotIndex;
        public Collider colli_glans;
        public GameObject Tweezers_pointer;
        public GameObject Tweezers;
        public GameObject cottonPad;
        public GameObject cotton;
        public GameObject puspan;
        public int cottonNum;
        public GameObject[] cottons_on_puspan;
        public bool isCottonUse;
        private float swipeThreshold = 10f;
        Vector3 swipeStartPosition;
        public int swipeNum;
        public int total_swipeNum;

        [Space]
        [Header("�ܼ����� ���� - ������ ����")]
        public GameObject insert_Obj;
        //public GameObject rope_insert;
        public Rope2 rope_insert;
        public Collider rope_insert_Colli;
        //public Transform rope_peeOrig;
        //public Vector3 rope_peeOrig_Pos;
        //public Camera cam_Pee;
        public GameObject pee_Particle;
        public GameObject pee_pad;
        //public GameObject pee_forceps;
        //public GameObject pee_forceps_inactive;
        //public Syringe Syringe_insert;
        Vector3 mouseStartPosition;
        public GameObject insertText;
        public Collider colli_extractRope;
        public GameObject rope_extracted;

        public GameObject guase_afterex;
        public GameObject guase_afterex_pointer;
        [Space]
        [Header("�ܼ����� ���� - ������")]
        public Collider colli_bag_onbad;
        public GameObject bag_onbad;
        #endregion
        #region �����ֻ� ����

        [Header("�����ֻ�")]
        [Header("�����ֻ�")]
        [Header("�����ֻ�")]
        [Header("����")]
        public Text missionText;
        public enum S2_3_State
        {
            start,
            computer,
            sink1,
            sink2,
            sink3,
            quiz1,
            quiz2,
            patient1,
            patient2,
            patient3,
            ample1,
            ample2,
            syringe1,
            syringe2,
            hand_disinfection1,
            hand_disinfection2,
            consultation1,
            consultation2,
            consultation3,
            consultation4,
            curtain,
            hand_disinfection3,
            hand_disinfection4,
            alcoholcotton1,
            alcoholcotton2,
            alcoholcotton3,
            muscleinjection1,
            muscleinjection2,
            muscleinjection3,
            muscleinjection4,
            muscleinjection5,
            muscleinjection6,
            muscleinjection7,
            clear1,
            clear2,
            clear3,
            clear4,
            clear5,
            clear6,
            End

        }
        public S2_3_State _s2_3_State;
        public enum ToolEnum
        {
            none = -1,
            �ռҵ�,
            ����,
            �ֻ��,
            ���ݼ�,
            Ŀư,

        }

        public ToolEnum _toolEnum;

        [Header("�����ֻ� - ����")]
        public Collider colli_Computer;
        public GameObject exclam_Computer;
        public GameObject exclam_sink;
        public GameObject chartPanal;
        public Collider colli_Sink;
        public Transform tr_Sink;

        [Header("�����ֻ� - ���� ���� ����")]
        public GameObject videoPanal;
        public VideoPlayer _videoPlayer;
        public GameObject quizPanal;
        public Toggle[] answerToggles;
        public List<int> quizAnswer = new List<int>();

        [Header("�����ֻ� - ȯ�� ���")]
        public Collider colli_patient;
        public GameObject exclam_Patient;

        [Header("�����ֻ� - ȯ�� ġ��")]
        public Camera cam_onBad;
        public GameObject toolsPanal;
        public GameObject[] toolHLs;
        Coroutine toolBlinkCoroutine;

        [Header("�����ֻ� - ȯ�� ġ�� - ���� �� �ֻ�")]
        public GameObject ample;
        public Collider colli_ample;
        public GameObject ample_Lid;
        public Outline ol_ample;
        public Syringe syringe_ample;
        public Syringe syringe_ample_insert;
        public Outline ol_syringe;
        public GameObject diHand_syringe;

        [Header("�����ֻ� - �Ƿ� ���")]
        public GameObject consultation_Panal;
        public InputField consultation_InputField;
        public GameObject[] consultation_Titles;
        public string[] consultation_Results;


        [Header("�����ֻ� - ȯ�� ġ�� - ���� �� �ֻ�")]
        public Animator anim_patientPants;
        public GameObject curtain;
        public GameObject hipMark;
        public Collider[] collis_cotton_area;
        [SerializeField]
        float Tweezers_pointer_distance2;
        public GameObject tweezers_pointer2;
        public GameObject tweezer_circleAnimed;
        public GameObject syringe_mark;

        [Header("�����ֻ� - �����ֻ� ����")]
        public Camera cam_Muscle;
        public GameObject syringe_Muscle_Obj;
        public Collider colli_syringe_Muscle;
        public Syringe_5ml syringe_Muscle;
        public Outline ol_syringe_Muscle;
        public Animator anim_inject;
        Vector3 mousePrePosition;
        public AngleSet syringe_angleSet;
        public Text text_angleFloat;
        bool isClicked;
        [Header("�����ֻ� - ����")]
        public GameObject tweezer_circleAnimed_muscle;


        #endregion
        #region Unity �̺�Ʈ �Լ�

        // Start is called before the first frame update
        void Start()
        {
            cam_main = Camera.main;
            zoomdis = -camZoomTr.transform.localPosition.z;
            isMove = false;
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            Zoom();
            Move();
            RayPoint();
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (_gameStateEnum)
            {
                case GameStateEnum.�ܼ�����:
                    {
                        if (other.CompareTag("PatientRoom"))
                        {
                            if (_s2_2_State == S2_2_State.prologue) P2_Patient();
                        }
                        break;
                    }

            }
        }

        #endregion
        #region Move
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
        #endregion
        void RayPoint()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            switch (_gameStateEnum)
            {
                case GameStateEnum.�ܼ�����:
                    {
                        switch (_rayState)
                        {
                            case RayState.none:
                                {
                                    return;
                                }
                            case RayState.click:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == myName)
                                                {
                                                    P2_Patient();
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case RayState.dragdrop:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            if (hits[i].collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hits[i].collider.name == myName)
                                                {
                                                    myProp_OrigPos = hits[i].transform.position;
                                                    myProp = hits[i].collider.gameObject;
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    else if (Input.GetMouseButton(0))
                                    {
                                        if (myProp)
                                        {
                                            Vector3 target = Input.mousePosition;
                                            target.z = Vector3.Distance(myCam.transform.position, myProp_OrigPos) * 0.8f;
                                            myProp.transform.position = Vector3.Lerp(myProp.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);
                                            //myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
                                        }
                                    }
                                    else if (Input.GetMouseButtonUp(0))
                                    {
                                        if (!myProp) return;
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray);
                                        foreach (var hit in hits)
                                        {
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == myTargetName)
                                                {
                                                    P2_Patient();
                                                    break;
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
                                    break;
                                }
                            case RayState.cart:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray);
                                        for (int k = 0; k < hits.Length; k++)
                                        {
                                            if (hits[k].collider.CompareTag("RaycastTarget"))
                                            {
                                                for (int i = 0; i < P2_ShelvesProps.Length; i++)
                                                {
                                                    if (!P2_ShelvesProps[i].gameObject.activeInHierarchy) continue;
                                                    if (P2_ShelvesProps[i].name == hits[k].collider.name)
                                                    {
                                                        myProp_OrigPos = hits[k].transform.position;
                                                        myProp = hits[k].collider.gameObject;
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (Input.GetMouseButton(0))
                                    {
                                        if (myProp)
                                        {
                                            Vector3 target = Input.mousePosition;
                                            target.z = Vector3.Distance(Camera.main.transform.position, myProp_OrigPos) * 0.8f;
                                            myProp.transform.position = Vector3.Lerp(myProp.transform.position, Camera.main.ScreenToWorldPoint(target), 0.5f);
                                            //myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
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
                                                    var e = (OnCartProbEnum)Enum.Parse(typeof(OnCartProbEnum), myProp.name);
                                                    OnCartProbs_C1[(int)e].SetActive(true);


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
                                                CheckCartProps();
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
                                    break;
                                }
                            case RayState.disinfect:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == OnCartProbEnum.gloves.ToString())
                                                {
                                                    if (!isGloves)
                                                    {

                                                        CheckDisinfect(hit.collider.gameObject);
                                                        break;
                                                    }
                                                }
                                                if (hit.collider.name == OnCartProbEnum.HandGEL.ToString())
                                                {
                                                    if (!isHandGel)
                                                    {
                                                        CheckDisinfect(hit.collider.gameObject);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case RayState.tweezers:
                                {
                                    Vector3 target = Input.mousePosition;

                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        switch (_s2_2_State)
                                        {
                                            case S2_2_State.DisinfectPubic_off:
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
                                                                _s2_2_State = S2_2_State.DisinfectPubic_on;
                                                            }
                                                            else if (hit.collider.name == colli_glans.name)
                                                            {
                                                                SetNotice("�ҵ����� ���� �ҵ��� �������ּ���!");
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                            case S2_2_State.DisinfectPubic_on:
                                                {
                                                    for (int i = 0; i < hits.Length; i++)
                                                    {
                                                        RaycastHit hit = hits[i];
                                                        if (hit.collider.tag.Equals("RaycastTarget"))
                                                        {
                                                            if (hit.collider.name == puspan.name)
                                                            {
                                                                swipeNum = 0;
                                                                cotton.SetActive(false);
                                                                try
                                                                {
                                                                    cottons_on_puspan[cottonNum].SetActive(true);
                                                                    cottonNum++;
                                                                }
                                                                catch
                                                                {

                                                                }
                                                                _s2_2_State = S2_2_State.DisinfectPubic_off;
                                                            }
                                                            else if (hit.collider.name == colli_glans.name)
                                                            {
                                                                if (isCottonUse)
                                                                {
                                                                    SetNotice("�ҵ����� �� �̻� ����� �� �����ϴ�.\n���ο� �ҵ����� ������ּ���!");
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
                                        if (!cotton.activeInHierarchy) return;
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        if (hits.Length != 0)
                                        {
                                            for (int i = 0; i < hits.Length; i++)
                                            {
                                                RaycastHit hit = hits[i];
                                                if (hit.collider.tag.Equals("RaycastTarget"))
                                                {
                                                    if (hit.collider.name == colli_glans.name)
                                                    {
                                                        target.z = Vector3.Distance(myCam.transform.position, hit.point) * 0.8f;
                                                        isCottonUse = true;
                                                        if (Mathf.Abs(Input.mousePosition.y - swipeStartPosition.y) > swipeThreshold)
                                                        {
                                                            swipeStartPosition = Input.mousePosition;
                                                            swipeNum++;
                                                            total_swipeNum++;

                                                            if (swipeNum > 5)
                                                            {
                                                                //isCottonUse = true;
                                                                SetNotice("�ҵ����� �� �̻� ����� �� �����ϴ�.\n���ο� �ҵ����� ������ּ���!");
                                                                swipeStartPosition = Vector3.zero;
                                                                return;
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
                                    Tweezers_pointer.transform.position = Vector3.Lerp(Tweezers_pointer.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);

                                    if (Input.GetMouseButtonUp(0))
                                    {
                                        swipeStartPosition = Vector3.zero;
                                        //swipeNum = 0;
                                    }
                                    if (total_swipeNum > 30)
                                    {
                                        P2_Patient();
                                    }
                                    break;
                                }
                            case RayState.insert:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == rope_insert_Colli.name)
                                                {
                                                    myProp = rope_insert.gameObject;
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
                                            rope_insert.Stick();
                                            Vector3 target = Input.mousePosition;
                                            target.z = Vector3.Distance(myProp.transform.position, myCam.transform.position);
                                            //print(target.z);
                                            //myProp.transform.position = bag_camera.ScreenToWorldPoint(target);
                                            myProp.transform.position = Vector3.Lerp(myProp.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);
                                            rope_insert.Stick();
                                        }
                                    }
                                    else if (Input.GetMouseButtonUp(0))
                                    {
                                        insertText.SetActive(false);
                                        myProp = null;
                                    }
                                    else
                                    {

                                        insertText.SetActive(false);
                                        myProp = null;
                                    }

                                    break;
                                }
                            case RayState.extract:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == rope_insert_Colli.name)
                                                {
                                                    myProp = rope_insert.gameObject;
                                                }
                                            }
                                        }
                                    }
                                    else if (Input.GetMouseButton(0))
                                    {
                                        if (myProp)
                                        {
                                            rope_insert.Stick();
                                            Vector3 target = Input.mousePosition;
                                            if (rope_insert.phase != 4) target.z = Vector3.Distance(myProp.transform.position, myCam.transform.position);
                                            else target.z = 0.3f;
                                            myProp.transform.position = Vector3.Lerp(myProp.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);
                                            rope_insert.Stick();
                                        }
                                    }
                                    else if (Input.GetMouseButtonUp(0))
                                    {
                                        if (!myProp) return;
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray);
                                        foreach (var hit in hits)
                                        {
                                            if (rope_insert.phase == 4)
                                            {
                                                P2_Patient();
                                                break;

                                            }
                                            //if (hit.collider.CompareTag("RaycastTarget"))
                                            //{
                                            //    if (hit.collider.name == myTargetName)
                                            //    {
                                            //        P2_Patient();
                                            //        break;
                                            //    }
                                            //}
                                        }
                                        myProp = null;
                                    }
                                    else
                                    {
                                        if (myProp)
                                        {
                                            myProp = null;
                                        }

                                    }

                                    break;
                                }
                            case RayState.guase:
                                {
                                    Vector3 target = Input.mousePosition;

                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.tag.Equals("RaycastTarget"))
                                            {
                                                if (hit.collider.name == colli_glans.name)
                                                {
                                                    swipeStartPosition = Input.mousePosition;
                                                }
                                            }
                                        }
                                    }


                                    if (Input.GetMouseButton(0))
                                    {
                                        if (swipeStartPosition == Vector3.zero) return;
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        if (hits.Length != 0)
                                        {
                                            for (int i = 0; i < hits.Length; i++)
                                            {
                                                RaycastHit hit = hits[i];
                                                if (hit.collider.tag.Equals("RaycastTarget"))
                                                {
                                                    if (hit.collider.name == colli_glans.name)
                                                    {
                                                        target.z = Vector3.Distance(myCam.transform.position, hit.point) * 0.8f;
                                                        if (Mathf.Abs(Input.mousePosition.y - swipeStartPosition.y) > swipeThreshold)
                                                        {
                                                            swipeStartPosition = Input.mousePosition;
                                                            total_swipeNum++;
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
                                    guase_afterex_pointer.transform.position = Vector3.Lerp(guase_afterex_pointer.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);

                                    if (Input.GetMouseButtonUp(0))
                                    {
                                        swipeStartPosition = Vector3.zero;
                                        //swipeNum = 0;
                                    }
                                    if (total_swipeNum >= 5)
                                    {
                                        P2_Patient();
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case GameStateEnum.�����ֻ�:
                    {
                        switch (_rayState)
                        {
                            case RayState.none:
                                {
                                    return;
                                }
                            case RayState.click:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == myName)
                                                {
                                                    C3_Patient();
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case RayState.drag:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == myName)
                                                {
                                                    myProp = hit.collider.gameObject;
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
                                            target.z = Vector3.Distance(myProp.transform.position, myCam.transform.position);
                                            //print(target.z);
                                            //myProp.transform.position = bag_camera.ScreenToWorldPoint(target);
                                            myProp.transform.position = Vector3.Lerp(myProp.transform.position, myCam.ScreenToWorldPoint(target), 0.9f);
                                        }
                                    }
                                    else if (Input.GetMouseButtonUp(0))
                                    {
                                        myProp = null;
                                    }
                                    else
                                    {

                                        myProp = null;
                                    }

                                    break;
                                }
                            case RayState.dragdrop:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            if (hits[i].collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hits[i].collider.name == myName)
                                                {
                                                    myProp_OrigPos = hits[i].transform.position;
                                                    myProp = hits[i].collider.gameObject;
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    else if (Input.GetMouseButton(0))
                                    {
                                        if (myProp)
                                        {
                                            Vector3 target = Input.mousePosition;
                                            target.z = Vector3.Distance(myCam.transform.position, myProp_OrigPos) * 0.8f;
                                            myProp.transform.position = Vector3.Lerp(myProp.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);
                                            //myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
                                        }
                                    }
                                    else if (Input.GetMouseButtonUp(0))
                                    {
                                        if (!myProp) return;
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray);
                                        foreach (var hit in hits)
                                        {
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == myTargetName)
                                                {
                                                    C3_Patient();
                                                    break;
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
                                    break;
                                }
                            case RayState.syringe:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == myName)
                                                {
                                                    myProp = hit.collider.gameObject;
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
                                            target.z = Vector3.Distance(myProp.transform.position, myCam.transform.position);
                                            //print(target.z);
                                            //myProp.transform.position = bag_camera.ScreenToWorldPoint(target);
                                            myProp.transform.position = Vector3.Lerp(myProp.transform.position, myCam.ScreenToWorldPoint(target), 0.9f) - new Vector3(0 , - 0.01f, 0 );
                                        }
                                    }
                                    else if (Input.GetMouseButtonUp(0))
                                    {
                                        myProp = null;
                                    }
                                    else
                                    {

                                        myProp = null;
                                    }

                                    break;
                                }
                            case RayState.alcoholcotton:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == collis_cotton_area[0].name)
                                                {
                                                    C3_Patient();
                                                }
                                                else if (hit.collider.name == collis_cotton_area[1].name ||
                                                    hit.collider.name == collis_cotton_area[2].name ||
                                                    hit.collider.name == collis_cotton_area[3].name)
                                                {
                                                    SetNotice("�ùٸ� ��ġ�� �ҵ��� �������ּ���!");
                                                }
                                            }
                                        }
                                    }
                                    Vector3 target = Input.mousePosition;
                                    target.z = Tweezers_pointer_distance2;
                                    tweezers_pointer2.transform.position = Vector3.Lerp(tweezers_pointer2.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);

                                    break;
                                }
                            case RayState.injectionarea:
                                {
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray, 100, 1 << 0);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            RaycastHit hit = hits[i];
                                            if (hit.collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hit.collider.name == collis_cotton_area[0].name)
                                                {
                                                    C3_Patient();
                                                }
                                                else if (hit.collider.name == collis_cotton_area[1].name ||
                                                    hit.collider.name == collis_cotton_area[2].name ||
                                                    hit.collider.name == collis_cotton_area[3].name)
                                                {
                                                    SetNotice("�ùٸ� ��ġ�� �ֻ縦 �����ּ���!");
                                                }
                                            }
                                        }
                                    }
                                    Vector3 target = Input.mousePosition;
                                    target.z = Tweezers_pointer_distance2;
                                    syringe_mark.transform.position = Vector3.Lerp(syringe_mark.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);
                                    break;
                                }
                            case RayState.rotateSyringe:
                                {
                                    text_angleFloat.text = syringe_angleSet.GetAngle().ToString();
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit[] hits = Physics.RaycastAll(ray);
                                        for (int i = 0; i < hits.Length; i++)
                                        {
                                            if (hits[i].collider.CompareTag("RaycastTarget"))
                                            {
                                                if (hits[i].collider.name == myName)
                                                {
                                                    mousePrePosition = Input.mousePosition;
                                                    //myProp_OrigPos = hits[i].transform.position;
                                                    myProp = hits[i].collider.gameObject;
                                                    isClicked = true;
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    else if (Input.GetMouseButton(0))
                                    {
                                        if (myProp)
                                        {
                                            Vector3 target = Input.mousePosition;
                                            Vector2 dragDirection = (mousePrePosition - target);
                                            print(dragDirection);
                                            if (dragDirection.sqrMagnitude > 5f)
                                            {
                                                isClicked = false;
                                                mousePrePosition = Input.mousePosition;
                                                if (dragDirection.x * dragDirection.y < 0) return;
                                                int dir = dragDirection.x >= 0 ? 1 : -1;
                                                syringe_angleSet.SetAngle(dir * dragDirection.magnitude / 10f);
                                            }
                                            //target.z = Vector3.Distance(myCam.transform.position, myProp_OrigPos) * 0.8f;
                                            //myProp.transform.position = Vector3.Lerp(myProp.transform.position, myCam.ScreenToWorldPoint(target), 0.5f);
                                            //myProp.transform.position = Camera.main.ScreenToWorldPoint(target);
                                        }
                                    }
                                    else if (Input.GetMouseButtonUp(0))
                                    {
                                        myProp = null;
                                        if (isClicked)
                                        {
                                            if (syringe_angleSet.IsAngle())
                                            {
                                                C3_Patient();
                                            }
                                            else
                                            {
                                                SetNotice("�ùٸ� ������ �������ּ���.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        myProp = null;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
        void Init()
        {
            switch(_gameStateEnum)
            {
                case GameStateEnum.�ܼ�����:
                    {
                        waterproof.SetActive(false);
                        cam_pubic.gameObject.SetActive(false);
                        cam_rope.gameObject.SetActive(false);
                        bag.SetActive(false);
                        insert_Obj.SetActive(false);
                        Burlapfold.SetActive(false);
                        rope_extracted.SetActive(false);
                        guase_afterex_pointer.SetActive(false);
                        pee_pad.SetActive(false);

                        s2StartPanal.SetActive(true);
                        _s2_2_State = S2_2_State.start;
                        //_s2PatientState = S2PatientState.Prologue;
                        //isSetBlink = false;
                        //CartInit();
                        break;
                    }
                case GameStateEnum.�����ֻ�:
                    {
                        isMove = true;
                        foreach (var t in answerToggles)
                        {
                            t.isOn = false;
                        }
                        foreach (var ol in FindObjectsOfType<Outline>())
                        {
                            ol.enabled = false;
                        }

                        cam_onBad.gameObject.SetActive(false);

                        videoPanal.SetActive(false);
                        toolsPanal.SetActive(false);
                        ClearToolHL();
                        quizAnswer.Clear();
                        quizAnswer.Add(1);
                        quizAnswer.Add(2);
                        quizAnswer.Add(4);
                        quizAnswer.Add(5);
                        quizAnswer.Add(8);

                        ample.SetActive(false);
                        ample_Lid.SetActive(true);

                        syringe_ample.gameObject.SetActive(false);
                        syringe_ample_insert.gameObject.SetActive(false);


                        cam_Muscle.gameObject.SetActive(false);
                        colli_syringe_Muscle.transform.parent.parent.gameObject.SetActive(false);

                        text_angleFloat.gameObject.SetActive(false);

                        C3_Patient();
                        break;
                    }
            }
        }
        #region �ܼ�����

        public void P2_Patient()
        {
            _rayState = RayState.none;
            myCam = null;
            myName = "";
            myTargetName = "";

            switch (_s2_2_State)
            {
                case S2_2_State.start:
                    {
                        //����
                        s2StartPanal.SetActive(false); 
                        Invoke("SetBlinkTimer", 5f);
                        _rayState = RayState.cart;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.cart:
                    {
                        //īƮ �� ��ǰ �巡�׾ص��
                        //s2StartPanal.SetActive(false);
                        //_rayState = RayState.cart;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.quiz:
                    {
                        //���� �����
                        SetNotice("�����Դϴ�!\n���� ���� �ܼ������� �ǽ��غ��ô�.\n���������� ���� ����ġ��Ƿ� �̵����ּ���.");
                        C1cart.SetActive(false);
                        C2cart_orig.SetActive(false);
                        C2cart.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.prologue:
                    {
                        //ȯ�ڽ� ����
                        SetNotice("���� ȯ�ڿ��� ���� �Ұ��� ȯ�� ����Ȯ�� ���� Ŀư�� ġ�� ������� ���� �����̴�. ���� ��Ʈ�� ������ ���� �ܼ����� ������ �̾������ ����.");
                        //_rayState = RayState.cart;
                        _rayState = RayState.click;
                        myCam = Camera.main;
                        myName = OnCartProbEnum.HandGEL.ToString();
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.Disinfect_1st:
                    {
                        //�ռҵ� �Ϸ� ��
                        SetNotice("�ռҵ��� �Ϸ��߽��ϴ�.");

                        _rayState = RayState.dragdrop;
                        myCam = Camera.main;
                        myName = OnCartProbs_C2[(int)OnCartProbEnum.bag].name;
                        myTargetName = colli_onBad.name;

                        NextPatientStep();
                        break;
                    }
                case S2_2_State.Bag:
                    {
                        //������Ʈ ���� ��
                        SetNotice("���� ������� ��ġ�ϰڽ��ϴ�.");
                        OnCartProbs_C2[(int)OnCartProbEnum.bag].SetActive(false);
                        bag.SetActive(true);

                        _rayState = RayState.dragdrop;
                        myCam = Camera.main;
                        myName = OnCartProbs_C2[(int)OnCartProbEnum.WaterproofClothfold].name;
                        myTargetName = colli_onPatient.name;

                        NextPatientStep();
                        break;
                    }
                case S2_2_State.WaterProof:
                    {
                        //����� ��ġ ��
                        SetNotice("�ռҵ��� �����ϰ� ��� �尩�� �����ϵ��� ����.");
                        OnCartProbs_C2[(int)OnCartProbEnum.WaterproofClothfold].SetActive(false);
                        waterproof.SetActive(true);
                        _rayState = RayState.disinfect;
                        myCam = Camera.main;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.Disinfect:
                    {
                        //�ռҵ� �� ����尩 ���� ��
                        SetNotice("���� ȯ�ںп��� �Ұ����� �����ּ���.");
                        _rayState = RayState.dragdrop;
                        myCam = Camera.main;
                        myName = OnCartProbs_C2[(int)OnCartProbEnum.Burlapfold].name;
                        myTargetName = colli_onPatient.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.Burlapfold:
                    {
                        //����� ���� ��
                        OnCartProbs_C2[(int)OnCartProbEnum.Burlapfold].SetActive(false);
                        Burlapfold.SetActive(true);
                        FadeOut(P2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.GelRope1:
                    {
                        //���� ��Ʈ �� on
                        isMove = false;
                        Camera.main.gameObject.SetActive(false);
                        cam_rope.gameObject.SetActive(true);
                        _rayState = RayState.click;
                        myCam = cam_rope;
                        myName = rope_orig.name;
                        NextPatientStep();

                        break;
                    }
                case S2_2_State.GelRope2:
                    {
                        //������ Ŭ�� �� 
                        rope_orig.SetActive(false);
                        rope_HL.SetActive(true);
                        _rayState = RayState.click;
                        myCam = cam_rope;
                        myName = guase.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.GelRope3:
                    {
                        //���� Ŭ�� ��
                        //�� Ŭ��
                        guase.SetActive(false);
                        guase_HL.SetActive(true);
                        _rayState = RayState.click;
                        myCam = cam_rope;
                        myName = Gel.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.GelRope4:
                    {
                        //�� Ŭ�� ��
                        //������ �� Ŭ�� 
                        guase_HL.SetActive(false);
                        guase_Gel.SetActive(true);
                        _rayState = RayState.click;
                        myCam = cam_rope;
                        myName = colli_ropeGel.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.GelRope5:
                    {
                        //�� Ŭ�� ��
                        //������ �� Ŭ�� 
                        guase_Gel.SetActive(false);
                        guase_spreadAnim.gameObject.SetActive(true);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.GelRope6:
                    {
                        //�� ���� �Ϸ� ��
                        // ���̵�ƿ� 
                        FadeOut(P2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.DisinfectPubic1:
                    {
                        //���̵���
                        //ī�޶� �������� �� �ɼ� ����
                        SetNotice("���� �䵵�ҵ��� �����ϰڽ��ϴ�.");
                        cam_rope.gameObject.SetActive(false);
                        cam_pubic.gameObject.SetActive(true);
                        pubicObjs.SetActive(true);
                        nonpubicObjs.SetActive(false);
                        _rayState = RayState.click;
                        myCam = cam_pubic;
                        myName = Tweezers.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.DisinfectPubic2:
                    {
                        //�ɼ¼��� ��
                        //�ɼ��� ���콺 ����ٴϰ� ���� 
                        Tweezers.SetActive(false);
                        Tweezers_pointer.SetActive(true);
                        _rayState = RayState.tweezers;
                        myCam = cam_pubic;
                        myName = Tweezers.name;
                        Vector3 target = Input.mousePosition;
                        target.z = Tweezers_pointer_distance;
                        Tweezers_pointer.transform.position = myCam.ScreenToWorldPoint(target);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.DisinfectPubic_off:
                case S2_2_State.DisinfectPubic_on:
                    {
                        Tweezers_pointer.SetActive(false);
                        Tweezers.SetActive(true);
                        SetNotice("�䵵 �ҵ��� �Ϸ�Ǿ����ϴ�.\n���� �������� �����ϰڽ��ϴ�." , P2_Patient);

                        _s2_2_State = S2_2_State.InsertCatheter1;

                        break;
                    }
                case S2_2_State.InsertCatheter1:
                    {
                        insert_Obj.SetActive(true);
                        _rayState = RayState.insert;
                        myCam = cam_pubic;
                        myName = rope_insert_Colli.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.InsertCatheter2:
                    {
                        insertText.SetActive(false);
                        SetNotice("�� 12~18cm, ������ ������ �Ϸ�Ǿ����ϴ�. �Һ��� �귯�������� Ȯ�����ּ���.", P2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.InsertCatheter3:
                    {
                        pee_Particle.SetActive(true);
                        //pee_pad.SetActive(true);
                        Invoke("ShowPeePad" , 1f);
                        P2_Patient(3f);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.InsertCatheter4:
                    {
                        SetNotice("2~4cm���� �߰������� ������ �����ϰڽ��ϴ�.", P2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.InsertCatheter5:
                    {
                        _rayState = RayState.insert;
                        myCam = cam_pubic;
                        myName = rope_insert_Colli.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.InsertCatheter6:
                    {
                        insertText.SetActive(false);
                        P2_Patient(3f);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.InsertCatheter7:
                    {
                        pee_Particle.SetActive(false);
                        P2_Patient(2f);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.InsertCatheter8:
                    {
                        SetNotice("�ܼ������� ����Ǿ����ϴ�. �������� ������ ȯ�θ� �۾��ּ���.", P2_Patient);
                        NextPatientStep();
                        break;
                    }
                //case S2_2_State.InsertCatheter9:
                //    {
                //        _rayState = RayState.extract;
                //        myCam = cam_pubic;
                //        myName = rope_insert_Colli.name;
                //        myTargetName = puspan.name;
                //        NextPatientStep();
                //        break;
                //    }
                case S2_2_State.ExtractCatheter1:
                    {
                        _rayState = RayState.extract;
                        myCam = cam_pubic;
                        myName = rope_insert_Colli.name;
                        myTargetName = colli_extractRope.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.ExtractCatheter2:
                    {
                        rope_insert.gameObject.SetActive(false);
                        rope_extracted.SetActive(true);

                        _rayState = RayState.click;
                        myCam = cam_pubic;
                        myName = guase_afterex.name;
                        NextPatientStep();


                        break;
                    }
                case S2_2_State.ExtractCatheter3:
                    {
                        guase_afterex.gameObject.SetActive(false);
                        guase_afterex_pointer.SetActive(true);
                        total_swipeNum = 0;
                        _rayState = RayState.guase;
                        myCam = cam_pubic;

                        Vector3 target = Input.mousePosition;
                        target.z = Tweezers_pointer_distance;
                        guase_afterex_pointer.transform.position = myCam.ScreenToWorldPoint(target);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.ExtractCatheter4:
                    {
                        FadeOut(P2_Patient);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.CleanUp1:
                    {

                        guase_afterex_pointer.SetActive(false);
                        SetNotice("�ܼ����� �۾��� ����Ǿ����ϴ�.\n���� �ֺ������� �������ּ���.", P2_Patient);
                        cam_main.gameObject.SetActive(true);
                        cam_pubic.gameObject.SetActive(false);

                        NextPatientStep();
                        break;
                    }
                case S2_2_State.CleanUp2:
                    {
                        //�Ұ��� ����
                        isMove = true;
                        _rayState = RayState.dragdrop;
                        myCam = cam_main;
                        myName = Burlapfold.name;
                        myTargetName = C2cart.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.CleanUp3:
                    {
                        //�Ұ��� ���� ��
                        //������Ʈ ����
                        Burlapfold.SetActive(false);
                        OnCartProbs_C2[(int)OnCartProbEnum.Burlapfold].SetActive(true);


                        _rayState = RayState.click;
                        myCam = cam_main;
                        myName = colli_bag_onbad.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.CleanUp4:
                    {
                        //������Ʈ ���� ��
                        //������Ʈ �̵���Ű��


                        bag.SetActive(false);
                        pubicObjs.SetActive(false);
                        bag_onbad.SetActive(true);
                        //OnCartProbs_C2[(int)OnCartProbEnum.Burlapfold].SetActive(true);


                        _rayState = RayState.dragdrop;
                        myCam = cam_main;
                        myName = bag_onbad.name;
                        myTargetName = C2cart.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.CleanUp5:
                    {
                        //������Ʈ �̵� �� 
                        //����� �̵���Ű��


                        bag_onbad.SetActive(false);
                        OnCartProbs_C2[(int)OnCartProbEnum.bag].SetActive(true);


                        _rayState = RayState.dragdrop;
                        myCam = cam_main;
                        myName = waterproof.name;
                        myTargetName = C2cart.name;
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.CleanUp6:
                    {
                        //����� �̵� �� 
                        //���� �̵�


                        waterproof.SetActive(false);
                        OnCartProbs_C2[(int)OnCartProbEnum.WaterproofClothfold].SetActive(true);
                        isMove = false;
                        P2_Patient(1f);
                        NextPatientStep();
                        break;
                    }
                case S2_2_State.End:
                    {
                        //����� �̵� �� 
                        //���� �̵�

                        question.SetActive(true);
                        break;
                    }



            }


        }

        void P2_Patient(float timer)
        {
            Invoke("P2_Patient", timer);
        }

        void NextPatientStep()
        {
            int i = (int)_s2_2_State;
            i++;
            _s2_2_State = (S2_2_State)i;
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
        
        public void OnClickS2StartButton()
        {
            isMove = true;
            s2StartPanal.SetActive(false);
            _s2_2_State = S2_2_State.cart;
            _rayState = RayState.cart;
            Invoke("SetBlinkTimer", 5f);
        }



        void SetBlinkTimer()
        {
            isSetBlink = true;
            SetBlink();
        }
        
        void CheckCartProps()
        {

            bool b = true;

            foreach (GameObject obj in OnCartProbs_C1)
            {
                b = b && obj.activeSelf;
            }
            if (b)
            {
                print("Complete!");
                NextPatientStep();
                question.SetActive(true);
                //Complete!
            }
            else
            {
                SetBlink();
            }
        }
        void SetBlink()
        {
            if (!isSetBlink) return;
            if (P2_ShelvesProp) P2_ShelvesProp.CancelBlink();
            while (true)
            {
                int index = UnityEngine.Random.Range(0, P2_ShelvesProps.Length);
                if (P2_ShelvesProps[index].gameObject.activeInHierarchy)
                {
                    P2_ShelvesProps[index].Blink();
                    P2_ShelvesProp = P2_ShelvesProps[index];
                    break;
                }
            }
        }

        void CheckDisinfect(GameObject _gameobject)
        {
            string msg = "";
            if (_gameobject.name == OnCartProbEnum.HandGEL.ToString())
            {
                isHandGel = true;
                _gameobject.GetComponent<Animator>().SetTrigger("trigger");
                msg = "�ռҵ��� �Ϸ�Ǿ����ϴ�.";
            }
            if (_gameobject.name == OnCartProbEnum.gloves.ToString())
            {
                isGloves = true;
                gloves_doctor.SetActive(true);
                _gameobject.SetActive(false);
                msg = "����尩�� ���� �Ǿ����ϴ�.";
            }
            if(isGloves && isHandGel)
            {
                SetNotice(msg, P2_Patient);
            }
            else
            {
                SetNotice(msg);
            }


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

        void ShowPeePad()
        {
            pee_pad.SetActive(true);
        }

        #endregion
        #region �����ֻ�
        public void SetMission(string msg = "")
        {
            missionText.text = msg;
            if (msg == "")
            {
                missionText.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                missionText.transform.parent.gameObject.SetActive(true);
            }
        }



        public void C3_Patient()
        {
            SetMission();
            _toolEnum = ToolEnum.none;
            _rayState = RayState.none;
            if(toolBlinkCoroutine != null) StopCoroutine(toolBlinkCoroutine);
            myCam = null;
            myName = "";
            myTargetName = "";
            switch (_s2_3_State)
            {
                case S2_3_State.start:
                    {

                        exclam_Computer.SetActive(true);
                        SetMission("��ǻ�� �տ��� ����ó���� Ȯ�����ּ���.");
                        _rayState= RayState.click;
                        myName = colli_Computer.name;
                        myCam = Camera.main;                        
                        NextPatientStep_C3();
                        break;
                    }

                case S2_3_State.computer:
                    {
                        SetMission("���� ���� �İ�, �����ֻ翡 �ʿ��� ��ǰ���� ����ּ���.");
                        exclam_Computer.SetActive(false);
                        exclam_sink.SetActive(true);
                        chartPanal.SetActive(true);
                        //SetMission("��ǻ�� �տ��� ����ó���� Ȯ�����ּ���.");
                        //_rayState2 = RayState2.click;
                        //myProp = colli_Computer.gameObject;
                        //myCam = Camera.main;
                        _rayState = RayState.click;
                        myName = colli_Sink.name;
                        myCam = Camera.main;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.sink1:
                    {
                        isMove = false;

                        _videoPlayer.Play();

                        FadeOut(C3_Patient);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.sink2:
                    {
                        //_videoPlayer.Stop();
                        //_videoPlayer.Prepare();
                        _videoPlayer.time = 0;
                        C3_Patient((float)_videoPlayer.clip.length - 1f);
                        videoPanal.SetActive(true);
                        //_videoPlayer.Stop();
                        //_videoPlayer.Prepare();
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.sink3:
                    {
                        FadeOut(C3_Patient);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.quiz1:
                    {
                        videoPanal.SetActive(false);
                        exclam_sink.SetActive(false);
                        _videoPlayer.Stop();
                        quizPanal.SetActive(true);
                        transform.position = tr_Sink.position;
                        transform.rotation = tr_Sink.rotation;
                        //NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.quiz2:
                    {
                        SetMission("����Ƿ� �̵��� �����ֻ縦 �������ּ���.");
                        isMove = true;
                        exclam_Patient.SetActive(true);
                        _rayState = RayState.click;
                        myName = colli_patient.name;
                        myCam = Camera.main;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.patient1:
                    {
                        FadeOut(C3_Patient);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.patient2:
                    {

                        exclam_Patient.SetActive(false);

                        cam_main.gameObject.SetActive(false);
                        cam_onBad.gameObject.SetActive(true);
                        SetNotice("ȯ�ڰ� ������ ���� �����ִ�.\n�Ƿ� īƮ�� ��ǰ���� ����Ͽ� �����ֻ縦 �����ϵ��� ����.", C3_Patient);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.patient3:
                    {
                        SetMission("�Ƿ� īƮ�� ��ǰ���� ����Ͽ� �����ֻ縦 �����ϵ��� ����.");
                        toolsPanal.SetActive(true);
                        //anim_patientPants.enabled = true;
                        SetToolClick(ToolEnum.����);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.ample1:
                    {
                        //�����ڽ� ���� Ŭ��
                        SetMission("�Ƿ� īƮ�� ��ǰ���� ����Ͽ� �����ֻ縦 �����ϵ��� ����.");
                        ample.SetActive(true);
                        NextPatientStep_C3();
                        ol_ample.enabled = true;
                        _rayState = RayState.click;
                        myName = colli_ample.name;
                        myCam = cam_onBad;
                        break;
                    }
                case S2_3_State.ample2:
                    {
                        //���� �Ѳ� Ŭ�� 
                        //�������� �ֻ�� Ŭ�� 
                        SetMission("�Ƿ� īƮ�� ��ǰ���� ����Ͽ� �����ֻ縦 �����ϵ��� ����.");

                        ol_ample.enabled = false;
                        ample_Lid.SetActive(false);
                        SetToolClick(ToolEnum.�ֻ��);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.syringe1:
                    {
                        //�������� �ֻ�� Ŭ��
                        SetMission("�Ƿ� īƮ�� ��ǰ���� ����Ͽ� �����ֻ縦 �����ϵ��� ����.");
                        syringe_ample.gameObject.SetActive(true);
                        syringe_ample.SetMax();
                        _rayState = RayState.click;
                        myName = colli_ample.name;
                        myCam = cam_onBad;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.syringe2:
                    {
                        //���� Ŭ���� �ֻ�� ����

                        syringe_ample.gameObject.SetActive(false);
                        syringe_ample_insert.gameObject.SetActive(true);
                        syringe_ample_insert.SetMax();
                        ol_syringe.enabled = true;
                        //�ֻ�� ����� ��

                        _rayState = RayState.syringe;
                        myName = syringe_ample_insert.underName();
                        myCam = cam_onBad;
                        SetMission("�Ƿ� īƮ�� ��ǰ���� ����Ͽ� �����ֻ縦 �����ϵ��� ����.");
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.hand_disinfection1:
                    {
                        //�ֻ�� ����� ��
                        //�ռҵ� ��ư ������
                        ample.SetActive(false);
                        //syringe_ample_insert.gameObject.SetActive(true);
                        syringe_ample_insert.SetMax();
                        syringe_mark.SetActive(true);
                        ClearToolHL();
                        SetMission("�Ƿ� īƮ�� ��ǰ���� ����Ͽ� �����ֻ縦 �����ϵ��� ����.");
                        SetToolClick(ToolEnum.�ռҵ�);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.hand_disinfection2:
                    {
                        //�ֻ�� ����� ��
                        //�ռҵ� ��ư ������
                        C3_Patient(2f);
                        diHand_syringe.SetActive(true);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.consultation1:
                    {
                        //�ռҵ� ����
                        //���â on
                        //�ڽ��� �Ұ��ϴ� ��縦 �ۼ����ּ���.
                        ClearToolHL();
                        diHand_syringe.SetActive(false);
                        consultation_Panal.SetActive(true);
                        consultation_InputField.text = "";
                        consultation_Titles[0].SetActive(true);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.consultation2:
                    {
                        //������ ������ ���� ȯ�ڸ� Ȯ�����ּ���.
                        consultation_Results[0] = consultation_InputField.text;
                        consultation_InputField.text = "";
                        consultation_Titles[0].SetActive(false);
                        consultation_Titles[1].SetActive(true);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.consultation3:
                    {
                        //������ ����, �๰ȿ��, ���ǻ���, ��� �������ּ���.
                        //�ڽ��� �Ұ��ϴ� ��縦 �ۼ����ּ���.
                        consultation_Results[1] = consultation_InputField.text;
                        consultation_InputField.text = "";
                        consultation_Titles[1].SetActive(false);
                        consultation_Titles[2].SetActive(true);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.consultation4:
                    {
                        //�ڽ��� �Ұ��ϴ� ��縦 �ۼ����ּ���.
                        consultation_Panal.SetActive(false);
                        consultation_Results[2] = consultation_InputField.text;
                        consultation_InputField.text = "";
                        consultation_Titles[2].SetActive(false);
                        SetToolClick(ToolEnum.Ŀư);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.curtain:
                    {
                        //�ڽ��� �Ұ��ϴ� ��縦 �ۼ����ּ���.
                        curtain.SetActive(true);
                        anim_patientPants.enabled = true;
                        
                        SetMission("�����ֻ縦 ���� �ּ���.");
                        SetToolClick(ToolEnum.�ռҵ�);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.hand_disinfection3:
                    {
                        ClearToolHL();
                        C3_Patient(2f);
                        diHand_syringe.SetActive(true);

                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.hand_disinfection4:
                    {
                        ClearToolHL();
                        diHand_syringe.SetActive(false);
                        SetToolClick(ToolEnum.���ݼ�);
                        SetMission("�����ֻ縦 ���� �ּ���.");
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.alcoholcotton1:
                    {
                        SetMission("�����ֻ縦 ���� �ּ���.");
                        tweezers_pointer2.SetActive(true);
                        cotton.SetActive(true);
                        _rayState = RayState.alcoholcotton;
                        myCam = cam_onBad;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.alcoholcotton2:
                    {
                        SetMission("�����ֻ縦 ���� �ּ���.");

                        C3_Patient(2f);
                        tweezer_circleAnimed.SetActive(true);
                        tweezers_pointer2.SetActive(false);
                        cotton.SetActive(false);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.alcoholcotton3:
                    {
                        ClearToolHL();
                        SetMission("�����ֻ縦 ���� �ּ���.");
                        SetNotice("�ҵ��� �Ϸ�Ǿ����ϴ�.");
                        tweezer_circleAnimed.SetActive(false);
                        SetToolClick(ToolEnum.�ֻ��);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.muscleinjection1:
                    {
                        SetMission("�����ֻ縦 ���� �ּ���.");
                        //syringe_mark.gameObject.SetActive(true);
                        _rayState = RayState.injectionarea;
                        myCam = cam_onBad;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.muscleinjection2:
                    {
                        SetMission("�����ֻ縦 ���� �ּ���.");
                        FadeOut(C3_Patient);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.muscleinjection3:
                    {
                        syringe_mark.gameObject.SetActive(false);
                        SetMission("�����ֻ縦 ���� �ּ���.");
                        cam_onBad.gameObject.SetActive(false);
                        cam_Muscle.gameObject.SetActive(true);
                        syringe_Muscle_Obj.SetActive(true);
                        syringe_Muscle.SetMax();
                        text_angleFloat.gameObject.SetActive(true);
                        _rayState = RayState.rotateSyringe;
                        myCam = cam_Muscle;
                        myName = colli_syringe_Muscle.name;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.muscleinjection4:
                    {
                        anim_inject.SetTrigger("in");
                        hipMark.gameObject.SetActive(false);
                        text_angleFloat.gameObject.SetActive(false);
                        C3_Patient(0.5f);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.muscleinjection5:
                    {
                        _rayState = RayState.syringe;
                        myCam = cam_Muscle;
                        myName = syringe_Muscle.underName();
                        ol_syringe_Muscle.enabled = true;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.muscleinjection6:
                    {
                        NextPatientStep_C3();
                        break;
                    }

                case S2_3_State.muscleinjection7:
                    {
                        ol_syringe_Muscle.enabled = false;
                        SetNotice("�����ֻ簡 �Ϸ�Ǿ����ϴ�.", C3_Patient);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.clear1:
                    {
                        syringe_angleSet.SetMove();
                        _rayState = RayState.drag;
                        myCam = cam_Muscle;
                        myName = colli_syringe_Muscle.name;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.clear2:
                    {
                        syringe_Muscle_Obj.SetActive(false);
                        ClearToolHL();
                        SetToolClick(ToolEnum.���ݼ�);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.clear3:
                    {
                        tweezers_pointer2.SetActive(true);
                        cotton.SetActive(true);
                        _rayState = RayState.alcoholcotton;
                        myCam = cam_Muscle;
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.clear4:
                    {
                        tweezers_pointer2.SetActive(false);
                        cotton.SetActive(false);
                        tweezer_circleAnimed_muscle.SetActive(true);
                        C3_Patient(2f);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.clear5:
                    {
                        tweezer_circleAnimed_muscle.SetActive(false);
                        anim_patientPants.SetTrigger("up");
                        FadeOut(C3_Patient);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.clear6:
                    {
                        cam_main.gameObject.SetActive(true);
                        cam_Muscle.gameObject.SetActive(false);
                        toolsPanal.SetActive(false);
                        isMove = false;
                        C3_Patient(1f);
                        NextPatientStep_C3();
                        break;
                    }
                case S2_3_State.End:
                    {
                        question.SetActive(true);
                        NextPatientStep_C3();
                        break;
                    }

            }
        }

        public void C3_Patient(float timer)
        {
            Invoke("C3_Patient", timer);
        }

        void NextPatientStep_C3()
        {
            int i = (int)_s2_3_State;
            i++;
            _s2_3_State = (S2_3_State)i;
        }

        public void OnClickQuizApplyButton()
        {
            for (int i = 0; i < answerToggles.Length; i++)
            {
                if (answerToggles[i].isOn != quizAnswer.Contains(i + 1))
                {
                    SetNotice("Ʋ�Ƚ��ϴ�. �ٽ� Ǯ���ּ���.");
                    return;
                }
            }

            quizPanal.SetActive(false);

            SetNotice("�����Դϴ�.\n���� ����Ƿ� �̵��� �����ֻ縦 �������ּ���.", C3_Patient);
            NextPatientStep_C3();
        }

        public void OnClickToolButton(int index)
        {
            if ((int)_toolEnum == index)
            {
                toolHLs[index].SetActive(true);
                Color c = toolHLs[index].GetComponent<Image>().color;
                c.a = 1;
                toolHLs[index].GetComponent<Image>().color = c;
                C3_Patient();
            }

        }

        void SetToolClick(ToolEnum toolEnum)
        {
            _toolEnum = toolEnum;
            toolBlinkCoroutine = StartCoroutine(BlinkToolHL(toolEnum));
        }


        void ClearToolHL()
        {
            foreach (var hl in toolHLs)
            {
                hl.SetActive(true);
                Color c = hl.GetComponent<Image>().color;
                c.a = 0;
                hl.GetComponent<Image>().color = c;
                //hl.SetActive(false);             
            }
        }


        IEnumerator BlinkToolHL(ToolEnum toolEnum)
        {
            WaitForEndOfFrame wa = new WaitForEndOfFrame();
            yield return new WaitForSeconds(5f);
            Image hl_Img = toolHLs[(int)toolEnum].GetComponent<Image>();
            Color c;
            float speed = 1.4f;
            while(true)
            {
                c = hl_Img.color;
                c.a += speed * Time.deltaTime;
                hl_Img.color = c;

                if(hl_Img.color.a < 0 || hl_Img.color.a > 1)
                {
                    speed = -speed;
                }


                //print($"c.a : {c.a} | speed : {speed}");
                yield return wa;
            }
        }

        public void Syringe_Min()
        {
            if (_s2_3_State == S2_3_State.muscleinjection7)
                C3_Patient();
        }
        public void Syringe_Max()
        {
            if (_s2_3_State == S2_3_State.hand_disinfection1)
                C3_Patient();

            if (_s2_3_State == S2_3_State.muscleinjection6)
                NextPatientStep_C3();
        }
        public void Syringe_Down()
        {
                SetNotice("���׿��� ���θ� Ȯ�����ּ���!");
                myProp = null;
        }
        #endregion
    }
}
