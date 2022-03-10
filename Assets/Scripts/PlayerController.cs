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
  public GameObject rampa;

  public bool rampaDestruida = false;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    // audioRecoleccion = GetComponent<AudioSource>();
    rampa = GameObject.FindWithTag("Rampa");
    Debug.Log(rampa);
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
      Instantiate(SonidoMoneda);
      Destroy(other.gameObject);
      //   audioRecoleccion.Play();
    }
    if (other.gameObject.CompareTag("Pared"))
    {
      Instantiate(SonidoPared);
      //   audioRecoleccion.Play();
    }
    if (other.gameObject.CompareTag("Tablero"))
    {
      if (!rampaDestruida)
      {
        rampa.SetActive(false);
        rampaDestruida = true;
        Debug.Log("Destruyendo");
      }
    }


  }
}
