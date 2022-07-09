using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace CG_N4
{
  public class Asteroide : Poligono
  {

    protected static int cooldown = 0;
    protected static int chamada = 0;
    private bool isDead = false;

    private int id;
    private int maxAsteroides;

    private static Cor marrom=new(0,100,0), branco=new(255,255,255), preto=new(0,0,0), cinza=new(75,0,130), prata=new(255,165,0),ciano=new(0,255,255);
    PrimitiveType[] primitivas = { PrimitiveType.Points, PrimitiveType.Lines, PrimitiveType.LineLoop, PrimitiveType.LineStrip, PrimitiveType.Triangles, PrimitiveType.TriangleStrip, PrimitiveType.TriangleFan, PrimitiveType.Quads, PrimitiveType.QuadStrip, PrimitiveType.Polygon };
    Cor[] cores = { marrom, preto, branco, cinza, prata, ciano};

    private double[]pontoInicial = new double[3];

    public Asteroide(char rotulo, Objeto paiRef, Ponto4D ponto, int maxAsteroides) : base(rotulo, paiRef, ponto)
    {
      this.maxAsteroides = maxAsteroides;
      this.id = chamada++;
      pontoInicial[0] = ponto.X; pontoInicial[1] = ponto.Y; pontoInicial[2] = ponto.Z; 
      PontosAdicionar(new(ponto.X+15,ponto.Y-5,ponto.Z));//1
      PontosAdicionar(new(ponto.X+27,ponto.Y-16,ponto.Z));//2
      PontosAdicionar(new(ponto.X+20,ponto.Y-20,ponto.Z));//3
      PontosAdicionar(new(ponto.X+6,ponto.Y-28,ponto.Z));//4
      PontosAdicionar(new(ponto.X-10,ponto.Y-14,ponto.Z));//5
      PontosAdicionar(new(ponto.X+8,ponto.Y-12,ponto.Z+8));//6
      PontosAdicionar(new(ponto.X+18,ponto.Y-18,ponto.Z+15));//7
      PontosAdicionar(new(ponto.X+18,ponto.Y-18,ponto.Z-15));//8
      PontosAdicionar(new(ponto.X+8,ponto.Y-12,ponto.Z-8));//9

    }

    protected override void DesenharObjeto()
    {
      if(isDead && (cooldown < 0) && (chamada % maxAsteroides == id)) 
      {
        pontosLista[0].Y = pontoInicial[1];
        pontosLista[1].Y = pontoInicial[1]-5;
        pontosLista[2].Y = pontoInicial[1]-16;
        pontosLista[3].Y = pontoInicial[1]-20;
        pontosLista[4].Y = pontoInicial[1]-28;
        pontosLista[5].Y = pontoInicial[1]-14;
        pontosLista[6].Y = pontoInicial[1]-12;
        pontosLista[7].Y = pontoInicial[1]-18;
        pontosLista[8].Y = pontoInicial[1]-18;
        pontosLista[9].Y = pontoInicial[1]-12;
        isDead = false;
        cooldown = 500;
      }
      cooldown--;
      chamada++;
      for (int i = 0; i < 10; i++)
      {
        if(pontosLista[4].Y > -200) {
          pontosLista[i].Y--;
        } else {
          isDead = true;
        }
      }
      BBox.AtualizaBBox(pontosLista);
      GL.LineWidth(5);
      //A
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[1].CorR, cores[1].CorG, cores[1].CorB);
      GL.Vertex3(pontosLista[0].X, pontosLista[0].Y,pontosLista[0].Z);
      GL.Vertex3(pontosLista[1].X, pontosLista[1].Y,pontosLista[1].Z);
      GL.Vertex3(pontosLista[6].X, pontosLista[6].Y,pontosLista[6].Z);
      GL.End();
      //B
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[0].CorR, cores[0].CorG, cores[0].CorB);
      GL.Vertex3(pontosLista[0].X, pontosLista[0].Y,pontosLista[0].Z);
      GL.Vertex3(pontosLista[5].X, pontosLista[5].Y,pontosLista[5].Z);
      GL.Vertex3(pontosLista[6].X, pontosLista[6].Y,pontosLista[6].Z);
      GL.End();
      //C
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[2].CorR, cores[2].CorG, cores[2].CorB);
      GL.Vertex3(pontosLista[4].X, pontosLista[4].Y,pontosLista[4].Z);
      GL.Vertex3(pontosLista[5].X, pontosLista[5].Y,pontosLista[5].Z);
      GL.Vertex3(pontosLista[6].X, pontosLista[6].Y,pontosLista[6].Z);
      GL.Vertex3(pontosLista[7].X, pontosLista[7].Y,pontosLista[7].Z);
      GL.End();
      //D
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[3].CorR, cores[3].CorG, cores[3].CorB);
      GL.Vertex3(pontosLista[1].X, pontosLista[1].Y,pontosLista[1].Z);
      GL.Vertex3(pontosLista[2].X, pontosLista[2].Y,pontosLista[2].Z);
      GL.Vertex3(pontosLista[7].X, pontosLista[7].Y,pontosLista[7].Z);
      GL.Vertex3(pontosLista[6].X, pontosLista[6].Y,pontosLista[6].Z);
      GL.End();
      //E
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[4].CorR, cores[4].CorG, cores[4].CorB);
      GL.Vertex3(pontosLista[3].X, pontosLista[3].Y,pontosLista[3].Z);
      GL.Vertex3(pontosLista[4].X, pontosLista[4].Y,pontosLista[4].Z);
      GL.Vertex3(pontosLista[7].X, pontosLista[7].Y,pontosLista[7].Z);
      GL.End();
      //F
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[5].CorR, cores[5].CorG, cores[5].CorB);
      GL.Vertex3(pontosLista[2].X, pontosLista[2].Y,pontosLista[2].Z);
      GL.Vertex3(pontosLista[3].X, pontosLista[3].Y,pontosLista[3].Z);
      GL.Vertex3(pontosLista[7].X, pontosLista[7].Y,pontosLista[7].Z);
      GL.End();
      //A-1
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[1].CorR, cores[1].CorG, cores[1].CorB);
      GL.Vertex3(pontosLista[0].X, pontosLista[0].Y,pontosLista[0].Z);
      GL.Vertex3(pontosLista[1].X, pontosLista[1].Y,pontosLista[1].Z);
      GL.Vertex3(pontosLista[8].X, pontosLista[8].Y,pontosLista[8].Z);
      GL.End();
      //B-1
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[0].CorR, cores[0].CorG, cores[0].CorB);
      GL.Vertex3(pontosLista[0].X, pontosLista[0].Y,pontosLista[0].Z);
      GL.Vertex3(pontosLista[5].X, pontosLista[5].Y,pontosLista[5].Z);
      GL.Vertex3(pontosLista[8].X, pontosLista[8].Y,pontosLista[8].Z);
      GL.End();
      //C-1
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[2].CorR, cores[2].CorG, cores[2].CorB);
      GL.Vertex3(pontosLista[4].X, pontosLista[4].Y,pontosLista[4].Z);
      GL.Vertex3(pontosLista[5].X, pontosLista[5].Y,pontosLista[5].Z);
      GL.Vertex3(pontosLista[8].X, pontosLista[8].Y,pontosLista[8].Z);
      GL.Vertex3(pontosLista[9].X, pontosLista[9].Y,pontosLista[9].Z);
      GL.End();
      //D-1
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[3].CorR, cores[3].CorG, cores[3].CorB);
      GL.Vertex3(pontosLista[1].X, pontosLista[1].Y,pontosLista[1].Z);
      GL.Vertex3(pontosLista[2].X, pontosLista[2].Y,pontosLista[2].Z);
      GL.Vertex3(pontosLista[9].X, pontosLista[9].Y,pontosLista[9].Z);
      GL.Vertex3(pontosLista[8].X, pontosLista[8].Y,pontosLista[8].Z);
      GL.End();
      //E-1
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[4].CorR, cores[4].CorG, cores[4].CorB);
      GL.Vertex3(pontosLista[3].X, pontosLista[3].Y,pontosLista[3].Z);
      GL.Vertex3(pontosLista[4].X, pontosLista[4].Y,pontosLista[4].Z);
      GL.Vertex3(pontosLista[9].X, pontosLista[9].Y,pontosLista[9].Z);
      GL.End();
      //F-1
      GL.Begin(PrimitiveType.Polygon);
      GL.Color3(cores[5].CorR, cores[5].CorG, cores[5].CorB);
      GL.Vertex3(pontosLista[2].X, pontosLista[2].Y,pontosLista[2].Z);
      GL.Vertex3(pontosLista[3].X, pontosLista[3].Y,pontosLista[3].Z);
      GL.Vertex3(pontosLista[9].X, pontosLista[9].Y,pontosLista[9].Z);
      GL.End();
    }

    public void matar() {
      foreach(Ponto4D ponto in pontosLista) {
        ponto.Y = -500;
      }
      isDead = true;
    }
}
}