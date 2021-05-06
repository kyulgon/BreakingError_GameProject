using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyB : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public MainUI.BTNType currentType;
    public Transform ButtonScale;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = ButtonScale.localScale;
    }

    public void OnButtonClick()
    {
        switch (currentType)
        {
            case MainUI.BTNType.PLAY:
                SceneLoader.LoadSceneHandle("Play", 0);
                break;

            case MainUI.BTNType.QUIT:
                Application.Quit();
                Debug.Log("finish");
                break;

            case MainUI.BTNType.ERROR:
                Error.LoadSceneHandle("Game", 0);
                break;

            case MainUI.BTNType.NEXT:
                MainScene.LoadSceneHandle("MainGame", 0);
                break;

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonScale.localScale = defaultScale;

    }

}

