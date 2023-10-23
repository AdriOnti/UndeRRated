using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RatController : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new(10f, 0f, 0f);
    [SerializeField] float period = 2f;
    public bool isStop = false;

    public float movementFactor;
    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon || isStop) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave;
        Vector3 offset = movementVector *  movementFactor;
        transform.position = startingPos + offset;
    }
}
