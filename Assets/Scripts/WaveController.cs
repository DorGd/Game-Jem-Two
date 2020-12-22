using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public float bottomLimit;
    public float topLimit;
    [SerializeField] private float buttonSpeed;

    [SerializeField] private Transform waveHeightController;
    [SerializeField] private bool dirBottomUp;

    // Start is called before the first frame update
    void Start()
    {
        waveHeightController = this.GetComponent<Transform>();
        dirBottomUp = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (dirBottomUp)
        {
            waveHeightController.position += new Vector3(0, buttonSpeed * 1 );
        }
        else
        {
            waveHeightController.position += new Vector3(0, buttonSpeed * - 1 );
        }
        if (waveHeightController.position.y > topLimit)
            dirBottomUp = false;
        else if (waveHeightController.position.y < bottomLimit )
        {
            dirBottomUp =  true;
        }
        

    }
}
