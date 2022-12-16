


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. UI;
using UnityEngine. SceneManagement;

public class ControlEscena : MonoBehaviour
{

    public GameObject Jugador;
    public Camera CamaraJuego;

    public GameObject[] BloquePreFab;               //Bloques Prefabricados
    public float PunteroJuego;
    public float LugarGeneracionSeguro = 12;        //Define a partir de qué posición en el eje x en la que es seguro cargar el nuevo bloque de obstáculos

    public Text TextoJuego;
    public bool Perdiste;


    // Start is called before the first frame update
    void Start()
    {
        PunteroJuego = -7;
        Perdiste = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Jugador != null){
        CamaraJuego.transform.position = new Vector3(Jugador.transform.position.x,
                                                    CamaraJuego.transform.position.y,
                                                    CamaraJuego.transform.position.z);
        if(Mathf.Floor(Jugador.transform.position.x) <= 0){
            TextoJuego.text = "Puntos = 0";
        }else{
        TextoJuego.text = "Puntos = " + Mathf.Floor(Jugador.transform.position.x);
        }
        }

        else{
            if(!Perdiste){
                TextoJuego.text += "\n Se terminó el juego! \n Presione R para reiniciar"; 
                Perdiste = true;
                
            }
            if(Perdiste){
                if(Input.GetKeyDown("r")){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }



        while( Jugador != null && PunteroJuego <Jugador.transform.position.x + LugarGeneracionSeguro){
            int IndiceBloque = Random.Range(0,4);
            if(PunteroJuego < 0){
                IndiceBloque = 0;
            }

            GameObject ObjetoBloque = Instantiate(BloquePreFab[IndiceBloque]); // Al instanciar un bloque, este se crea fuera del orden jerárquico de este control de escena y no podemos tener sobre él dentro de Unity

            ObjetoBloque.transform.SetParent(this.transform);           //Esta línea corrige el problema anterior
            Bloque bloque = ObjetoBloque.GetComponent<Bloque>();
            ObjetoBloque.transform.position = new Vector2(PunteroJuego + bloque.Tamaño/2, 0);
            PunteroJuego += bloque.Tamaño;
        }
    }
}
