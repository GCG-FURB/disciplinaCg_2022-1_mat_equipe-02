using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;
using System.Collections.Generic;

namespace CG_N4
{
  public class Poligono : ObjetoGeometria
  {
    private Ponto4D  pontoSelecionado;
    public Poligono(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef)
    {
      
      if(paiRef != null && this.GetType() == paiRef.GetType()) {
        paiRef.FilhoAdicionar(this);
      }
      base.PrimitivaTipo = PrimitiveType.LineStrip;
      PontosAdicionar(ponto);
      pontoSelecionado =  PontosUltimo();
    }

    protected override void DesenharObjeto()
    {
      GL.LineWidth(5);
      GL.Begin(base.PrimitivaTipo);
      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End();
    }
    public void atualizaPontoSelecionado(int x,int y){
        pontoSelecionado.X = x;
        pontoSelecionado.Y = y;
    }
    public int lenght() {
      return pontosLista.Count;
    }
    public bool foiSelecionado(double x,double y){
      bool selecionado = false;
      if(base.PrimitivaTipo == PrimitiveType.LineLoop){
        selecionado = GetBBox().VerificaScanline(x,y,pontosLista,false);
      }else{
        selecionado = GetBBox().VerificaScanline(x,y,pontosLista,true);
      }
      return selecionado;
    }
  }
}
