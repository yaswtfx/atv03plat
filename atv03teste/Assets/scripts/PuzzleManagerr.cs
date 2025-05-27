using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerr : MonoBehaviour
{
    public Transform panelPai;
        private PuzzlePiece primeiraPecaSelecionada = null;
    
        void Start()
        {
            EmbaralharPecas();
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
            int index1 = peca1.transform.GetSiblingIndex();
            int index2 = peca2.transform.GetSiblingIndex();
    
            peca1.transform.SetSiblingIndex(index2);
            peca2.transform.SetSiblingIndex(index1);
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
}
