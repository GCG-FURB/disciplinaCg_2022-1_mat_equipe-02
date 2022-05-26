/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Debug
#define CG_OpenGL
// #define CG_DirectX

using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Poligono : ObjetoGeometria
  {
    protected List<Ponto> pontosPoligono = new List<Ponto>();
    private Ponto ultimoPonto;
    public Poligono(char rotulo, Objeto paiRef,Ponto ponto) : base(rotulo, paiRef)
    {
      /*int[] vet = new int[10]{200,300,200,500,400,500,400,300,300,400};
      for(int i = 0;i < 9;i+=2){
      char id = Utilitario.charProximo(rotulo);
      Ponto A = new Ponto(id,paiRef,vet[i],vet[i+1]);
      base.PontosAdicionar(A.ponto);
      pontosPoligono.Add(A);
      }*/
      base.PrimitivaTipo = PrimitiveType.LineStrip;
      pontosPoligono.Add(ponto);
      char id = Utilitario.charProximo(rotulo);
      Ponto ponto2 = new Ponto(id,null,ponto.getX(),ponto.getY());
      pontosPoligono.Add(ponto2);
      ultimoPonto = ponto2;
    }

    protected override void DesenharObjeto()
    {
      GL.Begin(base.PrimitivaTipo);
      foreach (Ponto pto in pontosPoligono)
      {
        GL.Vertex2(pto.getX(), pto.getY());
      }
      GL.End();
    }
    public void atualizaUltimoPonto(double x,double y){
      ultimoPonto.setX(x);
      ultimoPonto.setY(y);
    }
    public void adicionarPonto(Ponto p){
      pontosPoligono.Add(p);
      ultimoPonto = p;
    }
    public void alternaPrimitiva(){
      if(base.PrimitivaTipo == PrimitiveType.LineStrip){
        base.PrimitivaTipo = PrimitiveType.LineLoop;
      }else{
        base.PrimitivaTipo = PrimitiveType.LineStrip;
      }
    }
  }
}