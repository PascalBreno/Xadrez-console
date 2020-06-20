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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tabuleiro);
                    Console.WriteLine();
                    Console.Write("Origem: "+'\n');
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                    partida.executaMovimento(origem,destino);
                }
            }
            catch (TabuleiroException e)
            {
                Console.Write(e.Message);
            }
            
        }
    }    
}
