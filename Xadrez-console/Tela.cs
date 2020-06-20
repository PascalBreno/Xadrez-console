using System;
using tabuleiro;
namespace Xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8-i+" ");
                for (int j=0; j < tab.colunas; j++)
                {
                    var peca = " ";
                    if (tab.peca(i, j) == null)
                    {
                        peca = "- ";
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i,j ));  
                    } 
                    Console.Write(peca);
                }
                Console.WriteLine();
            }
            Console.WriteLine(" A B C D E F G H");
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            { 
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
