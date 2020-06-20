using tabuleiro;

namespace Xadrez_console.Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }
        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.tabuleiro.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p,destino);
        }

        private void colocarPecas()
        {
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('c',1).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('d',2).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('e',2).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('e',1).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('d',1).toPosicao() );
        }
    }
}