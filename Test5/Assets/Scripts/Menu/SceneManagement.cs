using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string sceneToChange;
    public void SceneTransition(){
        SceneManager.LoadScene(sceneToChange);
    }
}
