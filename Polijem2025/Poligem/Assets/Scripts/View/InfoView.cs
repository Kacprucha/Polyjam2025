using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoView : MonoBehaviour
{
    [SerializeField] Text info;
    [SerializeField] Text buttonLabel;
    [SerializeField] Button button;

    CollectableInfo iteam;

    // Start is called before the first frame update
    void Start()
    {
        buttonLabel.text = "OK";
    }

    void OnEnable ()
    {
        button.onClick.AddListener (() => onButtonClicked ());
    }

    void OnDisable ()
    {
        button.onClick.RemoveListener (() => onButtonClicked ());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo (string info, CollectableInfo iteamToGet = null)
    {
        this.info.text = LocalizationManager.Instance.GetLocalizedValue(info);
        this.gameObject.SetActive (true);

        iteam = iteamToGet;
    }

    protected void onButtonClicked ()
    {
        if (iteam != null)
        {
            PlayerInfo.Instance.AddCollectable (iteam);
            iteam = null;
        }

        this.gameObject.SetActive (false);
    }
}
