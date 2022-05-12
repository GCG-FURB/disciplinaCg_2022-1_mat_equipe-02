using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

  class Program
    {
      static void Main(string[] args)
      {
        List<Objeto> objetos = new List<Objeto>();
        char objetoId = Utilitario.charProximo('@');
        Ponto4D[] vet = new Ponto4D[4];
        vet[0] = new Ponto4D(-100.0,-100.0);
        vet[1] = new Ponto4D(-100.0,100.0);
        vet[2] = new Ponto4D(100.0,100.0);
        vet[3] = new Ponto4D(100.0,-100.0);
        Spline eba = new Spline(objetoId,null,vet);
        objetos.Add(eba);
        Mundo window = Mundo.GetInstance(600, 600, objetos);
        window.Title = "CG_N2_6";
        window.setCameraPosition(-400, -400, 400, 400);
        adicionarKeys(window, eba);
        window.Run(1.0 / 60.0);
      }
      private static void adicionarKeys(Mundo window, Spline eba){
        window.addCustomKey(Key.E, eba.esquerda);
        window.addCustomKey(Key.D, eba.direita);
        window.addCustomKey(Key.C, eba.cima);
        window.addCustomKey(Key.B, eba.baixo);
        window.addCustomKey(Key.KeypadPlus, eba.aumentaT);
        window.addCustomKey(Key.KeypadMinus, eba.diminuiT);
        window.addCustomKey(Key.Keypad1, eba.ponto1);
        window.addCustomKey(Key.Keypad2, eba.ponto2);
        window.addCustomKey(Key.Keypad3, eba.ponto3);
        window.addCustomKey(Key.Keypad4, eba.ponto4);
        window.addCustomKey(Key.R, eba.reseta);
      }
    }