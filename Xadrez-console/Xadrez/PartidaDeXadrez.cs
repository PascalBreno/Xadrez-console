using System;
using System.Collections.Generic;
using System.Linq;
using tabuleiro;
using Xadrez;

namespace Xadrez_console.Xadrez
{
    public class PartidaDeXadrez
    {
        private readonly HashSet<Peca> pecas;
        private readonly HashSet<Peca> capturadas;
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual;
        public bool destino { get; set; }
        public bool terminada { get; private set; }
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            destino = false;
            colocarPecas();
        }

        private bool estaEmXeque(Cor cor)
        {
            Peca r = rei(cor);
            if (r == null)
                throw new TabuleiroException("Não tem rei da cor " + cor + " no jogo!");
            return pecasEmJogo(cor == Cor.Branca ? Cor.Preta : Cor.Branca).Select(peca => peca.movimentosPossiveis())
                .Any(mat => mat[r.posicao.linha, r.posicao.coluna]);
        }

        public bool testeCheckmate(Cor cor)
        {
            if (!estaEmXeque(cor)) return false;
            Peca r = rei(cor);
            if (r == null)
                throw new TabuleiroException("Não tem rei da cor " + cor + " no jogo!");

            var Peca = pecasEmJogo(cor == Cor.Branca ? Cor.Branca : Cor.Preta);
            foreach (var peca in Peca)
            {
                var movimentosPossiveis = peca.movimentosPossiveis();

                for (int i = 0; i < tabuleiro.linhas;i++)
                {
                    for (int j = 0; j < tabuleiro.colunas;j++)
                    {
                        if (!movimentosPossiveis[i, j]) continue;
                        Posicao destino = new Posicao(i,j);
                        Posicao Origem = peca.posicao;
                        Peca pecaCapturada = executaMovimento(peca.posicao, destino);
                        bool testXeque = estaEmXeque(cor);
                        desfazMovimento(Origem,destino, pecaCapturada);
                        if (!testXeque)
                            return false;
                    }
                }
                return true;
            }

            return true;
        }

        private Peca rei(Cor cor)
        {
            return pecasEmJogo(cor).OfType<Rei>().FirstOrDefault();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tabuleiro.peca(pos) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            if (jogadorAtual != tabuleiro.peca(pos).cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            if (!tabuleiro.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).movimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        private void desfazMovimento(Posicao origem, Posicao destino, Peca pecaDestino)
        {
            Peca pecaOrigem = tabuleiro.peca(destino);
            tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(pecaOrigem, origem);
            tabuleiro.colocarPeca(pecaDestino, destino);
            pecaOrigem.diminuirQteMovimentos();
            capturadas.Remove(pecaDestino);
        }

        private Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.tabuleiro.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p, destino);
            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);
            return pecaCapturada;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            return capturadas.Where(x => x.cor == cor).ToHashSet();
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            return pecas.Where(x => x.cor == cor && !capturadas.Contains(x)).ToHashSet();
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            
            jogadorAtual = jogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
            xeque = estaEmXeque(jogadorAtual);
            if (testeCheckmate(jogadorAtual))
                FinalizaJogo();
            turno++;
        }

        private void FinalizaJogo()
        {
            terminada = true;
            Console.WriteLine("Fim de Jogo!");
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tabuleiro, Cor.Branca));


            colocarNovaPeca('d', 8, new Rei(tabuleiro, Cor.Preta));
        }
    }
}