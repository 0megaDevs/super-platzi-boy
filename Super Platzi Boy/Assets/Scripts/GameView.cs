using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public static GameView instance;
    public Canvas gameViewCanvas;
    public Text coursesAmountText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int totalCourses = GameManager.instance.totalObjects;
        int collectecCourses = GameManager.instance.collectedObjects;
        coursesAmountText.text = $"{collectecCourses}/{totalCourses}";
    }

    public void Show()
    {
        gameViewCanvas.enabled = true;
    }

    public void Hide()
    {
        gameViewCanvas.enabled = false;
    }
}
