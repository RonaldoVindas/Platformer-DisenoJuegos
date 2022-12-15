using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public int FuerzaSalto;
    public int VelocidadMovimiento;
    public bool TocandoSuelo = false;
    public Animator animador;
    private AudioSource Musica;

    
    void Start()
    {
        Musica = GetComponent<AudioSource>();
        animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && TocandoSuelo == true){
            TocandoSuelo = false;
            animador.Play("Jump_Animation");
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerzaSalto));
        };
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(VelocidadMovimiento, this.GetComponent<Rigidbody2D>().velocity.y);
    }



    void OnCollisionEnter2D(Collision2D c1){
        TocandoSuelo = true;
        animador.Play("run-Animation");
        if(c1.collider.gameObject.tag == "Obstaculo"){
            GameObject.Destroy(this.gameObject);

        }
    }


    void OnTriggerEnter2D(Collider2D c1){ //c1 =significa "collision 1"
        TocandoSuelo = true;
        animador.Play("run-Animation");
        if(c1.tag == "Obstaculo"){
            GameObject.Destroy(this.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision){
        TocandoSuelo = false;
    }
    
}

