using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject beam;
    [SerializeField] private float time;
    [SerializeField] private float beamSize;

    private void Start()
    {
        ShootBeam();
    }

    private void ShootBeam()
    {
        beam.SetActive(true);
        Invoke(nameof(StopBeam), time);
    }

    private void StopBeam()
    {
        beam.SetActive(false);
        Invoke(nameof(ShootBeam), time);
    }
}