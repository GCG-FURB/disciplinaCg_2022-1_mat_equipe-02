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
        private Poligono objetoSelecionado = null;
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
            camera.Eye = new(300,300,0);camera.Far = 200;camera.Near = 600;


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
            }
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
            else if (e.Key == Key.Number3)
            {
                if (objetoSelecionado != null)
                {
                    objetoSelecionado.rotacionarEixoBBox(4);
                }
            }
            else if (e.Key == Key.Number4)
            {
                if (objetoSelecionado != null)
                {
                    objetoSelecionado.rotacionarEixoBBox(-4);
                }
            }
            else if (e.Key == Key.Right)
            {
                if (objetoSelecionado != null)
                {
                    objetoSelecionado.translacaoPoligonoX(true);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (objetoSelecionado != null)
                {
                    objetoSelecionado.translacaoPoligonoX(false);
                }
            }
            else if (e.Key == Key.P)
            {
                if (objetoSelecionado != null)
                {
                    objetoSelecionado.imprimePontos(adicionar);
                }
            }
            else if (e.Key == Key.R)
            {
                if (objetoSelecionado != null)
                {
                    objetoSelecionado.ObjetoCor.CorR = 255;
                    objetoSelecionado.ObjetoCor.CorG = 0;
                    objetoSelecionado.ObjetoCor.CorB = 0;
                }
            }
            else if (e.Key == Key.G)
            {
                if (objetoSelecionado != null)
                {
                    objetoSelecionado.ObjetoCor.CorR = 0;
                    objetoSelecionado.ObjetoCor.CorG = 255;
                    objetoSelecionado.ObjetoCor.CorB = 0;
                }
            }
            else if (e.Key == Key.B)
            {
                if (objetoSelecionado != null)
                {
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
            Poligono poligono = new(objetoId, objetoSelecionado, novoObjeto);
            if (objetoSelecionado == null)
                objetosLista.Add(poligono);
            objetoSelecionado = poligono;
        }

        private void adicionarPontoPoligono()
        {
            Ponto4D aurelio = new(mouseX, mouseY);
            objetoSelecionado.adicionarPontoPegaUltimo(aurelio);
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

        public void selecionarObjeto(Poligono obj)
        {
            if (obj != null)
            {
                objetoSelecionado = obj;
            }
        }
        
        public void addCustomKey(Key key, Action<Mundo> callback)
        {
        this.customKeys.Add(key, callback);
        }

        public void verificaColisao(Tiro tiro) {
            foreach(Poligono pol in objetosLista)
            {
                if(pol.GetType().Equals(typeof(Asteroide)))
                {
                    Asteroide asteroide = (Asteroide) pol;
                    if(pol.foiSelecionado(tiro.matriz.MultiplicarPonto(tiro.getPosicao()).X, tiro.getPosicao().Y)) {
                        asteroide.matar();
                        objetosLista.Remove(tiro);
                        tiro = null;
                        break;
                    }
                }
            }
        }            
        
}
}