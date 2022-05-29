using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public Canvas mainMenuCanvas;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.enabled = true;
    }

    public void HideMeinMenu()
    {
        mainMenuCanvas.enabled = false;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
