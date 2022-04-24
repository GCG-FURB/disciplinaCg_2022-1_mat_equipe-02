using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

  class Program
    {
      static void Main(string[] args)
      {
        char objetoId = Utilitario.charProximo('@');
        ObjetoGeometria obj_Circulo = new Circulo(objetoId, null, new Ponto4D(0, 0, 0), 100, 72);
        obj_Circulo.ObjetoCor.CorR = 255; obj_Circulo.ObjetoCor.CorG = 255; obj_Circulo.ObjetoCor.CorB = 0;
        Mundo window = Mundo.GetInstance(600, 600, obj_Circulo);
        window.Title = "CG_N2_2";
        window.setCameraPosition(-400, -400, 400, 400);
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