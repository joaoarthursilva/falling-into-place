using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject beam;
    [SerializeField] private float time;
    [SerializeField] private float beamSize;

    [SerializeField] private bool isFacingRight;

    private void Start()
    {
        ShootBeam();
    }

    public void UpdateBeamParams()
    {
        beam.transform.localScale = new Vector3(beamSize, beam.transform.localScale.y, beam.transform.localScale.z);
        beam.transform.localPosition =
            new Vector3(isFacingRight ? beamSize / 2 : -(beamSize / 2), beam.transform.localPosition.y,
                beam.transform.localPosition.z);
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