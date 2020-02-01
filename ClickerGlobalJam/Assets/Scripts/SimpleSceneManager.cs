using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SimpleSceneManager : MonoBehaviour
{   
    public void ChangeSene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
