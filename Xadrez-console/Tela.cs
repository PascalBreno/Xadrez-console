using System;
using tabuleiro;
using Xadrez_console.Xadrez;

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
            Console.WriteLine("  A B C D E F G H");
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            Console.WriteLine();
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna,linha);
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
