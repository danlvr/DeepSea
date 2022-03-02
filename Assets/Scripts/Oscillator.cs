using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Move the obstacles
/// </summary>
public class Oscillator : MonoBehaviour
{

    #region Variables

    private Vector3 _startingPosition;
    
    /// <summary>
    /// Final position of the obstacle.
    /// </summary>
    [Tooltip("Final position of the obstacle.")]
    [SerializeField] private Vector3 movementVector;
    
    /// <summary>
    /// Speed of movement.
    /// </summary>
    [Tooltip("Speed of movement.")] [SerializeField] private float period = 7f;
    
    #endregion

    #region LifeCyle
    
    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();
    }

    /// <summary>
    /// Move the obstacle.
    /// </summary>
    private void MoveObstacle()
    {
        if (period <= Mathf.Epsilon) return;

        var cycles = Time.time / period; //Continually growing over time.
        const float tau = Mathf.PI * 2;
        var rawSinWave = Mathf.Sin(cycles * tau); //Constant value of 6.283.

        var movementFactor = (rawSinWave + 1f) / 2f; // Recalculated to go from 0 to 1

        var offset = movementVector * movementFactor;
        transform.position = _startingPosition + offset;
    }

    #endregion
}
