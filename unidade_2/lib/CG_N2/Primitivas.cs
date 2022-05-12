using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace CG_N2
{
  public class Primitivas : ObjetoGeometria
  {
    private static Cor magenta=new(255,0,255), cyano=new(0,255,255), preto=new(0,0,0), amarelo=new(255,255,0);
    int counter = 0;
    Cor[] cores = { magenta, cyano, amarelo,  preto};
    PrimitiveType[] primitivas = { PrimitiveType.Points, PrimitiveType.Lines, PrimitiveType.LineLoop, PrimitiveType.LineStrip, PrimitiveType.Triangles, PrimitiveType.TriangleStrip, PrimitiveType.TriangleFan, PrimitiveType.Quads, PrimitiveType.QuadStrip, PrimitiveType.Polygon };
    public Primitivas(char rotulo, Objeto paiRef, Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base(rotulo, paiRef) {
      base.PontosAdicionar(ptoInfEsq);
      base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
      base.PontosAdicionar(ptoSupDir);
      base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));
    }

    protected override void DesenharObjeto()
    {
      GL.PointSize(8);
      GL.Begin(primitivas[counter % primitivas.Length]);
      for (int i = 0; i < pontosLista.Count; i++)
      {
        GL.Color3(cores[i].CorR, cores[i].CorG, cores[i].CorB);
        GL.Vertex2(pontosLista[i].X, pontosLista[i].Y);
      }
      GL.End();
    }
    public void nextPrimitive() {
      this.counter++;
    }
  }
}