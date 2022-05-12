using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;

namespace CG_N2
{
  public class Spline : ObjetoGeometria
  {
    protected Ponto4D[] controle = new Ponto4D[4];
    protected int ptSel = 0;
    protected int qtDiv = 40;
     private static Cor cyano=new(0,255,255), preto=new(0,0,0), amarelo=new(255,255,0), vermelho=new(255,0,0) ;
     Cor[] cores = {cyano, amarelo, preto,vermelho};
    public Spline(char rotulo, Objeto paiRef, Ponto4D[] ptsControle) : base(rotulo, paiRef)
    {
        for (int i = 0; i < 4 ; i++ ){
            controle[i] = ptsControle[i];
        }
        base.PrimitivaTipo = PrimitiveType.LineStrip;
        calPtsSpline();
    }

    private void calPtsSpline(){
        base.PontosRemoverTodos();
        double tinc = 1.0/qtDiv;
        double t = tinc;
        //todo ver se tem como colocar o tinc no for
        for(int i = 0; i <qtDiv;i++){
            
            double k0 = Math.Pow((1-t),3);
            double k1 = 3*t*Math.Pow((1-t),2);
            double k2 = 3*t*t*(1-t);
            double k3 = Math.Pow(t,3);
            double x = (k0*controle[0].X)+(k1*controle[1].X)+(k2*controle[2].X)+(k3*controle[3].X);
            double y = (k0*controle[0].Y)+(k1*controle[1].Y)+(k2*controle[2].Y)+(k3*controle[3].Y);
            base.PontosAdicionar(new Ponto4D(x,y));
            t += tinc;
        
        }
    }
    protected override void DesenharObjeto()
    {
      GL.Begin(base.PrimitivaTipo);
      GL.Color3(cores[0].CorR, cores[0].CorG, cores[0].CorB);
      GL.Vertex2(controle[3].X,controle[3].Y);
      GL.Vertex2(controle[2].X,controle[2].Y);
      GL.Vertex2(controle[1].X,controle[1].Y);
      GL.Vertex2(controle[0].X,controle[0].Y);
      GL.Color3(cores[1].CorR, cores[1].CorG, cores[1].CorB);
      GL.Vertex2(controle[0].X,controle[0].Y);
      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End();
      GL.PointSize(10);
      GL.Begin(PrimitiveType.Points);
      GL.Color3(cores[2].CorR, cores[2].CorG, cores[2].CorB);
        switch (ptSel){
            case 0:
                GL.Color3(cores[3].CorR, cores[3].CorG, cores[3].CorB);
                GL.Vertex2(controle[0].X,controle[0].Y);
                GL.Color3(cores[2].CorR, cores[2].CorG, cores[2].CorB);
                GL.Vertex2(controle[1].X,controle[1].Y);
                GL.Vertex2(controle[2].X,controle[2].Y);
                GL.Vertex2(controle[3].X,controle[3].Y);
                break;
            case 1:
                GL.Vertex2(controle[0].X,controle[0].Y);
                GL.Color3(cores[3].CorR, cores[3].CorG, cores[3].CorB);
                GL.Vertex2(controle[1].X,controle[1].Y);
                GL.Color3(cores[2].CorR, cores[2].CorG, cores[2].CorB);
                GL.Vertex2(controle[2].X,controle[2].Y);
                GL.Vertex2(controle[3].X,controle[3].Y);
                break;
            case 2:
                GL.Vertex2(controle[0].X,controle[0].Y);
                GL.Vertex2(controle[1].X,controle[1].Y);
                GL.Color3(cores[3].CorR, cores[3].CorG, cores[3].CorB);
                GL.Vertex2(controle[2].X,controle[2].Y);
                GL.Color3(cores[2].CorR, cores[2].CorG, cores[2].CorB);
                GL.Vertex2(controle[3].X,controle[3].Y);
                break;
            case 3:
                GL.Vertex2(controle[0].X,controle[0].Y);
                GL.Vertex2(controle[1].X,controle[1].Y);
                GL.Vertex2(controle[2].X,controle[2].Y);
                GL.Color3(cores[3].CorR, cores[3].CorG, cores[3].CorB);
                GL.Vertex2(controle[3].X,controle[3].Y);
                break;
            default:
                break;
        }
      GL.End();
      
    }
    public void esquerda(){
        controle[ptSel].X -= 2;
        calPtsSpline();
    }
    public void direita(){
        controle[ptSel].X += 2;
        calPtsSpline();
    }
    public void cima(){
        controle[ptSel].Y += 2;
        calPtsSpline();
    }
    public void baixo(){
        controle[ptSel].Y -= 2;
        calPtsSpline();
    }
    public void aumentaT(){
        if(qtDiv<=70){
            qtDiv++;
            calPtsSpline();
        }
    }
    public void diminuiT(){
        if(qtDiv>=2){
            qtDiv--;
            calPtsSpline();
        }
    }
    public void ponto1(){
        ptSel = 0;
    }
    public void ponto2(){
        ptSel = 1;
    }
    public void ponto3(){
        ptSel = 2;
    }
    public void ponto4(){
        ptSel = 3;
    }
    public void reseta(){
        controle[0] = new Ponto4D(-100,-100);
        controle[1] = new Ponto4D(-100,100);
        controle[2] = new Ponto4D(100,100);
        controle[3] = new Ponto4D(100,-100);
        ptSel = 0;
        qtDiv = 40;
        calPtsSpline();
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

  }
}