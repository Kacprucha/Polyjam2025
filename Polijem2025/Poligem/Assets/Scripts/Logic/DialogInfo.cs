using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogType
{
    Normal,
    Input, 
    Choice, 
    Item,
    BossDialog,
    ArchivistDialog,
    Dolphin,
    ToiletGuy,
    OurCompputer
}

[Serializable]
public class DialogInfo
{
    public DialogType type;
    public string initialMessage;
    public string defaultMessage;
    public bool isDialogFinished = false;
    public bool isDiaglogInitialized = false;
    public bool rnadomDialog = false;

    // Choice class params
    public List<string> ans;
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
        defaultMessage = final_msg;
    }

    public DialogInfo(string init_msg, string final_msg, CollectableInfo item_info)
    {
        type = DialogType.Item;
        initialMessage = init_msg;
        defaultMessage = final_msg;
        itemInfo = item_info;
    }

    public DialogInfo(string init_msg, string final_msg, List<string> msg, List<string> answers)
    {
        type = DialogType.Choice;
        initialMessage = init_msg;
        defaultMessage = final_msg;
        options = msg;
        ans = answers;
    }

    public DialogInfo(string init_msg, string final_msg, List<string> msg, string expected_val)
    {
        type = DialogType.Input;
        initialMessage = init_msg;
        defaultMessage = final_msg;
        expectedValue = expected_val;
        options = msg;
    }
}

