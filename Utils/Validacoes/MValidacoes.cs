using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoForca4.Utils.Validacoes
{
    using Utils.Graficos;
    internal class MValidacoes
    {
        // função para validar letra informada
        public static bool EhLetraValida(string verLetra, string usadas)
        {
            if (verLetra.Length != 1) // testa se informaou apenas uma letra
            {
                Console.Clear();
                //Console.WriteLine("\n\n ****************************************************");
                //Console.WriteLine(" * Digite apenas uma letra entre a e z (sem acento) *");
                //Console.WriteLine(" ****************************************************");
                MGraficos.ExibirMensagem("invalido", true);
                Console.WriteLine($"\n Pressione qualquer tecla para continuar...");
                Console.ReadKey(true);
                return false;
            }
            else
            {
                char.TryParse(verLetra, out char auxChar); // converte string para caractere para fazer comparação abaixo (string "a" -> char 'a')
                if (auxChar >= 'a' && auxChar <= 'z') // verifica se letra está entre a e z
                {
                    if (usadas.IndexOf(verLetra) == -1) // procura letras na string letrasUsadas
                    {
                        return true; //caso não encontre, letra é válida
                    }
                    else
                    {
                        Console.Clear();
                        //Console.WriteLine("\n\n *********************************");
                        //Console.WriteLine(" * Letra escolhida já foi usada! *");
                        //Console.WriteLine(" *********************************");
                        MGraficos.ExibirMensagem("usada", true);
                        Console.WriteLine($"\n Pressione qualquer tecla para continuar...");
                        Console.ReadKey(true);
                        return false;
                    }
                }
                else // caso caractere digitado não esteja entre a e z
                {
                    Console.Clear();
                    //Console.WriteLine("\n\n ****************************************************");
                    //Console.WriteLine(" * Digite apenas uma letra entre a e z (sem acento) *");
                    //Console.WriteLine(" ****************************************************");
                    MGraficos.ExibirMensagem("invalido", true);
                    Console.WriteLine($"\n Pressione qualquer tecla para continuar...");
                    Console.ReadKey(true);
                    return false;
                }
            }
        }//fim EhLetraValida
    }
}
