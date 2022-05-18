using CG_Biblioteca;
using CG_N2;
using OpenTK.Input;

class Program
{
    static void Main(string[] args)
    {
        List<Objeto> objetos = new List<Objeto>();
        char objetoId = Utilitario.charProximo('@');
        Circulo cirGrande = new Circulo(objetoId,null,new Ponto4D(100,100),100,72);
        objetos.Add(cirGrande);
        objetoId = Utilitario.charProximo(objetoId);
        Circulo cirPequeno = new Circulo(objetoId,null,new Ponto4D(100,100),20,20);
        objetos.Add(cirPequeno);
        double cateto  = Math.Sqrt(cirGrande.getRaio2/2);
        objetoId = Utilitario.charProximo(objetoId);
        Retangulo retan = new Retangulo(objetoId,null,new Ponto4D(cirGrande.getCentro.X - cateto, cirGrande.getCentro.Y - cateto),new Ponto4D(cirGrande.getCentro.X + cateto,cirGrande.getCentro.Y + cateto));
        objetos.Add(retan);
        cirPequeno.switchCentro();
        cirPequeno.ObjetoCor = new Cor(0,0,255);
        Mundo window = Mundo.GetInstance(600, 600,objetos);
        window.Title = "CG_N2_7";
        window.setCameraPosition(-100, -100, 300, 300);
        window.Objetos7(cirPequeno,cirGrande,retan);
        window.Run(1.0 / 60.0);
    }
}