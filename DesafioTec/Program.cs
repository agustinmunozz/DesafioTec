using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioTec
{
    public class Program
    {
        static void Main(string[] args)
        {
            var opcion = String.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine(@"Seleccione que metodo Ejecutar: " + Environment.NewLine + 
                                   " 1 - Simplificar " + Environment.NewLine +  " 2 - Validar Nombres" +
                                   Environment.NewLine + " 3 - Salir ");
                opcion = Console.ReadLine();
                if (opcion == "1" || opcion == "2")
                {
                    Console.WriteLine("Ingrese un parametro: ");
                    var parametro = Console.ReadLine();
                    var result = String.Empty;
                    if (opcion == "1")
                        result = Simplificar(parametro);
                    else if (opcion == "2")
                        result = ValidaName(parametro).ToString();

                    Console.WriteLine("El resultado es: " + result);
                    Console.ReadKey();
                }
            } while (opcion != "3");
        }

        public static string Simplificar(string fraccion)
        {
            try
            {
                if (String.IsNullOrEmpty(fraccion))
                    return "Debe ingresar un parametro";
                else if (!fraccion.Contains('/'))
                    return "El parametro ingresado no es una fracción";
                else
                {
                    var result = fraccion;
                    var lst = fraccion.Split("/");
                    var numerador = Convert.ToInt64(lst[0]);
                    var denominador = Convert.ToInt64(lst[1]);
                    
                    if (numerador == 0)
                        return "0";
                    if (denominador == 0)
                        return fraccion;
                    if (numerador == denominador)
                        return "1";
                    if (numerador % denominador == 0)
                        return (numerador / denominador).ToString();
                    var mdNumerador = obtenerDivisores(numerador);
                    var mdDenominador = obtenerDivisores(denominador);
                    var divisoresComunes = mdNumerador.Where(x => mdDenominador.Contains(x)).OrderByDescending(x => x).ToList();
                    if (divisoresComunes.Count > 0)
                        result = (numerador / Convert.ToInt64(divisoresComunes[0])).ToString() + "/" + (denominador / Convert.ToInt64(divisoresComunes[0])).ToString();

                    return result;
                }
            }   
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<long> obtenerDivisores(long valor)
        {
            try
            {
                var divisores = new List<long>();
                if (valor < 0)
                    valor *= -1;
                for (long i = valor; i != 0; i--)
                {
                    if (valor % i == 0)
                        divisores.Add(i);
                }

                return divisores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidaName(string parametro)
        {
            var result = true;
            if (String.IsNullOrEmpty(parametro))
                return false;
            var nombres = parametro.Split(" ");
            if (nombres.Count() == 2 || nombres.Count() == 3)
            {
                var usaInicial = false;
                for (int i = nombres.Count() -1; i >= 0; i--)
                {
                    //Valido que la primera letra esté en mayuscula
                    if (nombres[i][0].ToString() != nombres[i][0].ToString().ToUpper())
                        result = false;

                    //Valido que el apellido no sea una inicial
                    if (i == nombres.Count()-1) 
                    { 
                        if(nombres[i].Contains("."))
                            result = false;
                    }
                    else
                    {
                        if (nombres[i].Contains("."))
                        {
                            if (i == 0 && !usaInicial && nombres.Count() > 2)
                                result = false;

                            usaInicial = true;
                            if (nombres[i].IndexOf(".") != 1)
                                result = false;
                        }
                        else if (nombres[i].Length == 1)
                            result = false;
                    }
                }
            }
            else
                return false;

            return result;
        }
    }
}
