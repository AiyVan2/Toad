using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera FirstArea;
    [SerializeField] private CinemachineVirtualCamera SecondArea;
    [SerializeField] private CinemachineVirtualCamera ThirdArea;
    [SerializeField] private CinemachineVirtualCamera FourthArea;
    [SerializeField] private CinemachineVirtualCamera FifthArea;
    [SerializeField] private CinemachineVirtualCamera SixthArea;
    [SerializeField] private CinemachineVirtualCamera SeventhArea;
    [SerializeField] private CinemachineVirtualCamera EigthArea;
    [SerializeField] private CinemachineVirtualCamera NinthArea;



    public enum CameraArea
    {
        FirstArea,
        SecondArea,
        ThirdArea,
        FourthArea,
        FifthArea,
        SixthArea,
        SeventArea,
        EightArea,
        NinthArea,
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
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.SecondArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 10;
                    ThirdArea.Priority = 9;
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.ThirdArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 10;
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.FourthArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 9;
                    FourthArea.Priority = 10;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.FifthArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 9;
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 10;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.SixthArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 9;
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 10;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.SeventArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 9;
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 10;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.EightArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 9;
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 10;
                    NinthArea.Priority = 9;
                    break;
                case CameraArea.NinthArea:
                    FirstArea.Priority = 9;
                    SecondArea.Priority = 9;
                    ThirdArea.Priority = 9;
                    FourthArea.Priority = 9;
                    FifthArea.Priority = 9;
                    SixthArea.Priority = 9;
                    SeventhArea.Priority = 9;
                    EigthArea.Priority = 9;
                    NinthArea.Priority = 10;
                    break;
            }
        }
    }
}
