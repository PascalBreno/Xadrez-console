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
        public Peca vulneravelEnPassant;
        
        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            vulneravelEnPassant = null;
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
            var teste =  pecasEmJogo(cor == Cor.Branca ? Cor.Preta : Cor.Branca).Select(peca => peca.movimentosPossiveis())
                .Any(mat => mat[r.posicao.linha, r.posicao.coluna]);
            return teste;
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
        
            // #jogadaespecial roque pequeno
            if (pecaOrigem is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha,origem.coluna+3);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna+1);
                Peca T = tabuleiro.retirarPeca(destinoT);
                T.diminuirQteMovimentos();
                tabuleiro.colocarPeca(T,origemT);
            } 
            // #jogadaespecial roque grande
            if (pecaOrigem is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha,origem.coluna-4);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna-1);
                Peca T = tabuleiro.retirarPeca(destinoT);
                T.diminuirQteMovimentos();
                tabuleiro.colocarPeca(T,origemT);
            }
            
            // #jogadaEspecial en passant
            if (pecaOrigem is Peao)
            {
                if(origem.coluna!=destino.coluna && pecaDestino==vulneravelEnPassant)
                {
                    Peca peao = tabuleiro.retirarPeca(destino);
                    Posicao posP;
                    if (pecaOrigem.cor == Cor.Branca)
                    {
                        posP = new Posicao(3,destino.coluna);
                    }
                    else
                    {
                        posP=new Posicao(4,destino.coluna);
                    }
                    tabuleiro.colocarPeca(peao,posP);
                }
                
            }
        }

        private Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.tabuleiro.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p, destino);
            
            
            // #jogadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha,origem.coluna+3);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna+1);
                p.incrementarQteMovimentos();
                Peca T = tabuleiro.retirarPeca(origemT);
                tabuleiro.colocarPeca(T,destinoT);
            }
            // #jogadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna - 2) 
            {
                Posicao origemT = new Posicao(origem.linha,origem.coluna-4);
                Posicao destinoT = new Posicao(origem.linha,origem.coluna-1);
                p.incrementarQteMovimentos();
                Peca T = tabuleiro.retirarPeca(origemT);
                tabuleiro.colocarPeca(T,destinoT);
            }
            
            // #jogadaEspecial en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha+1,destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha-1,destino.coluna);
                    }
                    pecaCapturada = tabuleiro.retirarPeca(posP);
                }

                
            }
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
            
            Peca p = tabuleiro.peca(destino);

            // #jogadaEspecial promoção
            if (p is Peao)
            {
                if ((p.cor == Cor.Branca && destino.linha == 0) || (p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tabuleiro.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca Rainha = new Rainha(tabuleiro,p.cor);
                    tabuleiro.colocarPeca(Rainha,destino);
                    pecas.Add(Rainha);
                    p = tabuleiro.peca(destino);
                }
            }
            
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
            
            // #jogadaEspecial en passant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                vulneravelEnPassant = p;
            }
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
           
            colocarNovaPeca('e',1,new Rei(tabuleiro,Cor.Branca, this));
          
            colocarNovaPeca('a',7,new Peao(tabuleiro,Cor.Branca,this));
            
            
          
            colocarNovaPeca('e',8,new Rei(tabuleiro,Cor.Preta,this));
            
            
        }
    }
}