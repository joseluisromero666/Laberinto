using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    //private AudioSource audioRecoleccion;
    public GameObject SonidoMoneda;

    public GameObject SonidoPared;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // audioRecoleccion = GetComponent<AudioSource>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movimiento * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RecolectablePositivo"))
        {
            Instantiate (SonidoMoneda);
            Destroy(other.gameObject);
            //   audioRecoleccion.Play();
        }
        if (other.gameObject.CompareTag("Pared"))
        {
            Instantiate (SonidoPared);
            //   audioRecoleccion.Play();
        }
    }
}
