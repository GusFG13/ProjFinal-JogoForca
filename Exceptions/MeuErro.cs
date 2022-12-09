using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoForca4.Exceptions
{
    internal class MeuErro : Exception
    {
        public MeuErro(string mensagem)
        {
            Console.WriteLine("\n\n #################################################################################");
            Console.WriteLine("\n Parabéns, você achou um erro no código!");
            Console.WriteLine($"\n A mensagem de erro original foi: {mensagem}");
            Console.WriteLine("\n #################################################################################");
            Environment.Exit(0);
        }
    }
}
