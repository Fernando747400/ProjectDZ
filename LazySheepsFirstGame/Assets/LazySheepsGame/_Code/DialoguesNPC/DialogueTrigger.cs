using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;
    public TMP_Text dialogueText;
    public CanvasGroup dialogueCanvasGroup;
    private Dialogue _currentDialogue;
    public UnityEvent OnDialoguesCompleted;
    
    private bool _isDialogueActive;
    void Start()
    {
        dialogueCanvasGroup.alpha = 0;
    }

    // public void TestDialogue()
    // {
    //     _currentDialogue = dialogueData.Dialogues[0];
    //     ShowDialogue(_currentDialogue);
    // }

    public void ShowDialogue()
    {
        if(_isDialogueActive) return;
        _currentDialogue = dialogueData.Dialogues[0];
        ShowDialogue(_currentDialogue);
        _isDialogueActive = true;
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         ShowDialogue(_currentDialogue);
    //     }
    // }
    
    private void ShowDialogue(Dialogue dialogue)
    {
        dialogueText.text = dialogue.text;
        dialogueCanvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            StartCoroutine(DelayDialogue());
        });
    }
    
    private IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(5f);
        dialogueCanvasGroup.DOFade(0, 1f).OnComplete(() =>
        {
            GetNextDialogue(_currentDialogue);
        });
    }
    
    private Dialogue GetNextDialogue(Dialogue currentDialogue)
    {
        var index = dialogueData.Dialogues.IndexOf(currentDialogue);
        if (index < dialogueData.Dialogues.Count - 1)
        {
            _currentDialogue = dialogueData.Dialogues[index + 1];
            ShowDialogue(_currentDialogue);
        }
        else
        {
            dialogueCanvasGroup.DOFade(0, 1f).OnComplete(() =>
            {
                OnDialoguesCompleted?.Invoke();
                _isDialogueActive = false;

            });
        }
        return _currentDialogue;
    }
}

// #if UNITY_EDITOR_WIN
// [CustomEditor(typeof(DialogueTrigger))]
// public class DeactivatorCoreEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         DialogueTrigger dialogueTrigger = (DialogueTrigger) target;
//         
//         if (GUILayout.Button("Add force on trigger"))
//         {
//             dialogueTrigger.TestDialogue();
//         }
//     }
// }
//
// #endif
