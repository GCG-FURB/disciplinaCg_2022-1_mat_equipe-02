using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

  class Program
    {
      static void Main(string[] args)
      {
        List<Objeto> objetos = new List<Objeto>();
        char objetoId = Utilitario.charProximo('@');
        ObjetoGeometria c1 = new Circulo(objetoId, null, new Ponto4D(0, 0, 0), 100, 72);
        c1.ObjetoCor.CorR = 0; c1.ObjetoCor.CorG = 0; c1.ObjetoCor.CorB = 0;
        objetos.Add(c1);
        Mundo window = Mundo.GetInstance(600, 600, objetos);
        window.Title = "CG_N2_2";
        window.setCameraPosition(-400, -400, 400, 400);
        window.setBackgroundColor(1f, 1f, 1f, 1f);
        adicionarKeys(window);
        window.Run(1.0 / 60.0);
      }

      private static void adicionarKeys(Mundo window) {
        window.addCustomKey(Key.I, window.zoomIn);
        window.addCustomKey(Key.O, window.zoomOut);
        window.addCustomKey(Key.B, window.panBaixo);
        window.addCustomKey(Key.C, window.panCima);
        window.addCustomKey(Key.D, window.panDireita);
        window.addCustomKey(Key.E, window.panEsquerda);
      }

    }