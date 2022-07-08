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
        private Vector3 eye = new(300,300,0);
        private float far = 200, near = 600;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            camera.Eye = eye; camera.Far = far; camera.Near = near;
            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");
            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 600, 0, 600,0, 600);

        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            
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
}
}