using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        //transform.Rotate(Vector3.up, 90 * Time.deltaTime); // Rotar el objeto alrededor del eje Y a 90 grados por segundo
        // deltaTime corresponde a el tiempo que ha pasado desde el último frame,
        // lo que permite que la rotación sea consistente independientemente de la velocidad de fotogramas del juego.
    }
}
