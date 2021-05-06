using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public static string loadMain;
    public static int loadType2;

    private void Start()
    {
        StartCoroutine(LoadMain());
    }

    public static void LoadSceneHandle(string _name2, int _loadType2)
    {
        loadMain = _name2;
        loadType2 = _loadType2;

        SceneManager.LoadScene("MainGame");
    }


    IEnumerator LoadMain()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainGame");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (loadType2 == 0)
                Debug.Log("new game");

        }


    }



}

