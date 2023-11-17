using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{

    public Text ChatText; // ���� ä���� ������ �ؽ�Ʈ
    public Text CharacterName; // ĳ���� �̸��� ������ �ؽ�Ʈ

    public Text ChatText2; // ���� ä���� ������ �ؽ�Ʈ
    public Text CharacterName2; // ĳ���� �̸��� ������ �ؽ�Ʈ
    public AudioSource audio2;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

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
        foreach (var element in skipButton) // ��ư �˻�
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
        //�ؽ�Ʈ Ÿ���� ȿ��
        for (int a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;            
            yield return new WaitForSeconds(waitTime);
        }
        audio2.Stop();
        _player.isDaewha = true;
        //Ű�� �ٽ� ���� �� ���� ������ ���
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
        //�ؽ�Ʈ Ÿ���� ȿ��
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
        //Ű�� �ٽ� ���� �� ���� ������ ���
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