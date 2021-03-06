using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;


namespace CG_N2
{
  public class SegReta : ObjetoGeometria
  {
    private Ponto4D inicial;
    private Ponto4D final;
    public SegReta(char rotulo, Objeto paiRef, Ponto4D inicial,  Ponto4D final) : base(rotulo, paiRef)
    {
      this.inicial = inicial;
      this.final = final;
      base.PontosAdicionar(inicial);
      base.PontosAdicionar(final);
    }

    private double calculaAngulo() {
      return Math.Atan2(final.Y - inicial.Y, final.X - inicial.X) * 180 / Math.PI;
    }
    
    private double calculaRaio() {
      double deltaX = final.X - inicial.X;
      double deltaY = final.Y - inicial.Y;
      double deltaZ = final.Z - inicial.Z;
      return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
    }

    public void girarAntiHorario() {
      final = Matematica.GerarPtoDaCircunferencia(calculaAngulo() + 1, calculaRaio());
      final.X += inicial.X;
      final.Y += inicial.Y;
    }

    public void girarHorario() {
      final = Matematica.GerarPtoDaCircunferencia(calculaAngulo() - 1, calculaRaio());
      final.X += inicial.X;
      final.Y += inicial.Y;
    }

    public void aumentar() {
      final = Matematica.GerarPtoDaCircunferencia(calculaAngulo(), calculaRaio() + 1);
      final.X += inicial.X;
      final.Y += inicial.Y;
    }
    public void diminuir() {
      if (Math.Abs(final.X - inicial.X) < 3 && Math.Abs(final.Y - inicial.Y) < 3)
        return;
      final = Matematica.GerarPtoDaCircunferencia(calculaAngulo(), calculaRaio() - 1);
      final.X += inicial.X;
      final.Y += inicial.Y;
    }
    public void moverDireita() {
      final.X++;
      inicial.X++;
    }
    public void moverEsquerda() {
      final.X--;
      inicial.X--;
    }
    public void moverCima() {
      final.Y++;
      inicial.Y++;
    }
    public void moverBaixo() {
      final.Y--;
      inicial.Y--;
    }

    protected override void DesenharObjeto()
    {
      GL.LineWidth(5);
      GL.Begin(PrimitiveType.Lines);

      GL.Vertex2(inicial.X, inicial.Y);
      GL.Vertex2(final.X, final.Y);
      
      GL.End();
    }

  }
}