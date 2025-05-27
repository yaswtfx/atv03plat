using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public PuzzleManagerr puzzleManager;
    private Button button;
    private Image image;
    
    public int indiceCorreto;  // índice correto da peça

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(OnPieceClicked);
    }
    void Start()
    {
        // Guardar a posição correta da peça no início do jogo
        indiceCorreto = transform.GetSiblingIndex();
    }

    void OnPieceClicked()
    {
        puzzleManager.PecaClicada(this);
    }

    public void Destacar(bool destacar)
    {
        if (destacar)
            image.color = Color.yellow;
        else
            image.color = Color.white;
    }
}
