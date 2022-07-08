using CG_Biblioteca;

namespace CG_N4
{
  public class Tiro : Poligono
  {

    // TODO
    // TODO
    // TODO
    // TODO
    // REMOVER TIROS (FREE MEM);

    public Tiro(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef, ponto)
    {
      PontosAdicionar(new(ponto.X, ponto.Y - 5));
    }

    public Ponto4D getPosicao() {
      return pontosLista[0];
    }

    protected override void DesenharObjeto()
    {
      foreach (Ponto4D pto in pontosLista)
      {
        pto.Y++;
      }
      base.DesenharObjeto();
    }
  }

}