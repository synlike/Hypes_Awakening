using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CameraTarget : MonoBehaviour
{
    private const int LOW_PRIORITY = 10;
    private const int HIGH_PRIORITY = 20;

    private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    private CinemachineVirtualCamera currentCamera;

    [SerializeField]
    private CinemachineVirtualCamera playerCamera;
    [SerializeField]
    private CinemachineVirtualCamera playerLeftCamera;
    [SerializeField]
    private CinemachineVirtualCamera playerRightCamera;


    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform playerTarget;
    [SerializeField]
    private Transform leftTarget;
    [SerializeField]
    private Transform rightTarget;

    private void Awake()
    {
        PlayerEvents.LookDirectionChanged.Add(OnPlayerLookDirectionChanged);
    }

    private void Start()
    {
        cameras.Add(playerCamera);
        cameras.Add(playerLeftCamera);
        cameras.Add(playerRightCamera);

        currentCamera = playerCamera;

        for(int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i] == currentCamera)
            {
                cameras[i].Priority = HIGH_PRIORITY;
            }
            else
            {
                cameras[i].Priority = LOW_PRIORITY;
            }
        }
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    private void OnDestroy()
    {
        PlayerEvents.LookDirectionChanged.Remove(OnPlayerLookDirectionChanged);
    }

    private void OnPlayerLookDirectionChanged(ECameraTargetPosition position)
    {
        switch (position)
        {
            case ECameraTargetPosition.LEFT:
                SwitchCamera(playerRightCamera);
                break;
            case ECameraTargetPosition.RIGHT:
                SwitchCamera(playerLeftCamera);
                break;
            case ECameraTargetPosition.PLAYER:
                SwitchCamera(playerCamera);
                break;
        }
    }

    private void SwitchCamera(CinemachineVirtualCamera newCam)
    {
        currentCamera = newCam;

        currentCamera.Priority = HIGH_PRIORITY;

        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i] != currentCamera)
            {
                cameras[i].Priority = LOW_PRIORITY;
            }
        }
    }
}
