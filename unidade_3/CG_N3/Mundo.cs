/**
  Autor: Dalton Solano dos Reis
**/

//#define CG_Privado // código do professor.
#define CG_Gizmo  // debugar gráfico.
//#define CG_Debug // debugar texto.
#define CG_OpenGL // render OpenGL.
//#define CG_DirectX // render DirectX.

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
  class Mundo : GameWindow
  {
    private static Mundo instanciaMundo = null;

    private Mundo(int width, int height) : base(width, height) { }

    public static Mundo GetInstance(int width, int height)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height);
      return instanciaMundo;
    }

    private CameraOrtho camera = new CameraOrtho();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private Poligono objetoSelecionado = null;
    private char objetoId = '@';
    private bool bBoxDesenhar = false;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    private bool mouseMoverPto = false;

    private bool ePrimeiro = true;
    private Retangulo obj_Retangulo;
    private Poligono obj_Poligono;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      camera.xmin = 0; camera.xmax = 750; camera.ymin = 0; camera.ymax = 750;

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");

#if CG_OpenGL
      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
#endif
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
#if CG_OpenGL
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
#endif
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
#if CG_OpenGL
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
#endif
#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
#if CG_Gizmo
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
#endif
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.H)
        Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.S){
        objetoSelecionado.alternaPrimitiva();
      }
      else if (e.Key == Key.S){
        if(objetoSelecionado == null){
          
          //Dolinho
        }
      }
      else if (e.Key == Key.R){
      objetoSelecionado.ObjetoCor.CorR = 255;
      objetoSelecionado.ObjetoCor.CorG = 0;
      objetoSelecionado.ObjetoCor.CorB = 0;
      }
      else if (e.Key == Key.G){
      objetoSelecionado.ObjetoCor.CorR = 0;
      objetoSelecionado.ObjetoCor.CorG = 255;
      objetoSelecionado.ObjetoCor.CorB = 0;
      }
      else if (e.Key == Key.B){
      objetoSelecionado.ObjetoCor.CorR = 0;
      objetoSelecionado.ObjetoCor.CorG = 0;
      objetoSelecionado.ObjetoCor.CorB = 255;
      }
      else if (e.Key == Key.E)
      {
        Console.WriteLine("--- Objetos / Pontos: ");
        for (var i = 0; i < objetosLista.Count; i++)
        {
          Console.WriteLine(objetosLista[i]);
        }
      }
#if CG_Gizmo
      else if (e.Key == Key.O)
        bBoxDesenhar = !bBoxDesenhar;
#endif
      else if (e.Key == Key.V)
        mouseMoverPto = !mouseMoverPto;   //TODO: falta atualizar a BBox do objeto
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 750 - e.Position.Y; // Inverti eixo Y7
      /*Console.Write("X:");
      Console.Write(mouseX);
      Console.Write(" Y:");
      teste
      Console.WriteLine(mouseY);*/
      if(objetoSelecionado != null){
        objetoSelecionado.atualizaUltimoPonto(mouseX,mouseY);
      }
    }
     protected override void OnMouseDown(MouseButtonEventArgs e)
    {
      if(e.Button == MouseButton.Left){
        if(ePrimeiro){
          criarPoligonoNaTela();
          ePrimeiro = false;
        }
        else{
          adicionarPontoPoligono();
        }
      }
      if(e.Button == MouseButton.Right){
        objetoSelecionado.finalizaDesenho();
        objetoSelecionado = null;
        ePrimeiro = true;
      }
    }

    protected void criarPoligonoNaTela(){
      objetoId = Utilitario.charProximo(objetoId);
      Ponto jose = new(objetoId,null,mouseX,mouseY);
      objetoId = Utilitario.charProximo(objetoId);
      Poligono poligono = new(objetoId,null,jose);
      objetosLista.Add(poligono);
      objetoSelecionado = poligono;
    }

    private void adicionarPontoPoligono(){
      objetoId = Utilitario.charProximo(objetoId);
      Ponto aurelio = new(objetoId,null,mouseX,mouseY);
      objetoSelecionado.adicionarPonto(aurelio);
    }
#if CG_Gizmo
    private void Sru3D()
    {
#if CG_OpenGL
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      // GL.Color3(1.0f,0.0f,0.0f);
      GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      // GL.Color3(0.0f,1.0f,0.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      // GL.Color3(0.0f,0.0f,1.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
#endif
    }
#endif    
  }
  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N2";
      window.Run(1.0 / 60.0);
    }
  }
}
