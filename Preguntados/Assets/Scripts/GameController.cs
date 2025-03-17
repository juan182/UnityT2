using models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    string lineaLeida = "";
    List<PreguntasMultiples> listaPreguntasMultiples;
    List<PreguntasMultiples> listaPreguntasMultiplesFacil;
    List<PreguntasMultiples> listaPreguntasMultiplesDificil;
    List<PreguntasFV> listaPreguntasFV;
    List<PreguntasFV> listaPreguntasFVFacil;
    List<PreguntasFV> listaPreguntasFVDificil;
    List<PreguntasAbiertas> listaPreguntasAbierta;
    List<PreguntasAbiertas> listaPreguntasAbiertasFacil;
    List<PreguntasAbiertas> listaPreguntasAbiertasDificil;

    string respuestaPM;
    string respuestaFV;

    int perdidas;
    int ganadas;


    //PreguntasMultiples
    public TextMeshProUGUI textPregunta;
    public TextMeshProUGUI textResp1;
    public TextMeshProUGUI textResp2;
    public TextMeshProUGUI textResp3;
    public TextMeshProUGUI textResp4;
    public TextMeshProUGUI textVersC;
    public TextMeshProUGUI textVersI;
    public TextMeshProUGUI textRespuestaC;

    //FalsoVerdadero
    public TextMeshProUGUI textVersiculoF;
    public TextMeshProUGUI textVersiculoV;
    public TextMeshProUGUI textRespuestaF;
    public TextMeshProUGUI textRespuestaV;
    public TextMeshProUGUI textPreguntaFV;
    public TextMeshProUGUI textRespuestaCF;

    //PreguntasAbiertas
    public TextMeshProUGUI textPreguntaA;
    public TextMeshProUGUI textRespuestaA;
    public TextMeshProUGUI textVersiculoA;

    //PanelFinal
    public TextMeshProUGUI textGanadas;
    public TextMeshProUGUI textPerdidas;


    //Paneles
    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;
    public GameObject panelPrincipal;

    public GameObject panelFin;

    public GameObject panelPreguntaFV;
    public GameObject panelCorrectoF;
    public GameObject panelIncorrectoV;

    public GameObject panelPreguntaA;
    public GameObject panelRespuestaA;

    public Button continuar;

    public bool facil;

    
    
    


    // Start is called before the first frame update
    void Start()
    {
        listaPreguntasMultiples = new List<PreguntasMultiples>();
        listaPreguntasMultiplesFacil = new List<PreguntasMultiples>();
        listaPreguntasMultiplesDificil = new List<PreguntasMultiples>();

        listaPreguntasFV = new List<PreguntasFV>();
        listaPreguntasFVFacil = new List<PreguntasFV>();
        listaPreguntasFVDificil = new List<PreguntasFV>();

        listaPreguntasAbierta = new List<PreguntasAbiertas>();
        listaPreguntasAbiertasFacil = new List<PreguntasAbiertas>();
        listaPreguntasAbiertasDificil = new List<PreguntasAbiertas>();

        LecturaPreguntasMultiples();
        LecturaPreguntasFV();
        LecturaPreguntasAbiertas();
        mostrarOtraPregunta();

        ganadas = 0;
        perdidas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarOtraPregunta()
    {
       

        List<System.Object> preguntasDisponibles = new List<System.Object>();

        
        preguntasDisponibles.AddRange(listaPreguntasAbiertasFacil);
        preguntasDisponibles.AddRange(listaPreguntasFVFacil);
        preguntasDisponibles.AddRange(listaPreguntasMultiplesFacil);

        if (preguntasDisponibles.Count == 0)
        {
            preguntasDisponibles.AddRange(listaPreguntasAbiertasDificil);
            preguntasDisponibles.AddRange(listaPreguntasMultiplesDificil);
            preguntasDisponibles.AddRange(listaPreguntasFVDificil);
        }


        if (preguntasDisponibles.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, preguntasDisponibles.Count);
            object preguntaSeleccionada = preguntasDisponibles[i];

            if(preguntaSeleccionada is PreguntasMultiples)
            {
                mostrarPreguntasMultiples((PreguntasMultiples)preguntaSeleccionada);
                listaPreguntasMultiplesFacil.Remove((PreguntasMultiples)preguntaSeleccionada);
                listaPreguntasMultiplesDificil.Remove((PreguntasMultiples)preguntaSeleccionada);
            }
            else if(preguntaSeleccionada is PreguntasFV)
            {
                mostrarPreguntasFV((PreguntasFV)preguntaSeleccionada);
                listaPreguntasFVFacil.Remove((PreguntasFV)preguntaSeleccionada);
                listaPreguntasFVDificil.Remove((PreguntasFV)preguntaSeleccionada);
            }
            else if(preguntaSeleccionada is PreguntasAbiertas)
            {
                mostrarPreguntasAbiertas((PreguntasAbiertas)preguntaSeleccionada);
                listaPreguntasAbiertasFacil.Remove((PreguntasAbiertas)preguntaSeleccionada);
                listaPreguntasAbiertasDificil.Remove((PreguntasAbiertas)preguntaSeleccionada);
            }
            else
            {
                panelPrincipal.SetActive(false);
                panelPreguntaFV.SetActive(false);
                panelFin.SetActive(true);
            }
        }
        else if(preguntasDisponibles.Count==0)
        {
            mostrarScore();
        }
    }

    #region mostrar preguntas 
    public void mostrarPreguntasMultiples(PreguntasMultiples pregunta)
    {
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        panelPrincipal.SetActive(true);
        panelFin.SetActive(false);
        panelPreguntaFV.SetActive(false);
        panelPreguntaA.SetActive(false);

        textPregunta.text = pregunta.Pregunta;
        textResp1.text = pregunta.Respuesta1;
        textResp2.text = pregunta.Respuesta2;
        textResp3.text = pregunta.Respuesta3;
        textResp4.text = pregunta.Respuesta4;
        respuestaPM = pregunta.RespuestaCorrecta;
        textVersC.SetText(pregunta.Versiculo);
        textVersI.SetText(pregunta.Versiculo);
        textRespuestaC.SetText("La respuesta correcta es: " + pregunta.RespuestaCorrecta);
        
    }

    public void mostrarPreguntasFV(PreguntasFV pregunta)
    {
        panelPreguntaFV.SetActive(true);
        panelIncorrectoV.SetActive(false);
        panelCorrectoF.SetActive(false);
        panelPrincipal.SetActive(false);
        panelFin.SetActive(false);
        panelPreguntaA.SetActive(false);


        respuestaFV = pregunta.Respuesta;
        textVersiculoF.SetText(pregunta.Versiculo);
        textVersiculoV.SetText(pregunta.Versiculo);
        textPreguntaFV.SetText(pregunta.Pregunta);
        textRespuestaCF.SetText(pregunta.Respuesta);

    }

    public void mostrarPreguntasAbiertas(PreguntasAbiertas pregunta)
    {
        panelPreguntaFV.SetActive(false);
        panelPrincipal.SetActive(false);
        panelFin.SetActive(false);
        panelRespuestaA.SetActive(false);
        panelPreguntaA.SetActive(true);

        textPreguntaA.text = pregunta.Pregunta;

        textRespuestaA.SetText(pregunta.Respuesta);
        textVersiculoA.SetText(pregunta.Versiculo);
    }

    #endregion 

    

    public void mostrarScore()
    {
        panelPrincipal.SetActive(false);
        panelPreguntaFV.SetActive(false);
        panelPreguntaA.SetActive(false);
        panelFin.SetActive(true);

        textGanadas.SetText("Respuestas Correctas: " + ganadas.ToString());
        textPerdidas.SetText("Respuestas Incorrectas: " + perdidas.ToString());
    }

    public void cerrarQuiz()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
            
    }

    public void botonRespuestaAbierta()
    {
        
        panelFin.SetActive(false);
        panelPrincipal.SetActive(false);
        panelPreguntaFV.SetActive(false);
        panelPreguntaA.SetActive(true);
        panelRespuestaA.SetActive(true);
    }

    public void comprobarRespuesta1()
    {
        if (textResp1.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 1");

            panelCorrecto.SetActive(true);
            panelIncorrecto.SetActive(false);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            ganadas += 1;

        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 1");
            panelCorrecto.SetActive(false);
            panelIncorrecto.SetActive(true);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            perdidas += 1;
        }
    }

    public void comprobarRespuesta2()
    {
        if (textResp2.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 2");
            panelCorrecto.SetActive(true);
            panelIncorrecto.SetActive(false);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            ganadas += 1;

        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 2");
            panelCorrecto.SetActive(false);
            panelIncorrecto.SetActive(true);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            perdidas += 1;

        }
    }

    public void comprobarRespuesta3()
    {
        if (textResp3.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 3");
            panelCorrecto.SetActive(true);
            panelIncorrecto.SetActive(false);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            ganadas += 1;

        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 3");
            panelCorrecto.SetActive(false);
            panelIncorrecto.SetActive(true);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            perdidas += 1;

        }
    }

    public void comprobarRespuesta4()
    {
        if (textResp4.text.Equals(respuestaPM))
        {
            Debug.Log("Respuesta Correcta es la 4");
            panelCorrecto.SetActive(true);
            panelIncorrecto.SetActive(false);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            ganadas += 1;

        }
        else
        {
            Debug.Log("Respuesta Incorrecta es la 4");
            panelCorrecto.SetActive(false);
            panelIncorrecto.SetActive(true);
            panelPrincipal.SetActive(true);
            panelFin.SetActive(false);

            perdidas += 1;

        }
    }

    public void comprobarRespuestaFalso()
    {
        if (textRespuestaF.text.Equals(respuestaFV))
        {
            panelPreguntaFV.SetActive(true);
            panelIncorrectoV.SetActive(false);
            panelCorrectoF.SetActive(true);
            panelPrincipal.SetActive(false);
            panelFin.SetActive(false);

            Debug.Log("Respuesta Correcta es Falso");

            ganadas += 1;

        }
        else
        {
            panelPreguntaFV.SetActive(true);
            panelIncorrectoV.SetActive(true);
            panelCorrectoF.SetActive(false);
            panelPrincipal.SetActive(false);
            panelFin.SetActive(false);

            Debug.Log("Respuesta incorrecta");

            perdidas += 1;

        }
    }

    public void comprobarRespuestaVerdadero()
    {
        if (textRespuestaV.text.Equals(respuestaFV))
        {
            Debug.Log("Respuesta correcta es Verdadero");

            panelPreguntaFV.SetActive(true);
            panelIncorrectoV.SetActive(false);
            panelCorrectoF.SetActive(true);
            panelPrincipal.SetActive(false);
            panelFin.SetActive(false);

            ganadas += 1;

        }
        else
        {
            Debug.Log("Respuesta incorrecta");

            panelPreguntaFV.SetActive(true);
            panelIncorrectoV.SetActive(true);
            panelCorrectoF.SetActive(false);
            panelPrincipal.SetActive(false);
            panelFin.SetActive(false);

            perdidas += 1;

        }
    }

    #region Lectura archivos

    public void LecturaPreguntasAbiertas()
    {
        try
        {
            StreamReader srlpa = new StreamReader("Assets/Files/ArchivoPreguntasAbiertas.txt");
            while ((lineaLeida = srlpa.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string preguntaA = lineaPartida[0];
                string respuestaA = lineaPartida[1];
                string versiculoA = lineaPartida[2];
                string dificultad = lineaPartida[3];

                PreguntasAbiertas objPA = new PreguntasAbiertas(preguntaA,respuestaA,versiculoA,dificultad);

                listaPreguntasAbierta.Add(objPA);
                if (dificultad.Equals("facil"))
                {
                    listaPreguntasAbiertasFacil.Add(objPA);
                }
                else
                {
                    listaPreguntasAbiertasDificil.Add(objPA);
                }
            }
            Debug.Log("El tamaño de la lista preguntas abiertas es: " + listaPreguntasAbierta.Count);
        } catch (Exception e)
        {
            Debug.Log("Error!!!" + e.ToString());
        }
        finally
        {
            Debug.Log("Executing finally block.");
        }
    }

    public void LecturaPreguntasFV()
    {
        try
        {
            StreamReader srlvf = new StreamReader("Assets/Files/preguntasFalso_Verdadero.txt");
            while ((lineaLeida = srlvf.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string preguntaVf = lineaPartida[0];
                string respuestaVf = lineaPartida[1];
                string versiculoVf = lineaPartida[2];
                string dificultadVf = lineaPartida[3];

                PreguntasFV objPFV = new PreguntasFV(preguntaVf,respuestaVf,versiculoVf,dificultadVf);

                listaPreguntasFV.Add(objPFV);

                if (dificultadVf.Equals("facil"))
                {
                    listaPreguntasFVFacil.Add(objPFV);
                }
                else
                {
                    listaPreguntasFVDificil.Add(objPFV);
                }

            }
            Debug.Log("El tamaño de la lista es: " + listaPreguntasFV.Count);
        }
        catch (Exception e)
        {
            Debug.Log("Error!!!" + e.ToString());
        }
        finally
        {
            Debug.Log("Executing finally block.");
        }
    }

    
    public void LecturaPreguntasMultiples()
    {
        try
        {
            StreamReader srl = new StreamReader("Assets/Files/ArchivoPreguntasM.txt");
            while((lineaLeida=srl.ReadLine()) != null)
            {
                string[] lineaPartida = lineaLeida.Split("-");
                string pregunta = lineaPartida[0];
                string respuesta1 = lineaPartida[1];
                string respuesta2 = lineaPartida[2];
                string respuesta3 = lineaPartida[3];
                string respuesta4 = lineaPartida[4];
                string respuestaCorrecta = lineaPartida[5];
                string versiculo = lineaPartida[6];
                string dificultad = lineaPartida[7];

                PreguntasMultiples objPM = new PreguntasMultiples(pregunta, respuesta1, respuesta2,
                    respuesta3, respuesta4, respuestaCorrecta, versiculo, dificultad);

                listaPreguntasMultiples.Add(objPM);

                if (dificultad.Equals("facil"))
                {
                    listaPreguntasMultiplesFacil.Add(objPM);
                }
                else
                {
                    listaPreguntasMultiplesDificil.Add(objPM);
                }
            }
            Debug.Log("El tamaño de lista es " + listaPreguntasMultiples.Count);

        }
        catch(Exception e)
        {
            Debug.Log("ERROR!!!!" + e.ToString());
        }
        finally
        {
            Debug.Log("Executing finally block.");
        }
        
    }


    #endregion
}
