using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace CG_N2
{
  public class Circulo : ObjetoGeometria
  {

    int raio;
    int quantidadePontos;
    Ponto4D centro;
    public Circulo(char rotulo, Objeto paiRef, Ponto4D centro, int raio, int quantidadePontos) : base(rotulo, paiRef)
    {
      this.quantidadePontos = quantidadePontos;
      this.raio = raio;
      this.centro = centro;
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
    }

  }
}