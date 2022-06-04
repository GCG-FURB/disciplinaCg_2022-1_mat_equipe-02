using System;

namespace CG_Biblioteca
{
  /// <summary>
  /// Classe com funções matemáticas.
  /// </summary>
  public abstract class Matematica
  {
    /// <summary>
    /// Função para calcular um ponto sobre o perímetro de um círculo informando um ângulo e raio.
    /// </summary>
    /// <param name="angulo"></param>
    /// <param name="raio"></param>
    /// <returns></returns>
    public static Ponto4D GerarPtosCirculo(double angulo, double raio)
    {
      Ponto4D pto = new Ponto4D();
      pto.X = (raio * Math.Cos(Math.PI * angulo / 180.0));
      pto.Y = (raio * Math.Sin(Math.PI * angulo / 180.0));
      pto.Z = 0;
      return (pto);
    }

    public static double GerarPtosCirculoSimetrico(double raio)
    {
      return (raio * Math.Cos(Math.PI * 45 / 180.0));
    }
    
    public static double Calcula_ti(double yi,double y1,double y2){
      return ((yi-y1)/(y2-y1));
    }

    public static int Calcula(double x, double y, Ponto4D pt1, Ponto4D pt2){
      if(pt1.Y != pt2.Y){
        double ti = Calcula_ti(y,pt1.Y,pt2.Y);
        if(ti <= 1 && ti >= 0){
          double xi = pt1.X + (pt2.X -pt1.X)*ti;
          if(x != xi){
            if(xi > x && y > min(pt1.Y,pt2.Y) && y <= max(pt1.Y,pt2.Y)){
              return 1;
            }
          }
        }
      }
      return 0;
    }
    public static double max(double a, double b){
      if(a>b){
        return a;
      }
      if(b>a){
        return b;
      }
      return 0;
    }

    public static double min(double a, double b){
      if(a<b){
        return a;
      }
      if(b<a){
        return b;
      }
      return 0;
    }
  }
}