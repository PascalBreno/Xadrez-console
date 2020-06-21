using tabuleiro;
using Xadrez;

namespace Xadrez_console.Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual;
        public bool terminada { get; private set; }
        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tabuleiro.peca(pos)==null)
                throw  new TabuleiroException("Não existe peça na posição de origem escolhida!");
            if(jogadorAtual!=tabuleiro.peca(pos).cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            if (!tabuleiro.peca(pos).existeMovimentosPossiveis())
            {
                throw  new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).podeMoverPara(destino))
                throw  new TabuleiroException("Posição de destino inválida!");
            
        }
        private void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.tabuleiro.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p,destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            jogadorAtual = jogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private void colocarPecas()
        {
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('c',1).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('c',2).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('d',2).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('e',1).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Preta), new PosicaoXadrez('e',2).toPosicao() );
            tabuleiro.colocarPeca(new Rei(tabuleiro,Cor.Preta), new PosicaoXadrez('d',1).toPosicao() );
            
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Branca), new PosicaoXadrez('c',8).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Branca), new PosicaoXadrez('d',7).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Branca), new PosicaoXadrez('c',7).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Branca), new PosicaoXadrez('e',7).toPosicao() );
            tabuleiro.colocarPeca(new Torre(tabuleiro,Cor.Branca), new PosicaoXadrez('e',8).toPosicao() );
            tabuleiro.colocarPeca(new Rei(tabuleiro,Cor.Branca), new PosicaoXadrez('d',8).toPosicao() );
            
        }
    }
}