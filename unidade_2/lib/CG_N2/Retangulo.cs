/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Debug

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace CG_N2
{
  public class Retangulo : ObjetoGeometria
  {
    private Cor[] paleta = {new Cor(178,102,255),new Cor(255,255,50),new Cor(50,255,255)};
    public Retangulo(char rotulo, Objeto paiRef, Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base(rotulo, paiRef)
    {
      base.PontosAdicionar(ptoInfEsq);
      base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
      base.PontosAdicionar(ptoSupDir);
      base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));
      base.ObjetoCor = paleta[0];
    }

    protected override void DesenharObjeto()
    {
      GL.Begin(base.PrimitivaTipo);
      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End();
    }
    
    //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Retangulo: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }
#endif
    public void verificaBBox(double x, double y,bool limit){
      int aux = this.BBox.Verifica(x,y,limit);
      switch (aux){
      case 0:
        this.ObjetoCor = paleta[0];
      break;
      case 1:
        this.ObjetoCor = paleta[1];
      break;
      case 2:
        this.ObjetoCor = paleta[2];
      break;
      }
    }
  }
}