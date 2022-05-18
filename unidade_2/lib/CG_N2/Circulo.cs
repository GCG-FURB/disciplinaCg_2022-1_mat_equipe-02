using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;

namespace CG_N2
{
  public class Circulo : ObjetoGeometria
  {

    int raio;

    bool desenhaCentro = false;
    double raio2;
    int quantidadePontos;
    Ponto4D centro;

    double x,y;
    public Circulo(char rotulo, Objeto paiRef, Ponto4D centro, int raio, int quantidadePontos) : base(rotulo, paiRef)
    {
      this.quantidadePontos = quantidadePontos;
      this.raio = raio;
      this.raio2 = raio*raio;
      this.centro = centro;
      this.x = centro.X;
      this.y = centro.Y;
      criarPontosCircunferencia();
    }

    private void criarPontosCircunferencia()
    {
      base.PontosRemoverTodos();
      double angulo = 0;
      for(int i = 0; i < quantidadePontos; i++)
      {
        angulo += 360 / quantidadePontos;
        Ponto4D ponto = Matematica.GerarPtoDaCircunferencia(angulo, raio);
        ponto.X += centro.X;
        ponto.Y += centro.Y;
        base.PontosAdicionar(ponto);
      }
    }

    protected override void DesenharObjeto()
    {
      GL.PointSize(6);
      GL.Begin(PrimitiveType.Points);

      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End();
      if(desenhaCentro){
        GL.Begin(PrimitiveType.Points);
        GL.Vertex2(centro.X, centro.Y);
        GL.End();
      }
    }
    public void atualizacentro(double x,double y,Circulo ciculoGrande,Retangulo ret){
      double novoX  = centro.X + x;
      double novoY = centro.Y + y;
      double raioteste = Math.Pow(novoX -ciculoGrande.centro.X,2)+Math.Pow(novoY -ciculoGrande.centro.Y,2);
      if(raioteste <= ciculoGrande.raio2 && raioteste  >=ciculoGrande.raio2-150)
      {
        ret.verificaBBox(novoX,novoY,true);
        this.centro.X = novoX;
        this.centro.Y = novoY;
        criarPontosCircunferencia();
      }
      else if(raioteste < ciculoGrande.raio2-150){
        ret.verificaBBox(novoX,novoY,false);
        this.centro.X = novoX;
        this.centro.Y = novoY;
        criarPontosCircunferencia();
      }
      else
      {

      }

    }
    public int getRaio => raio;

    public double getRaio2 => raio2;

    public Ponto4D getCentro => centro;
    public void switchCentro(){
      if(desenhaCentro == false){
        desenhaCentro = true;
      }else{
        desenhaCentro = false;
      }
    }

    public void reset(){
      this.centro.X = this.x;
      this.centro.Y = this.y;
      criarPontosCircunferencia();
    }
  }
}