using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

namespace CG_N2_4
{
  internal class Primitivas : ObjetoGeometria
  {

    int counter = 0;
    Cor[] cores = { new Cor(106,13,173), new Cor(0,255,255), new Cor(255,255,0), new Cor(0,0,0) };
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
      GL.Begin(primitivas[counter]);
      for (int i = 0; i < pontosLista.Count(); i++)
      {
        GL.Color3(cores[i].CorR, cores[i].CorG, cores[i].CorB);
        GL.Vertex2(pontosLista[i].X, pontosLista[i].Y);
      }
      GL.End();
    }
    public void nextPrimitive() {
      if(counter == primitivas.Count() - 1) {
        counter = 0;
      } else {
        this.counter++;
      }
    }
  }

  class Program
    {
      static void Main(string[] args)
      {
        char objetoId = Utilitario.charProximo('@');
        Ponto4D ptoInfEsq = new Ponto4D(200,200);
        Ponto4D ptoSupDir = new Ponto4D(-200,-200);
        Primitivas obj_Primitivas = new Primitivas(objetoId, null, ptoInfEsq, ptoSupDir);
        Mundo window = Mundo.GetInstance(600, 600, obj_Primitivas);
        window.Title = "CG_N2_4";
        window.setCameraPosition(-400, -400, 400, 400);
        window.addCustomKey(Key.Space, obj_Primitivas.nextPrimitive);
        window.Run(1.0 / 60.0);
      }

    }
}
