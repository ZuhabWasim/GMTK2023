using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    [SerializeField]
    private float _transitionTime = 1f;

    IEnumerator TransitionAndLoad(int levelIndex)
    {
        transition.SetBool("Start", true);
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel(int levelIndex) {
        StartCoroutine(TransitionAndLoad(levelIndex));
    }

    public void StartGame()
    {
        LoadLevel(1);
    }

    public void OnGameOver()
    {
        Debug.Log("game ending caught");
        LoadLevel(0);
    }

    public void EndGame()
    {
        // If we are running in a standalone build of the game
        #if UNITY_STANDALONE
            Application.Quit(); // Quit the application
        #endif

        // If we are running in the editor
        #if UNITY_EDITOR
            UnityEditor
                .EditorApplication
                .isPlaying = false; // Stop playing the scene
        #endif
    }
}
