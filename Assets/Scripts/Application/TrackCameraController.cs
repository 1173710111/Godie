using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCameraController : MonoBehaviour
{
    public GameObject trackCamera;
    public GameObject track;
    public void StartMove()
    {
        trackCamera.SetActive(true);
        track.SetActive(true);
    }

    public void Finished()
    {
        trackCamera.SetActive(false);
        track.SetActive(false);
    }
}
