using CG_Biblioteca;
using CG_N2;

  class Program
    {

      private static Ponto4D[] centros = {new(0,100), new(-100,-100), new(100,-100)};
      static void Main(string[] args)
      {
        List<Objeto> objetos = criarObjetos();
        Mundo window = Mundo.GetInstance(600, 600, objetos);
        window.Title = "CG_N2_3";
        window.setCameraPosition(-300, -300, 300, 300);
        window.Run(1.0 / 60.0);
      }

      private static List<Objeto> criarObjetos() {
        List<Objeto> objetos = new List<Objeto>();
        for(int i = 0; i < 3; i++) {
          adicionaCircunferencia(i, objetos);
          adicionaSegReta(i, objetos);
        }
        return objetos;
      }

      private static void adicionaCircunferencia(int i, List<Objeto> objetos) {
        char objetoId = Utilitario.charProximo((char)(i));
        ObjetoGeometria circunferencia = new Circulo(objetoId, null,  centros[i], 100, 72);
        circunferencia.ObjetoCor.CorR = 0; circunferencia.ObjetoCor.CorG = 0; circunferencia.ObjetoCor.CorB = 0;
        objetos.Add(circunferencia);
      }
      private static void adicionaSegReta(int i, List<Objeto> objetos) {
        char objetoRetaId = Utilitario.charProximo((char)(i));
        ObjetoGeometria segReta = new SegReta(objetoRetaId, null,  centros[i], centros[(i + 1) % 3]);
        segReta.ObjetoCor.CorR = 0; segReta.ObjetoCor.CorG = 255; segReta.ObjetoCor.CorB = 255;
        objetos.Add(segReta);
      }

    }