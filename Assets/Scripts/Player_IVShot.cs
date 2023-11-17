using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

using SWITHFACTORY.CYJ;
using System;
using UnityEngine.EventSystems;

public class Player_IVShot : MonoBehaviour
{

    [Header("플레이어 관련")]
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


    [Header("공통")]
    public Animator FadeAnim;
    Action fadeCallBack;
    public GameObject notice;
    Action noticeCallback;
    [SerializeField]
    Outline ol;

    [Header("Ray")]
    public GameObject myProp;
    Vector3 myProp_OrigPos;
    public Camera myCam;
    public string myName;
    public string myTargetName;
    public enum RayState
    {
        button = -1,
        none = 0,
        click,
        drag,
        dragdrop

    }
    public RayState _rayState;
    public int btnNum;

    [Header("IV")]
    public Text missionText;
    public enum IVState
    {
        start,
        chart,
        sink1,
        sink2,
        sink3,
        sink4,
        quiz1,
        quiz2,
        cart1,
        cart2,
        cart3,
        cart4,
        cart5,
        cart6,
        cart7,
        cart8,
        cart9,
        bed1,
        bed2,
        bed3,
        bed4,
        chamber1,
        chamber2,
        chamber3,
        chamber4,
        bed5,
        bed6,
        bed7,
        bed8,
        bed9,
        bed10,
        bed11,
        bed12,
        bed13,
        bed14,
        bed15,
        bed16,

    }
    public IVState _ivState;

    [Header("시작하기")]
    public Collider colli_Computer;
    public GameObject exclam_Computer;
    public GameObject exclam_sink;
    public GameObject chartPanal;
    public Collider colli_Sink;
    public Transform tr_Sink;



    [Header("물건 선택 퀴즈")]
    public GameObject videoPanal;
    public VideoPlayer _videoPlayer;
    public GameObject quizPanal;
    public Toggle[] answerToggles;
    public List<int> quizAnswer = new List<int>();


    [Header("세트 준비")]
    public Camera cam_cart;
    public GameObject cartObj;
    public GameObject toolButton;
    //public GameObject IV_inCart;
    public GameObject iv_Select;
    //public GameObject iv_Select_Side;
    public GameObject iv_Select_BluePlug;
    //public GameObject medicationcard_inCart;
    public GameObject medicationcard_Select;
    public GameObject medicationcard_in_IV;
    public GameObject ivSet;
    public GameObject ivSet_FlowRegulator_Enable;
    public GameObject ivSet_FlowRegulator_Disable;
    public Collider colli_IVSet_ivTube;
    public GameObject iv_inIVset;

    [Header("침상")]
    public Camera cam_bed;
    public GameObject bedObj;
    public Collider colli_iv_Rack;
    public GameObject ivSet_bed;
    public GameObject ivSet_in_Rack;
    public GameObject iv_Chamber; //랙에걸린점적통
    public int chamberNum;
    public GameObject iv_Chamber_waterEff; //점적통 안 물 이펙트
    public Camera cam_Chamber;//챔버 줌인 카메라
    public GameObject ivSet_inRack_FlowRegulator_Enable;
    public GameObject ivSet_inRack_FlowRegulator_Disable;
    public GameObject iv_Rack_flowEff;

    [Header("침상 옆")]
    public Camera cam_side;
    public GameObject tourniquet;
    public GameObject tourniquet_inBody;





    #region Unity 이벤트 함수 및 초기화

    // Start is called before the first frame update
    void Start()
    {
        //cam_main = Camera.main;
        zoomdis = -camZoomTr.transform.localPosition.z;
        isMove = false;
        _rayState = RayState.none;
        Init();
    }

