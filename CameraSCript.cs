using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSCript : MonoBehaviour
{
    //Parametros da camera
    [Header("Componentes")]
    public GameObject player;
    float rotX;
    Vector3 distance;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        distance = transform.position - player.transform.position;
    }

    void Update()
    {
        cameraMove();
    }
    //Funções da camera
    void cameraMove()
    {
        rotX += Input.GetAxis("Mouse Y");
        rotX = Mathf.Clamp(rotX, -15f, 15f);

        Quaternion rotation = Quaternion.Euler(-rotX, player.transform.eulerAngles.y, 0);

        transform.position = player.transform.position + rotation * distance;
        transform.LookAt(GameObject.Find("CameraTarget").transform.position);
    }
}
