using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

class Program
{
    static void Main(string[] args)
    {
        Mundo window = Mundo.GetInstance(600, 600);
        window.Title = "CG_N2_7";
        window.setCameraPosition(-400, -400, 400, 400);
        window.Run(1.0 / 60.0);
    }
}