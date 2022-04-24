using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using CG_N2;

namespace CG_N2_1
{
  internal class Circulo : ObjetoGeometria
  {

    int raio;
    int quantidadePontos;
    Ponto4D centro;
    public Circulo(char rotulo, Objeto paiRef, Ponto4D centro, int raio, int quantidadePontos) : base(rotulo, paiRef)
    {
      base.PontosAdicionar(centro);
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
        base.PontosAdicionar(Matematica.GerarPtosCirculo(angulo, raio));
      }
    }

    protected override void DesenharObjeto()
    {
      GL.PointSize(8);
      GL.Begin(PrimitiveType.Points);

      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      
      GL.End();
    }

  }

  class Program
    {
      static void Main(string[] args)
      {
        char objetoId = Utilitario.charProximo('@');
        ObjetoGeometria obj_Circulo = new Circulo(objetoId, null, new Ponto4D(0, 0, 0), 100, 72);
        obj_Circulo.ObjetoCor.CorR = 255; obj_Circulo.ObjetoCor.CorG = 255; obj_Circulo.ObjetoCor.CorB = 0;
        Mundo window = Mundo.GetInstance(600, 600, obj_Circulo);
        window.Title = "CG_N2_1";
        window.setCameraPosition(-300, -300, 300, 300);
        window.Run(1.0 / 60.0);
      }
    }
}
