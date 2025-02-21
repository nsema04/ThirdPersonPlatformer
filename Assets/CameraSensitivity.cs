using Unity.Cinemachine;
using UnityEngine;
public class CameraSensitivity : MonoBehaviour
{
    public float mouseXSpeed = 100f;
    public float mouseYSpeed = 100f;

    private CinemachineFreeLook freeLookCam;
    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (freeLookCam != null)
        {
            // Get mouse input and apply speed for sensitivity
            currentX += Input.GetAxis("Mouse X") * mouseXSpeed * Time.deltaTime;
            currentY -= Input.GetAxis("Mouse Y") * mouseYSpeed * Time.deltaTime;

            // Clamp the vertical rotation to avoid flipping
            currentY = Mathf.Clamp(currentY, -80f, 80f);

            // Apply the rotation to the FreeLook camera
            freeLookCam.m_XAxis.Value = currentX;
            freeLookCam.m_YAxis.Value = currentY;
        }
    }
}
