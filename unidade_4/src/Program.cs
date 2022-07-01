using CG_Biblioteca;
using CG_N4;
using OpenTK;

class Program
  {
    static void Main(string[] args)
    {
      ToolkitOptions.Default.EnableHighResolution = false;
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N4";
      window.Run(1.0 / 60.0);
    }
  }