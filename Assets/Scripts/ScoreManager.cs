using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // Instancia para acceso global
    public int currentScore = 0;  // Puntuaci�n actual
    public TextMeshProUGUI scoreText;  // Referencia al texto donde se mostrar� la puntuaci�n

    private void Awake()
    {
        // Asegura que haya solo una instancia de ScoreManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // Destruye el objeto si ya existe una instancia
        }
    }

    private void Start()
    {
        // Aseg�rate de mostrar la puntuaci�n al iniciar
        UpdateScoreUI();
    }

    // M�todo para agregar puntos
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();  // Actualiza la interfaz de usuario despu�s de agregar puntos
    }

    // M�todo para actualizar el texto de la puntuaci�n
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();  // Muestra la puntuaci�n en el texto
        }
    }
}
