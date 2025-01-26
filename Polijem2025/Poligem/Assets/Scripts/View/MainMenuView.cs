using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        startButton.onClick.AddListener(() => onButtonClicked());
    }

    void OnDisable()
    {
        startButton.onClick.RemoveListener(() => onButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void onButtonClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
