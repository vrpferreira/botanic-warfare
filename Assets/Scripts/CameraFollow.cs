using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O objeto a ser seguido
    public float smoothSpeed = 5f; // A suavidade do movimento da câmera

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posição desejada da câmera com base na posição do alvo
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Usa o método Lerp para suavizar o movimento da câmera
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
