using System;
using tabuleiro;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            Posicao P = new Posicao(3, 4);
            Console.WriteLine("Hello World! " + P);
        }
    }
}
