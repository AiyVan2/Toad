using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera FirstArea;
    [SerializeField] private CinemachineVirtualCamera SecondArea;
    [SerializeField] private CinemachineVirtualCamera FirstLevelThirdArea;


    public enum CameraArea
    {
        FirstArea,
        SecondArea,
        FirstLevelThirdArea,
    }

    [SerializeField] private CameraArea area;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (area)
            {
                case CameraArea.FirstArea:
                    FirstArea.Priority = 10;
                    SecondArea.Priority = 9;
                    FirstLevelThirdArea.Priority = 9;
                    break;
                case CameraArea.SecondArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 10;
                    FirstLevelThirdArea.Priority = 9;
                    break;
                case CameraArea.FirstLevelThirdArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    FirstLevelThirdArea.Priority = 10;
                    break;
            }
        }
    }
}
