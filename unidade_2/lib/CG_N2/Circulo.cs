using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using CG_N2;

public class Circulo : ObjetoGeometria
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
        base.PontosAdicionar(Matematica.GerarPtoNaCircunferencia(angulo, raio));
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