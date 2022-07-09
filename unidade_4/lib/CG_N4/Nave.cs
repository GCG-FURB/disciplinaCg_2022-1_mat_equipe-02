using CG_Biblioteca;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace CG_N4
{
  public class Nave : Poligono
  {
    private static Cor azul=new(0,0,255), verde=new(0,255,0), preto=new(0,0,0), amarelo=new(255,255,0), Vermelho = new(255,0,0), roxo = new(128,0,128);
    PrimitiveType[] primitivas = { PrimitiveType.Points, PrimitiveType.Lines, PrimitiveType.LineLoop, PrimitiveType.LineStrip, PrimitiveType.Triangles, PrimitiveType.TriangleStrip, PrimitiveType.TriangleFan, PrimitiveType.Quads, PrimitiveType.QuadStrip, PrimitiveType.Polygon };
    Cor[] cores = { azul, verde, Vermelho, amarelo, roxo, preto};
    public List<Tiro> tiros = new List<Tiro>();

    private int anguloResultante;

    public Nave(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef, ponto)
    {
      PontosAdicionar(new(ponto.X + 20, ponto.Y + 20, ponto.Z));
      PontosAdicionar(new(ponto.X + 40, ponto.Y, ponto.Z));
      PontosAdicionar(new(ponto.X, ponto.Y, ponto.Z - 10));
      PontosAdicionar(new(ponto.X + 20, ponto.Y + 20, ponto.Z - 10));
      PontosAdicionar(new(ponto.X + 40, ponto.Y, ponto.Z - 10));
      PrimitivaTipo = PrimitiveType.Polygon;
    }

    int cooldown = 0;

    protected override void DesenharObjeto()
    {
      GL.LineWidth(5);
      GL.Begin(base.PrimitivaTipo);
      GL.Normal3(0, 0, -1);
      GL.Color3(cores[0].CorR, cores[0].CorG, cores[0].CorB);
      GL.Vertex3(pontosLista[0].X, pontosLista[0].Y,pontosLista[0].Z);
      GL.Vertex3(pontosLista[1].X, pontosLista[1].Y,pontosLista[1].Z);
      GL.Vertex3(pontosLista[2].X, pontosLista[2].Y,pontosLista[2].Z);
      GL.End();
      GL.Begin(base.PrimitivaTipo);
      GL.Normal3(1, 0, 0);
      GL.Color3(cores[1].CorR, cores[1].CorG, cores[1].CorB);
      GL.Vertex3(pontosLista[1].X, pontosLista[1].Y,pontosLista[1].Z);
      GL.Vertex3(pontosLista[2].X, pontosLista[2].Y,pontosLista[2].Z);
      GL.Vertex3(pontosLista[5].X, pontosLista[5].Y,pontosLista[5].Z);
      GL.Vertex3(pontosLista[4].X, pontosLista[4].Y,pontosLista[4].Z);
      GL.End();
      GL.Begin(base.PrimitivaTipo);
      GL.Normal3(0, 0, 1);
      GL.Color3(cores[2].CorR, cores[2].CorG, cores[2].CorB);
      GL.Vertex3(pontosLista[3].X, pontosLista[3].Y,pontosLista[3].Z);
      GL.Vertex3(pontosLista[4].X, pontosLista[4].Y,pontosLista[4].Z);
      GL.Vertex3(pontosLista[5].X, pontosLista[5].Y,pontosLista[5].Z);
      GL.End();
      GL.Begin(base.PrimitivaTipo);
      GL.Color3(cores[3].CorR, cores[3].CorG, cores[3].CorB);
      GL.Normal3(0, -1, 0);
      GL.Vertex3(pontosLista[0].X, pontosLista[0].Y,pontosLista[0].Z);
      GL.Vertex3(pontosLista[2].X, pontosLista[2].Y,pontosLista[2].Z);
      GL.Vertex3(pontosLista[5].X, pontosLista[5].Y,pontosLista[5].Z);
      GL.Vertex3(pontosLista[3].X, pontosLista[3].Y,pontosLista[3].Z);
      GL.End();
      GL.Begin(base.PrimitivaTipo);
      GL.Normal3(-1, 0, 0);
      GL.Color3(cores[4].CorR, cores[4].CorG, cores[4].CorB);
      GL.Vertex3(pontosLista[0].X, pontosLista[0].Y,pontosLista[0].Z);
      GL.Vertex3(pontosLista[1].X, pontosLista[1].Y,pontosLista[1].Z);
      GL.Vertex3(pontosLista[4].X, pontosLista[4].Y,pontosLista[4].Z);
      GL.Vertex3(pontosLista[3].X, pontosLista[3].Y,pontosLista[3].Z);
      GL.End();
      cooldown--;
    }

    public Ponto4D getPonto(int ponto) {
      return pontosLista[ponto];
    }

    public void rotacionarEixoBBox(int angulo) {
      anguloResultante += angulo;
      if(anguloResultante > 45){
        anguloResultante = 45;
      }
      if(anguloResultante < -45){
        anguloResultante = -45;
      }
      matriz.AtribuirIdentidade();
      base.rotacionarEixoBBox(anguloResultante);
    }

    public void mover(int distancia) {
      matriz.AtribuirIdentidade();
      if(pontosLista[0].X+distancia > 0 && pontosLista[2].X+distancia < 600){
        foreach (Ponto4D ponto in pontosLista)
        {
          ponto.X += distancia;
        }
      }else{
        return;
      }
      this.BBox.AtualizaBBox(pontosLista);
      base.rotacionarEixoBBox(anguloResultante);
    }

    public void atirar(Mundo context) {
      if(cooldown > 0)
        return;
      Tiro tiro = new('N', null, new(pontosLista[1].X, pontosLista[1].Y));
      Transformacao4D aux = new();
      aux = aux.MultiplicarMatriz(matriz);
      tiro.matriz = aux;
      context.addObjetoNaLista(tiro);
      cooldown = 150;
    }
}
}