    void Init()
    {
        isMove = true;

        quizAnswer.Add(1);
        quizAnswer.Add(3);
        quizAnswer.Add(4);
        quizAnswer.Add(6);
        quizAnswer.Add(7);
        quizAnswer.Add(8);
        quizAnswer.Add(10);

        cam_main.gameObject.SetActive(true);
        cam_cart.gameObject.SetActive(false);
        toolButton.SetActive(false);



        cam_cart.gameObject.SetActive(false);
        iv_Select.SetActive(false);
        //iv_Select_Side.SetActive(false);
        iv_Select_BluePlug.SetActive(true);
        medicationcard_Select.SetActive(false);
        medicationcard_in_IV.SetActive(false);
        ivSet.SetActive(false);
        ivSet_FlowRegulator_Enable.SetActive(true);
        ivSet_FlowRegulator_Disable.SetActive(false);
        colli_IVSet_ivTube.gameObject.SetActive(true);

        iv_inIVset.SetActive(false);
        cartObj.SetActive(true);
        bedObj.SetActive(false);
        ivSet_bed.SetActive(false);
        ivSet_in_Rack.SetActive(false);
        chamberNum = 0;
        iv_Chamber_waterEff.SetActive(false);
        cam_Chamber.gameObject.SetActive(false);
        ivSet_inRack_FlowRegulator_Enable.SetActive(true);
        ivSet_inRack_FlowRegulator_Disable.SetActive(false);
        iv_Rack_flowEff.SetActive(false);






        IV_Patient();
    }
    void Update()
    {
        Zoom();
        Move();
        RayPoint();
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
                        RaycastHit[] hits = Physics.RaycastAll(ray, 100);
                        for (int i = 0; i < hits.Length; i++)
                        {
                            RaycastHit hit = hits[i];
                            if (hit.collider.CompareTag("RaycastTarget"))
                            {
                                if (hit.collider.name == myName)
                                {
                                    print("click!");
                                    IV_Patient();
                                    return;
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
                                    print("dragdrop!");
                                    IV_Patient();
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
        }
    }
    #endregion


    #region 정맥주사

    public void IV_Patient()
    {
        SetMission();
        SetOutLine();
        _rayState = RayState.none;
        myCam = null;
        myName = "";
        myTargetName = "";
        btnNum = -1;
        switch (_ivState)
        {
            case IVState.start:
                {

                    SetMission("컴퓨터 앞으로 이동하세요.");
                    exclam_Computer.SetActive(true);
                    //
                    _rayState = RayState.click;
                    myCam = cam_main;
                    myName = colli_Computer.name;

                    NextPatientStep_IV();

                    break;
                }
            case IVState.chart:
                {



                    exclam_Computer.SetActive(false);
                    chartPanal.SetActive(true);
                    //


                    NextPatientStep_IV();

                    break;
                }
            case IVState.sink1:
                {

                    chartPanal.SetActive(false);
                    //
                    SetMission("정맥투약에 필요한 물품들을\n모두 고르시오");
                    //

                    exclam_sink.SetActive(true);
                    _rayState = RayState.click;
                    myCam = cam_main;
                    myName = colli_Sink.name;


                    NextPatientStep_IV();
                    break;
                }
            case IVState.sink2:
                {

                    isMove = false;
                    _videoPlayer.Play();
                    FadeOut(IV_Patient);
                    NextPatientStep_IV();

                    break;
                }
            case IVState.sink3:
                {

                    exclam_sink.SetActive(false);
                    _videoPlayer.time = 0;
                    IV_Patient((float)_videoPlayer.clip.length - 1f);
                    videoPanal.SetActive(true);
                    NextPatientStep_IV();

                    break;
                }
            case IVState.sink4:
                {

                    FadeOut(IV_Patient);
                    NextPatientStep_IV();

                    break;
                }
            case IVState.quiz1:
                {
                    videoPanal.SetActive(false);
                    exclam_sink.SetActive(false);
                    _videoPlayer.Stop();

                    SetMission("정맥투약에 필요한 물품들을\n모두 고르시오");
                    quizPanal.SetActive(true);
                    transform.position = tr_Sink.position;
                    transform.rotation = tr_Sink.rotation;
                    NextPatientStep_IV();
                    break;
                }
            case IVState.quiz2:
                {
                    FadeOut(IV_Patient);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.cart1:
                {
                    //cam_cart.gameObject.SetActive(true);
                    cam_main.gameObject.SetActive(false);
                    cam_cart.gameObject.SetActive(true);

                    toolButton.SetActive(true);
                    SetMission("필요 물품들을 사용하여 수액백과 정맥 주사용\n수액세트를 연결하여 수액을 준비하도록 하자.");
                    SetToolButton(1);
                    NextPatientStep_IV();
                    //수액버튼 클릭
                    break;
                }
            case IVState.cart2:
                {

                    //Intravenous_inCart.SetActive(false);
                    iv_Select.SetActive(true);

                    SetToolButton(2);
                    NextPatientStep_IV();
                    //투약카드 버튼 클릭
                    break;
                }
            case IVState.cart3:
                {

                    //medicationcard_inCart.SetActive(false);
                    medicationcard_Select.SetActive(true);

                    _rayState = RayState.click;
                    myCam = cam_cart;
                    myName = iv_Select.name;
                    SetOutLine(iv_Select);
                    //수액 선택
                    NextPatientStep_IV();
                    break;
                }
            case IVState.cart4:
                {
                    //투약카드 수액에 붙음
                    medicationcard_Select.SetActive(false);
                    medicationcard_in_IV.SetActive(true);


                    SetToolButton(3);
                    //수액세트 선택
                    NextPatientStep_IV();
                    break;
                }
            case IVState.cart5:
                {

                    ivSet.SetActive(true);
                    //iv_Select.SetActive(false);
                    //iv_Select_Side.SetActive(true);


                    _rayState = RayState.click;
                    myCam = cam_cart;
                    myName = ivSet_FlowRegulator_Enable.name;
                    SetOutLine(ivSet_FlowRegulator_Enable);
                    //수액조절기 선택
                    NextPatientStep_IV();
                    break;
                }
            case IVState.cart6:
                {

                    ivSet_FlowRegulator_Disable.SetActive(true);
                    ivSet_FlowRegulator_Enable.SetActive(false);

                    //마개 선택
                    _rayState = RayState.click;
                    myCam = cam_cart;
                    myName = iv_Select_BluePlug.name;
                    SetOutLine(iv_Select_BluePlug);

                    NextPatientStep_IV();
                    break;
                }
            case IVState.cart7:
                {

                    //ivSet_FlowRegulator_Off.SetActive(true);
                    iv_Select_BluePlug.SetActive(false);

                    //수액을 관에 드래그앤드랍

                    _rayState = RayState.dragdrop;
                    myCam = cam_cart;
                    myName = iv_Select.name;
                    myTargetName = colli_IVSet_ivTube.name;
                    SetOutLine(colli_IVSet_ivTube.gameObject);

                    NextPatientStep_IV();
                    break;
                }
            case IVState.cart8:
                {
                    iv_Select.SetActive(false);
                    colli_IVSet_ivTube.gameObject.SetActive(false);
                    iv_inIVset.SetActive(true);




                    IV_Patient(0.5f);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.cart9:
                {
                    FadeOut(IV_Patient);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed1:
                {
                    cartObj.SetActive(false);
                    bedObj.SetActive(true);
                    cam_cart.gameObject.SetActive(false);
                    cam_bed.gameObject.SetActive(true);
                    //랙 선택
                    SetToolButton(4);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed2:
                {
                    colli_iv_Rack.gameObject.SetActive(true);
                    //수액세트 ㅅ ㅓㄴ택
                    SetToolButton(3);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed3:
                {

                    ivSet_bed.SetActive(true);

                    //수액 걸기

                    _rayState = RayState.dragdrop;
                    myCam = cam_bed;
                    myName = ivSet_bed.name;
                    myTargetName = colli_iv_Rack.name;


                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed4:
                {
                    ivSet_bed.SetActive(false);
                    ivSet_in_Rack.SetActive(true);
                    NextPatientStep_IV();
                    IV_Patient();
                    break;


                }
            case IVState.chamber1:
                {
                    //점적통 클릭 설정
                    _rayState = RayState.click;
                    myCam = cam_bed;
                    myName = iv_Chamber.name;
                    SetOutLine(iv_Chamber);
                    NextPatientStep_IV();
                    break;
                }

            case IVState.chamber2:
                {
                    //점적통 클릭 인식
                    chamberNum++;
                    if (chamberNum < 3)
                    {
                        _ivState = IVState.chamber1;
                        IV_Patient(1f);
                    }
                    else //다 클릭했을 때 
                    {
                        FadeOut(IV_Patient);
                        NextPatientStep_IV();
                    }

                    break;
                }
            case IVState.chamber3:
                {
                    //점적통 줌인 카메라로 이펙트 보여주기
                    cam_bed.gameObject.SetActive(false);
                    cam_Chamber.gameObject.SetActive(true);

                    iv_Chamber_waterEff.SetActive(true);

                    NextPatientStep_IV();

                    IV_Patient(1.5f);
                    break;
                }
            case IVState.chamber4:
                {
                    //페이드 아웃
                    FadeOut(IV_Patient);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed5:
                {
                    //전체샷
                    cam_Chamber.gameObject.SetActive(false);
                    cam_bed.gameObject.SetActive(true);
                    //조절기 클릭하기
                    _rayState = RayState.click;
                    myCam = cam_bed;
                    myName = ivSet_inRack_FlowRegulator_Enable.name;
                    SetOutLine(ivSet_inRack_FlowRegulator_Enable);

                    break;
                }
            case IVState.bed6:
                {
                    //유량조절기 클릭
                    ivSet_inRack_FlowRegulator_Enable.SetActive(false);
                    ivSet_inRack_FlowRegulator_Disable.SetActive(true);
                    //2초뒤실행

                    IV_Patient(2f);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed7:
                {
                    //수액 끝에서 물 나오는 이펙트 on
                    iv_Rack_flowEff.SetActive(true);
                    //0.5초뒤 가능하게
                    IV_Patient(0.5f);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed8:
                {

                    //유량조절기 상호작용세팅
                    _rayState = RayState.click;
                    myCam = cam_bed;                                                    
                    myName = ivSet_inRack_FlowRegulator_Disable.name;
                    SetOutLine(ivSet_inRack_FlowRegulator_Disable);
                    break;
                }

            case IVState.bed9:
                {

                    //수액 끝에서 물 나오는 이펙트 off
                    iv_Rack_flowEff.SetActive(true);

                    IV_Patient(1f);
                    break;
                }
            case IVState.bed10:
                {

                    FadeOut(IV_Patient);
                    NextPatientStep_IV();
                    break;
                }
            case IVState.bed11:
                {

                    FadeOut(IV_Patient);
                    NextPatientStep_IV();
                    break;
                }

        }
    }



    public void IV_Patient(float timer)
    {
        Invoke("IV_Patient", timer);
    }


    void NextPatientStep_IV()
    {
        int i = (int)_ivState;
        i++;
        _ivState = (IVState)i;
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



    public void OnClickQuizApplyButton()
    {
        for (int i = 0; i < answerToggles.Length; i++)
        {
            if (answerToggles[i].isOn != quizAnswer.Contains(i + 1))
            {
                SetNotice("틀렸습니다. 다시 풀어주세요.");
                return;
            }
        }

        quizPanal.SetActive(false);

        SetNotice("정답입니다.\n이제 진료실로 이동해 근육주사를 진행해주세요.", IV_Patient);
    }

    void SetToolButton(int iii)
    {

        _rayState = RayState.button;
        btnNum = iii;
    }

    void SetOutLine(GameObject obj = null, float width = 3f )
    {
        if (!obj)
        {
            if (ol)
            {
                ol.enabled = false;
                ol = null;
            }

            return;
        }
        if (obj.GetComponent<Outline>())
        {
            ol = obj.GetComponent<Outline>();
        }
        else
        {

            ol = obj.AddComponent<Outline>();
        }
        ol.OutlineColor = Color.yellow;
        ol.OutlineWidth = width;
        ol.enabled = true;
    }

    #endregion
}
