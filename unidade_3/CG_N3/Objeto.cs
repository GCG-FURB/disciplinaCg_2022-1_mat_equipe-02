/**
  Autor: Dalton Solano dos Reis
**/

#define CG_OpenGL

using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;
using System;

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
    public void translacaoPoligonoX(bool direcao) {
      Transformacao4D matriz2 = new();
      
      if(direcao) {
      matriz2.AtribuirTranslacao(10, 0, 0);
      matriz.MultiplicarMatriz(matriz2);
      //AtribuirTranslacao();
      }
      else {
      matriz2.AtribuirTranslacao(-10, 0, 0);
      matriz.MultiplicarMatriz(matriz2);
      }
    }

    public void translacaoPoligonoY(bool direcao) {
      Transformacao4D matriz2 = new();
      
      if(direcao) {
      matriz2.AtribuirTranslacao(0, 10, 0);
      matriz.MultiplicarMatriz(matriz2);
      //AtribuirTranslacao();
      } else {
      matriz2.AtribuirTranslacao(0, -10, 0);
      matriz.MultiplicarMatriz(matriz2);
      }
    }

    
    public void aumentarObjetoOrigem(){
      double[]dados = matriz.ObterDados();
      Transformacao4D aux = new();
      aux.AtribuirElemento(0,dados[0]*1.0);
      aux.AtribuirElemento(5,dados[5]*1.0);
      aux.AtribuirElemento(10,dados[10]*1.0);
      matriz.MultiplicarMatriz(aux);
    }
    public void diminuiObjetoOrigem(){
      double[]dados = matriz.ObterDados();
      Transformacao4D aux = new();
      aux.AtribuirElemento(0,dados[0]*0.9);
      aux.AtribuirElemento(5,dados[5]*0.9);
      aux.AtribuirElemento(10,dados[10]*0.9);
      matriz.MultiplicarMatriz(aux);
    }
    public void aumentarObjeto(){
      Transformacao4D aux = new();
      Ponto4D centro = bBox.obterCentro; 
      aux.AtribuirElemento(12,-centro.X);
      aux.AtribuirElemento(13,-centro.Y);
      aux.AtribuirElemento(14,-centro.Z);
      matriz.MultiplicarMatriz(aux);
      aux.AtribuirIdentidade();
      aumentarObjetoOrigem();
      aux.AtribuirElemento(12,centro.X);
      aux.AtribuirElemento(13,centro.Y);
      aux.AtribuirElemento(14,centro.Z);
      matriz.MultiplicarMatriz(aux);
    }
    public void diminuiObjeto(){
      double[]dados = matriz.ObterDados();
      Ponto4D centro = bBox.obterCentro;
      matriz.AtribuirTranslacao(dados[12] - centro.X,dados[13] - centro.Y,dados[14] - centro.X);
      matriz.AtribuirEscala(dados[0]*0.9,dados[5]*0.9,dados[10]*0.9);
      matriz.AtribuirTranslacao(dados[12],dados[13],dados[14]);
      
    }
    public void toOrigem(){
      double[]dados = matriz.ObterDados();
      Ponto4D centro = bBox.obterCentro;
      matriz.AtribuirTranslacao(dados[12] - centro.X, dados[13] - centro.Y, dados[14] - centro.Z);
    }
    public void Identidade(){
      matriz.AtribuirIdentidade();
    }
    public void mostraMatriz(){
      double[]dados = matriz.ObterDados();
      Console.WriteLine("|"+dados[0]+"|"+dados[4]+"|"+dados[8]+"|"+dados[12]);
      Console.WriteLine("|"+dados[1]+"|"+dados[5]+"|"+dados[9]+"|"+dados[13]);
      Console.WriteLine("|"+dados[2]+"|"+dados[6]+"|"+dados[10]+"|"+dados[14]);
      Console.WriteLine("|"+dados[3]+"|"+dados[7]+"|"+dados[11]+"|"+dados[15]);
    }
  }
}