using CG_N4;
using OpenTK;
using OpenTK.Input;

class Program
  {
    static void Main(string[] args)
    {
      ToolkitOptions.Default.EnableHighResolution = false;
      Mundo window = Mundo.GetInstance(600, 600);
      Nave nave = new('N', null, new(280));
      nave.alternaPrimitiva();
      window.addObjetoNaLista(nave);
      window.selecionarObjeto(nave);
      window.addCustomKey(Key.Up, nave.atirar);
      window.Title = "CG_N4";
      window.Run(1.0 / 60.0);
    }
  }