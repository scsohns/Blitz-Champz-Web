using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpSceneSwap : MonoBehaviour
{
    public void Onclick()
    {
        SceneManager.LoadScene(3);
    }
}
