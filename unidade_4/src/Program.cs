using CG_N4;
using OpenTK;
using OpenTK.Input;

class Program
  {

    static int NUM_MAX_ASTEROIDES = 1;

    static void Main(string[] args)
    {

      ToolkitOptions.Default.EnableHighResolution = false;
      Mundo window = Mundo.GetInstance(600, 600);
      Nave nave = new('N', null, new(280));
      nave.alternaPrimitiva();
      window.addObjetoNaLista(nave);
      window.selecionarObjeto(nave);
      window.addCustomKey(Key.Up, nave.atirar);
      Asteroide asteroide = new Asteroide('N', null, new(100, 650), 1);
      window.addCustomKey(Key.Space, asteroide.matar);
      window.addObjetoNaLista(asteroide);
      window.Title = "CG_N4";
      gerarAsteroides(window);
      window.Run(1.0 / 60.0);
    }

    private static void gerarAsteroides(Mundo window) {
      for (int i =0; i < NUM_MAX_ASTEROIDES; i++) {

      }
    }
  }
