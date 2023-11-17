using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWITHFACTORY.CYJ;

[System.Serializable]
public class QuestArray //행에 해당되는 이름
{
    public string[] question;
    public int answer;
}

public class QuestionManager : MonoBehaviour
{
    public Text questionLabel;
    public AnswerButton[] answerButtons;
    public string[] questionText;
    int questionNum = -1;
    public QuestArray[] answerText;

    public Player2 _player;

    // Start is called before the first frame update
    void Start()
    {
        NextQuestion(true);
    }

    public void NextQuestion(bool tf)
    {
        if (!tf)
        {
            _player.SetNotice("다시 선택해주세요!");
            return;
        }
        questionNum++;
        if (1 == questionNum)
        {
            _player.SetNotice("정답입니다!");
            _player._patientState = Player2.PatientState.End;
            gameObject.SetActive(false);
            _player.conver.SetActive(true);
            return;
        }    
        questionLabel.text = questionText[questionNum];
        // 버튼 텍스트와 정답 여부 할당
        for (int i = 0; i < 5; i++)
        {
            answerButtons[i].answerText.text = answerText[questionNum].question[i];
            answerButtons[i].isCorrect = ( i == answerText[questionNum].answer - 1 );
        }       
    }
}
