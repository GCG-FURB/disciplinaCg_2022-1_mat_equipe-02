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
    private Ponto4D  ultimoPonto;
    public Poligono(char rotulo, Objeto paiRef,Ponto4D ponto) : base(rotulo, paiRef)
    {
      /*int[] vet = new int[10]{200,300,200,500,400,500,400,300,300,400};
      for(int i = 0;i < 9;i+=2){
      char id = Utilitario.charProximo(rotulo);
      Ponto A = new Ponto(id,paiRef,vet[i],vet[i+1]);
      base.PontosAdicionar(A.ponto);
      pontosPoligono.Add(A);
      }*/
      base.PrimitivaTipo = PrimitiveType.LineStrip;
      PontosAdicionar(ponto);
      Ponto4D a = new(ponto.X,ponto.Y);
      PontosAdicionar(a);
      ultimoPonto =  PontosUltimo();
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
    public void atualizaUltimoPonto(double x,double y){
      ultimoPonto.X = x;
      ultimoPonto.Y = y;
    }
    public void adicionarPonto(Ponto4D p){
      PontosAdicionar(p);
      ultimoPonto = PontosUltimo();
    }
    public void alternaPrimitiva(){
      if(base.PrimitivaTipo == PrimitiveType.LineStrip){
        base.PrimitivaTipo = PrimitiveType.LineLoop;
      }else{
        base.PrimitivaTipo = PrimitiveType.LineStrip;
      }
    }
    public void finalizaDesenho(){
      PontosRemoverUltimo();
    }
    public bool foiSelecionado(double x,double y){
      bool selecionado = false;
      if(base.PrimitivaTipo == PrimitiveType.LineLoop){
        selecionado = GetBBox().Verifica(x,y,pontosLista,false);
      }else{
        selecionado = GetBBox().Verifica(x,y,pontosLista,true);
      }
      return selecionado;
    }
  }
}