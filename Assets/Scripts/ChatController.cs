using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{

    public Text ChatText; // 실제 채팅이 나오는 텍스트
    public Text CharacterName; // 캐릭터 이름이 나오는 텍스트

    public Text ChatText2; // 실제 채팅이 나오는 텍스트
    public Text CharacterName2; // 캐릭터 이름이 나오는 텍스트
    public AudioSource audio2;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키

    public string writerText = "";
    public float waitTime = 0.1f;
    bool isButtonClicked = false;
    Player2 _player;
    public bool isWait;
    void Start()
    {
        audio2 = GetComponent<AudioSource>();
        _player = GetComponent<Player2>();
        //StartCoroutine(TextPractice());
    }

    void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
    }


    public IEnumerator NormalChat(string narrator , string narration)
    {
        if (_player._state != Player2.State.Beginning)
        {
            //audio2.Play();
        }
        CharacterName.text = narrator;
        writerText = "";
        WaitForSeconds wait = new WaitForSeconds(waitTime);
        while (isWait)
        {
            yield return wait;
        }
        //텍스트 타이핑 효과
        for (int a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;            
            yield return new WaitForSeconds(waitTime);
        }
        audio2.Stop();
        _player.isDaewha = true;
        //키를 다시 누를 떄 까지 무한정 대기
        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }

    public IEnumerator NormalChat2(string narrator , string narration)
    {
        audio2.Play();
        CharacterName2.text = narrator;
        writerText = "";
        WaitForSeconds wait = new WaitForSeconds(waitTime);
        //텍스트 타이핑 효과
        while (isWait)
        {
            yield return wait;
        }
        for (int a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText2.text = writerText;
            yield return new WaitForSeconds(waitTime);
        }
        audio2.Stop();
        _player.isDaewha = true;
        //키를 다시 누를 떄 까지 무한정 대기
        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }
}