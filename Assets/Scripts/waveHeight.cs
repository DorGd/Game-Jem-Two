using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveHeight : MonoBehaviour
{
    [SerializeField] private float bottomSeaLevel;
    [SerializeField] private float topSeaLevel;
    [SerializeField] private float targetSeaLevel;
    [SerializeField] private Transform wave;
    [SerializeField] private float waveSpeed;
    [SerializeField] private float waveTimer;
    [SerializeField] private float waveStaticTime;


    void Start()
    {
        targetSeaLevel = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if ((wave.position.y - targetSeaLevel) > 1)
        {
            wave.position += new Vector3(0,waveSpeed * -1);
        }
        else if((wave.position.y - targetSeaLevel) < -1)
        {
            wave.position += new Vector3(0, waveSpeed * 1);
        }
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
        }
        else
        {
            lowerWave();
        }
    }

    public void raiseWave(float heightPercentage)
    {
        targetSeaLevel = (Random.Range(0, 0.2f) + heightPercentage) * (topSeaLevel - bottomSeaLevel);
        waveTimer = waveStaticTime;
    }

    void lowerWave()
    {
        targetSeaLevel = bottomSeaLevel;
    }

}
