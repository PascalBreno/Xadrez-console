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

        public abstract bool[,] movimentosPossiveis();
        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }
    }
}
