using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;

    Rigidbody myRigidbody;
    AudioSource audioSource;
    bool isThrusting;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopThrusting()
    {
        mainBoosterParticles.Stop();
        audioSource.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }

    void StopRotating()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    void ApplyRotation(float rotation)
    {
        myRigidbody.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        myRigidbody.freezeRotation = false; //Unfreezing rotation so the physic system can control it
    }
}