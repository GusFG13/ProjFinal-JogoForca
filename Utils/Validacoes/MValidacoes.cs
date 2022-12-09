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
                        MGraficos.ExibirMensagem("usada", true);
                        Console.WriteLine($"\n Pressione qualquer tecla para continuar...");
                        Console.ReadKey(true);
                        return false;
                    }
                }
                else // caso caractere digitado não esteja entre a e z
                {
                    Console.Clear();
                    MGraficos.ExibirMensagem("invalido", true);
                    Console.WriteLine($"\n Pressione qualquer tecla para continuar...");
                    Console.ReadKey(true);
                    return false;
                }
            }
        }//fim EhLetraValida

        public static bool VerificarSeLetraExiste(string letra, string palavra)
        {
            if (palavra.IndexOf(letra) == -1) //letra não existe
            {
                return false;
            }
            return true;
        } //fim VerificarSeLetraExiste

        public static bool VerificarSePerdeu(int totalErros)
        {
            if (totalErros == 6)
            {
                return true; //perdeu
            }
            return false;
        }//fim VerificarSePerdeu

        public static bool VerificarSeGanhou(string palavraChaveOculta)
        {
            if (palavraChaveOculta.IndexOf("_") == -1) //caso não encontre mais underlines em palavraOculta, jogador adivinhou palavra
            {
                return true; //ganhou
            }
            return false;
        }//fim VerificarSeGanhou
    }
}
