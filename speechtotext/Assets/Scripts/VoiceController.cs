using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using TextSpeech;

public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    [SerializeField] Text uiText;
    [SerializeField] Text status;
    private void Start()
    {
        Setup(LANG_CODE);
        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;

        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

    }
    private void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }

    #region TextToSpeech

    public void StartSpeaking(string msg)
    {
        msg += " " + uiText.text;
        TextToSpeech.instance.StartSpeak(msg);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    private void OnSpeakStart()
    {
        Debug.Log("Talking started");
        status.text = "Talking started.";
    }

    private void OnSpeakStop()
    {
        Debug.Log("Talking stop");
        status.text = "Talking stopped.";
    }
    #endregion

    #region SpeechToText

    public void StartListening()
    {
        print("i am listninig.");
        status.text = "In Listening.";
        SpeechToText.instance.isShowPopupAndroid = false;
        SpeechToText.instance.StartRecording();
    }

    public void StartListeningInPOpup()
    {
        SpeechToText.instance.isShowPopupAndroid = true;
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        status.text = "Stopped listening.";
        SpeechToText.instance.StopRecording();
    }

    private void OnPartialSpeechResult(string result)
    {
        uiText.text = result;
    }

    private void OnFinalSpeechResult(string result)
    {
        uiText.text = result;
    }


    #endregion
}
