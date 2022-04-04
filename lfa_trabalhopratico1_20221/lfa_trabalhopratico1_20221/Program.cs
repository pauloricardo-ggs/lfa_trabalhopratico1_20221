using System;
using System.IO;

namespace lfa_trabalhopratico1_20221
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var caminho = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var linhas = File.ReadAllLines($@"{caminho}\automato.txt");

            var automato = new Automato(linhas);

            Console.WriteLine($"O autômato é um {automato.DeterminarTipo()}\n");
            automato.Imprimir();
            automato.ConverterNFAParaDFA();

            Console.Read();
        }
    }
}
