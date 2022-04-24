using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace CG_N2
{
  public class SegReta : ObjetoGeometria
  {
    public SegReta(char rotulo, Objeto paiRef, Ponto4D inicial,  Ponto4D final) : base(rotulo, paiRef)
    {
      base.PontosAdicionar(inicial);
      base.PontosAdicionar(final);
    }

    protected override void DesenharObjeto()
    {
      GL.LineWidth(5);
      GL.Begin(PrimitiveType.Lines);

      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      
      GL.End();
    }

  }
}