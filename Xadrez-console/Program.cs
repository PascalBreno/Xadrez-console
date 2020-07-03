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
                        Tela.imprimirPartida(partida);
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);
                        Console.Clear();
                        Tela.imprimirPartida(partida,origem);
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem,destino);
                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        if(partida.destino)
                            partida.destino = !partida.destino;
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
