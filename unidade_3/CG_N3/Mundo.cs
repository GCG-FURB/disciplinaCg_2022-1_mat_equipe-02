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
    int mouseX, mouseY;
    private bool mouseMoverPto = false;

    private bool ehDesenhoJaIniciado = false;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      camera.xmin = 0; camera.xmax = 750; camera.ymin = 0; camera.ymax = 750;

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");


      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);

    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);

    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();

      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.H)
        Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.S) {
        objetoSelecionado.alternaPrimitiva();
      }
      else if (e.Key == Key.A) {
        if(objetoSelecionado != null) {
          bBoxDesenhar = true;
          objetoSelecionado = (Poligono) objetosLista[(objetosLista.IndexOf(objetoSelecionado) + 1 ) % objetosLista.Count];
        } else {
          objetoSelecionado = (Poligono) objetosLista[0];
        }
      }
      else if (e.Key == Key.R) {
        if(objetoSelecionado != null) {
          objetoSelecionado.ObjetoCor.CorR = 255;
          objetoSelecionado.ObjetoCor.CorG = 0;
          objetoSelecionado.ObjetoCor.CorB = 0;
        }
      }
      else if (e.Key == Key.G) {
        if(objetoSelecionado != null) {
          objetoSelecionado.ObjetoCor.CorR = 0;
          objetoSelecionado.ObjetoCor.CorG = 255;
          objetoSelecionado.ObjetoCor.CorB = 0;
        }
      }
      else if (e.Key == Key.B) {
        if(objetoSelecionado != null) {
          objetoSelecionado.ObjetoCor.CorR = 0;
          objetoSelecionado.ObjetoCor.CorG = 0;
          objetoSelecionado.ObjetoCor.CorB = 255;
        }
      }
      else if (e.Key == Key.E)
      {
        Console.WriteLine("--- Objetos / Pontos: ");
        for (var i = 0; i < objetosLista.Count; i++)
        {
          Console.WriteLine(objetosLista[i]);
        }
      }
      else if (e.Key == Key.O) {
        bBoxDesenhar = !bBoxDesenhar;
      }
      else if (e.Key == Key.V)
        mouseMoverPto = !mouseMoverPto;
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 750 - e.Position.Y;
      if(objetoSelecionado != null)
      {
        objetoSelecionado.atualizaUltimoPonto(mouseX,mouseY);
      }
    }
     protected override void OnMouseDown(MouseButtonEventArgs e)
    {
      if(e.Button == MouseButton.Left) {
        if(!ehDesenhoJaIniciado){
          criarPoligonoNaTela();
          ehDesenhoJaIniciado = true;
        }
        else {
          adicionarPontoPoligono();
        }
      }
      if(e.Button == MouseButton.Right) {
        
        if(objetoSelecionado != null) {
          if(objetoSelecionado.lenght() < 3) {
            objetosLista.RemoveAt(objetosLista.IndexOf(objetoSelecionado));
            objetoSelecionado = null;
            ehDesenhoJaIniciado = false;
          } else {
            objetoSelecionado.finalizaDesenho();
            ehDesenhoJaIniciado = false;
          }
        }
      }
    }

    protected void criarPoligonoNaTela(){
      objetoId = Utilitario.charProximo(objetoId);
      Ponto primeiroPonto = new(objetoId,null,mouseX,mouseY);
      objetoId = Utilitario.charProximo(objetoId);
      Poligono poligono = new(objetoId,null,primeiroPonto);
      objetosLista.Add(poligono);
      objetoSelecionado = poligono;
    }

    private void adicionarPontoPoligono(){
      objetoId = Utilitario.charProximo(objetoId);
      Ponto novoPonto = new(objetoId,null,mouseX,mouseY);
      objetoSelecionado.adicionarPonto(novoPonto);
    }
  
  }
  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N3";
      window.Run(1.0 / 60.0);
    }
  }
}
