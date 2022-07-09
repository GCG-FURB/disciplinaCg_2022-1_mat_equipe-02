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

        private CameraPerspective camera = new CameraPerspective();
        internal List<Poligono> objetosLista = new List<Poligono>();
        private Nave nave = null;
        private char objetoId = '@';
        private bool bBoxDesenhar = false;
        private IDictionary<Key, Action<Mundo>> customKeys = new Dictionary<Key, Action<Mundo>>();
        private bool adicionar = false;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);

            camera.Eye = new(300,300,740);
            camera.At = new Vector3(300, 300, 0);
            camera.Far = 1000; 
            camera.Near = 0.1f;
            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");
            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            camera.Fovy = (float)Math.PI / 4;
            camera.Aspect = Width / (float)Height;

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(camera.Fovy, camera.Aspect, camera.Near, camera.Far);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(camera.Eye, camera.At, camera.Up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            Sru3D();
            for (var i = 0; i < objetosLista.Count; i++) {
                objetosLista[i].Desenhar();
                if(objetosLista[i].GetType().Equals(typeof(Tiro)))
                    verificaColisao((Tiro) objetosLista[i]);
                else {
                    verificaColisao((Asteroide) objetosLista[i]);
                }
            }
            nave.Desenhar();
            if (bBoxDesenhar && (nave != null))
                nave.BBox.Desenhar();
            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.H)
                Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape)
                Exit();
            else if (e.Key == Key.Number3)
            {
                if (nave != null)
                {
                    nave.rotacionarEixoBBox(4);
                }
            }
            else if (e.Key == Key.Number4)
            {
                if (nave != null)
                {
                    nave.rotacionarEixoBBox(-4);
                }
            }
            else if (e.Key == Key.Right)
            {
                if (nave != null)
                {
                    nave.mover(3);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (nave != null)
                {
                    nave.mover(-3);
                }
            }
            else if (e.Key == Key.P)
            {
                if (nave != null)
                {
                    nave.imprimePontos(adicionar);
                }
            }
            else if (e.Key == Key.R)
            {
                if (nave != null)
                {
                    nave.ObjetoCor.CorR = 255;
                    nave.ObjetoCor.CorG = 0;
                    nave.ObjetoCor.CorB = 0;
                }
            }
            else if (e.Key == Key.G)
            {
                if (nave != null)
                {
                    nave.ObjetoCor.CorR = 0;
                    nave.ObjetoCor.CorG = 255;
                    nave.ObjetoCor.CorB = 0;
                }
            }
            else if (e.Key == Key.B)
            {
                if (nave != null)
                {
                    nave.ObjetoCor.CorR = 0;
                    nave.ObjetoCor.CorG = 0;
                    nave.ObjetoCor.CorB = 255;
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
            else if (customKeys.Count > 0) {
                if(customKeys.ContainsKey(e.Key))
                    customKeys[e.Key](this);
            }
            else
                Console.WriteLine(" __ Tecla não implementada.");
        }

        public void addObjetoNaLista(Poligono obj)
        {
            this.objetosLista.Add(obj);
        }

        public List<Poligono> getListaObjetos()
        {
            return this.objetosLista;
        }

        public void removeAsteroide(Poligono asteroide)
        {
            if (asteroide != null)
            {
                objetosLista.Remove(asteroide);
            }
        }

        public void setarNave(Nave naveCandidata)
        {
            if (naveCandidata != null)
            {
                nave = naveCandidata;
            }
        }
        
        public void addCustomKey(Key key, Action<Mundo> callback)
        {
        this.customKeys.Add(key, callback);
        }

        private void gameOver() {
            objetosLista = new List<Poligono>();
        }

        public void verificaColisao(Tiro tiro) {
            foreach(Poligono pol in objetosLista)
            {
                if(pol.GetType().Equals(typeof(Asteroide)))
                {
                    Asteroide asteroide = (Asteroide) pol;
                    if(asteroide.foiSelecionado(tiro.matriz.MultiplicarPonto(tiro.getPosicao()).X, tiro.getPosicao().Y)) {
                        asteroide.matar();
                        objetosLista.Remove(tiro);
                        tiro = null;
                        break;
                    }
                }
            }
        }
        public void verificaTiroFora(Tiro tiro){
            if(tiro.balaPerdida()){
                objetosLista.Remove(tiro);
                tiro = null;
            }
        }
        public void verificaColisao(Asteroide asteroide) {               
            if(asteroide.foiSelecionado(nave.matriz.MultiplicarPonto(nave.getPonto(0)).X, nave.getPonto(0).Y)
            || asteroide.foiSelecionado(nave.matriz.MultiplicarPonto(nave.getPonto(1)).X, nave.getPonto(1).Y)
            || asteroide.foiSelecionado(nave.matriz.MultiplicarPonto(nave.getPonto(2)).X, nave.getPonto(2).Y)) {
                gameOver();
            }
        }
        private void Sru3D()
        {
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            // GL.Color3(1.0f,0.0f,0.0f);
            GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
            GL.Vertex3(0, 0, 0); GL.Vertex3(20, 0, 0);
            // GL.Color3(0.0f,1.0f,0.0f);
            GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 20, 0);
            // GL.Color3(0.0f,0.0f,1.0f);
            GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 20);
            GL.End();
        }
}
}