using UnityEngine;
using System; 

public class TrocaPecasCommand : ICommand
{
    private PuzzlePiece peca1, peca2;
    private int index1, index2;

    public TrocaPecasCommand(PuzzlePiece p1, PuzzlePiece p2)
    {
        peca1 = p1;
        peca2 = p2;
        index1 = p1.transform.GetSiblingIndex();
        index2 = p2.transform.GetSiblingIndex();
    }

    public void Execute()
    {
        peca1.transform.SetSiblingIndex(index2);
        peca2.transform.SetSiblingIndex(index1);
    }

    public void Undo()
    {
        peca1.transform.SetSiblingIndex(index1);
        peca2.transform.SetSiblingIndex(index2);
    }
}
