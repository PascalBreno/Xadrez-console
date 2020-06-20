using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using tabuleiro;

namespace Xadrez
{
    class Rei : Peca 
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }
        public override string ToString()
        {
            return "R";        
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool [tab.linhas,tab.colunas];
            List<Posicao> pos = new List<Posicao>();
            
            //acima
            pos.Add(new Posicao(posicao.linha-1, posicao.coluna));
            pos.Add(new Posicao(posicao.linha+1, posicao.coluna));
            pos.Add(new Posicao(posicao.linha, posicao.coluna-1));
            pos.Add(new Posicao(posicao.linha, posicao.coluna+1));
            pos.Add(new Posicao(posicao.linha-1,posicao.coluna-1));
            pos.Add(new Posicao(posicao.linha+1,posicao.coluna+1));
            pos.Add(new Posicao(posicao.linha-1,posicao.coluna+1));
            pos.Add(new Posicao(posicao.linha+1,posicao.coluna-1));
            
            foreach (var posicaoAux in pos)
            {
                if (tab.posicaoValida(posicaoAux) && podeMover(posicaoAux))
                {
                    mat[posicaoAux.linha, posicaoAux.coluna] = true;
                }
            }

            return mat;
        }
    }
}
