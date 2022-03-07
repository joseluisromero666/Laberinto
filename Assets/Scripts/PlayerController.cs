using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private AudioSource audioRecoleccion;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioRecoleccion=GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
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

            Destroy(other.gameObject);
               audioRecoleccion.Play();
     
        }


    }
}
