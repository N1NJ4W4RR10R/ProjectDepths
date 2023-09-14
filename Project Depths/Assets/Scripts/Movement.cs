using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngineAudio;
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            startMainThrust();
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            stopMainThrust();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A)) {
            startRightThrust();
        }
        else if (Input.GetKey(KeyCode.D)){
            startLeftThrust();
        }
        else {
            stopLeftRightThrustParticles();
        }
    }

    void startMainThrust() {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngineAudio);
        }
        if (!mainThrusterParticles.isPlaying) {
            mainThrusterParticles.Play();
        }
    }   

    void stopMainThrust() {
        audioSource.Pause();
        mainThrusterParticles.Stop();
    }

    void startRightThrust() {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying) {
            rightThrusterParticles.Play();
        }
    }   

    void startLeftThrust() {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying) {
            leftThrusterParticles.Play();
        }
    }    

    void stopLeftRightThrustParticles() {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }    

    private void ApplyRotation(float rotationThisFrame) {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    }
}
