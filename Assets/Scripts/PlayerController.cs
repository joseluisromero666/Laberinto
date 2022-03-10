using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    //private AudioSource audioRecoleccion;
    public GameObject SonidoMoneda;

    public GameObject SonidoMonedaNegativa;

    public GameObject SonidoMonedaTiempo;

    public GameObject SonidoMonedaTiempoNegativo;

    public GameObject SonidoPared;

    public GameObject SonidoDesaparecer;

    public GameObject SonidoGanador;

    private GameObject rampa;

    private int puntos;

    private float StartTime;

    public Text textoPuntos;

    public Text textoStartTime;

    private bool rampaDestruida = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // audioRecoleccion = GetComponent<AudioSource>();
        rampa = GameObject.FindWithTag("Rampa");
        Debug.Log (rampa);
        StartTime = Time.time;
        puntos = 0;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movimiento * speed);
        StartTime += 1 * Time.deltaTime;
        textoStartTime.text = "Tiempo : " + StartTime.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RecolectablePositivo"))
        {
            Instantiate (SonidoMoneda);
            puntos += 100;
            textoPuntos.text = "Puntos : " + puntos.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("RecolectableNegativo"))
        {
            Instantiate (SonidoMonedaNegativa);
            puntos -= 50;
            textoPuntos.text = "Puntos : " + puntos.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("RecolectableTiempo"))
        {
            Instantiate (SonidoMonedaTiempo);
            StartTime -= 10;
            textoStartTime.text = "Tiempo : " + StartTime.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("RecolectableTiempoNegativo"))
        {
            Instantiate (SonidoMonedaTiempoNegativo);
            StartTime += 20;
            textoStartTime.text = "Tiempo : " + StartTime.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Llave"))
        {
            Instantiate (SonidoGanador);
            Destroy(other.gameObject);
            SceneManager.LoadScene(1);
        }
        if (other.gameObject.CompareTag("Pared"))
        {
            Instantiate (SonidoPared);
        }
        if (other.gameObject.CompareTag("Tablero"))
        {
            if (!rampaDestruida)
            {
                rampa.SetActive(false);
                rampaDestruida = true;
                Instantiate (SonidoDesaparecer);
                Debug.Log("Destruyendo");
            }
        }
    }
}
