using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the Rocket collision: reloads the current level if it crashes, go to the next level if it lands in the landing pad.
/// </summary>

public class CollisionHandler : MonoBehaviour
{
    #region Parameters
  
    /// <summary>
    /// Delay before level load and restart.
    /// </summary>
    [Tooltip("Delay before level load and restart.")]
    [SerializeField] private float levelLoadDelay = 2.0f;
    
    /// <summary>
    /// Delay before restarting after crashing.
    /// </summary>
    [Tooltip("Delay before restarting after crashing.")]
    [SerializeField] private float crashDelay = 2.0f;
    
    /// <summary>
    ///  Sound when the Rocket crashes.
    /// </summary>
    [Tooltip("Sound when the Rocket crashes.")]
    [SerializeField] private AudioClip crashingSound;
    
    /// <summary>
    ///  Particles when the Rocket crashes.
    /// </summary>
    [Tooltip("Particles when the Rocket crashes.")]
    [SerializeField] private ParticleSystem crashingParticle;
    
    #endregion
    
    #region Cached
    
    /// <summary>
    /// The Rocket AudioSource component
    /// </summary>
    private AudioSource _audioSource;
    
    #endregion

    #region States

    private bool _isTransitioning;

    #endregion

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning) return;
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit friendly");
                break;
            
            case "Finish":
                StartSuccessSequence();
                break;
       
            default:
                StartCrashSequence();
                break;
        }

    }

    /// <summary>
    ///  When the Rocket lands on the landing pad play sound and effects then call the load next level method.
    /// </summary>
    private void StartSuccessSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    /// <summary>
    ///  When the Rocket crashes play sound and effects then call the reload current level method.
    /// </summary>
    private void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashingSound);
        crashingParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel),crashDelay);
    }
    
    /// <summary>
    /// Reloads the current level.
    /// </summary>
    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    /// <summary>
    /// Loads the next level when the Rocket lands. In the last level, reloads the 1st one.
    /// </summary>
    private void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
