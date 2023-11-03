using System;
using System.Collections;
using UnityEngine;

public class NPC_TickManager : MonoBehaviour
{
    public event EventHandler OnTick;
    [Tooltip("The time between each tick in seconds")]
    [SerializeField]private float tickRate = 3;
    void Start()
    {
        StartCoroutine(Tick());
    }
    
    private IEnumerator Tick()
    {
        while (true)
        {
            OnTick?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(tickRate);
        }
    }
    
}
