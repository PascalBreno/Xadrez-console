using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    public abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            posicao = null;
            this.tab = tab;
            this.cor = cor;
            qteMovimentos = 0;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            foreach (var validation in mat)
            {
                if (validation)
                {
                    return true;
                }
            }
            return false;
        }

        public bool movimentoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }
        public abstract bool[,] movimentosPossiveis();
        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }
        public void diminuirQteMovimentos()
        {
            qteMovimentos--;
        }
    }
}
