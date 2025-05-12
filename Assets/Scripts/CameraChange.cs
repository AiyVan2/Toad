using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera FirstArea;
    [SerializeField] private CinemachineVirtualCamera SecondArea;
    [SerializeField] private CinemachineVirtualCamera ThirdArea;


    public enum CameraArea
    {
        FirstArea,
        SecondArea,
        ThirdArea,
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
                    ThirdArea.Priority = 9;
                    break;
                case CameraArea.SecondArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 10;
                    ThirdArea.Priority = 9;
                    break;
                case CameraArea.ThirdArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 10;
                    break;
            }
        }
    }
}
