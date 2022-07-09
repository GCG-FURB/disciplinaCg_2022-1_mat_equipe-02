using CG_N4;
using OpenTK;
using OpenTK.Input;

class Program
  {

    static int NUM_MAX_ASTEROIDES = 3;

    static void Main(string[] args)
    {

      ToolkitOptions.Default.EnableHighResolution = false;
      Mundo window = Mundo.GetInstance(600, 600);
      Nave nave = new('N', null, new(300,0,10));
      window.setarNave(nave);
      window.addCustomKey(Key.Up, nave.atirar);
      window.Title = "CG_N4";
      gerarAsteroides(window);
      window.Run(1.0 / 60.0);
    }

    private static void gerarAsteroides(Mundo window) {
      for (int i =0; i < NUM_MAX_ASTEROIDES; i++) {
        Asteroide asteroide = new Asteroide('N', null, new(100 * i + 50, 650), NUM_MAX_ASTEROIDES);
        window.addObjetoNaLista(asteroide);
      }
    }
  }
