using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private string[] keywords = new string[] { "left", "right" };
    public PlayerController playerController;

    void Start()
    {
        keywordRecognizer = new KeywordRecognizer(keywords);
        keywordRecognizer.OnPhraseRecognized += OnVoiceCommandRecognized;
        keywordRecognizer.Start();
    }

    private void OnVoiceCommandRecognized(PhraseRecognizedEventArgs args)
    {
        switch (args.text)
        {
            case "left":
                playerController.MoveLeft();
                break;
            case "right":
                playerController.MoveRight();
                break;
        }
    }
}
