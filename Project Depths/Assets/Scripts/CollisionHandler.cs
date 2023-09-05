using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Hit Friendly Object");
                break;
            case "Finish":
                Debug.Log("Hit Finish Object");
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel() {
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }
}
