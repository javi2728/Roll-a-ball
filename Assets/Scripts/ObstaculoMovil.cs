using UnityEngine;

public class ObstaculoMovil : MonoBehaviour
{
    public Vector3 puntoA = new Vector3(-5f, 0.5f, 0f);
    public Vector3 puntoB = new Vector3(5f, 0.5f, 0f);
    public float velocidad = 3f;

    private Vector3 destino;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destino = puntoB;

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 nuevaPosicion = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidad * Time.fixedDeltaTime
        );

        if (rb != null)
        {
            rb.MovePosition(nuevaPosicion);
        }
        else
        {
            transform.position = nuevaPosicion;
        }

        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            if (destino == puntoA)
            {
                destino = puntoB;
            }
            else
            {
                destino = puntoA;
            }
        }
    }
}