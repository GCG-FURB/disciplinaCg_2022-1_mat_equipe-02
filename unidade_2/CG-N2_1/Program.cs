using CG_Biblioteca;
using CG_N2;

namespace CG_N2_1
{
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
