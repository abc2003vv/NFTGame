using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 4.0f; // Điều chỉnh độ mượt của camera
    public Vector3 offset = new Vector3(0.0f, 5.5f, -3.25f); // Offset dựa trên thông số của bạn
    public Vector3 fixedRotation = new Vector3(50f, 10f, 0.0f); // Giữ nguyên góc quay camera

    void LateUpdate()
    {
        if (target == null) return;

        // Cập nhật vị trí camera theo nhân vật, giữ nguyên độ cao và góc nhìn
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Giữ góc quay cố định
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}
