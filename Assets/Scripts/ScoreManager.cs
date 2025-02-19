using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // Instancia para acceso global
    public int currentScore = 0;  // Puntuación actual
    public TextMeshProUGUI scoreText;  // Referencia al texto donde se mostrará la puntuación

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
        // Asegúrate de mostrar la puntuación al iniciar
        UpdateScoreUI();
    }

    // Método para agregar puntos
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();  // Actualiza la interfaz de usuario después de agregar puntos
    }

    // Método para actualizar el texto de la puntuación
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();  // Muestra la puntuación en el texto
        }
    }
}
