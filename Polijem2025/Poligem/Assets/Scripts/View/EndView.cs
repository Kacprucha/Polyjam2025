using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndView : MonoBehaviour
{
    [SerializeField] Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener (() =>
        {
            SceneManager.LoadScene ("MainMenu");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
