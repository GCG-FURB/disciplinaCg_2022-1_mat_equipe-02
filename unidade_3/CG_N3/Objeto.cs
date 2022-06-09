/**
  Autor: Dalton Solano dos Reis
**/

#define CG_OpenGL

using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal abstract class Objeto
  {
    protected char rotulo;
    private Cor objetoCor = new Cor(255, 255, 255, 255);
    public Cor ObjetoCor { get => objetoCor; set => objetoCor = value; }
    private PrimitiveType primitivaTipo = PrimitiveType.LineLoop;
    public PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    private float primitivaTamanho = 1;
    public float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }
    private BBox bBox = new BBox();
    public BBox BBox { get => bBox; set => bBox = value; }
    public Transformacao4D matriz = new Transformacao4D();
    private List<Objeto> objetosLista = new List<Objeto>();

    public Objeto(char rotulo, Objeto paiRef)
    {
      this.rotulo = rotulo;
    }

    public void Desenhar()
    {
#if CG_OpenGL
      GL.PushMatrix();
      GL.MultMatrix(matriz.ObterDados());
      GL.Color3(objetoCor.CorR, objetoCor.CorG, objetoCor.CorB);
      GL.LineWidth(primitivaTamanho);
      GL.PointSize(primitivaTamanho);
#endif
      DesenharGeometria();
      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }
      GL.PopMatrix();
    }
    protected abstract void DesenharGeometria();
    public void FilhoAdicionar(Objeto filho)
    {
      this.objetosLista.Add(filho);
    }
    public void FilhoRemover(Objeto filho)
    {
      this.objetosLista.Remove(filho);
    }
    public BBox GetBBox(){
      return bBox;
    }
    public void aumentarObjetoOrigem(){
      double[]dados = matriz.ObterDados();
      matriz.AtribuirEscala(dados[0]*1.1,dados[5]*1.1,dados[10]*1.1);
    }
    public void diminuiObjetoOrigem(){
      double[]dados = matriz.ObterDados();
      matriz.AtribuirEscala(dados[0]*0.9,dados[5]*0.9,dados[10]*0.9);
    }
    public void aumentarObjeto(){
      double[]dados = matriz.ObterDados();
      Ponto4D centro = bBox.obterCentro;
      matriz.AtribuirTranslacao(dados[12] - centro.X,dados[13] - centro.Y,dados[14] - centro.Z);
      matriz.AtribuirEscala(dados[0]*1.1,dados[5]*1.1,dados[10]*1.1);
      matriz.AtribuirTranslacao(dados[12],dados[13],dados[14]);
    }
    public void diminuiObjeto(){
      double[]dados = matriz.ObterDados();
      Ponto4D centro = bBox.obterCentro;
      matriz.AtribuirTranslacao(dados[12] - centro.X,dados[13] - centro.Y,dados[14] - centro.Z);
      matriz.AtribuirEscala(dados[0]*0.9,dados[5]*0.9,dados[10]*0.9);
      matriz.AtribuirTranslacao(dados[12],dados[13],dados[14]);
    }
  }
}