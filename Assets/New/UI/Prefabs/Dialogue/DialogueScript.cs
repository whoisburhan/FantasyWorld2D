using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GS.Dialogue
{
    public class DialogueScript : MonoBehaviour
    {
        #region Primary
        [Header("Primary")]
        [SerializeField] private CanvasGroup dialogueCanvasGroup;
        [SerializeField] private Button primarySkipButton;
        #endregion

        #region  Dialogue Box 1
        [Header("Dialogue Box - 1")]
        [SerializeField] private Image dialogueBox1SpeakerImg;
        [SerializeField] private Text dialogueBox1SpeakerName;
        [SerializeField] private Image dialgueBox1SpeakerNameHolder;
        [SerializeField] private Text dialogueBox1SpeakerSpeech;
        [SerializeField] private Button dialogueBox1SkipButton;
        [SerializeField] private Image dialogueBox1SkipButtonHint;
        #endregion

        #region  Dialogue Box 2
        [Header("Dialogue Box - 2")]
        [SerializeField] private Image dialogueBox2SpeakerImg;
        [SerializeField] private Text dialogueBox2SpeakerName;
        [SerializeField] private Image dialgueBox2SpeakerNameHolder;
        [SerializeField] private Text dialogueBox2SpeakerSpeech;
        [SerializeField] private Button dialogueBox2SkipButton;
        [SerializeField] private Image dialogueBox2SkipButtonHint;
        #endregion


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) DisplaySpeech("My Name is Sarah! I love jeans...", dialogueBox1SpeakerSpeech);
            if (Input.GetKeyDown(KeyCode.S)) DisplaySpeech("My favourite color is green", dialogueBox1SpeakerSpeech);
            if (Input.GetKeyDown(KeyCode.D)) DisplaySpeech("Great i love jeans too", dialogueBox2SpeakerSpeech);
            if (Input.GetKeyDown(KeyCode.F)) DisplaySpeech("I wear belt with jeans", dialogueBox2SpeakerSpeech);

        }


        #region  Speech Display
        string previousSpeech;
        Text previousSpeekerText;
        public void DisplaySpeech(string speech, Text speekerText)
        {
            if (previousSpeech != null)
                previousSpeekerText.text = previousSpeech;

            StopAllCoroutines();
            StartCoroutine(WriteSpeech(speech, speekerText));
        }

        private IEnumerator WriteSpeech(string speech, Text speakerText)
        {
            speakerText.text = "";

            previousSpeech = speech;
            previousSpeekerText = speakerText;

            foreach (char letter in speech.ToCharArray())
            {
                speakerText.text += letter;
                yield return null;
            }
        }

        #endregion

        #region Upadate Speaker

        public void UpdateSpeakerInfo(Sprite speakerImg, string speakerName, SpeakerType speakerType, DialogueBoxSide dialogueBoxSide)
        {
            UpdateSpeakerImg(speakerImg,dialogueBoxSide);
            UpdateSpeakerNameAndType(speakerName,speakerType,dialogueBoxSide);
        }

        private void UpdateSpeakerImg(Sprite speakerImg, DialogueBoxSide dialogueBoxSide)
        {
            switch(dialogueBoxSide)
            {
                case DialogueBoxSide.DialogueBox_Left:
                    dialogueBox1SpeakerImg.sprite = speakerImg;
                    break;
                case DialogueBoxSide.DialogueBox_Right:
                    dialogueBox2SpeakerImg.sprite = speakerImg;
                    break;
            }
           
        }

        private void UpdateSpeakerNameAndType(string speakerName, SpeakerType speakerType, DialogueBoxSide dialogueBoxSide)
        {
            switch(dialogueBoxSide)
            {
                case DialogueBoxSide.DialogueBox_Left:
                    dialogueBox1SpeakerName.text = speakerName;
                    UpdateSpeakerTypeHintBar(speakerType, dialgueBox1SpeakerNameHolder);
                    break;
                case DialogueBoxSide.DialogueBox_Right:
                    dialogueBox2SpeakerName.text = speakerName;
                    UpdateSpeakerTypeHintBar(speakerType, dialgueBox2SpeakerNameHolder);
                    break;
            }
        }

        private void UpdateSpeakerTypeHintBar(SpeakerType speakerType, Image speakerTypeHolder)
        {
            speakerTypeHolder.color = speakerType == SpeakerType.Player ? Color.green : speakerType == SpeakerType.NPC ? Color.white : Color.red; // red when SpeakerType == enemy 
        }

        #endregion

        private void Reset()
        {
            dialogueBox1SpeakerImg.sprite = null;
            dialogueBox1SpeakerName.text = "";
            dialgueBox1SpeakerNameHolder.color = Color.white;
            dialogueBox1SpeakerSpeech.text = "";
            dialogueBox1SkipButton.gameObject.SetActive(false);
            dialogueBox1SkipButtonHint.gameObject.SetActive(false);
        }

    }
}