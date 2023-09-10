using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip deathExplosion;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start() {
           audioSource = GetComponent<AudioSource>();
        }
    
    private void OnCollisionEnter(Collision other) {
        if (isTransitioning) {
            return;
        }

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Hit Friendly Object");
                break;
            case "Finish":
                StartWinSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

        void StartWinSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathExplosion);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadLevelDelay);
    }

    void ReloadLevel() {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
