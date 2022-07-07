using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;
using System.Collections.Generic;

namespace CG_N4
{
  public class Poligono : ObjetoGeometria
  {
    private Ponto4D  pontoSelecionado;
    private bool Modificavel;
    public Poligono(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef)
    {
      
      if(paiRef != null && this.GetType() == paiRef.GetType()) {
        paiRef.FilhoAdicionar(this);
      }
      Modificavel = true;
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
      if(Modificavel){
        pontoSelecionado.X = x;
        pontoSelecionado.Y = y;
      }
    }
    public void removerMaisProximo(int x,int y){
      selecionaMaisProximo(x,y);
      pontosLista.Remove(pontoSelecionado);
      GetBBox().AtualizaBBox(pontoSelecionado.X,pontoSelecionado.Y,pontosLista);
    }
    public void fimMover(){
        GetBBox().AtualizaBBox(pontoSelecionado.X,pontoSelecionado.Y,pontosLista);
    }
    public void adicionarPontoPegaUltimo(Ponto4D p){
      if(Modificavel){
        PontosAdicionar(p);
        pontoSelecionado = PontosUltimo();
      }
    }
    public int lenght() {
      return pontosLista.Count;
    }
    public void alternaPrimitiva(){
      if(base.PrimitivaTipo == PrimitiveType.LineStrip){
        base.PrimitivaTipo = PrimitiveType.LineLoop;
      }else{
        base.PrimitivaTipo = PrimitiveType.LineStrip;
      }
    }
    public void finalizaDesenho(){
      if(Modificavel){
        PontosRemoverUltimo();
        Modificavel = false;
      }
    }
    public bool foiSelecionado(double x,double y){
      bool selecionado = false;
      // List<Ponto4D> pontosAtualizados = new List<Ponto4D>();
      // foreach(Ponto4D ponto in pontosLista) {
      //   pontosAtualizados.Add(matriz.MultiplicarPonto(ponto));
      // }
      if(base.PrimitivaTipo == PrimitiveType.LineLoop){
        selecionado = GetBBox().VerificaScanline(x,y,pontosLista,false);
      }else{
        selecionado = GetBBox().VerificaScanline(x,y,pontosLista,true);
      }
      return selecionado;
    }
    
    public void translacaoPoligono() {
      //AtribuirTranslacao();
    }
    public void selecionaMaisProximo(int x,int y){
      Ponto4D temp = new();
      double disMenor = 100000;
      for (var i = 0; i < pontosLista.Count; i++)
        {
          double aux = Math.Sqrt((Math.Pow(pontosLista[i].X-x,2))+(Math.Pow(pontosLista[i].Y-y,2)));
          if( aux < disMenor){
            disMenor = aux;
            temp = pontosLista[i];
          }
        }
      pontoSelecionado = temp;
    }
    public void pegaUltimo(){
      pontoSelecionado = PontosUltimo();
    }
    public void trocaModificado(){
      if(Modificavel){
        Modificavel = false;
      }else{
        Modificavel = true;
      }
    }
  }
}
