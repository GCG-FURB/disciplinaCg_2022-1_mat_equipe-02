using CG_N4;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

  internal class Tiro : Poligono
  {

    // TODO
    // TODO
    // TODO
    // TODO
    // REMOVER TIROS (FREE MEM);

    public Tiro(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef, ponto)
    {
        adicionarPontoPegaUltimo(new(ponto.X, ponto.Y - 5));
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