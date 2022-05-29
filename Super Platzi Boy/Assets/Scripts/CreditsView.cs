using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsView : MonoBehaviour
{
    public static CreditsView instance;
    public Canvas creditsViewCanvas;
    public Animator creditsAnimator;

    private void Awake()
    {
        if (instance == null)
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

    }

    public void Show()
    {
        creditsViewCanvas.enabled = true;
        Play();
        Invoke("BackToMenu", 20);
    }

    void Play()
    {
        creditsAnimator.SetTrigger("startCredits");
    }

    public void Hide()
    {
        creditsViewCanvas.enabled = false;
    }

    void BackToMenu()
    {
        GameManager.instance.BackToMenu();
    }
}
