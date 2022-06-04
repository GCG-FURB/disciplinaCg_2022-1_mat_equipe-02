using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Poligono : ObjetoGeometria
  {
    private Ponto4D  ultimoPonto;
    public Poligono(char rotulo, Objeto paiRef, Ponto4D ponto) : base(rotulo, paiRef)
    {
      /*
      if(paiRef != null && this.GetType() == paiRef.GetType()) {
        paiRef.FilhoAdicionar(this);
        copiarPropriedadesPai((Poligono) paiRef, rotulo);
      }*/

      base.PrimitivaTipo = PrimitiveType.LineStrip;
      PontosAdicionar(ponto);
      Ponto4D a = new(ponto.X,ponto.Y);
      PontosAdicionar(a);
      ultimoPonto =  PontosUltimo();
    }

    /*private void copiarPropriedadesPai(Poligono pai, char id) {
      foreach(Ponto ponto in pai.pontosPoligono) {
        char objetoId = Utilitario.charProximo(id);
        Ponto novoPonto = new(objetoId,null,ponto.getX(),ponto.getY());
        adicionarPonto(novoPonto);
      }
      this.ObjetoCor = pai.ObjetoCor;
    }*/
    protected override void DesenharObjeto()
    {
      GL.LineWidth(6);
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