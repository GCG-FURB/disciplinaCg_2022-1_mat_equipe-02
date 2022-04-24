using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

namespace CG_N2_4
{
  class Program
    {
      static void Main(string[] args)
      {
        List<Objeto> objetos = new List<Objeto>();
        char objetoId = Utilitario.charProximo('@');
        Ponto4D ptoInfEsq = new Ponto4D(200,200);
        Ponto4D ptoSupDir = new Ponto4D(-200,-200);
        Primitivas obj_Primitivas = new Primitivas(objetoId, null, ptoInfEsq, ptoSupDir);
        objetos.Add(obj_Primitivas);
        Mundo window = Mundo.GetInstance(600, 600, objetos);
        window.Title = "CG_N2_4";
        window.setCameraPosition(-400, -400, 400, 400);
        window.addCustomKey(Key.Space, obj_Primitivas.nextPrimitive);
        window.Run(1.0 / 60.0);
      }

    }
}
