using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Room currentRoom;

    public float moveSpeedWhenRoomChange;
    public float cameraAfterMoveHeightShift;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if(currentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * CalculateCameraMoveSpeed());
    }

    private Vector3 GetCameraTargetPosition()
    {
        if(currentRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = currentRoom.GetRoomCentre() - new Vector3(0, 0 , 0.5f);

        targetPosition.y = CalculateNewCameraHeight();

        return targetPosition;
    }

    private float CalculateCameraMoveSpeed()
    {
        //mozna zrobic to lepiej.
        return currentRoom.width + currentRoom.length * moveSpeedWhenRoomChange;
    }

    private float CalculateNewCameraHeight()
    {
        //poprawic. ten "algorytm" nie jest najlepszy.
        //https://answers.unity.com/questions/1707551/object-to-appear-in-full-in-camera-view-no-matter-1.html
        //^wydaje sie byc dobrym rozwiazaniem.
        return currentRoom.width + currentRoom.length / cameraAfterMoveHeightShift;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
