/**
  Autor: Dalton Solano dos Reis
**/

#define CG_OpenGL

using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;
using System;

namespace CG_N4
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
      matriz = matriz.MultiplicarMatriz(matriz2);
      //AtribuirTranslacao();
      }
      else {
      matriz2.AtribuirTranslacao(-10, 0, 0);
      matriz = matriz.MultiplicarMatriz(matriz2);
      }
    }

    public void translacaoPoligonoY(bool direcao) {
      Transformacao4D matriz2 = new();
      
      if(direcao) {
      matriz2.AtribuirTranslacao(0, 10, 0);
      matriz = matriz.MultiplicarMatriz(matriz2);
      //AtribuirTranslacao();
      } else {
      matriz2.AtribuirTranslacao(0, -10, 0);
      matriz = matriz.MultiplicarMatriz(matriz2);
      }
    }

    public void Rotacao(double angulo)
    {
      Transformacao4D matrizTmpTranslacao = new();
      Transformacao4D matrizTmpRotacao = new();
      RotacaoEixo(angulo, 'z', matrizTmpRotacao);
      matriz = matrizTmpRotacao.MultiplicarMatriz(matriz);
    }

    public void RotacaoEixo(double angulo, char eixoRotacao, Transformacao4D matrizTmpRotacao)
    {
      switch (eixoRotacao)
      {
        case 'x':
          matrizTmpRotacao.AtribuirRotacaoX(Transformacao4D.DEG_TO_RAD * angulo);
          break;
        case 'y':
          matrizTmpRotacao.AtribuirRotacaoY(Transformacao4D.DEG_TO_RAD * angulo);
          break;
        case 'z':
          matrizTmpRotacao.AtribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * angulo);
          break;
      }
    }

    public void rotacionarEixoBBox(double angulo) {
      Transformacao4D matrizTmpTranslacao = new();
      Transformacao4D matrizTmpRotacao = new();
      Transformacao4D matrizTmpTranslacaoInversa = new();
      Ponto4D pontoPivo = matriz.MultiplicarPonto(bBox.obterCentro);
      matrizTmpTranslacao.AtribuirTranslacao(-pontoPivo.X, -pontoPivo.Y, -pontoPivo.Z);
      matriz = matrizTmpTranslacao.MultiplicarMatriz(matriz);
      RotacaoEixo(angulo, 'z', matrizTmpRotacao);
      matriz = matrizTmpRotacao.MultiplicarMatriz(matriz);

      matrizTmpTranslacaoInversa.AtribuirTranslacao(pontoPivo.X, pontoPivo.Y, pontoPivo.Z);
      matriz = matrizTmpTranslacaoInversa.MultiplicarMatriz(matriz);
    }

    
    public void aumentarObjetoOrigem(){
      Transformacao4D aux = new();
      aux.AtribuirEscala(1.1,1.1,1.1);
      matriz = aux.MultiplicarMatriz(matriz);
    }
    public void diminuiObjetoOrigem(){
      Transformacao4D aux = new();
      aux.AtribuirEscala(0.9,0.9,0.9);
      matriz = aux.MultiplicarMatriz(matriz);
    }
    public void aumentarObjeto(){
      Transformacao4D aux = new();
      Ponto4D centro = bBox.obterCentro;
      aux.AtribuirTranslacao(-centro.X,-centro.Y,centro.Z);
      matriz = aux.MultiplicarMatriz(matriz);
      aux.AtribuirIdentidade();
      aumentarObjetoOrigem();
      aux.AtribuirTranslacao(centro.X,centro.Y,centro.Z);
      matriz = aux.MultiplicarMatriz(matriz);
    }
    public void diminuiObjeto(){
      Transformacao4D aux = new();
      Ponto4D centro = bBox.obterCentro;
      aux.AtribuirTranslacao(-centro.X,-centro.Y,centro.Z);
      matriz = aux.MultiplicarMatriz(matriz);
      aux.AtribuirIdentidade();
      diminuiObjetoOrigem();
      aux.AtribuirTranslacao(centro.X,centro.Y,centro.Z);
      matriz = aux.MultiplicarMatriz(matriz);
      
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