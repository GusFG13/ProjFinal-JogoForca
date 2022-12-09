using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoForca4.Utils.Graficos
{
    internal class MGraficos
    {
        // função que imprime a tela do jogo para o usuário
        // recebe como parâmetros o total de erros, a palavra oculta (com as letras encontradas), a dica/categoria, e a string de letras usadas
        public static void ExibirTelaJogo(int erros, string pOculta, string textoDica, string usadas)
        {
            Console.Clear();
            Console.WriteLine("  +---+   ");
            Console.WriteLine("  |   |   ");
            // desenha a forca de acordo com o número de erros
            if (erros > 0)
            {
                Console.WriteLine("  |   O   ");
                if (erros > 1)
                {
                    if (erros == 2)
                    {
                        Console.WriteLine("  |   |   ");
                    }
                    else if (erros == 3)
                    {
                        Console.WriteLine("  |  /|   ");
                    }
                    else
                    {
                        Console.WriteLine("  |  /|\\   ");
                    }
                    if (erros > 4)
                    {
                        if (erros == 5)
                        {
                            Console.WriteLine("  |  /    ");
                        }
                        else
                        {
                            Console.WriteLine("  |  / \\  ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  |       ");
                    }
                }
                else
                {
                    Console.WriteLine("  |       ");
                    Console.WriteLine("  |       ");
                }
            }
            else
            {
                Console.WriteLine("  |       ");
                Console.WriteLine("  |       ");
                Console.WriteLine("  |       ");
            }
            Console.WriteLine("  |       ");
            Console.WriteLine(" =========");
            Console.WriteLine($"\n {pOculta}"); // exibe a palavra oculta com as letra já 
            Console.WriteLine($"\n Total de erros: {(erros == 5 ? $"{erros} - Última chance!!!" : erros)}"); // usa o operador ternário para incluir aviso ao jogador caso esteja na última chance
            Console.WriteLine($" Letras usadas: {usadas.ToUpper()}");
            Console.WriteLine($"\n A dica da palavra-chave é: {textoDica}");
        }//fim ExibirTelaJogo

        // função que exibe uma vinheta simples, na abertura do jogo e em caso de vitória ou derrota
        // Desenho adaptado de https://ascii.co.uk/art/hangman. Acesso em 05/12/2022
        // Texto para arte ascii gerado em http://patorjk.com/software/taag/#p=display&f=Big&t=Type%20Something%20. Acesso em 03/12/2022
        public static void ExibirAnimacao(string tipo)
        {
            string[] arrTextoAnimacao;
            int comp_animacao;
            int tempo = 150;

            switch (tipo)
            {
                case "inicio":
                    arrTextoAnimacao = new string[] { "  ______ ", " |  ____|", " | |__ ___  _ __ ___ __ _", " |  __/ _ \\| '__/ __/ _` |", " | | | (_) | | | (_| (_| |", " |_|  \\___/|_|  \\___\\__,_|" };
                    break;
                case "acertou":
                    arrTextoAnimacao = new string[] { "                       _                _   _   _ ", "    /\\                | |              | | | | | |", "   /  \\   ___ ___ _ __| |_ ___  _   _  | | | | | |", "  / /\\ \\ / __/ _ \\ '__| __/ _ \\| | | | | | | | | |", " / ____ \\ (_|  __/ |  | || (_) | |_| | |_| |_| |_|", "/_/    \\_\\___\\___|_|   \\__\\___/ \\__,_| (_) (_) (_)" };
                    break;
                case "perdeu":
                    //arrTextoAnimacao = new string[] { "  _____             _              _   _   _ ", " |  __ \\           | |            | | | | | |", " | |__) |__ _ __ __| | ___ _   _  | | | | | |", " |  ___/ _ \\ '__/ _` |/ _ \\ | | | | | | | | |", " | |  |  __/ | | (_| |  __/ |_| | |_| |_| |_|", " |_|   \\___|_|  \\__,_|\\___|\\__,_| (_) (_) (_) " };
                    arrTextoAnimacao = new string[] { "  ___________.._______", " | .__________))______|", " | | / /      ||", " | |/ /       ||", " | | /        ||.-''.", " | |/         |/  _  \\", " | |          ||  `/,|", " | |          (\\\\`_.'", " | |         .-`--'.", " | |        /Y     Y\\", " | |       // |   | \\\\", " | |      //  |   |  \\\\", " | |     ')   |   |   (`", " | |          ||'||", " | |          || ||", " | |          || ||", " | |          || ||", " | |         / | | \\", " ''''''''''|_`-' `-' |'''|", " |'|'''''''\\ \\       ''|'|", " | |        \\ \\        | |", " : :         \\ \\       : :", " . .          `'       . ." };
                    tempo = 100;
                    break;
                default:
                    arrTextoAnimacao = new string[] { "###", "###", "###" };
                    break;
            }

            comp_animacao = arrTextoAnimacao.Length; // guarda quantas linhas tem o texto

            Console.Clear();

            for (int i = (comp_animacao - 1); i >= 0; i--) // primeira iteração exibe apena a linha mais abaixo, cada nova iteração exibe uma linha a mais
            {
                for (int j = i; j < comp_animacao; j++)
                {
                    Console.WriteLine(" " + arrTextoAnimacao[j]);
                }
                if (i > 0) // aguarda antes de limpar a tela para nova iteração
                {
                    Thread.Sleep(tempo); //milissegundos
                }
                else // última iteração, texto exibido completamente
                {
                    Console.WriteLine("\n\n Pressione qualquer tecla para continuar...");
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }//fim void ExibirAnimacao

        public static void ExibirMensagem(string msg)
        {
            // para mensagem inicial
            string auxMoldura = new string('-', (msg.Length - 2));
            auxMoldura = "+" + auxMoldura + "+";
            Console.WriteLine($"\n\n {auxMoldura}");
            Console.WriteLine($" {msg}");
            Console.WriteLine($" {auxMoldura}");
        }//fim void ExibirMensagem
        public static void ExibirMensagem(int erros)
        {
            // para mensagem informando que a letra escolhida não existe na palavra
            Console.WriteLine("\n\n ************************************************");
            Console.WriteLine(" ! A letra digitada não existe na palavra-chave !");
            Console.WriteLine($" ! Você errou {erros} {(erros > 1 ? "vezes!" : "vez!  ")}                          !");
            Console.WriteLine(" ************************************************");
        }//fim void ExibirMensagem
        public static void ExibirMensagem(string msg, char c)
        {
            // para revelar palavra quando jogador perdeu a partida
            string auxMoldura = new string(c, msg.Length);
            Console.WriteLine($"\n\n {auxMoldura}");
            Console.WriteLine($" {msg}");
            Console.WriteLine($" {auxMoldura}");
        }//fim void ExibirMensagem
        public static void ExibirMensagem(string tipo, bool fixa)
        {
            //para mensagens fixas
            switch (tipo)
            {
                case "parabens":
                    Console.WriteLine($"\n\n *******************************************");
                    Console.WriteLine($" * PARABÉNS!!! Você adivinhou a palavra!!! *");
                    Console.WriteLine($" *******************************************");
                    break;
                case "novamente":
                    Console.WriteLine("\n\n +----------------------------------+");
                    Console.WriteLine(" | Deseja jogar novamente (s ou n)? |");
                    Console.WriteLine(" +----------------------------------+");
                    break;
                case "obrigado":
                    Console.WriteLine("\n\n *************************");
                    Console.WriteLine(" * Obrigado por jogar!!! *");
                    Console.WriteLine(" *************************");
                    break;
                case "invalido":
                    Console.WriteLine("\n\n ****************************************************");
                    Console.WriteLine(" * Digite apenas uma letra entre a e z (sem acento) *");
                    Console.WriteLine(" ****************************************************");
                    break;
                case "usada":
                    Console.WriteLine("\n\n *********************************");
                    Console.WriteLine(" * Letra escolhida já foi usada! *");
                    Console.WriteLine(" *********************************");
                    break;
                default:
                    Console.WriteLine("");
                    break;
            }
        }//fim void ExibirMensagem

        public static void ImprimirEquipe()
        {
            string equipe = "Grupo 8";
            string[] programadores = { "Danielle Rodrigues", "Gustavo Fabiano Gonçalves", "Leandro Aparecido de Paiva" };

            Console.WriteLine($"\n\n\n Criado por: {equipe}");
            foreach (string prog in programadores)
            {
                Thread.Sleep(500); // aguarda por 500 milissegundos
                Console.WriteLine($"  - {prog}");
            }
            Thread.Sleep(500);
        }
    }
}
