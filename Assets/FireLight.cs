using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Light2D Light2D;
    private float currentIntensity;
    void Start()
    {
        currentIntensity = Light2D.intensity;
    }
    // Update is called once per frame
    void Update()
    {
        Light2D.intensity = currentIntensity + Random.Range(-0.05f, 0.06f);
    }
}
