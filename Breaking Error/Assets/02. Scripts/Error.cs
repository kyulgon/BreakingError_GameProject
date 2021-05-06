using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Error : MonoBehaviour
{
    public static string loadError;
    public static int loadType1;

    private void Start()
    {
        StartCoroutine(LoadError());
    }

    public static void LoadSceneHandle(string _name, int _loadType1)
    {
        loadError = _name;
        loadType1 = _loadType1;
        
        SceneManager.LoadScene("Game");
    }

   
    IEnumerator LoadError()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (loadType1 == 0)
                Debug.Log("new game");
           
        }


    }

    

}

