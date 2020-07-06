using System;
using System.Collections.Generic;
using tabuleiro;
using Xadrez_console.Xadrez;

namespace Xadrez_console
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida, Posicao origem = null)
        {
            if (partida.testeCheckmate(partida.jogadorAtual))
            {
                
                Console.WriteLine("Fim de Jogo!");
            }
            else
            {
                if(partida.xeque)
                    Console.WriteLine("VOCÊ ESTÁ EM XEQUE!");
                if (partida.destino)
                {
                    bool[,] posicaoPossiveis = partida.tabuleiro.peca(origem).movimentosPossiveis();
                    imprimirTabuleiro(partida.tabuleiro,posicaoPossiveis);
                }
                else
                {
                    imprimirTabuleiro(partida.tabuleiro);
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                imprimirPecasCapturadas(partida);
                Console.WriteLine("Turno " + partida.turno);
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
                Console.WriteLine(partida.destino ? "Destino: " : "Origem: ");
                partida.destino = !partida.destino;
            }
            
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças Capturadas");
            Console.Write("Brancas: ");
            imprimirPecasCapturadas(partida.pecasCapturadas(Cor.Branca));
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirPecasCapturadas(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void imprimirPecasCapturadas(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (var x in conjunto)
            {
                Console.Write(x+ " ");
            }
            Console.Write("]");
            Console.WriteLine();
        }
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    imprimirPeca(tab.peca(i, j));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPosiveis)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPosiveis[i, j])
                        Console.BackgroundColor = fundoAlterado;
                    else

                        Console.BackgroundColor = fundoOriginal;

                    imprimirPeca(tab.peca(i, j));
                }

                Console.BackgroundColor = fundoOriginal;
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca + " ");
                    Console.ForegroundColor = aux;
                }
            }
        }
    }
}