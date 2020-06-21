using System.Collections.Generic;
using System.Linq;
using tabuleiro;
using Xadrez;

namespace Xadrez_console.Xadrez
{
    public class PartidaDeXadrez
    {
        private HashSet<Peca>  pecas;
        private HashSet<Peca> capturadas;
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual;
        public bool terminada { get; private set; }
        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tabuleiro.peca(pos)==null)
                throw  new TabuleiroException("Não existe peça na posição de origem escolhida!");
            if(jogadorAtual!=tabuleiro.peca(pos).cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            if (!tabuleiro.peca(pos).existeMovimentosPossiveis())
            {
                throw  new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).podeMoverPara(destino))
                throw  new TabuleiroException("Posição de destino inválida!");
            
        }
        private void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.tabuleiro.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p,destino);
            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            return capturadas.Where(x => x.cor == cor).ToHashSet();
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            return pecas.Where(x => x.cor == cor && capturadas.Contains(x)).ToHashSet();
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            jogadorAtual = jogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('c',1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('c',2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('d',2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('e',1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('e',2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('d',1, new Rei(tabuleiro, Cor.Branca));
            
            colocarNovaPeca('c',7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('c',8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('d',7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('e',7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('e',8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('d',8, new Rei(tabuleiro, Cor.Preta));
            
        }
    }
}