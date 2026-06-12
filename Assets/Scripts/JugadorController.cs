using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorController : MonoBehaviour
{
    private Rigidbody rb;

    public float velocidad = 5f;

    private bool puedeMoverse = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!puedeMoverse)
        {
            return;
        }

        Vector2 entrada = Vector2.zero;

        Keyboard teclado = Keyboard.current;

        if (teclado == null)
        {
            return;
        }

        if (teclado.wKey.isPressed || teclado.upArrowKey.isPressed)
        {
            entrada.y += 1;
        }

        if (teclado.sKey.isPressed || teclado.downArrowKey.isPressed)
        {
            entrada.y -= 1;
        }

        if (teclado.dKey.isPressed || teclado.rightArrowKey.isPressed)
        {
            entrada.x += 1;
        }

        if (teclado.aKey.isPressed || teclado.leftArrowKey.isPressed)
        {
            entrada.x -= 1;
        }

        Vector3 movimiento = new Vector3(entrada.x, 0.0f, entrada.y).normalized;

        rb.AddForce(movimiento * velocidad);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);

            if (NivelManager.Instancia != null)
            {
                NivelManager.Instancia.RecogerColeccionable();
            }
        }
    }

    public void BloquearMovimiento()
    {
        puedeMoverse = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}