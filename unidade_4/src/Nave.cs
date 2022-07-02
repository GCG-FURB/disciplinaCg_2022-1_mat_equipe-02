using CG_N4;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

  internal class Nave : Poligono
  {

    public List<Tiro> tiros = new List<Tiro>();

    public Nave(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef, ponto)
    {
        adicionarPontoPegaUltimo(new(ponto.X + 20, 20));
        adicionarPontoPegaUltimo(new(ponto.X + 40));
    }

    public void atirar(Mundo context) {
      Console.Write(this);
      Tiro tiro = new('N', null, new(pontosLista[2].X, pontosLista[2].Y));
      tiro.matriz = matriz;
      context.addObjetoNaLista(tiro);
    }
}