using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDePrueba : MonoBehaviour
{
    // Instancio un objeto de la clase Personaje
    Personaje personaje1 = new Personaje("Aquiles", 100);
    Personaje personaje2 = new Personaje("Hector", 100);
    // Start is called before the first frame update
    void Start()
    {
        // Estado inicial de los personajes
        Debug.Log($"Estado inicial: {personaje1.Nombre} tiene {personaje1.Vida} de vida.");
        Debug.Log($"Estado inicial: {personaje2.Nombre} tiene {personaje2.Vida} de vida.");

        // Acciones de ataque
        personaje1.AtacarConDerecha(personaje2);
        personaje1.AtacarConDerecha(personaje2);
        personaje2.AtacarConDerecha(personaje1);

        // Estado final de los personajes
        Debug.Log($"Estado final: {personaje1.Nombre} tiene {personaje1.Vida} de vida.");
        Debug.Log($"Estado final: {personaje2.Nombre} tiene {personaje2.Vida} de vida.");
    }


    // Update is called once per frame
    // Este comportamiento viene dado por la clase MonoBehaviour,
    // que es la clase base de todos los scripts en Unity.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log($"{personaje1.Nombre} ataca a {personaje2.Nombre} presionando la tecla W.");
            // TODO: revisar por qué no imprime el mensaje de ataque en la consola con cada ataque, pero sí imprime el estado actual de los personajes.
            personaje1.AtacarConDerecha(personaje2);
            Debug.Log($"Estado actual: {personaje2.Nombre} tiene {personaje2.Vida} de vida.");
        }
    }
}
