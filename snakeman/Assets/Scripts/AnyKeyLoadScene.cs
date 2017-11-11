using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyLoadScene : MonoBehaviour {

    public string sceneToLoad;

    void Update()
    {
        if (Input.anyKey && sceneToLoad != null)
            SceneManager.LoadScene(sceneToLoad);
    }
}
