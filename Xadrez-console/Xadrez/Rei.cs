using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using tabuleiro;
using Xadrez_console.Xadrez;

namespace Xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
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

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            if (p == null) return false;
            return p is Torre && p.cor == cor && p.qteMovimentos == 0;
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
            
            foreach (var posicaoAux in pos.Where(posicaoAux => tab.posicaoValida(posicaoAux) && podeMover(posicaoAux)))
            {
                mat[posicaoAux.linha, posicaoAux.coluna] = true;
            }
            
            // #jogadaEspecial Roque
            if (qteMovimentos != 0 || partida.xeque) return mat;
            // #jogadaespecial roque pequeno
            Posicao postT1 = new Posicao(posicao.linha,posicao.coluna+3);
            if (testeTorreParaRoque(postT1))
            {
                Posicao p1 = new Posicao(posicao.linha,posicao.coluna+1);
                Posicao p2 = new Posicao(posicao.linha,posicao.coluna+2);
                if (tab.peca(p1) is null && tab.peca(p2) is null)
                {
                    mat[posicao.linha, posicao.coluna + 2] = true;
                }
            }
            // #jogadaespecial roque grande
            postT1 = new Posicao(posicao.linha,posicao.coluna-4);
            if (!testeTorreParaRoque(postT1)) return mat;
            {
                Posicao p1 = new Posicao(posicao.linha,posicao.coluna-1);
                Posicao p2 = new Posicao(posicao.linha,posicao.coluna-2);
                Posicao p3 = new Posicao(posicao.linha,posicao.coluna-3);
                if (tab.peca(p1) is null && tab.peca(p2) is null && tab.peca(p3) is null)
                {
                    mat[posicao.linha, posicao.coluna - 2] = true;
                }
            }

            return mat;
        }
    }
}
