using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelManager : MonoBehaviour
{
    public static NivelManager Instancia { get; private set; }

    [Header("Textos UI")]
    public TMP_Text textoContador;
    public TMP_Text textoGanar;
    public TMP_Text textoTimer;
    public TMP_Text textoNombreMatricula;

    [Header("Configuración del nivel")]
    public int totalColeccionables = 12;
    public float tiempoInicial = 60f;

    [Tooltip("Escena que se cargará al ganar. Por ahora puedes dejarla vacía.")]
    public string siguienteEscena = "";

    [Tooltip("Escena que se cargará al perder. Por ahora puedes dejarla vacía.")]
    public string escenaAlPerder = "";

    public string nombreMatricula = "Tu Nombre - Tu Matrícula";

    private int contador;
    private float tiempoRestante;
    private bool nivelTerminado;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        contador = 0;
        tiempoRestante = tiempoInicial;
        nivelTerminado = false;

        if (textoGanar != null)
        {
            textoGanar.text = "";
        }

        if (textoNombreMatricula != null)
        {
            textoNombreMatricula.text = nombreMatricula;
        }

        ActualizarContador();
        ActualizarTimer();
    }

    void Update()
    {
        if (nivelTerminado)
        {
            return;
        }

        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            ActualizarTimer();
            PerderNivel();
            return;
        }

        ActualizarTimer();
    }

    public void RecogerColeccionable()
    {
        if (nivelTerminado)
        {
            return;
        }

        contador++;
        ActualizarContador();

        if (contador >= totalColeccionables)
        {
            GanarNivel();
        }
    }

    void ActualizarContador()
    {
        if (textoContador != null)
        {
            textoContador.text = "Contador: " + contador + "/" + totalColeccionables;
        }
    }

    void ActualizarTimer()
    {
        if (textoTimer != null)
        {
            textoTimer.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante);
        }
    }

    void GanarNivel()
    {
        nivelTerminado = true;

        if (textoGanar != null)
        {
            textoGanar.text = "¡Ganaste!";
        }

        BloquearJugador();

        if (!string.IsNullOrWhiteSpace(siguienteEscena))
        {
            StartCoroutine(CambiarEscenaDespuesDe(5f, siguienteEscena));
        }
    }

    void PerderNivel()
    {
        nivelTerminado = true;

        if (textoGanar != null)
        {
            textoGanar.text = "¡Perdiste!";
        }

        BloquearJugador();

        if (!string.IsNullOrWhiteSpace(escenaAlPerder))
        {
            StartCoroutine(CambiarEscenaDespuesDe(5f, escenaAlPerder));
        }
    }

    void BloquearJugador()
    {
        JugadorController jugador = FindAnyObjectByType<JugadorController>();

        if (jugador != null)
        {
            jugador.BloquearMovimiento();
        }
    }

    IEnumerator CambiarEscenaDespuesDe(float segundos, string nombreEscena)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(nombreEscena);
    }
}