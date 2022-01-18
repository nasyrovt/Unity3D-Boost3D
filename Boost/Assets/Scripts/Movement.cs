using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

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
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotation)
    {
        myRigidbody.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        myRigidbody.freezeRotation = false; //Unfreezing rotation so the physic system can control it
    }
}