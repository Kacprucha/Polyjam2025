using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    [SerializeField] Sprite heroPortret;
    [SerializeField] Image background;
    [SerializeField] Image portret;
    [SerializeField] Text npcName;
    [SerializeField] Text dialogOption;
    [SerializeField] GameObject buttonOption;
    [SerializeField] public GameObject inputOption;

    [SerializeField] List<Button> buttonsList;
    [SerializeField] List<Text> titleElemets;

    [SerializeField] InputField inputField;

    List<string> randomKyes = new List<string> { "defaultDialog_Fish0", "defaultDialog_Fish1", "defaultDialog_Fish2", "defaultDialog_Fish3", "defaultDialog_Fish4", "defaultDialog_Fish5", "defaultDialog_Fish6", "defaultDialog_Fish7", "defaultDialog_Fish8", "defaultDialog_Fish9", "defaultDialog_Fish10", "defaultDialog_Fish11" };

    bool finalBossDialogFinieshed = false;

    int stageOfTalkingToArchivist = 0;
    int stageOfTalkingToToiletGuy = 0;
    int stageOfTalkingToDolphin = 0;
    bool finishWithMyComputer = false;

    DialogInfo currentDialog = null;
    Sprite npcPortret = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape) && this.gameObject.activeSelf)
        {
            CloseDialog ();
        }

        if (Input.GetKeyDown (KeyCode.Return) && this.gameObject.activeSelf)
        {
            if (currentDialog == null) 
            {
                return;
            }

            if (currentDialog.type == DialogType.Input)
            {
                onPlayerInputHandle ();
            }
            else if (currentDialog.isDiaglogInitialized && currentDialog.type != DialogType.Normal && !currentDialog.isDialogFinished)
            {
                handleDialog (currentDialog);
            }
            else if (currentDialog.isDialogFinished)
            {
                CloseDialog ();
            }
        }
    }

    public void Show (DialogInfo dialog, string name, Sprite background = null, Sprite portret = null)
    {
        currentDialog = dialog;

        this.gameObject.SetActive (true);

        foreach (Button button in buttonsList)
        {
            button.gameObject.SetActive (false);
        }

        if (background != null)
        {
            this.background.gameObject.SetActive (true);
            this.background.sprite = background;
        }
        else
        {
            this.background.gameObject.SetActive (false);
        }

        if (portret != null)
        {
            this.portret.gameObject.SetActive (true);
            this.portret.sprite = portret;
            this.portret.preserveAspect = true;

            npcPortret = portret;
        }
        else
        {
            this.portret.gameObject.SetActive (false);
            npcPortret = null;
        }

        npcName.text = name;

        handleDialog (dialog);

        //dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.message);
    }

    public void CloseDialog ()
    {
        if (finalBossDialogFinieshed)
        {
            PlayerInfo.Instance.RestartGame ();
        }

        inputOption.SetActive (false);
        this.gameObject.SetActive (false);
    }

    protected void handleDialog (DialogInfo dialog)
    {
        dialogOption.gameObject.SetActive (false);
        buttonOption.SetActive (false);
        inputOption.SetActive (false);

        foreach (Button button in buttonsList)
        {
            button.onClick.RemoveAllListeners ();
        }

        // Show initial message
        if (!dialog.isDiaglogInitialized)
        {
            dialogOption.gameObject.SetActive (true);
            if (dialog.type == DialogType.Normal)
                dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.defaultMessage);
            else
                dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.initialMessage);
            dialog.isDiaglogInitialized = true;
            return;
        }

        // Show default message
        if (dialog.isDialogFinished == true)
        {
            dialogOption.gameObject.SetActive (true);
            dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.defaultMessage);
            return;
        }

        // Continue main dialouge
        switch (dialog.type)
        {
            case DialogType.Normal:
                {
                    string message = "";
                    if (dialog.rnadomDialog)
                    {
                        int choice = Random.Range (0, randomKyes.Count);
                        message = randomKyes[choice];
                    }
                    else
                    {
                        message = dialog.defaultMessage;
                    }
                    generateNormalInput (message);

                    break;
                }

            case DialogType.Input:
                {
                    inputOption.SetActive (true);

                    titleElemets[1].text = LocalizationManager.Instance.GetLocalizedValue (dialog.defaultMessage);
                    break;
                }

            case DialogType.Item:
                {
                    buttonOption.SetActive (true);

                    buttonsList[0].gameObject.SetActive (true);
                    buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue ("pickRice");
                    buttonsList[0].onClick.AddListener (() =>
                    {
                        dialog.isDialogFinished = true;
                        PlayerInfo.Instance.AddCollectable (dialog.itemInfo);
                        CloseDialog ();

                    });

                    titleElemets[0].text = LocalizationManager.Instance.GetLocalizedValue (dialog.defaultMessage);


                    break;
                }
            case DialogType.Choice:
                {
                    portret.sprite = heroPortret;

                    buttonOption.SetActive (true);

                    for (int i = 0; i < dialog.options.Count; i++)
                    {
                        buttonsList[i].gameObject.SetActive (true);
                        buttonsList[i].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[i]);
                        int index = i;
                        buttonsList[i].onClick.AddListener (() => onDialogOptinClicked (index));
                    }

                    break;
                }

            case DialogType.BossDialog:
                {
                    portret.sprite = heroPortret;

                    if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfRaport) < 5 && PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfTueFiles) < 5)
                    {
                        dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.ans[0]);
                        dialogOption.gameObject.SetActive (true);
                    }
                    else
                    {
                        buttonOption.SetActive (true);

                        if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfRaport) >= 5)
                        {
                            buttonsList[0].gameObject.SetActive (true);
                            buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue ("raportGiveDaily");
                            buttonsList[0].onClick.AddListener (() =>
                            {
                                buttonOption.SetActive (false);
                                generateNormalInput ("raportNatural");
                                finalBossDialogFinieshed = true;
                            });
                        }

                        if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfTueFiles) >= 5)
                        {
                            buttonsList[1].gameObject.SetActive (true);
                            buttonsList[1].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue ("raportBossGive");
                            buttonsList[1].onClick.AddListener (() =>
                            {
                                buttonOption.SetActive (false);
                                generateNormalInput ("bossTerminate");
                                finalBossDialogFinieshed = true;
                            });
                        }

                    }
                    break;
                }

            case DialogType.ArchivistDialog:
                {
                    portret.sprite = heroPortret;
                    buttonOption.SetActive (true);

                    if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.Rice) + stageOfTalkingToArchivist == 1)
                    {
                        stageOfTalkingToArchivist++;
                    }

                    switch (stageOfTalkingToArchivist)
                    {
                        case 0:
                            {
                                buttonsList[0].gameObject.SetActive (true);
                                buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[0]);
                                buttonsList[0].onClick.AddListener (() =>
                                {
                                    buttonOption.SetActive (false);
                                    generateNormalInput (dialog.ans[0]);
                                });

                                buttonsList[1].gameObject.SetActive (true);
                                buttonsList[1].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[1]);
                                buttonsList[1].onClick.AddListener (() =>
                                {
                                    buttonOption.SetActive (false);
                                    generateNormalInput (dialog.ans[1]);
                                });
                                break;
                            }

                        case 1:
                            {
                                buttonsList[0].gameObject.SetActive (true);
                                buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[0]);
                                buttonsList[0].onClick.AddListener (() =>
                                {
                                    buttonOption.SetActive (false);
                                    generateNormalInput (dialog.ans[1]);
                                });

                                buttonsList[1].gameObject.SetActive (true);
                                buttonsList[1].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[1]);
                                buttonsList[1].onClick.AddListener (() =>
                                {
                                    PlayerInfo.Instance.DelateCollectable (TypeOfCollectable.Rice);
                                    PlayerInfo.Instance.AddCollectable (dialog.itemInfo);
                                    buttonOption.SetActive (false);
                                    generateNormalInput (dialog.ans[2]);
                                    stageOfTalkingToArchivist = 2;
                                });
                                break;
                            }

                        case 2:
                            {
                                buttonsList[0].gameObject.SetActive (true);
                                buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[0]);
                                buttonsList[0].onClick.AddListener (() =>
                                {
                                    buttonOption.SetActive (false);
                                    generateNormalInput (dialog.ans[0]);
                                });

                                break;
                            }
                    }

                    break;
                }

            case DialogType.ToiletGuy:
                {
                    if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.ToiletPaper) + stageOfTalkingToToiletGuy == 1)
                    {
                        stageOfTalkingToToiletGuy++;
                    }

                    switch (stageOfTalkingToToiletGuy)
                    {
                        case 0:

                            dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.ans[0]);
                            dialogOption.gameObject.SetActive (true);

                            break;

                        case 1:

                            PlayerInfo.Instance.DelateCollectable (TypeOfCollectable.ToiletPaper);
                            PlayerInfo.Instance.AddCollectable (dialog.itemInfo);

                            dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.ans[1]);
                            dialogOption.gameObject.SetActive (true);

                            stageOfTalkingToToiletGuy++;

                            break;

                        case 2:

                            dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.ans[2]);
                            dialogOption.gameObject.SetActive (true);

                            break;
                    }

                    break;
                }

            case DialogType.Dolphin:
                {
                    portret.sprite = heroPortret;
                    buttonOption.SetActive (true);

                    switch (stageOfTalkingToDolphin)
                    {
                        case 0:

                            buttonsList[0].gameObject.SetActive (true);
                            buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[0]);
                            buttonsList[0].onClick.AddListener (() =>
                            {
                                buttonOption.SetActive (false);
                                generateNormalInput (dialog.ans[0]);
                                stageOfTalkingToDolphin++;
                            });

                            buttonsList[1].gameObject.SetActive (true);
                            buttonsList[1].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[1]);
                            buttonsList[1].onClick.AddListener (() =>
                            {
                                buttonOption.SetActive (false);
                                generateNormalInput (dialog.ans[1]);
                                stageOfTalkingToDolphin++;
                            });

                            break;

                        case 1:

                            buttonsList[0].gameObject.SetActive (true);
                            buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[2]);
                            buttonsList[0].onClick.AddListener (() =>
                            {
                                buttonOption.SetActive (false);
                                generateNormalInput (dialog.ans[2]);
                                stageOfTalkingToDolphin++;
                            });

                            buttonsList[1].gameObject.SetActive (true);
                            buttonsList[1].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[3]);
                            buttonsList[1].onClick.AddListener (() =>
                            {
                                buttonOption.SetActive (false);
                                generateNormalInput (dialog.ans[3]);
                                stageOfTalkingToDolphin++;
                            });

                            break;

                        case 2:

                            buttonsList[0].gameObject.SetActive (true);
                            buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[4]);
                            buttonsList[0].onClick.AddListener (() =>
                            {
                                PlayerInfo.Instance.AddCollectable (dialog.itemInfo);
                                buttonOption.SetActive (false);
                                generateNormalInput (dialog.ans[4]);
                                stageOfTalkingToDolphin++;
                            });

                            break;

                        case 3:

                            buttonOption.SetActive (false);
                            generateNormalInput (dialog.ans[5]);

                            break;
                    }

                    break;
                }

            case DialogType.OurCompputer:
                {
                    if (!finishWithMyComputer)
                    {
                        if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfRaport) < 5 && PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfTueFiles) < 5)
                        {
                            dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (dialog.ans[0]);
                            dialogOption.gameObject.SetActive (true);
                        }
                        else
                        {
                            buttonOption.SetActive (true);

                            if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfRaport) >= 5)
                            {
                                buttonsList[0].gameObject.SetActive (true);
                                buttonsList[0].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[0]);
                                buttonsList[0].onClick.AddListener (() =>
                                {
                                    buttonOption.SetActive (false);
                                    generateNormalInput (dialog.ans[1]);
                                });
                            }

                            if (PlayerInfo.Instance.HowManyCollectable (TypeOfCollectable.PartOfTueFiles) >= 5)
                            {
                                buttonsList[1].gameObject.SetActive (true);
                                buttonsList[1].GetComponentInChildren<Text> ().text = LocalizationManager.Instance.GetLocalizedValue (dialog.options[1]);
                                buttonsList[1].onClick.AddListener (() =>
                                {
                                    buttonOption.SetActive (false);
                                    generateNormalInput (dialog.ans[2]);
                                    PlayerInfo.Instance.PermitionToLeav = true;
                                    finishWithMyComputer = true;
                                });
                            }

                        }
                    }
                    else
                    {
                        buttonOption.SetActive (false);
                        generateNormalInput (dialog.ans[3]);
                    }
                    break;
                }

            default:
                {
                    Debug.LogFormat ("%s given dialog type not implemented", dialog.type);
                    break;
                }
        }
    }

    protected void onDialogOptinClicked (int index)
    {
        if (npcPortret != null)
        {
            portret.sprite = npcPortret;
        }

        buttonOption.SetActive (false);
        dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (currentDialog.ans[index]);
        dialogOption.gameObject.SetActive (true);
        currentDialog.isDialogFinished = true;
    }

    protected void onPlayerInputHandle ()
    {
        string playerInput = inputField.text;

        if (playerInput == currentDialog.expectedValue)
        {
            currentDialog.isDialogFinished = true;
            PlayerInfo.Instance.AddCollectable (currentDialog.itemInfo);
            CloseDialog ();
        }
        else
        {
            inputField.text = "";
        }
    }

    protected void generateNormalInput (string text)
    {
        if (npcPortret != null)
        {
            portret.sprite = npcPortret;
        }

        dialogOption.text = LocalizationManager.Instance.GetLocalizedValue (text);
        dialogOption.gameObject.SetActive (true);
    }
}
