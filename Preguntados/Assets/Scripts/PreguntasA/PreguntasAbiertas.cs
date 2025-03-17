using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreguntasAbiertas 
{
    private string pregunta;
    private string respuesta;
    private string versiculo;
    private string dificultad;

    public PreguntasAbiertas()
    {
    }

    public PreguntasAbiertas(string pregunta, string respuesta, string versiculo, string dificultad)
    {
        this.pregunta = pregunta;
        this.respuesta = respuesta;
        this.versiculo = versiculo;
        this.dificultad = dificultad;
    }

    public string Pregunta { get => pregunta; set => pregunta = value; }
    public string Respuesta { get => respuesta; set => respuesta = value; }
    public string Versiculo { get => versiculo; set => versiculo = value; }
    public string Dificultad { get => dificultad; set => dificultad = value; }
}
