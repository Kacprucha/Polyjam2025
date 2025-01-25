using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public enum DialogType
{
    Input, 
    Choice, 
    Item, 
    Normal
}

[Serializable]
public class DialogInfo
{
    public DialogType type;
    public string initialMessage;
    public string finalMessage;

    // Choice/Input class params
    public List<string> options;

    // Item class params
    public CollectableInfo itemInfo;

    // Input class params
    public string expectedValue;

    public DialogInfo(string init_msg, string final_msg)
    {
        type = DialogType.Normal;
        initialMessage = init_msg;
        finalMessage = final_msg;
    }

    public DialogInfo(string init_msg, string final_msg, CollectableInfo item_info)
    {
        type = DialogType.Item;
        initialMessage = init_msg;
        finalMessage = final_msg;
        itemInfo = item_info;
    }

    public DialogInfo(string init_msg, string final_msg, List<string> msg)
    {
        type = DialogType.Choice;
        initialMessage = init_msg;
        finalMessage = final_msg;
        options = msg;
    }

    public DialogInfo(string init_msg, string final_msg, List<string> msg, string expected_val)
    {
        type = DialogType.Input;
        initialMessage = init_msg;
        finalMessage = final_msg;
        expectedValue = expected_val;
        options = msg;
    }
}

