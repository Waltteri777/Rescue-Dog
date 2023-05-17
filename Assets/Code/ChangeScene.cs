using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public SceneTransitionCollision sceneChange;
    public Animator transition;
    public float transitionTime = 1f;

    private void Update()
    {
        if (sceneChange.isSceneChange == true)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
    
    private IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(2f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
