using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Player2 : MonoBehaviour
{
    public enum State
    {
        Start,
        Beginning,
        Town,
        Patients,
        SG,
        Patients_Diabates,
        SG_Diabates,
        SG_Diabates_Insulin,
    }
    public State _state;

    public enum StartState
    {
        Cloth,
        Computer,
        Phone,
        Bag,
        Item,
        End
    }

    public enum PatientState
    {
        Prologue,
        Start,
        Pat,
        Daewha,
        Anim,
        Question,
        End,        
    }

    public enum SGState
    {
        SG1,
        SG2,
        SG3,
        SG4,
        SG5,
        SG6,
        SG7,
        SG8,
        SG9,
        SG10,
        SG11,
        SG12,
        SG13,
        SG14,
        SG15,
        SG16,
        SG17,
        SG18,
        SG19,
        SG20,
        SG21,
        SG22,
        SG23,
        SG24,
        SG25,
        SG26,
        SG27,
        SG28,
        SG29,
        SG30,
        SG31,
        SG32,
    }
    public SGState _sgstate;

    delegate void GetRay();
    [Header("공통")]
    public GameObject portal;
    public StartState _startstate;        
    public int moveSpeed;
    public GameObject fadeOut;
    public GameObject notice;
    public Animator anim;
    public GameObject lookCamera;
    public float rotateSpeed;
    Quaternion cameraFixRotation;
    Vector3 dir;

    [Header("Scene1")]
    public GameObject questionPanel;
    public GameObject chartPanel;
    public GameObject mainQuestPanel;
    public GameObject missionText;
    public GameObject bag;
    public GameObject callPanel;
    public BoxCollider[] door;
    public GameObject[] noticeObj;
    public GameObject[] startObj;
    public List<GameObject> myItems = new List<GameObject>();
    public Toggle[] questionToggle;
    public List<bool> myAnswer;
    public GameObject videoPanel;
    public GameObject handvideoPanel;
    public GameObject notice1_success;
    public GameObject notice1_fail;

    [Header("Scene3")]
    public GameObject patient;
    public GameObject patientMale;
    public GameObject playerView;
    NavMeshAgent maleAgent;
    Animator patientAnim;
    Animator patientMaleAnim;
    public GameObject question;
    public Transform maleTr;
    public GameObject reButton;    
    bool isStart;    

    [Header("Chat")]
    public PatientState _patientState;
    public GameObject conver;
    public string[] daehwa;
    public int converNum = -1;
    int answerNum;
    public int stateAnswerNum;
    public int stateConverNum;
    [TextArea]
    public string stateConverString;
    public bool isDaewha;
    public Text nickname;
    public Text daehwaName;
    ChatController chatManager;
    public Image[] call;
    public Sprite[] callSay;
    public AudioClip[] voices;
    public AudioSource _audio;

    [Header("SG")]
    public Toggle toolToggle;
    public GameObject[] hand;//0은 맨손, 1은 글러브, 2는 씻기
    public GameObject maincam;
    public GameObject servecam;
    public GameObject handcam;
    public GameObject allProp;
    public GameObject gauze;
    public GameObject sodoc;
    public GameObject chahyul;
    public GameObject chahyulChim;
    public GameObject measure;
    public GameObject measurePaper;
    public GameObject blood;
    public GameObject myinput;
    public GameObject resultinput;
    public GameObject result;
    public GameObject finalPanel;
    InputField myinputfield;
    public GameObject myInputFail;
    public InputField[] resultInputs;
    public GameObject[] handSodoc;
    public GameObject curtain;//No8
    public GameObject waterproof;
    public GameObject plaster;
    public GameObject pincet;
    public GameObject hydrogel;
    public GameObject mediform;
    public GameObject cottons;
    public GameObject myOBJ;
    public BoxCollider hip;
    public Material[] cottonMt;
    public GameObject[] messengerImage;
    public Animator[] toolsAnim;
    public Text[] resultText;
    public InputField[] resultInput;
    Animator nowAnim;



    [Header("SG_insulin")]
    public GameObject insulin_Chart;
    public GameObject insulinAmple;
    public Animator patient_Anim;
    public Camera cam_main;
    public Camera cam_Behind;
    public GameObject syPosPanel;
    public GameObject cotton_Swap;
    public GameObject sy_Hand;
    public GameObject sylenge_inject;
    public GameObject sy_Sheet;


    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
        Time.timeScale = 5;
#endif


        if (_state == State.Patients || _state == State.Patients_Diabates)
        {
            patientAnim = patient.GetComponent<Animator>();
            patientMaleAnim = patientMale.GetComponent<Animator>();
            maleAgent = patientMale.GetComponent<NavMeshAgent>();
            _patientState = PatientState.Prologue;            
        }

        if (_state == State.SG)
        {
            patientAnim = patient.GetComponent<Animator>();
            _sgstate = SGState.SG1;
            servecam.SetActive(false);
            maincam.SetActive(true);
            myinputfield = myinput.transform.GetChild(0).GetComponent<InputField>();
            SetAnim(0);
        }

        else if (_state == State.SG_Diabates || _state == State.SG_Diabates_Insulin)
        {
            patientAnim = patient.GetComponent<Animator>();
            _sgstate = SGState.SG1;
            servecam.SetActive(false);
            maincam.SetActive(true);
            myinputfield = myinput.transform.GetChild(0).GetComponent<InputField>();

            if (_state == State.SG_Diabates_Insulin)
            {
                cam_main = Camera.main;
                SetMessage("R-I Scale을 확인 후, 투약카드와 대조하자.");
            }
        }

        else
            isStart = true;

        chatManager = GetComponent<ChatController>();
        cameraFixRotation = lookCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)_state > 0 && isStart)
        {
            Move();
            RayPoint();
        }        
        
        switch ((int)_state)
        {
            case 1:
                Beginning();
                break;

            case 3:
                Patients();                
                break;

            case 5:
                Patients_Diabates();
                break;

            case 4:
                SG();
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    for (int i = 0; i < resultInputs.Length; i++)
                    {
                        if (resultInputs[i].isFocused)
                        {
                            if (i == 5)
                                return;
                            resultInputs[i + 1].Select();
                            break;
                        }
                    }
                }
                break;

            case 6:
                SG_Diabates();
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    for (int i = 0; i < resultInputs.Length; i++)
                    {
                        if (resultInputs[i].isFocused)
                        {
                            if (i == 5)
                                return;
                            resultInputs[i + 1].Select();
                            break;
                        }
                    }
                }
                break;

            case 7:
                SG_Diabates_Insulin();
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    for (int i = 0; i < resultInputs.Length; i++)
                    {
                        if (resultInputs[i].isFocused)
                        {
                            if (i == 5)
                                return;
                            resultInputs[i + 1].Select();
                            break;
                        }
                    }
                }
                break;
        }
    }

    void Beginning()
    {
        if (_startstate == StartState.Cloth)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray , out hit , 100 , 1 << 0))
                {
                    if (hit.collider.tag.Equals("Cloth"))
                    {                        
                        _startstate = StartState.Computer;
                        noticeObj[0].SetActive(false);
                        noticeObj[1].SetActive(true);
                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }            
        }

        else if (_startstate == StartState.Computer)
        {            
            if (Input.GetMouseButtonDown(0))
            {
                Action act = () =>
                {
                    missionText.transform.parent.gameObject.SetActive(false);
                    chartPanel.SetActive(true);
                    _startstate = StartState.Phone;
                    noticeObj[1].SetActive(false);
                    noticeObj[2].SetActive(true);
                    startObj[1].GetComponent<Outline>().enabled = false;
                    startObj[2].GetComponent<Outline>().enabled = true;
                };
                RayPoint("Computer", act);
            }                  
        }

        else if (_startstate == StartState.Phone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Action act = () =>
                {
                    GameObject.FindGameObjectWithTag("Phone").GetComponent<AudioSource>().Stop();
                    callPanel.SetActive(true);                    
                    isDaewha = false;
                    converNum++;
                    SetVoice(0);
                    string[] test = daehwa[converNum].Split('_');
                    StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                    startObj[2].GetComponent<Outline>().enabled = false;
                    startObj[3].GetComponent<Outline>().enabled = true;
                };
                RayPoint("Phone", act);
            }           
        }

        else if (_startstate == StartState.Bag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Action act = () =>
                {
                    bag.SetActive(true);
                    startObj[0].GetComponent<Outline>().enabled = true;
                    missionText.transform.parent.gameObject.SetActive(false);
                    noticeObj[3].SetActive(false);
                    _startstate = StartState.Item;
                    startObj[3].SetActive(false);
                    startObj[3].GetComponent<Outline>().enabled = false;
                    for (int i = 0; i < startObj[2].transform.childCount; i++)
                    {
                        startObj[2].transform.GetChild(i).gameObject.layer = 6;
                    }
                    
                };
                RayPoint("Bag" , act);                
            }            
        }

        else if (_startstate == StartState.Item)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Action act = () =>
                {
                    for (int i = 0; i < startObj[2].transform.childCount; i++)
                    {
                        startObj[2].transform.GetChild(i).gameObject.layer = 0;
                    }
                    _startstate = StartState.End;
                    questionPanel.SetActive(true);
                    videoPanel.SetActive(true);
                    startObj[0].GetComponent<Outline>().enabled = false;
                    startObj[0].GetComponent<BoxCollider>().enabled = false;
                };
                RayPoint("Item" , act);                
            }  
        }

        else if (_startstate == StartState.End)
        {
            interval += Time.deltaTime;
            if (interval > 8)
            {
                interval = 0;
                _startstate = StartState.Item;
                videoPanel.SetActive(false);
            }
        }
    }

    Action sgact;
    public GameObject nextButton;
    float interval;
    public bool isSG;
    #region SG
    void SG()
    {
        switch (_sgstate)
        {
            case SGState.SG1:
                if (isSG)
                {
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(1);
                            SetMessage("물품 꺼내기 버튼을 눌러 보건 가방 안의 물건을\n이동식 카트 위에 올려주세요.");
                        };
                    }
                }
                break;

            case SGState.SG2:
                RayPoint();
                if (isSG)
                {
                    for (int i = 0; i < allProp.transform.childCount; i++)
                    {
                        allProp.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    gauze.SetActive(false);
                    nextButton.SetActive(true);
                    missionText.transform.parent.gameObject.SetActive(false);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(2);
                        SetMessage("드레싱 세트에 필요한 물품들을 추가해주세요.");
                    };
                }
                break;

            case SGState.SG3:
                if (isSG)
                {
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(3);
                        SetMessage("드레싱 세트에 필요한 물품들을 추가해주세요.");
                    };
                }
                break;

            case SGState.SG4:
                if (isSG)
                {
                    cottons.SetActive(true);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;
                        missionText.transform.parent.gameObject.SetActive(false);
                        fadeOut.SetActive(true);
                        toolToggle.isOn = false;
                        Invoke("GoPatient" , 2);
                    };
                }
                break;

            case SGState.SG5:               
                sgact = () =>
                {
                    myinput.SetActive(false);
                    myinputfield.text = "";
                    missionText.transform.parent.gameObject.SetActive(false);
                    SetAnim(0);
                };
                break;

            case SGState.SG6:
                if (isSG)
                {
                    handSodoc[1].SetActive(true);
                    handSodoc[1].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 4)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            handSodoc[1].SetActive(false);
                            handSodoc[1].GetComponent<Animator>().enabled = false;
                            interval = 0;
                            isSG = false;
                            toolToggle.isOn = false;
                        };
                    }                   
                }
                break;

            case SGState.SG7:
                nextButton.SetActive(true);
                myinput.SetActive(true);
                messengerImage[1].SetActive(true);
                messengerImage[0].SetActive(false);
                sgact = () =>
                {
                    myinputfield.text = "";
                    isSG = false;                    
                };
                break;

            case SGState.SG8:           
                messengerImage[2].SetActive(true);
                messengerImage[1].SetActive(false);                
                sgact = () =>
                {
                    myinput.SetActive(false);
                    myinputfield.text = "";
                    missionText.transform.parent.gameObject.SetActive(false);
                    SetAnim(4);
                };                
                break;

            case SGState.SG9:
                if (isSG)
                {
                    curtain.SetActive(true);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(5);
                    };
                }               
                break;

            case SGState.SG10:
                if (isSG)
                {
                    patientAnim.SetTrigger("Left");
                    interval += Time.deltaTime;
                    if (interval > 2)
                    {                        
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            SetAnim(6);
                            isSG = false;
                            interval = 0;
                        };
                    }                    
                }
                break;

            case SGState.SG11:
                if (isSG)
                {
                    waterproof.SetActive(true);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        SetAnim(0);
                        isSG = false;
                        handSodoc[1].GetComponent<Animator>().Rebind();
                    };
                }
                break;

            case SGState.SG12:
                if (isSG)
                {
                    handSodoc[1].SetActive(true);
                    handSodoc[1].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 4)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            SetAnim(7);
                            handSodoc[1].SetActive(false);
                            handSodoc[1].GetComponent<Animator>().enabled = false;
                            interval = 0;
                            isSG = false;
                        };
                    }
                }
                break;

            case SGState.SG13:
                if (isSG)
                {
                    hand[0].SetActive(false);
                    hand[1].SetActive(true);
                    nextButton.SetActive(true);
                    patientAnim.SetTrigger("Hip");
                    sgact = () =>
                    {
                        isSG = false;
                        SetMessage("메디폼을 클릭해 제거해주세요.");
                    };
                }
                break;

            case SGState.SG14://
                if (isSG)
                {
                    interval += Time.deltaTime;
                    if (interval > 1 && interval < 2)
                    {
                        plaster.transform.parent = hand[1].transform.GetChild(1);
                    }

                    else if (interval > 2)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            missionText.transform.parent.gameObject.SetActive(false);
                            SetAnim(12);
                            interval = 0;
                            isSG = false;                            
                        };
                    }                    
                }

                else
                {
                    RayPoint();
                }
                break;

            case SGState.SG15://떼어낸 거즈와 폴리글러브를 같이 폐기물에 버린다
                if (isSG)
                {
                    hand[1].SetActive(false);
                    hand[0].SetActive(true);
                    plaster.SetActive(false);
                    nextButton.SetActive(true);                    
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(0);
                    };
                }
                break;

            case SGState.SG16://손소독제로 손위생을 실시한다
                if (isSG)
                {
                    handSodoc[1].SetActive(true);
                    handSodoc[1].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 4)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            handSodoc[1].SetActive(false);
                            handSodoc[1].GetComponent<Animator>().enabled = false;
                            interval = 0;
                            isSG = false;
                            SetAnim(8);
                        };
                    }
                }
                break;

            case SGState.SG17://소독된 셑트를 열고 손소독을 실시한후 멸균장갑을 착용 > 멸균장갑 클릭
                if (isSG)
                {
                    hand[1].SetActive(true);
                    hand[1].GetComponent<Animator>().enabled = false;
                    hand[0].SetActive(false);                    
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(9);
                    };
                }
                break;

            case SGState.SG18://포셉아이콘을 클릭하면 플레이어의 손에 포셉이 쥐어진다.
                if (isSG)
                {
                    pincet.SetActive(true);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(3);
                    };
                }
                break;

            case SGState.SG19://포셉을 손에 쥔 체로 소독솜 아이콘을 클릭하면 소독솜이 포셉 끝에 찝혀있다.
                if (isSG)
                {
                    pincet.transform.GetChild(0).gameObject.SetActive(true);
                    hip.enabled = true;
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        SetMessage("소독솜을 움직여 상처부위를 소독해주세요.");
                        isSG = false;
                    };
                }
                break;

            case SGState.SG20://1번 시행하고 나면 하얗던 소독솜이 누렇게 변하고 쓰레기통 아이콘을 클릭하여 소독솜을 버리고 소독솜 아이콘을 클릭
                if (isSG)
                {
                    pincet.transform.GetChild(0).gameObject.SetActive(false);
                    pincet.transform.GetChild(0).GetComponent<MeshRenderer>().material = cottonMt[0];
                    hand[1].transform.GetChild(1).localPosition = new Vector3(0.1096f , 0 , 0.04f);
                    hand[1].transform.GetChild(1).localEulerAngles = new Vector3(-51 , -69 , -13);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        recycleNum++;
                        pincet.GetComponent<Pincet>().num = 0;                        
                        isSG = false;
                    };
                }

                else
                {
                    pincet.GetComponent<BoxCollider>().enabled = true;
                    RayPoint();                    
                }
                break;

            case SGState.SG21://거즈아이콘을 누르면 핀셋 끝에 거즈가 찝혀있다.
                if (isSG)
                {
                    missionText.transform.parent.gameObject.SetActive(false);
                    pincet.transform.GetChild(1).gameObject.SetActive(true);                    
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        SetMessage("거즈를 움직여 물기를 닦아주세요.");
                        isSG = false;
                    };
                }
                break;

            case SGState.SG22://핀셋을 드레그하여 상처부위 위로 가져가면 거즈로 상처부위를 톡톡두드리는 애니메이션이 나오며 물기를 제거한다.
                if (isSG)
                {
                    interval += Time.deltaTime;
                    missionText.transform.parent.gameObject.SetActive(false);
                    if (interval > 3)
                    {                                             
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            hand[1].GetComponent<Animator>().enabled = false;                            
                            SetAnim(12);
                            interval = 0;
                            isSG = false;
                        };
                    }                    
                }

                else
                {
                    RayPoint();
                }
                break;

            case SGState.SG23://쓰레기통 아이콘을 누르면 핀셋과 거즈 둘 다 사라진다.
                if (isSG)
                {
                    pincet.SetActive(false);
                    nextButton.SetActive(true);
                    hand[1].transform.GetChild(1).localPosition = new Vector3(0.1096f , 0 , 0.04f);
                    hand[1].transform.GetChild(1).localEulerAngles = new Vector3(-51 , -69 , -13);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(10);
                    };
                }
                break;

            case SGState.SG24://하이드로 겔 아이콘을 누르면 플레이어의 손에 하이드로 겔이라는 연고가 잡힌다.
                if (isSG)
                {
                    hydrogel.SetActive(true);
                    interval += Time.deltaTime;
                    if (interval > 3)
                    {                        
                        hydrogel.SetActive(false);
                        nextButton.SetActive(true);                        
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(11);
                        };
                    }
                }
                break;

            case SGState.SG25://메디폼 아이콘을 누르면 플레이어의 손에 메디폼이 잡힌다.
                if (isSG)
                {
                    mediform.SetActive(true);
                    interval += Time.deltaTime;
                    if (interval > 2)
                    {
                        //mediform.SetActive(false);
                        nextButton.SetActive(true);
                        patientAnim.SetTrigger("Hipup");
                        sgact = () =>
                        {
                            hand[1].GetComponent<Animator>().enabled = false;
                            interval = 0;
                            isSG = false;                            
                        };
                    }
                }
                break;

            case SGState.SG26://쓰레기통 아이콘을 누르면 핀셋과 거즈 둘 다 사라진다.
                _sgstate = SGState.SG27;
                if (isSG)
                {
                    //patientAnim.SetTrigger("Front");                    
                    interval += Time.deltaTime;
                    if (interval > 2)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {                            
                            isSG = false;
                            interval = 0;
                            toolToggle.isOn = false;
                        };
                    }
                }
                break;

            case SGState.SG27:
                myinput.SetActive(true);
                messengerImage[2].SetActive(false);
                messengerImage[3].SetActive(true);
                nextButton.SetActive(true);
                sgact = () =>
                {
                    myinputfield.text = "";
                    myinput.SetActive(false);
                    isSG = false;
                    interval = 0;
                    SetAnim(4);
                };
                break;

            case SGState.SG28:
                if (isSG)
                {
                    curtain.SetActive(false);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        fadeOut.SetActive(true);
                        Invoke("ReturnPatient" , 2);
                        isSG = false;                     
                    };
                }
                break;

            case SGState.SG29:
                if (isSG)
                {
                    for (int i = 0; i < allProp.transform.childCount; i++)
                    {
                        allProp.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    cottons.SetActive(false);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        SetAnim(0);
                        isSG = false;
                    };
                }
                break;

            case SGState.SG30:
                if (isSG)
                {
                    hand[2].SetActive(true);
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            toolToggle.isOn = false;
                        };
                    }
                }
                break;

            case SGState.SG31:
                resultinput.SetActive(true);
                messengerImage[3].SetActive(false);
                messengerImage[4].SetActive(true);
                nextButton.SetActive(true);
                sgact = () =>
                {
                    resultText[0].text = DateTime.Now.ToString("yyyy-MM-dd");
                    for (int i = 0; i < resultInputs.Length; i++)
                    {
                        resultText[i + 1].text = resultInputs[i].text;
                    }
                    resultinput.SetActive(false);                                     
                    result.SetActive(true);
                };
                break;
        }       
    }

    void SG_Diabates()
    {
        switch (_sgstate)
        {
            case SGState.SG1:

                if (isSG)
                {
                    hand[2].SetActive(true);
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(1);                            
                            myinput.SetActive(true);
                        };
                    }
                }
                break;

            case SGState.SG2:
                sgact = () =>
                {
                    isSG = false;                    
                    messengerImage[1].SetActive(true);
                    messengerImage[0].SetActive(false);
                    myinputfield.text = "";
                    myinput.SetActive(true);                    
                };
                break;

            case SGState.SG3:     //3P           
                sgact = () =>
                {
                    isSG = false;                    
                    messengerImage[2].SetActive(true);
                    messengerImage[1].SetActive(false);
                    myinputfield.text = "";
                    myinput.SetActive(true);                    
                };
                break;

            case SGState.SG4://4P
                sgact = () =>//5p
                {
                    myinput.SetActive(false);
                    isSG = false;
                    SetMessage("환자의 피부상태를 확인하자");                    
                };
                break;

            case SGState.SG5://5p

                if (isSG)
                {
                    handcam.SetActive(true);
                    maincam.SetActive(false);
                    missionText.transform.parent.gameObject.SetActive(false);
                    nextButton.SetActive(true);
                    sgact = () =>//6p
                    {
                        isSG = false;                        
                        chahyul.SetActive(true);
                        chahyulChim.SetActive(true);
                        SetMessage("환자의 피부상태에 맞게 채혈침 삽입 깊이를 조절하자");
                    };
                }               
                break;

            case SGState.SG6:
                if (isSG)
                {
                    chahyul.GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 4)
                    {
                        nextButton.SetActive(true);
                    }
                    sgact = () =>
                    {                        
                        isSG = false;
                        measure.SetActive(true);
                        nextButton.SetActive(false);
                        SetMessage("혈당 측정기의 전원을 작동 시키자.");
                        interval = 0;
                    };
                }                  
               
                break;

            case SGState.SG7:
                if (isSG)
                {
                    measure.transform.GetChild(4).gameObject.SetActive(true);                    
                    nextButton.SetActive(true);
                    sgact = () =>
                    {                        
                        isSG = false;
                        nextButton.SetActive(false);
                        SetMessage("혈당 측정기에 검사지를 삽입 시키자.");
                    };
                }
                break;

            case SGState.SG8:
                if (isSG)
                {
                    measurePaper.SetActive(true);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;
                        nextButton.SetActive(false);
                        patient.SetActive(false);
                        hand[0].SetActive(true);
                        SetMessage("소독솜을 이용하여 채혈부위를 소독하자.");                        
                    };
                }                
                break;

            case SGState.SG9:
                if (isSG)
                {
                    sodoc.SetActive(true);
                    interval += Time.deltaTime;
                    if (interval > 4)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            isSG = false;
                            nextButton.SetActive(false);
                            SetMessage("채혈침을 이용해 환자 손가락 끝부분을 천자 하자.");
                            sodoc.SetActive(false);
                            interval = 0;
                        };
                    }
                }
                break;

            case SGState.SG10:
                if (isSG)
                {
                    chahyul.GetComponent<Animator>().SetInteger("Move" , 1);
                    interval += Time.deltaTime;
                    if (interval > 1.5f)
                    {
                        blood.SetActive(true);
                        nextButton.SetActive(true);
                        //chahyul.SetActive(false);                        
                        sgact = () =>
                        {
                            chahyul.GetComponent<Animator>().Rebind();
                            chahyul.GetComponent<Animator>().enabled = false;
                            isSG = false;
                            nextButton.SetActive(false);
                            SetMessage("환자의 혈액방울을 검사지에 묻히자.");
                            interval = 0;
                        };
                    }
                }
                break;

            case SGState.SG11:
                if (isSG)
                {
                    measure.GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 1.5f)
                    {
                        blood.SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {                            
                            isSG = false;
                            interval = 0;
                            SetMessage("혈당측정에 사용한 손상성폐기물을 처리하자.");
                        };
                    }
                    
                }
                break;

            case SGState.SG12:
                if (isSG)
                {
                    chahyul.SetActive(false);
                    measure.SetActive(false);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        nextButton.SetActive(false);
                        isSG = false;
                        myinput.SetActive(true);
                        messengerImage[3].SetActive(true);
                        messengerImage[2].SetActive(false);                        
                    };
                }
                break;

            case SGState.SG13:           
                sgact = () =>
                {
                    myinput.SetActive(false);
                    isSG = false;
                    SetMessage("손 소독을 진행한다.");
                };
                break;

            case SGState.SG14://
                if (isSG)
                {
                    handcam.SetActive(false);
                    maincam.SetActive(true);
                    hand[2].SetActive(true);
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;                            
                            myinput.SetActive(true);
                            messengerImage[4].SetActive(true);
                            messengerImage[3].SetActive(false);
                        };
                    }
                }
                break;

            case SGState.SG15://떼어낸 거즈와 폴리글러브를 같이 폐기물에 버린다                
                sgact = () =>
                {
                    myinput.SetActive(false);
                    SceneManager.LoadScene(9);
                    //finalPanel.SetActive(true);
                };
                break;
        }
    }

    void SG_Diabates_Insulin()
    {
        switch (_sgstate)
        {
            case SGState.SG1:
                if (isSG)
                {                    
                    sgact = () =>
                    {                        
                        isSG = false;
                        //SetMessage("손 소독을 진행한다.");
                    };
                }
                break;

            case SGState.SG2:
                SetMessage("손 소독을 진행한다.");
                if (isSG)
                {
                    hand[2].SetActive(true);
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(1);

                            SetMessage("인슐린을 준비하자.");

                            //myinput.SetActive(true);
                        };
                    }
                }                
                break;

            case SGState.SG3:     //3P                                  
                if(isSG)
                {
                    insulinAmple.SetActive(true);
                    insulin_Chart.SetActive(true);
                    interval += Time.deltaTime;
                    if (interval > 7)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {

                            insulinAmple.SetActive(false);
                            insulin_Chart.SetActive(false);
                            interval = 0;
                            isSG = false;

                            SetAnim(1);


                            //myinput.SetActive(true);
                        };
                    }

                    //sgact = () =>
                    //{
                    //    isSG = false;

                    //    insulinAmple.SetActive(true);

                    //messengerImage[2].SetActive(true);
                    //messengerImage[1].SetActive(false);
                    //myinputfield.text = "";
                    //myinput.SetActive(true);
                    //};
                }
                break;

            case SGState.SG4://4P

                SetMessage("손 소독을 진행한다.");
                if (isSG)
                {
                    hand[2].SetActive(true);
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(1);
                            missionText.transform.parent.gameObject.SetActive(false);
                            //SetMessage("인슐린을 준비하자.");

                            myinput.SetActive(true);
                            myinputfield.text = "";
                        };
                    }
                }
                break;

            case SGState.SG5://5p
                sgact = () =>
                {
                    isSG = false;
                    messengerImage[1].SetActive(true);
                    messengerImage[0].SetActive(false);
                    myinputfield.text = "";
                    myinput.SetActive(true);
                };
                //if (isSG)
                //{

                //    messengerImage[1].SetActive(true);
                //    messengerImage[0].SetActive(false);
                //    myinputfield.text = "";
                //    myinput.SetActive(true);
                //    isSG = false;


                //    //handcam.SetActive(true);
                //    //maincam.SetActive(false);
                //    //missionText.transform.parent.gameObject.SetActive(false);
                //    //nextButton.SetActive(true);
                //    //sgact = () =>//6p
                //    //{
                //    //    isSG = false;
                //    //    chahyul.SetActive(true);
                //    //    chahyulChim.SetActive(true);
                //    //    SetMessage("환자의 피부상태에 맞게 채혈침 삽입 깊이를 조절하자");
                //    //};
                //}
                break;

            case SGState.SG6:

                sgact = () =>
                {
                    isSG = false;
                    messengerImage[2].SetActive(true);
                    messengerImage[1].SetActive(false);
                    myinputfield.text = "";
                    myinput.SetActive(true);
                };



                //if (isSG)
                //{
                //    chahyul.GetComponent<Animator>().enabled = true;
                //    interval += Time.deltaTime;
                //    if (interval > 4)
                //    {
                //        nextButton.SetActive(true);
                //    }
                //    sgact = () =>
                //    {
                //        isSG = false;
                //        measure.SetActive(true);
                //        nextButton.SetActive(false);
                //        SetMessage("혈당 측정기의 전원을 작동 시키자.");
                //        interval = 0;
                //    };
                //}

                break;

            case SGState.SG7:

                sgact = () =>
                {
                    isSG = false;
                    messengerImage[3].SetActive(true);
                    messengerImage[2].SetActive(false);
                    myinputfield.text = "";
                    myinput.SetActive(false);
                    cam_main.gameObject.SetActive(false);
                    cam_Behind.gameObject.SetActive(true);
                    patient_Anim.SetTrigger("Left");
                    patient_Anim.SetTrigger("Hip"); 
                };
                
                //if (isSG)
                //{
                //    measure.transform.GetChild(4).gameObject.SetActive(true);
                //    nextButton.SetActive(true);
                //    sgact = () =>
                //    {
                //        isSG = false;
                //        nextButton.SetActive(false);
                //        SetMessage("혈당 측정기에 검사지를 삽입 시키자.");a
                //    };
                //}
                break;

            case SGState.SG8:

                interval += Time.deltaTime;
                if (interval > 3)
                {

                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        interval = 0;
                        isSG = false;
                        SetAnim(1);

                        //SetMessage("인슐린을 준비하자.");

                        SetMessage("동일 부위에 계속 주사하지 않도록 주사 부위를 선정하자.");
                        syPosPanel.SetActive(true);
                        //myinputfield.text = "";
                    };
                }

                //if (isSG)
                //{
                //    measurePaper.SetActive(true);
                //    nextButton.SetActive(true);
                //    sgact = () =>
                //    {
                //        isSG = false;
                //        nextButton.SetActive(false);
                //        patient.SetActive(false);
                //        hand[0].SetActive(true);
                //        SetMessage("소독솜을 이용하여 채혈부위를 소독하자.");
                //    };
                //}
                break;

            case SGState.SG9:

                sgact = () =>
                {

                    syPosPanel.SetActive(false);
                    myinput.SetActive(true);
                    isSG = false;
                    missionText.transform.parent.gameObject.SetActive(false);
                };

                //if (isSG)
                //{
                //    sodoc.SetActive(true);
                //    interval += Time.deltaTime;
                //    if (interval > 4)
                //    {
                //        nextButton.SetActive(true);
                //        sgact = () =>
                //        {
                //            isSG = false;
                //            nextButton.SetActive(false);
                //            SetMessage("채혈침을 이용해 환자 손가락 끝부분을 천자 하자.");
                //            sodoc.SetActive(false);
                //            interval = 0;
                //        };
                //    }
                //}
                break;

            case SGState.SG10:

                sgact = () =>
                {
                    isSG = false;
                    messengerImage[4].SetActive(true);
                    messengerImage[3].SetActive(false);
                    myinputfield.text = "";
                    myinput.SetActive(false);
                };
                break;

            case SGState.SG11:
                SetMessage("손 소독을 진행한다.");
                if (isSG)
                {
                    hand[3].SetActive(true);
                    handSodoc[2].SetActive(true);
                    handSodoc[2].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[2].SetActive(false);
                        handSodoc[2].GetComponent<Animator>().enabled = false;
                        hand[3].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(1);

                            SetMessage("주사 부위의 피부를 소독하자.");

                            //myinput.SetActive(true);
                            //myinputfield.text = "";
                        };
                    }
                }




                //if (isSG)
                //{
                //    measure.GetComponent<Animator>().enabled = true;
                //    interval += Time.deltaTime;
                //    if (interval > 1.5f)
                //    {
                //        blood.SetActive(false);
                //        nextButton.SetActive(true);
                //        sgact = () =>
                //        {
                //            isSG = false;
                //            interval = 0;
                //            SetMessage("혈당측정에 사용한 손상성폐기물을 처리하자.");
                //        };
                //    }

                //}
                break;

            case SGState.SG12:
                if (isSG)
                {
                    cotton_Swap.SetActive(true);
                    cotton_Swap.GetComponent<Animator>().enabled = true;
                    //chahyul.SetActive(false);
                    //measure.SetActive(false);
                    //nextButton.SetActive(true);

                    interval += Time.deltaTime;
                    if (interval > 3)
                    {
                        cotton_Swap.GetComponent<Animator>().enabled = false;
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            cotton_Swap.SetActive(false);
                            cotton_Swap.GetComponent<Animator>().enabled = false;
                            interval = 0;
                            isSG = false;
                            SetAnim(1);

                            SetMessage("인슐린을 투여하자.");

                            //myinput.SetActive(true);
                            //myinputfield.text = "";
                        };
                    }



                    //sgact = () =>
                    //{
                    //    nextButton.SetActive(false);
                    //    isSG = false;
                    //    myinput.SetActive(true);
                    //    messengerImage[3].SetActive(true);
                    //    messengerImage[2].SetActive(false);
                    //};
                }
                break;

            case SGState.SG13:
                if (isSG)
                {
                    sy_Hand.SetActive(true);
                    sylenge_inject.SetActive(true);
                    interval += Time.deltaTime;
                    if (interval > 5)
                    {

                        //patient_Anim.SetTrigger("Front");
                        //patient_Anim.SetTrigger("Hipup");
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            sy_Hand.SetActive(false);
                            sylenge_inject.SetActive(false);

                            SetMessage("주사 부위의 피부를 소독솜으로 눌러주자.");
                        };
                    }
                }
                break;

            case SGState.SG14://
                if (isSG)
                {
                    cotton_Swap.SetActive(true);
                    //chahyul.SetActive(false);
                    //measure.SetActive(false);
                    //nextButton.SetActive(true);

                    interval += Time.deltaTime;
                    if (interval > 4)
                    {
                        cotton_Swap.SetActive(false);
                        cotton_Swap.GetComponent<Animator>().enabled = false;
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(1);
                            cam_Behind.gameObject.SetActive(false);
                            cam_main.gameObject.SetActive(true);
                            
                            SetMessage("주사 부위를 기록지에 기록하자.");
                            sy_Sheet.SetActive(true);

                            //myinput.SetActive(true);
                            //myinputfield.text = "";
                        };
                    }



                    //sgact = () =>
                    //{
                    //    nextButton.SetActive(false);
                    //    isSG = false;
                    //    myinput.SetActive(true);
                    //    messengerImage[3].SetActive(true);
                    //    messengerImage[2].SetActive(false);
                    //};
                }
                break;

            case SGState.SG15://떼어낸 거즈와 폴리글러브를 같이 폐기물에 버린다                
                sgact = () =>
                {
                    sy_Sheet.SetActive(false);
                    SetMessage("인슐린 투여에 사용된 손상성 폐기물을 처리하자.");
                };
                break;

            case SGState.SG16://손소독제로 손위생을 실시한다
                if (isSG)
                {

                    //폐기물 삭제 코드


                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;

                        SetMessage("손 소독을 진행한다.");

                        nextButton.SetActive(false);
                        //myinput.SetActive(true);
                        //myinputfield.text = "";
                    };



                }
                break;

            case SGState.SG17://소독된 셑트를 열고 손소독을 실시한후 멸균장갑을 착용 > 멸균장갑 클릭
                if (isSG)
                {
                    hand[2].SetActive(true);
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            //SetMessage("인슐린을 준비하자.");
                            /////최종최종최종최종
                            missionText.transform.parent.gameObject.SetActive(false);
                            finalPanel.SetActive(true);
                        };
                    }
                }
                break;

            case SGState.SG18://포셉아이콘을 클릭하면 플레이어의 손에 포셉이 쥐어진다.
                if (isSG)
                {
                    pincet.SetActive(true);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(3);
                    };
                }
                break;

            case SGState.SG19://포셉을 손에 쥔 체로 소독솜 아이콘을 클릭하면 소독솜이 포셉 끝에 찝혀있다.
                if (isSG)
                {
                    pincet.transform.GetChild(0).gameObject.SetActive(true);
                    hip.enabled = true;
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        SetMessage("소독솜을 움직여 상처부위를 소독해주세요.");
                        isSG = false;
                    };
                }
                break;

            case SGState.SG20://1번 시행하고 나면 하얗던 소독솜이 누렇게 변하고 쓰레기통 아이콘을 클릭하여 소독솜을 버리고 소독솜 아이콘을 클릭
                if (isSG)
                {
                    pincet.transform.GetChild(0).gameObject.SetActive(false);
                    pincet.transform.GetChild(0).GetComponent<MeshRenderer>().material = cottonMt[0];
                    hand[1].transform.GetChild(1).localPosition = new Vector3(0.1096f , 0 , 0.04f);
                    hand[1].transform.GetChild(1).localEulerAngles = new Vector3(-51 , -69 , -13);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        recycleNum++;
                        pincet.GetComponent<Pincet>().num = 0;
                        isSG = false;
                    };
                }

                else
                {
                    pincet.GetComponent<BoxCollider>().enabled = true;
                    RayPoint();
                }
                break;

            case SGState.SG21://거즈아이콘을 누르면 핀셋 끝에 거즈가 찝혀있다.
                if (isSG)
                {
                    missionText.transform.parent.gameObject.SetActive(false);
                    pincet.transform.GetChild(1).gameObject.SetActive(true);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        SetMessage("거즈를 움직여 물기를 닦아주세요.");
                        isSG = false;
                    };
                }
                break;

            case SGState.SG22://핀셋을 드레그하여 상처부위 위로 가져가면 거즈로 상처부위를 톡톡두드리는 애니메이션이 나오며 물기를 제거한다.
                if (isSG)
                {
                    interval += Time.deltaTime;
                    missionText.transform.parent.gameObject.SetActive(false);
                    if (interval > 3)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            hand[1].GetComponent<Animator>().enabled = false;
                            SetAnim(12);
                            interval = 0;
                            isSG = false;
                        };
                    }
                }

                else
                {
                    RayPoint();
                }
                break;

            case SGState.SG23://쓰레기통 아이콘을 누르면 핀셋과 거즈 둘 다 사라진다.
                if (isSG)
                {
                    pincet.SetActive(false);
                    nextButton.SetActive(true);
                    hand[1].transform.GetChild(1).localPosition = new Vector3(0.1096f , 0 , 0.04f);
                    hand[1].transform.GetChild(1).localEulerAngles = new Vector3(-51 , -69 , -13);
                    sgact = () =>
                    {
                        isSG = false;
                        SetAnim(10);
                    };
                }
                break;

            case SGState.SG24://하이드로 겔 아이콘을 누르면 플레이어의 손에 하이드로 겔이라는 연고가 잡힌다.
                if (isSG)
                {
                    hydrogel.SetActive(true);
                    interval += Time.deltaTime;
                    if (interval > 3)
                    {
                        hydrogel.SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            SetAnim(11);
                        };
                    }
                }
                break;

            case SGState.SG25://메디폼 아이콘을 누르면 플레이어의 손에 메디폼이 잡힌다.
                if (isSG)
                {
                    mediform.SetActive(true);
                    interval += Time.deltaTime;
                    if (interval > 2)
                    {
                        //mediform.SetActive(false);
                        nextButton.SetActive(true);
                        patientAnim.SetTrigger("Hipup");
                        sgact = () =>
                        {
                            hand[1].GetComponent<Animator>().enabled = false;
                            interval = 0;
                            isSG = false;
                        };
                    }
                }
                break;

            case SGState.SG26://쓰레기통 아이콘을 누르면 핀셋과 거즈 둘 다 사라진다.
                _sgstate = SGState.SG27;
                if (isSG)
                {
                    //patientAnim.SetTrigger("Front");                    
                    interval += Time.deltaTime;
                    if (interval > 2)
                    {
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            isSG = false;
                            interval = 0;
                            toolToggle.isOn = false;
                        };
                    }
                }
                break;

            case SGState.SG27:
                myinput.SetActive(true);
                messengerImage[2].SetActive(false);
                messengerImage[3].SetActive(true);
                nextButton.SetActive(true);
                sgact = () =>
                {
                    myinputfield.text = "";
                    myinput.SetActive(false);
                    isSG = false;
                    interval = 0;
                    SetAnim(4);
                };
                break;

            case SGState.SG28:
                if (isSG)
                {
                    curtain.SetActive(false);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        fadeOut.SetActive(true);
                        Invoke("ReturnPatient" , 2);
                        isSG = false;
                    };
                }
                break;

            case SGState.SG29:
                if (isSG)
                {
                    for (int i = 0; i < allProp.transform.childCount; i++)
                    {
                        allProp.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    cottons.SetActive(false);
                    nextButton.SetActive(true);
                    sgact = () =>
                    {
                        SetAnim(0);
                        isSG = false;
                    };
                }
                break;

            case SGState.SG30:
                if (isSG)
                {
                    hand[2].SetActive(true);
                    handSodoc[0].SetActive(true);
                    handSodoc[0].GetComponent<Animator>().enabled = true;
                    interval += Time.deltaTime;
                    if (interval > 13)
                    {
                        handSodoc[0].SetActive(false);
                        handSodoc[0].GetComponent<Animator>().enabled = false;
                        hand[2].SetActive(false);
                        nextButton.SetActive(true);
                        sgact = () =>
                        {
                            interval = 0;
                            isSG = false;
                            toolToggle.isOn = false;
                        };
                    }
                }
                break;

            case SGState.SG31:
                resultinput.SetActive(true);
                messengerImage[3].SetActive(false);
                messengerImage[4].SetActive(true);
                nextButton.SetActive(true);
                sgact = () =>
                {
                    resultText[0].text = DateTime.Now.ToString("yyyy-MM-dd");
                    for (int i = 0; i < resultInputs.Length; i++)
                    {
                        resultText[i + 1].text = resultInputs[i].text;
                    }
                    resultinput.SetActive(false);
                    result.SetActive(true);
                };
                break;
        }
    }
    public int recycleNum;

    public void SceneClick(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void OnButtonClick(int num)
    {
        if ((int)_sgstate != num)
        {
            notice1_fail.SetActive(true);
            return;
        }
        if (nowAnim != null)
        {
            nowAnim.enabled = false;
            Color c = nowAnim.transform.GetChild(1).GetComponent<Image>().color;
            nowAnim.transform.GetChild(1).GetComponent<Image>().color = new Color(c.r , c.g , c.b , 0);
            nowAnim = null;
        }

        isSG = true;
        switch (num)
        {
            case 0:
                if(_state == State.SG)
                hand[2].SetActive(true);
                isSG = true;
                break;

            case 1:                
                isSG = true;
                break;

            case 2:
                if (_state == State.SG)
                    gauze.SetActive(true);
                isSG = true;
                break;

            case 3:
                isSG = true;
                break;

            case 4:
                isSG = true;
                break;

            case 5:
                isSG = true;
                break;

            case 8:
                isSG = true;
                break;

            case 9:
                isSG = true;
                break;

            case 10:
                isSG = true;
                break;

            case 11:
                isSG = true;
                break;

            case 12:
                isSG = true;
                break;

            case 14:
                isSG = true;
                break;

            case 23:
                hand[1].GetComponent<Animator>().enabled = true;
                hand[1].GetComponent<Animator>().SetTrigger("Hydro");
                break;

            case 24:                
                hand[1].GetComponent<Animator>().SetTrigger("Medi");
                break;
        }        
    }

    public void OnDiabateButtonClick(int num)
    {
        if ((int)_sgstate != num)
        {
            notice1_fail.SetActive(true);
            return;
        }
        if (nowAnim != null)
        {
            nowAnim.enabled = false;
            Color c = nowAnim.transform.GetChild(1).GetComponent<Image>().color;
            nowAnim.transform.GetChild(1).GetComponent<Image>().color = new Color(c.r , c.g , c.b , 0);
            nowAnim = null;
        }

        isSG = true;
        switch (num)
        {
            case 0:                
                isSG = true;
                break;

            case 1:
                isSG = true;
                break;

            case 2:                
                isSG = true;
                break;

            case 3:
                isSG = true;
                break;

            case 4:
                isSG = true;
                break;

            case 5:
                isSG = true;
                break;

            case 8:
                isSG = true;
                break;

            case 9:
                isSG = true;
                break;

            case 10:
                isSG = true;
                break;

            case 11:
                isSG = true;
                break;

            case 12:
                isSG = true;
                break;

            case 14:
                isSG = true;
                break;

            case 23:
                break;

            case 24:                
                break;
        }
    }



    public void NextButtonClick()
    {
        if (_sgstate == SGState.SG20)
            _sgstate = ( recycleNum == 2 ) ? SGState.SG21 : SGState.SG19;
        else
            _sgstate = (SGState)( (int)_sgstate + 1 );

        if(_sgstate == SGState.SG21) SetAnim(2);
        else if (_sgstate == SGState.SG19)SetAnim(3);
        sgact();
        nextButton.SetActive(false);
    }

    public void NextDiabateButtonClick()
    {
        if (_sgstate == SGState.SG5 || _sgstate == SGState.SG6 || _sgstate == SGState.SG7)
        {
            if (myinputfield.text.Length < 4)
            {
                myInputFail.SetActive(true);
                return;
            }

            else
            {
                _sgstate = (SGState)( (int)_sgstate + 1 );
                myInputFail.SetActive(false);
            }
        }

        else
        {
            _sgstate = (SGState)( (int)_sgstate + 1 );
        }
        
        sgact();
        nextButton.SetActive(false);
    }

    public void SetAnim(int num)
    {
        //if (nowAnim != null)
        //    nowAnim.enabled = false;
        //toolsAnim[num].enabled = true;
        //nowAnim = toolsAnim[num];
    }

    void GoPatient()
    {
        myinput.SetActive(true);
        messengerImage[0].SetActive(true);        
        maincam.SetActive(false);
        servecam.SetActive(true);
        fadeOut.SetActive(false);
    }

    void ReturnPatient()
    {
        SetAnim(13);
        maincam.SetActive(true);
        servecam.SetActive(false);
        fadeOut.SetActive(false);
    }
    #endregion

    void Patients()
    {
        if (_patientState == PatientState.Prologue)
        {
            float dis = Vector3.Distance(transform.position , patientMale.transform.position);
            if (dis < 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startObj[0].SetActive(false);
                    anim.SetInteger("Move" , 0);
                    isStart = false;
                    isDaewha = false;
                    _patientState = PatientState.Start;
                    conver.SetActive(true);
                    converNum++;
                    SetVoice(converNum);
                    string[] test = daehwa[converNum].Split('_');
                    StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                }
            }
        }

        else if (_patientState == PatientState.Start)
        {
            if (!isStart)
            {
                
            }

            else
            {
                float disMale = Vector3.Distance(patientMale.transform.position , patient.transform.position);                
                if (disMale < 3)
                {
                    patientMaleAnim.SetInteger("Move" , 0);            
                    _patientState = PatientState.Pat;
                    missionText.transform.parent.gameObject.SetActive(false);
                }
            }
        }

        else if (_patientState == PatientState.Pat)
        {
            float dis = Vector3.Distance(transform.position , patient.transform.position);
            if (dis < 4)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startObj[1].SetActive(false);
                    anim.SetInteger("Move" , 0);
                    isDaewha = false;
                    _patientState = PatientState.Daewha;
                    conver.SetActive(true);
                    converNum++;
                    string[] test = daehwa[converNum].Split('_');
                    SetConver(0);
                    SetVoice(converNum);
                    chatManager.CharacterName2.text = "할머니";
                    StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                    isStart = false;
                }
            }
        }

        else if (_patientState == PatientState.Daewha)
        {
            if (!isStart)
            {
                
            }

            else
            {
                interval += Time.deltaTime;
                if (interval > 12)
                {                    
                    //_patientState = PatientState.Anim;
                    reButton.SetActive(true);
                    nextButton.SetActive(true);
                }
            }
        }

        else if (_patientState == PatientState.Anim)
        {
            
        }

        else if (_patientState == PatientState.End)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (converNum == 20)
                {
                    conver.SetActive(false);
                    converNum = 0;
                    _patientState = PatientState.Question;
                    chatManager.audio2.Stop();
                    SceneManager.LoadScene(4);
                    return;
                }

                if (isDaewha)
                {
                    isDaewha = false;
                    conver.SetActive(true);
                    converNum++;
                    chatManager.waitTime = 0.1f;
                    string[] test = daehwa[converNum].Split('_');
                    StartCoroutine(chatManager.NormalChat(test[0] , test[1]));                   
                }

                else
                {
                    chatManager.waitTime = 0;
                }
            }
        }
    }

    void Patients_Diabates()
    {
        if (_patientState == PatientState.Prologue)
        {
            float dis = Vector3.Distance(transform.position , patientMale.transform.position);
            if (dis < 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startObj[0].SetActive(false);
                    anim.SetInteger("Move" , 0);
                    isStart = false;
                    isDaewha = false;
                    _patientState = PatientState.Start;
                    conver.SetActive(true);
                    converNum++;
                    SetVoice(converNum);
                    string[] test = daehwa[converNum].Split('_');
                    StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                }
            }
        }

        else if (_patientState == PatientState.Start)
        {
            if (!isStart)
            {

            }

            else
            {
                float disMale = Vector3.Distance(patientMale.transform.position , maleTr.transform.position);
                if (disMale < 1)
                {
                    patientMaleAnim.SetInteger("Move" , 0);
                    _patientState = PatientState.Pat;
                    missionText.transform.parent.gameObject.SetActive(false);
                }
            }
        }

        else if (_patientState == PatientState.Pat)
        {
            float dis = Vector3.Distance(transform.position , maleTr.transform.position);
            if (dis < 4)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    patientMale.transform.LookAt(transform);
                    startObj[1].SetActive(false);
                    anim.SetInteger("Move" , 0);
                    isDaewha = false;
                    _patientState = PatientState.Daewha;
                    conver.SetActive(true);
                    converNum++;
                    string[] test = daehwa[converNum].Split('_');
                    SetConver(0);
                    SetVoice(converNum);
                    chatManager.CharacterName2.text = "할머니";
                    StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                    isStart = false;
                }
            }
        }

        else if (_patientState == PatientState.Daewha)
        {
            if (!isStart)
            {

            }

            else
            {
                interval += Time.deltaTime;
                if (interval > 12)
                {
                    //_patientState = PatientState.Anim;
                    reButton.SetActive(true);
                    nextButton.SetActive(true);
                }
            }
        }

        else if (_patientState == PatientState.Anim)
        {

        }



        else if (_patientState == PatientState.Question)
        {
            interval += Time.deltaTime;
            if (interval > 11)
            {
                //videoPanel.SetActive(false);
                questionPanel.SetActive(true);
            }
        }
    }

    #region 대화
    public void NextConver()
    {
        if (_state == State.Patients)
        {
            if (_patientState == PatientState.Start)
            {
                if (isDaewha)
                {
                    isDaewha = false;
                    conver.SetActive(true);
                    converNum++;
                    chatManager.waitTime = 0.1f;
                    string[] test = daehwa[converNum].Split('_');
                    SetConver(1);
                    SetVoice(converNum);
                    StartCoroutine(chatManager.NormalChat2(test[0] , test[1]));
                }

                else
                {
                    chatManager.waitTime = 0;
                }

                if (converNum == 2)
                {
                    converNum--;
                    conver.SetActive(false);
                    chatManager.audio2.Stop();
                    _audio.Stop();
                    _audio.clip = null;
                    maleAgent.destination = maleTr.position;
                    patientMaleAnim.SetInteger("Move" , 1);
                    isStart = true;
                    missionText.transform.parent.gameObject.SetActive(true);
                }
            }

            else if (_patientState == PatientState.Daewha)
            {
                if (isDaewha)
                {
                    isDaewha = false;
                    conver.SetActive(true);
                    converNum++;
                    SetVoice(converNum);
                    chatManager.waitTime = 0.1f;
                    string[] test = daehwa[converNum].Split('_');
                    if (test[0] == "보건진료소장")
                    {
                        SetConver(0);
                        StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                    }

                    else
                    {
                        SetConver(1);
                        StartCoroutine(chatManager.NormalChat2(test[0] , test[1]));
                    }

                    if (converNum == 7)
                    {
                        isStart = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                        maincam.SetActive(false);
                        servecam.SetActive(true);
                        reButton.SetActive(false);
                        nextButton.SetActive(false);
                    }

                    else if (converNum == 8)
                    {
                        reButton.SetActive(false);
                        nextButton.SetActive(true);
                        _patientState = PatientState.Anim;
                    }
                }

                else
                {
                    chatManager.waitTime = 0;
                }
            }

            else if (_patientState == PatientState.Anim)
            {
                if (isDaewha)
                {
                    isDaewha = false;
                    conver.SetActive(true);
                    converNum++;
                    if (voices.Length > converNum)
                    {
                        SetVoice(converNum);
                    }
                    chatManager.waitTime = 0.1f;
                    string[] test = daehwa[converNum].Split('_');
                    if (test[0] == "보건진료소장")
                    {
                        SetConver(0);
                        StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                    }

                    else
                    {
                        SetConver(1);
                        StartCoroutine(chatManager.NormalChat2(test[0] , test[1]));
                    }

                    if (converNum == 9)
                    {
                        servecam.SetActive(false);
                        playerView.SetActive(true);
                        playerView.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Duizyp");
                        patientAnim.SetTrigger("Left");
                    }

                    else if (converNum == 10)
                    {
                        servecam.SetActive(true);
                        playerView.SetActive(false);
                        hand[2].SetActive(false);
                        hand[0].SetActive(true);
                        patientAnim.SetTrigger("Hip");
                    }

                    else if (converNum == 13)
                    {
                        hand[0].SetActive(false);
                        hand[1].SetActive(true);
                        patientAnim.SetTrigger("Hip");
                    }
                }

                else
                {
                    chatManager.waitTime = 0;
                }

                if (converNum == 14)
                {
                    converNum--;
                    conver.SetActive(false);
                    _patientState = PatientState.Question;
                    chatManager.audio2.Stop();
                    _audio.Stop();
                    _audio.clip = null;
                    SceneManager.LoadScene(4);
                }
            }
        }

        else if (_state == State.Patients_Diabates)
        {
            if (_patientState == PatientState.Start)
            {
                if (isDaewha)
                {
                    isDaewha = false;
                    conver.SetActive(true);
                    converNum++;
                    chatManager.waitTime = 0.1f;
                    string[] test = daehwa[converNum].Split('_');
                    SetConver(1);
                    SetVoice(converNum);
                    StartCoroutine(chatManager.NormalChat2(test[0] , test[1]));
                }

                else
                {
                    chatManager.waitTime = 0;
                }

                if (converNum == 2)
                {
                    converNum--;
                    conver.SetActive(false);
                    chatManager.audio2.Stop();
                    _audio.Stop();
                    _audio.clip = null;
                    maleAgent.destination = maleTr.position;
                    patientMaleAnim.SetInteger("Move" , 1);
                    isStart = true;
                    missionText.transform.parent.gameObject.SetActive(true);
                }
            }

            else if (_patientState == PatientState.Daewha)
            {
                if (isDaewha)
                {
                    isDaewha = false;
                    conver.SetActive(true);
                    converNum++;
                    chatManager.waitTime = 0.1f;
                    string[] test = daehwa[converNum].Split('_');
                    if (test[0] == "보건진료소장")
                    {
                        SetConver(0);
                        StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                    }

                    else
                    {
                        SetConver(1);
                        StartCoroutine(chatManager.NormalChat2(test[0] , test[1]));
                    }
                    SetVoice(converNum);
                    if (converNum == 7)
                    {
                        isStart = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                        maincam.SetActive(false);
                        servecam.SetActive(true);
                        conver.SetActive(false);
                        fadeOut.SetActive(true);
                        chatManager.isWait = true;
                        Invoke("FadeDiabates" , 1);
                        patientMaleAnim.SetInteger("Move" , 2);
                        patientMale.GetComponent<NavMeshAgent>().enabled = false;
                        patientMale.transform.position = new Vector3(15.92f , 3.67f , 76.5f);
                        patientMale.transform.eulerAngles = new Vector3(0 , 20 , 0);
                        _patientState = PatientState.Anim;
                    }
                }

                else
                {
                    chatManager.waitTime = 0;
                }
            }

            else if (_patientState == PatientState.Anim)
            {
                if (isDaewha)
                {
                    isDaewha = false;
                    conver.SetActive(true);
                    converNum++;                    
                    if (converNum == 18)
                    {
                        _patientState = PatientState.Question;
                        fadeOut.SetActive(true);
                        fadeOut.GetComponent<Animator>().enabled = false;
                        fadeOut.GetComponent<Image>().color = new Color(0 , 0 , 0 , 1);
                        fadeOut.transform.GetChild(0).gameObject.SetActive(true);
                        Invoke("FadeDiabates" , 3);
                        return;
                    }
                    SetVoice(converNum);
                    chatManager.waitTime = 0.1f;
                    string[] test = daehwa[converNum].Split('_');
                    if (test[0] == "보건진료소장")
                    {
                        SetConver(0);
                        StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                    }

                    else
                    {
                        SetConver(1);
                        StartCoroutine(chatManager.NormalChat2(test[0] , test[1]));
                    }

                    if (converNum == 15)
                    {
                        chatManager.isWait = true;
                        fadeOut.SetActive(true);
                        conver.SetActive(false);
                        Invoke("FadeDiabates" , 1);
                    }

                    else if (converNum == 17)
                    {
                        chatManager.audio2.Stop();
                    }
                }

                else
                {
                    chatManager.waitTime = 0;
                }
            }
        }
    }

    void SetVoice(int i)
    {
        if (_audio.clip == voices[i])
            return;
        _audio.clip = voices[i];
        _audio.Play();
    }

    public void ReButtonClick()
    {
        reButton.SetActive(false);
        nextButton.SetActive(false);
        servecam.transform.GetChild(1).GetComponent<Animator>().Play("Take 001");
        interval = 0;
    }

    void SetConver(int i)
    {
        conver.transform.GetChild(1 - i).gameObject.SetActive(false);
        conver.transform.GetChild(i).gameObject.SetActive(true);
    }

    public void SetNotice(string t)
    {
        notice.SetActive(true);
        notice.transform.GetChild(0).GetComponent<Text>().text = t;
    }

    public void SetMessage(string t)
    {
        missionText.transform.parent.gameObject.SetActive(true);
        missionText.GetComponent<Text>().text = t;
    }

    public void DaeHwaNextButtonClick()
    {
        if (converNum == stateConverNum)
        {
            _startstate = StartState.Bag;
            converNum = 0;
            callPanel.SetActive(false);
            noticeObj[2].SetActive(false);
            noticeObj[3].SetActive(true);
            missionText.transform.parent.gameObject.SetActive(true);
            missionText.GetComponent<Text>().text = stateConverString;
            return;
        }
        if (isDaewha)
        {
            isDaewha = false;
            converNum++;
            chatManager.waitTime = 0.1f;
            string[] test = daehwa[converNum].Split('_');
            SetVoice(converNum);
            if (test[0] == "보건진료소장")
            {
                StartCoroutine(chatManager.NormalChat(test[0] , test[1]));
                //call[0].sprite = callSay[1];
                //call[1].sprite = callSay[0];
            }

            else
            {
                StartCoroutine(chatManager.NormalChat2(test[0] , test[1]));
                //call[0].sprite = callSay[0];
                //call[1].sprite = callSay[1];
            }

        }

        else
        {
            chatManager.waitTime = 0;
        }
    }
    #endregion       

    public void QuestionButtonClick(bool tf)
    {
        myAnswer.Add(tf);        
    }

    int failNum;
    public void ApplyButtonClick()
    {
        for (int i = 0; i < myAnswer.Count; i++)
        {
            if (!myAnswer[i])
                break;
            answerNum++;
        }
        if (answerNum == stateAnswerNum)
        {
            if (_state == State.Patients_Diabates)
            {
                SceneManager.LoadScene(8);
                return;
            }
            _startstate = StartState.End;
            for (int i = 0; i < door.Length; i++)
            {
                door[i].enabled = true;
            }
            //SetNotice("정답입니다. 밖으로 나가\n문정례 할머니 집으로 가주세요.");
            portal.SetActive(true);
            notice1_success.SetActive(true);
            questionPanel.SetActive(false);
            answerNum = 0;
            myAnswer.Clear();
        }
        else
        {
            if (_state == State.Patients_Diabates)
            {
                if (failNum < 2)
                {
                    failNum++;
                    notice1_fail.SetActive(true);
                    Invoke("Fail" , 1);
                    for (int i = 0; i < questionToggle.Length; i++)
                    {
                        questionToggle[i].isOn = false;
                    }
                    return;
                }

                else
                {
                    SceneManager.LoadScene(8);
                }
            }
            notice1_fail.SetActive(true);
            portal.SetActive(true);
            for (int i = 0; i < questionToggle.Length; i++)
            {
                questionToggle[i].isOn = false;
            }
            questionPanel.SetActive(false);
            myAnswer.Clear();
            answerNum = 0;
        }
    }

    void Fail()
    {
        notice1_fail.SetActive(false);
    }
    #region 조작
    void Move()
    {
        dir = lookCamera.transform.forward * Input.GetAxis("Vertical") + lookCamera.transform.right * Input.GetAxis("Horizontal");

        if (dir.sqrMagnitude != 0)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.Euler(new Vector3(0f , Mathf.Atan2(dir.x , dir.z) * Mathf.Rad2Deg , 0f)) , 0.1f);
            anim.SetInteger("Move" , 1);
            moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 5 : 2;
        }

        else
            anim.SetInteger("Move" , 0);

        lookCamera.transform.rotation = cameraFixRotation;

        if (Input.GetMouseButton(1))
            CameraRotate();
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

        lookCamera.transform.rotation = Quaternion.Slerp(lookCamera.transform.rotation , Quaternion.Euler(rot) , 2f); // 자연스럽게 회전  
        cameraFixRotation = lookCamera.transform.rotation;
    }

    void RayPoint()
    {
        if (myOBJ != null)
        {
            if (Input.GetMouseButton(0))
            {
                Camera cam = servecam.GetComponent<Camera>();
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                myOBJ.transform.localPosition = new Vector3(myOBJ.transform.localPosition.x , myOBJ.transform.localPosition.y , 0.2f);
                myOBJ.transform.Translate(x * 0.1f , y * 0.1f , 0);
                myOBJ.transform.localEulerAngles = Vector3.zero;
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray , out hit , 100 , 1 << 0))
                {
                    if (hit.collider.tag.Equals("Open") && hit.collider.gameObject.GetComponent<Animator>())
                    {
                        Animator anim = hit.collider.gameObject.GetComponent<Animator>();
                        anim.SetTrigger("Open");
                    }

                    else if (hit.collider.tag.Equals("Plasta"))
                    {
                        isSG = true;
                        hand[1].GetComponent<Animator>().enabled = true;
                    }

                    else if (hit.collider.tag.Equals("Pincet"))
                    {
                        myOBJ = hit.collider.transform.parent.gameObject;
                    }
                }
            }
        }
    }

    void RayPoint(string tag , Action getray)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray , out hit , 100 , 1 << 0))
            {
                if (hit.collider.tag.Equals(tag))
                {
                    getray();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            fadeOut.SetActive(true);
            conver.SetActive(false);
            Invoke("Fade" , 2);
        }
    }
    #endregion    

    void Fade()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void FadeDiabates()
    {
        fadeOut.SetActive(false);
        conver.SetActive(true);
        chatManager.isWait = false;
        if (converNum == 18)
        {
            videoPanel.SetActive(true);
        }        
        //patientMale.transform.position = new Vector3(16 , 3.67f , 76.12f);
        //patientMale.transform.eulerAngles = new Vector3(0 , 20 , 0);        
    }

    public void SetDaeHwa(string charName , string daehwaText)
    {
        nickname.text = charName;
        daehwaName.text = daehwaText;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
