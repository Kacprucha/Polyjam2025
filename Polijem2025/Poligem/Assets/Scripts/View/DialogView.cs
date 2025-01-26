using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image portret;
    [SerializeField] Text npcName;
    [SerializeField] Text dialogOption;
    [SerializeField] GameObject buttonOption;
    [SerializeField] GameObject inputOption;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape) && this.gameObject.activeSelf)
        {
            this.gameObject.SetActive (false);
        }
    }

    public void Show (DialogInfo dialog, string name, Sprite background = null, Sprite portret = null)
    {
        dialogOption.gameObject.SetActive(false);
        buttonOption.SetActive(false);
        inputOption.SetActive(false);
        this.gameObject.SetActive (true);

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
        }
        else
        {
            this.portret.gameObject.SetActive (false);
        }

        // Show initial message
        if (dialog.isDiaglogInitialized == false)
        {
            dialogOption.gameObject.SetActive(true);
            if(dialog.type == DialogType.Normal)
                dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.defaultMessage);
            else
                dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.initialMessage);
            dialog.isDiaglogInitialized = true;
            return;
        }

        // Show default message
        if (dialog.isDialogFinished == true)
        {
            dialogOption.gameObject.SetActive(true);
            dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.defaultMessage);
            return;
        }

        // Continue main dialouge
        switch (dialog.type)
        {
            case DialogType.Normal:
                {
                    dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.defaultMessage);
                    dialogOption.gameObject.SetActive(true);
                        break;
                }

            case DialogType.Input:
                {
                    inputOption.SetActive(true);
                    Text title = inputOption.transform.Find("title").gameObject.GetComponent<Text>();

                    title.text = LocalizationManager.Instance.GetLocalizedValue(dialog.defaultMessage);
                        break;
                }

            case DialogType.Item:
                {
                    buttonOption.SetActive(true);

                    Text title = buttonOption.transform.Find("title").gameObject.GetComponent<Text>();

                    GameObject buttonContainer = buttonOption.transform.Find("button container").gameObject;
                    

                    title.text = LocalizationManager.Instance.GetLocalizedValue(dialog.defaultMessage);


                    break;
                }
            case DialogType.Choice:
                {
                    buttonOption.SetActive(true);
                    break;
                }

            default:
                {
                    Debug.LogFormat("%s given dialog type not implemented", dialog.type);
                    break;
                }
        }

        //dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.message);
        npcName.text = name;
    }
}
