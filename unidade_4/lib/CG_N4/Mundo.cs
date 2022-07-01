#define CG_OpenGL // render OpenGL.
using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;


namespace CG_N4
{
  public class Mundo : GameWindow
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
    internal List<Poligono> objetosLista = new List<Poligono>();
    private Poligono objetoSelecionado = null;
    private char objetoId = '@';
    private bool bBoxDesenhar = false;
    int mouseX, mouseY;
    private bool mouseMoverPto = false;
    private bool adicionar = false;
    private bool ehDesenhoJaIniciado = false;
    private bool moverAtivo = false;
    private bool removerAtivo = false;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      camera.xmin = 0; camera.xmax = 600; camera.ymin = 0; camera.ymax = 600;

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
        if(objetoSelecionado != null){
          objetoSelecionado.alternaPrimitiva();
        }
      }else if(e.Key == Key.PageDown){
        if(objetoSelecionado != null){
          objetoSelecionado.aumentarObjetoOrigem();
        }
      }else if(e.Key == Key.PageUp){
        if(objetoSelecionado != null){
          objetoSelecionado.diminuiObjetoOrigem();
        }
      }else if(e.Key == Key.Home){
        if(objetoSelecionado != null){
          objetoSelecionado.diminuiObjeto();
        }
      }else if(e.Key == Key.Number1){
        if(objetoSelecionado != null){
          objetoSelecionado.Rotacao(4);
        }
      }else if(e.Key == Key.Number2){
        if(objetoSelecionado != null){
          objetoSelecionado.Rotacao(-4);
        }
      }else if(e.Key == Key.Number3){
        if(objetoSelecionado != null){
          objetoSelecionado.rotacionarEixoBBox(4);
        }
      }else if(e.Key == Key.Number4){
        if(objetoSelecionado != null){
          objetoSelecionado.rotacionarEixoBBox(-4);
        }
      }else if(e.Key == Key.End){
        if(objetoSelecionado != null){
          objetoSelecionado.aumentarObjeto();
        }
      }else if(e.Key == Key.K){
        if(objetoSelecionado != null){
          objetoSelecionado.toOrigem();
        }
      }else if(e.Key == Key.I){
        if(objetoSelecionado != null){
          objetoSelecionado.Identidade();
        }
      }else if(e.Key == Key.M){
        if(objetoSelecionado != null){
          objetoSelecionado.mostraMatriz();
        }
      }
      else if (e.Key == Key.Right) {
        if(objetoSelecionado != null){
          objetoSelecionado.translacaoPoligonoX(true);
        }
      }
      else if (e.Key == Key.Left) {
        if(objetoSelecionado != null){
          objetoSelecionado.translacaoPoligonoX(false);
        }
      }
      else if (e.Key == Key.Up) {
        if(objetoSelecionado != null){
          objetoSelecionado.translacaoPoligonoY(true);
        }
      }
      else if (e.Key == Key.Down) {
        if(objetoSelecionado != null){
          objetoSelecionado.translacaoPoligonoY(false);
        }
      }

      else if (e.Key == Key.C){
        if(objetoSelecionado != null){
          objetosLista.Remove(objetoSelecionado);
          objetoSelecionado = null;
        }
      }
      else if (e.Key == Key.P){
        if(objetoSelecionado != null){
          objetoSelecionado.imprimePontos(adicionar);
        }
      }
      else if (e.Key == Key.Space){
        if(adicionar){
          adicionar = false;
        }else{
          adicionar = true;
        }
      }
      else if (e.Key == Key.D){
       //remove o ponto mais proximo do mouse do poligono selecionado
       if(!adicionar){
        if(objetoSelecionado != null){
          if(!moverAtivo){
            objetoSelecionado.removerMaisProximo(mouseX,mouseY);
          }
        }
       }
      }
      else if (e.Key == Key.V){
       //move o ponto mais proximo do mouse do poligono selecionado
       if(!adicionar){
        if(objetoSelecionado != null){
          if(moverAtivo){
              objetoSelecionado.pegaUltimo();
              objetoSelecionado.trocaModificado();
              objetoSelecionado.fimMover();
              moverAtivo = false;
          }else{
            objetoSelecionado.selecionaMaisProcimo(mouseX,mouseY);
            objetoSelecionado.trocaModificado();
            moverAtivo = true; 
          }
        }
       }
      }
      else if (e.Key == Key.A){
        objetoSelecionado = null;
        bool a = false;
        for (var i = 0; i < objetosLista.Count; i++){
          a = objetosLista[i].foiSelecionado(mouseX,mouseY);
          if(a){
            objetoSelecionado = objetosLista[i];
          }
        }
        
      }
      else if (e.Key == Key.R){
        if (objetoSelecionado != null) {
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
      mouseX = e.Position.X; mouseY = 600 - e.Position.Y;
      if(objetoSelecionado != null)
      {
          objetoSelecionado.atualizaPontoSelecionado(mouseX,mouseY);
      }
    }
     protected override void OnMouseDown(MouseButtonEventArgs e)
    {
       if(adicionar){
        if(e.Button == MouseButton.Left){
          if(!ehDesenhoJaIniciado){
            criarPoligonoNaTela();
            ehDesenhoJaIniciado = true;
          }
          else{
            adicionarPontoPoligono();
          }
        }
        if(e.Button == MouseButton.Right){
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
    }

    protected void criarPoligonoNaTela(){
      Ponto4D novoObjeto = new(mouseX, mouseY);
      objetoId = Utilitario.charProximo(objetoId);
      Poligono poligono = new(objetoId, objetoSelecionado, novoObjeto);
      if(objetoSelecionado == null)
        objetosLista.Add(poligono);
      objetoSelecionado = poligono;
    }

    private void adicionarPontoPoligono(){
      Ponto4D aurelio = new(mouseX,mouseY);
      objetoSelecionado.adicionarPontoPegaUltimo(aurelio);
    }
  
  }
  class Program
  {
    static void Main(string[] args)
    {
      ToolkitOptions.Default.EnableHighResolution = false;
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N4";
      window.Run(1.0 / 60.0);
    }
  }
}
