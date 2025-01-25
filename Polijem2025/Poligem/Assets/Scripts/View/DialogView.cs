using System.Collections;
using System.Collections.Generic;
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

        switch(dialog.type)
        {
            case DialogType.Normal:
                dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.message);
                break;

            case DialogType.Input:
                break;

            case DialogType.Item:
                break;

            case DialogType.Choice:
                break;

            default:
                Debug.LogFormat("%s given dialog type not implemented", dialog.type);
                break;
        }

        dialogOption.text = LocalizationManager.Instance.GetLocalizedValue(dialog.message);
        npcName.text = name;
    }
}
