using CG_Biblioteca;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace CG_N4
{
  public class Nave : Poligono
  {
    private static Cor magenta=new(255,0,255), cyano=new(0,255,255), preto=new(0,0,0), amarelo=new(255,255,0);
    PrimitiveType[] primitivas = { PrimitiveType.Points, PrimitiveType.Lines, PrimitiveType.LineLoop, PrimitiveType.LineStrip, PrimitiveType.Triangles, PrimitiveType.TriangleStrip, PrimitiveType.TriangleFan, PrimitiveType.Quads, PrimitiveType.QuadStrip, PrimitiveType.Polygon };
    Cor[] cores = { magenta, cyano, amarelo,  preto};
    public List<Tiro> tiros = new List<Tiro>();

    public Nave(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef, ponto)
    {
        adicionarPontoPegaUltimo(new(ponto.X + 20, 20));
        adicionarPontoPegaUltimo(new(ponto.X + 40));
    }

    int cooldown = 0;

    protected override void DesenharObjeto()
    {
      GL.LineWidth(5);
      GL.Begin(base.PrimitivaTipo);
      for (int i = 0; i < pontosLista.Count; i++)
      {
        GL.Color3(cores[i].CorR, cores[i].CorG, cores[i].CorB);
        GL.Vertex2(pontosLista[i].X, pontosLista[i].Y);
      }
      GL.End();
      cooldown--;
    }

    public Ponto4D getPosicao() {
      return pontosLista[1];
    }

    public void atirar(Mundo context) {
      if(cooldown > 0)
        return;
      Tiro tiro = new('N', null, new(pontosLista[1].X, pontosLista[1].Y));
      tiro.matriz = matriz;
      context.addObjetoNaLista(tiro);
      cooldown = 150;
    }
}
}