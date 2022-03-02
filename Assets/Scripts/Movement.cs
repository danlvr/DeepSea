using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Rocket movement
/// </summary>
public class Movement : MonoBehaviour
{
    #region Parameters
    
    /// <summary>
    /// Engine thruster sound.
    /// </summary>
    [Tooltip("Engine thruster sound.")]
    [SerializeField] private AudioClip engineSound;
    
    /// <summary>
    /// Main Booster particles.
    /// </summary>
    [Tooltip("Main Booster particles.")]
    [SerializeField] private ParticleSystem mainBoosterParticle;
    
    /// <summary>
    /// Left Booster particles.
    /// </summary>
    [Tooltip("Left Booster particles.")]
    [SerializeField] private ParticleSystem leftBoosterParticle;
    
    /// <summary>
    /// Right Booster particles.
    /// </summary>
    [Tooltip("Right Booster particles.")]
    [SerializeField] private ParticleSystem rightBoosterParticle;
    
    /// <summary>
    /// Number that multiplies the booster thrust
    /// </summary>
    [Tooltip("Number that multiplies the booster thrust")]
    [SerializeField] private float thrustForce = 1000.0f;

    /// <summary>
    /// Number that multiplies the rotation force
    /// </summary>
    [Tooltip("Number that multiplies the rotation force")]
    [SerializeField] private float rotationForce = 100.0f;
    
    #endregion

    #region Cached
    
    /// <summary>
    /// The Rocket Rigidbody component
    /// </summary>
    private Rigidbody _rigidbody;

    /// <summary>
    /// The Rocket AudioSource component
    /// </summary>
    private AudioSource _audioSource;

    #endregion

    #region LifeCycle

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quiting game...");
        }
    }

    /// <summary>
    /// Process input keys to generate the rocket thrust.
    /// </summary>
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    /// <summary>
    /// Generate the rocket thrust force, sound and particle.
    /// </summary>
    private void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * (thrustForce * Time.deltaTime));

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(engineSound, 0.2f);
        }

        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }
    
    /// <summary>
    /// Stop the rocket thrust sound and particle.
    /// </summary>
    private void StopThrusting()
    {
        _audioSource.Stop();
        mainBoosterParticle.Stop();
    }

    /// <summary>
    /// Process input keys to generate the rocket rotation
    /// </summary>
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    /// <summary>
    /// Rotates the Rocket to the right.
    /// </summary>
    private void RotateRight()
    {
        _rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.back * (rotationForce * Time.deltaTime));
        _rigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over

        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    }

    /// <summary>
    /// Rotates the Rocket to the left.
    /// </summary>
    private void RotateLeft()
    {
        _rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * (rotationForce * Time.deltaTime));
        _rigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
       
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }

    /// <summary>
    /// Stop the rotation particles.
    /// </summary>
    private void StopRotating()
    {
        rightBoosterParticle.Stop();
        leftBoosterParticle.Stop();
    }
    
    #endregion

}
