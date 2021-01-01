using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AnyKeyLoadScene : MonoBehaviour {

    public string sceneToLoad;

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed && sceneToLoad != null)
            SceneManager.LoadScene(sceneToLoad);
    }
}
