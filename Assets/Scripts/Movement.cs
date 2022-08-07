using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float mainRotation = 1000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;


    Rigidbody rb;
    AudioSource aS;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
        
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
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //or rb.AddRelativeForce(0,1,0);
        if (!aS.isPlaying)
        {
            aS.PlayOneShot(mainEngine);


        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }
   
    void StopThrusting()
    {
        aS.Stop();
        mainBooster.Stop();
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRotate();
        }

    }

    void RotateLeft()
    {
        ApplyRotation(mainRotation);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-mainRotation);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }
    void StopRotate()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }   
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }


}


