using System;
using System.Threading;
using tabuleiro;
using Xadrez_console.Xadrez;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tabuleiro);
                        Console.WriteLine("Turno " + partida.turno);
                        Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);
                        Console.Clear();
                        bool[,] posicaoPossiveis = partida.tabuleiro.peca(origem).movimentosPossiveis();
                        Tela.imprimirTabuleiro(partida.tabuleiro, posicaoPossiveis);
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem,destino);
                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine((e.Message));
                        Console.ReadLine();

                    }
                }
            }
            catch (TabuleiroException e)
            {
                Console.Write(e.Message);
            }
            
        }
    }    
}
