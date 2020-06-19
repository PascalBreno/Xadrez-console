using System;
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
                PosicaoXadrez pos = new PosicaoXadrez('c',7);
                Console.WriteLine(pos);
                Console.ReadLine();
            }
            catch (TabuleiroException e)
            {
                Console.Write(e.Message);
            }
            
        }
    }    
}
