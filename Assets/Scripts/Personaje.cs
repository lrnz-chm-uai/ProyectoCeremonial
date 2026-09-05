using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creo la clase Personaje
public class Personaje
{
    // Defino las propiedades

    // Nombre del personaje
    public string Nombre { get; set; }
    // Vida del personaje
    public int Vida { get; set; } // get & set son los métodos de acceso a la propiedad

    // Constructor de la clase Personaje
    public Personaje(string nombre, int vida)
    {
        Nombre = nombre;
        Vida = vida;
    }

    // Metodos que definen el comportamiento del personaje
    public void AtacarConDerecha(Personaje unObjetivo)
    {
        // Lógica de ataque
        unObjetivo.Vida -= 10; // El ataque resta 10 de vida al objetivo
    }
}
