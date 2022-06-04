using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Ponto : ObjetoGeometria
  {
    private Ponto4D ponto;
    public Ponto(char rotulo, Objeto paiRef,double x, double y) : base(rotulo, paiRef)
    {
        this.ponto = new Ponto4D (x,y);
        base.PrimitivaTipo = PrimitiveType.Points;
    }

    protected override void DesenharObjeto()
    {
      GL.Begin(base.PrimitivaTipo);
        GL.Vertex2(ponto.X, ponto.Y);
      GL.End();
    }
    public double getX(){
      return this.ponto.X;
    }
    public double getY(){
      return this.ponto.Y;
    }

    public Ponto4D getPonto4D() {
      return ponto;
    }
    public void setX(double x){
      this.ponto.X = x;
    }
    public void setY(double y){
      this.ponto.Y = y;
    }
  }
}
