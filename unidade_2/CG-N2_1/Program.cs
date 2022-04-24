using CG_Biblioteca;
using CG_N2;

namespace CG_N2_1
{
  class Program
  {
    static void Main(string[] args)
    {
      List<Objeto> objetos = new List<Objeto>();
      char objetoId = Utilitario.charProximo('@');
      ObjetoGeometria c1 = new Circulo(objetoId, null, new Ponto4D(0, 0, 0), 100, 72);
      c1.ObjetoCor.CorR = 255; c1.ObjetoCor.CorG = 255; c1.ObjetoCor.CorB = 0;
      objetos.Add(c1);
      Mundo window = Mundo.GetInstance(600, 600, objetos);
      window.Title = "CG_N2_1";
      window.setCameraPosition(-300, -300, 300, 300);
      window.Run(1.0 / 60.0);
    }
  }
}
