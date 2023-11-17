using UnityEngine;
using UnityEngine.UI;
namespace SWITHFACTORY.CYJ
{
    public class AnswerButton : MonoBehaviour
    {
        public Text answerText;
        public bool isCorrect = false;
        QuestionManager_S2 quest;
        private void Start()
        {
            Button button = GetComponent<Button>();
            quest = FindObjectOfType<QuestionManager_S2>();
            button.onClick.AddListener(() => { PressAnswerBtnClick(); });
        }

        void PressAnswerBtnClick()
        {
            quest.NextQuestion(isCorrect);
        }
    }

}




