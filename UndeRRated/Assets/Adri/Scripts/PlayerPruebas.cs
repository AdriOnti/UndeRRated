using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPruebas : MonoBehaviour
{
    // Start is called before the first frame update

    public float velocidadMovimiento;
    public float limiteDerecha = 10f;
    public float limiteIzquierda = -10f;
    public float fuerzaSalto;
    public float groundedY;

    private Rigidbody rb;
    private bool enMovimiento = false;
    private bool enSalto = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        // Verifica si se presiona la tecla de movimiento (por ejemplo, derecha o izquierda)
        if (movimientoHorizontal != 0)
        {
            enMovimiento = true;
        }
        else
        {
            enMovimiento = false;
        }

        // Verifica si se presiona la tecla de salto
        if (Input.GetButtonDown("Jump"))
        {
            enSalto = true;
            if (transform.position.y == groundedY) Saltar();
        }
    }

    private void FixedUpdate()
    {
        // Mueve el personaje hacia la derecha o izquierda si está en movimiento y dentro de los límites establecidos
        if (enMovimiento)
        {
            float movimientoHorizontal = Input.GetAxis("Horizontal");
            Vector3 movimiento = new Vector3(movimientoHorizontal * velocidadMovimiento, 0, 0);
            Vector3 nuevaPosicion = transform.position + movimiento * Time.fixedDeltaTime;

            // Verifica si la nueva posición está dentro de los límites
            if (nuevaPosicion.x > limiteDerecha)
            {
                nuevaPosicion.x = limiteDerecha;
            }
            else if (nuevaPosicion.x < limiteIzquierda)
            {
                nuevaPosicion.x = limiteIzquierda;
            }

            rb.MovePosition(nuevaPosicion);
        }
    }

    private void Saltar()
    {
        // Aplica una fuerza vertical al personaje para simular el salto
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }
}
