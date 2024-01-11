using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private float shake = 0;
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (shake > 0)
        {
            camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0;
        }
    }
}

