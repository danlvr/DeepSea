using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Plays the level background music.
/// </summary>
public class PlayLevelMusic : MonoBehaviour
{
    
    #region Cached
   
    /// <summary>
    /// The level AudioSource component
    /// </summary>
    private AudioSource _audioSource;
    
    /// <summary>
    /// Instance reference.
    /// </summary>
    private static PlayLevelMusic Instance { get; set; }
    
    #endregion
    
    #region LifeCycle
    
    // Start is called before the first frame update
    void Start()
    {
        // Checks if another instance of this object is present. 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
       
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();

    }
    
    #endregion
}
