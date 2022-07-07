/**
  Autor: Dalton Solano dos Reis
**/
/**
  Modificado por Gabriel Reifegerste e Júlio Brych
**/

using System;

namespace CG_N4
{
  public abstract class Utilitario
  {
    public static char charProximo(char atual) {
      return Convert.ToChar(atual + 1);
    }
    public static void AjudaTeclado()
    {
      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra esta ajuda. ");
      Console.WriteLine(" [Escape  ] sair. ");
      Console.WriteLine(" [Left    ] N3-Exe10: move a nave para a esquerda. ");
      Console.WriteLine(" [Right   ] N3-Exe10: move a nave para a direita. ");
      Console.WriteLine(" [Up      ] N3-Exe10: atira. ");
      Console.WriteLine(" [  3     ] N3-Exe12: rotação anti-horária da nave. ");
      Console.WriteLine(" [  4     ] N3-Exe12: rotação horária da nave. ");
      Console.WriteLine(" [  R     ] N3-Exe08: atribui a cor vermelha à nave. ");
      Console.WriteLine(" [  G     ] N3-Exe08: atribui a cor verde à nave. ");
      Console.WriteLine(" [  B     ] N3-Exe08: atribui a cor azul à nave. ");
      Console.WriteLine("  --- ");
    }

  }
}