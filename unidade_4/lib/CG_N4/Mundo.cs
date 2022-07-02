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
        private Poligono nave = null;
        private char objetoId = '@';
        private bool bBoxDesenhar = false;
        private IDictionary<Key, Action<Mundo>> customKeys = new Dictionary<Key, Action<Mundo>>();

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
            if (bBoxDesenhar && (nave != null))
                nave.BBox.Desenhar();
            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.H)
                Utilitario.AjudaTeclado();
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
                    nave.translacaoPoligonoX(true);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (nave != null)
                {
                    nave.translacaoPoligonoX(false);
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

        protected void criarPoligonoNaTela()
        {
            Ponto4D novoObjeto = new(mouseX, mouseY);
            objetoId = Utilitario.charProximo(objetoId);
            Poligono poligono = new(objetoId, nave, novoObjeto);
            if (nave == null)
                objetosLista.Add(poligono);
            nave = poligono;
        }

        private void adicionarPontoPoligono()
        {
            Ponto4D aurelio = new(mouseX, mouseY);
            nave.adicionarPontoPegaUltimo(aurelio);
        }

        public void addObjetoNaLista(Poligono obj)
        {
            this.objetosLista.Add(obj);
        }

        public void removeAsteroide(Poligono asteroide)
        {
            if (asteroide != null)
            {
                objetosLista.Remove(asteroide);
            }
        }

        public void selecionarObjeto(Poligono obj)
        {
            if (obj != null)
            {
                nave = obj;
            }
        }
        
        public void addCustomKey(Key key, Action<Mundo> callback)
        {
        this.customKeys.Add(key, callback);
        }
    }
}
