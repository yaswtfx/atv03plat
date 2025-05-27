using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleManagerr : MonoBehaviour
{
    public Transform panelPai;
    private PuzzlePiece primeiraPecaSelecionada = null;
    public GameObject telaDeVitoria; // Painel da UI
    public Button botaoJogarNovamente;
    public Button botaoReplay;
    private bool _jogoCompleto = false;
    
    private Stack<ICommand> comandosExecutados = new Stack<ICommand>();
    private List<Transform> ordemInicial = new List<Transform>();
        
    void Start()
    {
        EmbaralharPecas();
        SalvarOrdemInicial();
        
        // Deixa a tela de vitória invisível
        telaDeVitoria.SetActive(false);

        // Associa os botões
        botaoJogarNovamente.onClick.AddListener(ReiniciarJogo);
        botaoReplay.onClick.AddListener(ExecutarReplay);
    }
        
    public void PecaClicada(PuzzlePiece pecaClicada)
    {
        if (primeiraPecaSelecionada == null) 
        {
            primeiraPecaSelecionada = pecaClicada;
            primeiraPecaSelecionada.Destacar(true);
        }
        else
        {
            if (pecaClicada == primeiraPecaSelecionada)
            {
                primeiraPecaSelecionada.Destacar(false);
                primeiraPecaSelecionada = null;
                return;
            }
        
            TrocarPecas(primeiraPecaSelecionada, pecaClicada);
            primeiraPecaSelecionada.Destacar(false);
            primeiraPecaSelecionada = null;
        }
    }
        
    void TrocarPecas(PuzzlePiece peca1, PuzzlePiece peca2)
    {
        ICommand comando = new TrocaPecasCommand(peca1, peca2);
        comando.Execute();
        comandosExecutados.Push(comando);
                
        // Verifica se o puzzle foi completado
        if (PuzzleCompleto())
        {
            VerificarVitoria();
            FinalizarJogo();
        }
    }
        
    void EmbaralharPecas()
    {
        List<Transform> pecas = new List<Transform>();
        
        foreach (Transform peca in panelPai)
        {
            pecas.Add(peca);
        }
        
        for (int i = 0; i < pecas.Count; i++)
        {
            Transform temp = pecas[i];
            int randomIndex = Random.Range(i, pecas.Count);
            pecas[i] = pecas[randomIndex];
            pecas[randomIndex] = temp;
        }
        
        for (int i = 0; i < pecas.Count; i++)
        {
            pecas[i].SetSiblingIndex(i);
        }
    }
    
    void SalvarOrdemInicial()
    {
        ordemInicial.Clear();
        foreach (Transform peca in panelPai)
        {
            ordemInicial.Add(peca);
        }
    }
    bool PuzzleCompleto()
    {
        foreach (Transform peca in panelPai)
        {
            PuzzlePiece puzzlePiece = peca.GetComponent<PuzzlePiece>();
            if (puzzlePiece.indiceCorreto != peca.GetSiblingIndex()) 
                return false;
        }
        return true;
    }
            
    void FinalizarJogo()
    {
        Debug.Log("Parabéns! Você completou o puzzle!");
        // Aqui você pode ativar UI, desabilitar seleção, etc.
    }
    
    public void DesfazerUltimoMovimento()
    {
        if (comandosExecutados.Count > 0)
        {
            ICommand comando = comandosExecutados.Pop();
            comando.Undo();
        }
    }
    
    public void VerificarVitoria()
    {
        for (int i = 0; i < panelPai.childCount; i++)
        {
            if (panelPai.GetChild(i).GetComponent<PuzzlePiece>().transform.GetSiblingIndex() != i)
                return;
        }

        _jogoCompleto = true;
        telaDeVitoria.SetActive(true);
    }

    void ReiniciarJogo()
    {
        // Reinicia a cena
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void ExecutarReplay()
    {
        
    }
    
    
    
}
