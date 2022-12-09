namespace JogoForca4
{
    using Utils.Validacoes;
    using Utils.Graficos;
    using JogoForca4.Exceptions;
    using System.IO;

    internal class Program
    {
        /*   Grupo 8:
         *   - Danielle Rodrigues
         *   - Gustavo Fabiano Gonçalves
         *   - Leandro Aparecido de Paiva
         *   
         *   Enunciado:
         *   Realize a implementação do Jogo da Forca em C#.
         *
         *   O jogo da forca é um jogo em que o jogador tem que acertar qual é a palavra proposta, 
         *   tendo como dica o número de letras e o tema ligado à palavra. A cada letra errada, é 
         *   desenhada uma parte do corpo do enforcado. O jogo termina ou com o acerto da palavra 
         *   ou com o término do preenchimento das partes corpóreas do enforcado.
         *
         *   Durante a execução do programa, o jogador deve escrever uma letra na linha de comando, 
         *   caso a letra exista na palavra-chave o jogo deve imprimir a letra digitada na posição 
         *   correspondente, caso não exista, o jogo deve imprimir em tela a mensagem "A letra digitada 
         *   não existe na palavra-chave", e exibir o total de erros. A classe que encapsula a lógica do 
         *   jogo deve conter métodos para verificar se a letra existe na palavra-chave, se o jogador 
         *   completou a palavra-chave e se a quantidade máxima de tentativas foi atingida.
         *
         *   Nota: Pode-se imprimir em tela o desenho do corpo do enforcado conforme a evolução do jogo 
         *   ou apenas informar a quantidade de tentativas restantes. As palavras-chave devem ser previamente 
         *   armazenadas em um vetor de strings e a cada execução deve ser selecionada uma dessas palavras de 
         *   forma aleatória.
         *
         *   Ao final, disponibilizem aqui mesmo no Class, o link para o repositório onde estará o fonte.
         */
        static void Main(string[] args)
        {
            #region declaração de variáveis
            string equipe = "Grupo 8";
            string[] programadores = { "Danielle Rodrigues", "Gustavo Fabiano Gonçalves", "Leandro Aparecido de Paiva" };

            Random numAleat = new Random(); // gera número aleatório para escolher a palavra na matriz
            bool novaPartida = false; // variável de saída do do-while que controla novas partidas
            bool ganhou; // true quando ganhou, inicializada com false no começo de cada partida
            bool perdeu; // true quando perdeu, inicializada com false no começo de cada partida
            bool empateCompr; // true quando mais de uma palavra correta tem o mesmo comprimento

            int linha; // guarda número aleatório para a linha da matriz (entre 1 e 11, na linha 0 estão as categorias)
            int coluna; // guarda número aleatório para a coluna da matriz (decide a categoria da palavra sorteada)
            int erros; // guarda total de erros numa partida, com 6 o jogador perde. Inicializada no começo de cada partida
            int posLetra; // auxiliar para verificar e substituir uma letra que o jogador acertou na strings da palavra oculta
            int totalJogos = 0; // contador do número de partidas jogadas
            int numVitorias = 0; // contador de palavras descobertas
            int numLetras = 0; // contador do número de letras da palavra-chave

            string dica; // guarda a categoria da palavra-chave, determinada por matrizDePalavras[0,coluna]
            string palavra; // guarda a palavra-chave determinada por matrizDePalavras[linha,coluna]
            string letra; // palpite do jogador
            string letrasUsadas; // guarda todas as letras já usadas na partida
            string palavraOculta; // string composta por "_ " para cada letra da palavra-chave, conforme o jogador acerta um palpite, cada "_" é substituído pela letra informada
            string mensagemInicial; // frase que informa ao jogador qual a dica e o número de letras da palavra-chave, inicializada no começo de cada partida
            string revelaPalavra; // frase que revela a palavra-chave quando o jogador perde a partida, inicializada apenas se o jogador perder alguma vez
            string maiorPalavra = ""; // guarda a maior palavra que o jogador acertou, em caso de empate, fica a última delas
            string resposta; // recebe s ou n para a pergunta se o jogador quer nova partida

            DateTime inicio = DateTime.Now; // guarda hora de início da partida. Método Stopwatch stopwatch = Stopwatch.StartNew(); seria outra forma para marcar tempo
            Dictionary<string, ushort[]> dicPalavrasJogou = new Dictionary<string, ushort[]>();//chave: palavra que acertou; Valor: array com 3 posições {Vezes jogadas, vezes acertou, tamanho palavra}
            const int vezesJogadas = 0; // índice para vetor no dicionário para posição que guarda total de vezes que a palavra foi jogada
            const int vezesAcertou = 1; // índice para vetor no dicionário para posição que guarda total de vezes que jogador acertou aquela palavra
            const int tamanhoPalavra = 2; // índice para vetor no dicionário
            
            //lista de palavras está no arquivo ListaPalavras.csv, que deverá estar na mesma pasta do arquivo .exe
            //o arquivo deve ter a primeira linha com as categorias, separadas por vírgula e nas linhas seguintes, as palavras naquelas categorias.
            //todas as "colunas" do arquivo devem ter o mesmo número de palavras
            string Caminho = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ListaPalavras.csv";

            try {
                FileStream fs = File.Open(Caminho,FileMode.Open);
                fs.Close();
            } catch(Exception ex)
            {
                Console.WriteLine("Houve um erro na leitura do arquivo ListaPalavras.csv\n\n");
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
            string[] LinhasCSV = File.ReadAllLines(Caminho);
            //vetor de vetores com base de dados de dicas e palavras
            //categoria estará na linha índice 0; cada "coluna" terá 10 palavras referentes a cada dica
            string[][] matrizDePalavrasCSV = new string[LinhasCSV.Length][]; //cria um array onde cada elemento também é um array

            #endregion

            for (int i = 0; i < LinhasCSV.Length; i++)
            {
                matrizDePalavrasCSV[i] = LinhasCSV[i].Split(',');
            }
 
            MGraficos.ExibirAnimacao("inicio"); // chama função definida abaixo que exibe uma vinheta de abertura

            // imprime o nome da equipe
            Console.WriteLine($"\n\n\n Criado por: {equipe}");
            foreach (string prog in programadores)
            {
                Thread.Sleep(500); // aguarda por 500 milissegundos
                Console.WriteLine($"  - {prog}");
            }
            Thread.Sleep(500);
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey(true); //aguarda até o jogador pressionar alguma tecla; true indica que a tecla não aparecerá no console

            do //inicio jogo
            {
                #region preparação início partida
                //preparação para nova partida
                ganhou = perdeu = false;
                totalJogos++;
                linha = numAleat.Next(1, matrizDePalavrasCSV.Length);  // gera um número entre 1 e tamanho do primeiro índice da matriz (linha 0 é só dicas)
                coluna = numAleat.Next(0, matrizDePalavrasCSV[linha].Length); // gera um número entre 0 e tamanho do segundo índice da matriz
                /********************************* TESTES ************************************/
                //linha = numAleat.Next(1, 4);  // gera um número entre 1 e tamanho do primeiro índice da matriz (linha 0 é só dicas)
                //coluna = numAleat.Next(0, 2); // gera um número entre 0 e tamanho do segundo índice da matriz
                /*****************************************************************************/
                dica = matrizDePalavrasCSV[0][coluna];
                palavra = matrizDePalavrasCSV[linha][coluna].ToLower();
                letrasUsadas = "";
                palavraOculta = string.Concat(Enumerable.Repeat("_ ", palavra.Length));
                erros = 0;
                Console.Clear();  
                mensagemInicial = $"| A palavra é um(a) {dica} e tem {palavra.Length} letras. |";
                MGraficos.ExibirMensagem(mensagemInicial);
                Console.WriteLine("\n Pressione qualquer tecla para continuar...");
                Console.ReadKey(true);
                #endregion
                do //inicio partida
                {
                    #region lógica do jogo
                    //pedir nova letra para jogador
                    do
                    {
                        MGraficos.ExibirTelaJogo(erros, palavraOculta, dica, letrasUsadas);
                        Console.Write($"\n\n Digite uma letra entre a e z (sem acentos): ");
                        letra = Console.ReadLine().ToLower();
                    } while (!MValidacoes.EhLetraValida(letra, letrasUsadas)); // chama função definida abaixo para validar a letra

                    letrasUsadas = letrasUsadas + letra + " "; // acrescenta nova letra na lista de usadas

                    //procurar letra na palavra-chave
                    posLetra = palavra.IndexOf(letra);
                    if (posLetra == -1) //letra não encontrada na palavra
                    {
                        erros++;
                        Console.Clear();
                        MGraficos.ExibirMensagem(erros);
                        Console.WriteLine($"\n Pressione qualquer tecla para continuar...");
                        Console.ReadKey(true); 
                    }
                    else //letra encontrda
                    {
                        while (posLetra != -1) // substitui todas a ocorrências da letra em palavra-chave nas respectivas posições da variável palavraOculta
                        {
                            /*
                             * substitui o underline na string oculta
                             * multiplica por 2 pois a string tem o dobro do tamanho da palavra original (underline e espaço para cada caracter)
                             * para facilitar a visualização: "teste" seria representado por _ _ _ _ _ 
                            */
                            palavraOculta = palavraOculta.Remove(posLetra * 2, 1).Insert(posLetra * 2, letra);
                            posLetra++;
                            posLetra = palavra.IndexOf(letra, posLetra);
                        }
                    }
                    //verifica se as condições de vitória ou derrota foram atingidas
                    if (erros == 6)
                    {
                        perdeu = true;
                    }
                    if (palavraOculta.IndexOf("_") == -1) //caso não encontre mais underlines em palavraOculta, jogador adivinhou palavra
                    {
                        ganhou = true;
                    }
                    #endregion
                } while (!ganhou && !perdeu); // continua enquanto ganhou e perdeu forem false

                #region verifica vitória ou derrota
                if (ganhou)
                {
                    numVitorias++;
                    if (dicPalavrasJogou.ContainsKey(palavra)){
                        dicPalavrasJogou[palavra][vezesJogadas]++;
                        dicPalavrasJogou[palavra][vezesAcertou]++;
                    }
                    else
                    {
                        dicPalavrasJogou.Add(palavra, new ushort[] { 1, 1, (ushort)palavra.Length });
                    }
                    MGraficos.ExibirAnimacao("acertou");
                }
                else
                {
                    if (dicPalavrasJogou.ContainsKey(palavra))
                    {
                        dicPalavrasJogou[palavra][vezesJogadas]++;
                    }
                    else
                    {
                        dicPalavrasJogou.Add(palavra, new ushort[] { 1, 0, (ushort)palavra.Length });
                    }
                    MGraficos.ExibirAnimacao("perdeu");
                }
                #endregion

                #region nova partida
                //perguntar se jogador que nova partida
                do
                {
                    MGraficos.ExibirTelaJogo(erros, palavraOculta, dica, letrasUsadas);
                    if (ganhou)
                    {
                        MGraficos.ExibirMensagem("parabens", true);
                    }
                    if (perdeu) // revela a palavra-chave, apenas caso jogador não tenha adivinhado
                    {
                        revelaPalavra = $"! A palavra era: {palavra.ToUpper()} !";
                        MGraficos.ExibirMensagem(revelaPalavra, '*');
                    }
                    MGraficos.ExibirMensagem("novamente", true);
                    resposta = Console.ReadLine().ToLower();
                    if (resposta == "s")
                    {
                        novaPartida = true;
                    }
                    else if (resposta == "n")
                    {
                        novaPartida = false;
                    }
                } while (resposta != "s" && resposta != "n");
                #endregion

            } while (novaPartida);

            #region finaliza jogo
            //Exibir estatísticas do jogo
            Console.Clear();
            Thread.Sleep(250);
            Console.WriteLine($"\n\n Você jogou {totalJogos} partida(s) em {(DateTime.Now - inicio).Minutes} minuto(s) e acertou {numVitorias} palavra(s) ({(numVitorias * 100.0d / totalJogos).ToString("0.##")}%).");
            Thread.Sleep(250);
            Console.WriteLine("\n As palavras que você jogou foram (No. vezes):");
            Thread.Sleep(250);
            foreach ((string p, ushort[] stats) in dicPalavrasJogou)
            {
                Console.Write($" {p} ({stats[vezesJogadas]})");
                Thread.Sleep(250);
            }
            Console.Write("\n");
            if (numVitorias > 0)
            {
                Thread.Sleep(250);
                Console.WriteLine("\n As palavras que você acertou (No. vezes):");
                Thread.Sleep(250);
                empateCompr = false; // reinicia para verificação em nova partida
                foreach ((string p, ushort[] stats) in dicPalavrasJogou)
                {
                    if (stats[vezesAcertou] > 0)
                    { 
                        Console.Write($" {p} ({stats[vezesAcertou]})");
                        Thread.Sleep(250);
                        if (stats[tamanhoPalavra] > numLetras)
                        {
                            numLetras = stats[tamanhoPalavra];
                            maiorPalavra = p;
                            empateCompr = false;
                        }
                        else if (stats[tamanhoPalavra] == numLetras)
                        {
                            numLetras = stats[tamanhoPalavra];
                            maiorPalavra = (maiorPalavra + " / " + p);
                            empateCompr = true;
                        }
                    }
                }
                Console.Write("\n");
                if (empateCompr)
                {
                    Console.WriteLine($"\n As maiores palavras que acertou foram {maiorPalavra}, com {numLetras} letras.");
                }
                else
                {
                    Console.WriteLine($"\n A maior palavra que acertou foi {maiorPalavra}, com {numLetras} letras.");
                }
            }
            Thread.Sleep(250);
            try 
            {
                //throw new Exception();
                MGraficos.ExibirMensagem("obrigado", true);
                Console.WriteLine($"\n Pressione qualquer tecla para encerrar...");
                Console.ReadKey(true);
            } 
            catch(Exception ex)
            {
                throw new MeuErro(ex.Message);
                //Console.WriteLine("erro");
            }
            #endregion
        }//Fim Main
    }
}