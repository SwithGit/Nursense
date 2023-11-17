using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace SWITHFACTORY.CYJ
{

    [System.Serializable]
    public class QuestArray //행에 해당되는 이름
    {
        public string[] question;
        public int answer;
    }

    public class QuestionManager_S2 : MonoBehaviour
    {
        public Text questionLabel;
        public AnswerButton[] answerButtons;
        public string[] questionText;
        int questionNum = -1;
        int max_questionNum;
        public QuestArray[] answerText;

        public Player_S2 _player;
        public Player_S2_2 _Player_S2_2;
        private void Awake()
        {
            _player = FindObjectOfType<Player_S2>();
            _Player_S2_2 = FindObjectOfType<Player_S2_2>();

        }
        // Start is called before the first frame update
        void Start()
        {
            if(_player)
            {
                switch (_player._scenarioState)
                {
                    case Player_S2.ScenarioState.S1:
                        {
                            questionNum = -1;
                            max_questionNum = 5;
                            break;
                        }
                    case Player_S2.ScenarioState.S2:
                        {
                            questionNum = 4;
                            max_questionNum = 10;
                            break;
                        }
                }
            }
            else if(_Player_S2_2)
            {
                switch (_Player_S2_2._gameStateEnum)
                {
                    case GameStateEnum.단순도뇨:
                        {
                            questionNum = 9;
                            max_questionNum = 15;
                            break;
                        }
                    case GameStateEnum.근육주사:
                        {
                            questionNum = 14;
                            max_questionNum = questionText.Length;
                            break;
                        }
                }
            }

            NextQuestion(true);
        }

        public void NextQuestion(bool tf)
        {
            if (!tf)
            {
                if (_player) _player.SetNotice("다시 선택해주세요!");
                else if (_Player_S2_2) _Player_S2_2.SetNotice("다시 선택해주세요!");
                return;
            }
            questionNum++;
            //if (questionNum == 6)
            //{
               
            //}
            if (max_questionNum == questionNum)
            {
                if(_player) _player.SetNotice("준비된 문제가 없습니다!");
                else if(_Player_S2_2) _Player_S2_2.SetNotice("준비된 문제가 없습니다!");
                return;
            }
            questionLabel.text = questionText[questionNum];
            // 버튼 텍스트와 정답 여부 할당
            for (int i = 0; i < 5; i++)
            {
                answerButtons[i].answerText.text = answerText[questionNum].question[i];
                answerButtons[i].isCorrect = (i == answerText[questionNum].answer - 1);
            }
            if (questionNum == 6)
            {
                _player.S2_AfterQuiz1();
                gameObject.SetActive(false);
            }
            if (questionNum == 11)
            {
                _Player_S2_2.P2_Patient();
                gameObject.SetActive(false);
            }
        }
    }

}

