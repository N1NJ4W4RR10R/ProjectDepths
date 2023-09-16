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
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    ParticleSystem gameParticleSystem;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() {
           audioSource = GetComponent<AudioSource>();
           gameParticleSystem = GetComponent<ParticleSystem>();
        }

    void Update() {
        ActionDevTool();
    }
    
    private void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionDisabled) {
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
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(deathExplosion);
        crashParticles.Play();
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

    void ActionDevTool() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();  
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        } 
    }
}
