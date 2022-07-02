using CG_N4;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

  internal class Asteroide : Poligono
  {

    protected static int cooldown = 0;
    protected static int chamada = 0;
    private bool isDead = false;

    private int id;
    private int maxAsteroides;

    private static Cor marrom=new(255,248,220), branco=new(255,255,255), preto=new(0,0,0);
    PrimitiveType[] primitivas = { PrimitiveType.Points, PrimitiveType.Lines, PrimitiveType.LineLoop, PrimitiveType.LineStrip, PrimitiveType.Triangles, PrimitiveType.TriangleStrip, PrimitiveType.TriangleFan, PrimitiveType.Quads, PrimitiveType.QuadStrip, PrimitiveType.Polygon };
    Cor[] cores = { marrom, branco, marrom,  preto};

    public Asteroide(char rotulo, Objeto paiRef, Ponto4D ponto, int maxAsteroides) : base(rotulo, paiRef, ponto)
    {
      this.maxAsteroides = maxAsteroides;
      this.id = chamada++;
      adicionarPontoPegaUltimo(new(ponto.X + 50, 550));
      adicionarPontoPegaUltimo(new(ponto.X, 450));
      adicionarPontoPegaUltimo(new(ponto.X - 50, 550));
    }

    protected override void DesenharObjeto()
    {
      if(isDead && (cooldown < 0) && (chamada % maxAsteroides == id)) 
      {
        pontosLista[0].Y = 650;
        pontosLista[1].Y = 550;
        pontosLista[2].Y = 450;
        pontosLista[3].Y = 550;
        isDead = false;
        cooldown = 50000;
      }
      cooldown--;
      chamada++;
      GL.LineWidth(5);
      GL.Begin(PrimitiveType.Polygon);
      for (int i = 0; i < pontosLista.Count; i++)
      {
        if(pontosLista[0].Y > -200) {
          pontosLista[i].Y--;
        } else {
          isDead = true;
        }
        GL.Vertex2(pontosLista[i].X, pontosLista[i].Y);
      }
      GL.End();
    }

    public void matar(Mundo mundo) {
      foreach(Ponto4D ponto in pontosLista) {
        ponto.Y = -500;
      }
      isDead = true;
    }
}