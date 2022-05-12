using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

  class Program
    {
      static void Main(string[] args)
      {
        char objetoRetaId = Utilitario.charProximo('@');
        SegReta segReta = new SegReta(objetoRetaId, null,  new Ponto4D(0,0), Matematica.GerarPtoDaCircunferencia(45, 100));
        segReta.ObjetoCor.CorR = 0; segReta.ObjetoCor.CorG = 0; segReta.ObjetoCor.CorB = 0;
        Mundo window = Mundo.GetInstance(600, 600, new List<Objeto>{segReta});
        window.Title = "CG_N2_5";
        window.setCameraPosition(-300, -300, 300, 300);
        adicionarKeys(window, segReta);
        window.Run(1.0 / 60.0);
      }

      private static void adicionarKeys(Mundo window, SegReta segReta) {
        window.addCustomKey(Key.Z, segReta.girarAntiHorario);
        window.addCustomKey(Key.X, segReta.girarHorario);
        window.addCustomKey(Key.S, segReta.aumentar);
        window.addCustomKey(Key.A, segReta.diminuir);
        window.addCustomKey(Key.W, segReta.moverDireita);
        window.addCustomKey(Key.Q, segReta.moverEsquerda);
        window.addCustomKey(Key.E, segReta.moverCima);
        window.addCustomKey(Key.D, segReta.moverBaixo);
      }

    }