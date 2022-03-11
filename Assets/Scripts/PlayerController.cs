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

    public GameObject SonidoRisa;

    private GameObject rampa;

    private int puntos;

    private float StartTime;

    public Text textoPuntos;

    public Text textoGanador;

    public Text textoStartTime;

    public GameObject[] respawns;

    private bool rampaDestruida = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // audioRecoleccion = GetComponent<AudioSource>();
        rampa = GameObject.FindWithTag("Rampa");
        Debug.Log (rampa);
        StartTime = 0;
        puntos = 0;
        respawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
        if (respawns.Length >= 2)
        {
            int random = Random.Range(0, respawns.Length);
            transform.position = respawns[random].transform.position;
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movimiento * speed);
        StartTime += 1 * Time.deltaTime;
        textoStartTime.text = "Tiempo : " + StartTime.ToString("F0");
    }

    public void cambiarEscena()
    {
        SceneManager.LoadScene(1);
    }

    async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RecolectablePositivo"))
        {
            Instantiate (SonidoMoneda);
            Debug.Log("Puntos sin Bonus :" + puntos.ToString());
            puntos += 100;
            Debug.Log("Puntos con Bonus :" + puntos.ToString());
            textoPuntos.text = "Puntos : " + puntos.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("RecolectableNegativo"))
        {
            Instantiate (SonidoMonedaNegativa);
            Debug.Log("Puntos sin Bonus :" + puntos.ToString());
            puntos -= 50;
            Debug.Log("Puntos con Bonus :" + puntos.ToString());
            textoPuntos.text = "Puntos : " + puntos.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("RecolectableTiempo"))
        {
            Instantiate (SonidoMonedaTiempo);
            Debug.Log("Tiempo sin Bonus :" + StartTime.ToString());
            StartTime -= 10;
            Debug.Log("Tiempo con Bonus :" + StartTime.ToString());
            textoStartTime.text = "Tiempo : " + StartTime.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("RecolectableTiempoNegativo"))
        {
            Instantiate (SonidoMonedaTiempoNegativo);
            Debug.Log("Tiempo sin Bonus :" + StartTime.ToString());
            StartTime += 10;
            Debug.Log("Tiempo con Bonus :" + StartTime.ToString());
            textoStartTime.text = "Tiempo : " + StartTime.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Llave"))
        {
            Instantiate (SonidoGanador);
            textoGanador.text = "!GANASTE¡";
            Destroy(other.gameObject);
            Debug.Log("Tiempo Total :" + StartTime.ToString());
            Debug.Log("Puntos Totales :" + puntos.ToString());
            Invoke("cambiarEscena", 4f);
        }
        if (other.gameObject.CompareTag("Pared"))
        {
            Instantiate (SonidoPared);
        }
        if (other.gameObject.CompareTag("hueco"))
        {
            Instantiate (SonidoRisa);
            int random = Random.Range(0, respawns.Length);
            transform.position = respawns[random].transform.position;
            puntos -= 100;
            textoPuntos.text = "Puntos : " + puntos.ToString();
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
