using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;       // Velocidad de movimiento horizontal
    public float forwardSpeed = 10f;   // Velocidad de movimiento hacia adelante
    public float laneDistance = 2.5f;  // Distancia entre las "líneas" o "carriles"
    public float horizontalLimit = 5f; // Limite de movimiento horizontal (izquierda/derecha)

    private int currentLane = 0;       // -1 = izquierda, 0 = centro, 1 = derecha

    void Update()
    {
        // Movimiento automático hacia adelante en el eje Z
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Limitar el movimiento horizontal dentro de los límites especificados
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -horizontalLimit, horizontalLimit);
        transform.position = position;
    }

    // Método para moverse a la izquierda
    public void MoveLeft()
    {
        if (currentLane > -1) // Solo moverse si no está ya en el límite izquierdo
        {
            currentLane--;
            Vector3 newPosition = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMove(newPosition));
        }
    }

    // Método para moverse a la derecha
    public void MoveRight()
    {
        if (currentLane < 1) // Solo moverse si no está ya en el límite derecho
        {
            currentLane++;
            Vector3 newPosition = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMove(newPosition));
        }
    }

    // Movimiento suave hacia la nueva posición horizontal (izquierda/derecha)
    private IEnumerator SmoothMove(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // Duración del movimiento suave

        Vector3 startPosition = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
