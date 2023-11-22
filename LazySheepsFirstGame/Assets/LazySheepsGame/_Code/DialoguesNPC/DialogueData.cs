using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{
    public List<Dialogue> Dialogues;
}

[Serializable]
public class Dialogue
{
    public string ID;
    public string text;
}