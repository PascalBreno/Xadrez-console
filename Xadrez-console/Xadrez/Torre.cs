using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using tabuleiro;

namespace Xadrez_console.Xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor): base(tab, cor)
        {
            
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
