using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MyButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MainUI.BTNType currentType;
    public Transform ButtonScale;
    Vector3 defaultScale;

    // 에러위에 올라갔을때 소리
    public AudioSource errorAudioSource;
    public AudioSource nextAudioSource;


    private void Start()
    {
        defaultScale = ButtonScale.localScale;
    }

    public void OnButtonClick()
    {
        switch (currentType)
        {
            case MainUI.BTNType.PLAY:
                SceneLoader.LoadSceneHandle("Load", 0);
                break;

            case MainUI.BTNType.QUIT:
                Application.Quit();
                Debug.Log("finish");
                break;

            case MainUI.BTNType.ERROR:
                Error.LoadSceneHandle("Next", 0);
                break;

            case MainUI.BTNType.NEXT:
                MainScene.LoadSceneHandle("MainGame", 0);
                break;

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonScale.localScale = defaultScale * 1.2f;
        
        if(SceneManager.GetActiveScene().name == "Error")
        {
            Debug.Log("A");
            errorAudioSource.Play();
        }

        if (SceneManager.GetActiveScene().name == "Next")
        {
            Debug.Log("B");
            nextAudioSource.Play();
        }

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonScale.localScale = defaultScale;

    }

}

