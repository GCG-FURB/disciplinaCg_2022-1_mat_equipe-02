/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Gizmo
// #define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace CG_N2
{
  public class Mundo : GameWindow
  {
    private static Mundo instanciaMundo = null;

    private IDictionary<Key, Action> customKeys = new Dictionary<Key, Action>();

    private Mundo(int width, int height, List<Objeto> objs) : base(width, height) {
      this.objetosLista = objs;
     }

    private Mundo(int width, int height) : base(width, height) {
     }

    public static Mundo GetInstance(int width, int height, List<Objeto> objs)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height, objs);
      return instanciaMundo;
    }
    public static Mundo GetInstance(int width, int height)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height);
      return instanciaMundo;
    }

    private CameraOrtho camera = new CameraOrtho();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private ObjetoGeometria objetoSelecionado = null;
    private char objetoId = '@';
    private bool bBoxDesenhar = false;

    private Circulo circuloPequeno;
    private Circulo circuloGrande;

    private Retangulo retanguloBox;

    private float[] backgroundColors = new float[4];
    private bool backgroundAlterado;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    int varX,varY; //deslocamento do mouse
    private bool mouseMoverPto = false;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

public void setCameraPosition(double xMin, double yMin, double xMax, double yMax)
{
  camera.xmin = xMin; camera.xmax = xMax; camera.ymin = yMin; camera.ymax = yMax;
}
public void addCustomKey(Key key, Action callback)
{
  this.customKeys.Add(key, callback);
}

public void zoomIn() {
  camera.ZoomIn();
}

public void zoomOut() {
  camera.ZoomOut();
}

public void panBaixo() {
  camera.PanBaixo();
}

public void panCima() {
  camera.PanCima();
}

public void panDireita() {
  camera.PanDireita();
}

public void panEsquerda() {
  camera.PanEsquerda();
}

public void setBackgroundColor(float red, float green, float blue, float alpha) {
      backgroundColors[0] = red;
      backgroundColors[1] = green;
      backgroundColors[2] = blue;
      backgroundColors[3] = alpha;
      backgroundAlterado = true;
}
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);  

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");

#if CG_Privado
      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta = new Privado_SegReta(objetoId, null, new Ponto4D(50, 150), new Ponto4D(150, 250));
      obj_SegReta.ObjetoCor.CorR = 255; obj_SegReta.ObjetoCor.CorG = 255; obj_SegReta.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_SegReta);
      objetoSelecionado = obj_SegReta;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo = new Privado_Circulo(objetoId, null, new Ponto4D(100, 300), 50);
      obj_Circulo.ObjetoCor.CorR = 0; obj_Circulo.ObjetoCor.CorG = 255; obj_Circulo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Circulo);
      objetoSelecionado = obj_Circulo;
#endif
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
      
#if CG_Gizmo      
      Sru3D();
#endif
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
      else if (e.Key == Key.V)
        mouseMoverPto = !mouseMoverPto;   //TODO: falta atualizar a BBox do objeto
      else if (e.Key == Key.J)
      {
        circuloPequeno.atualizacentro(varX,varY,circuloGrande,retanguloBox);
        varX = 0;
        varY = 0;
      }
      else if (e.Key == Key.U)
        Console.WriteLine("Dolinho");
      else if (customKeys.Count > 0) {
          if(customKeys.ContainsKey(e.Key))
              customKeys[e.Key]();
        }
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    //TODO: não está considerando o NDC
     protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      int mouseXatual = e.Position.X; int mouseYatual = 600 - e.Position.Y; // Inverti eixo Y
      varX = mouseXatual - mouseX;
      varY = mouseYatual - mouseY;
      mouseX = mouseXatual;
      mouseY = mouseYatual;
    }
    public void Objetos7(Circulo pequeno,Circulo grande,Retangulo ret){
      this.circuloPequeno = pequeno;
      this.circuloGrande = grande;
      this.retanguloBox = ret;
    }

    protected override void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs e){
      if(e.Key == Key.J){
        circuloPequeno.reset();
      }
    }

#if CG_Gizmo
    private void Sru3D()
    {
      GL.LineWidth(1);
      if (backgroundAlterado) {
        GL.ClearColor(backgroundColors[0], backgroundColors[1], backgroundColors[2], backgroundColors[3]);
        GL.Clear(ClearBufferMask.ColorBufferBit);
      }
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
    }
#endif    
  }
}
