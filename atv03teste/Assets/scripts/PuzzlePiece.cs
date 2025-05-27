using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public PuzzleManagerr puzzleManager;
    private Button button;
    private Image image;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(OnPieceClicked);
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
