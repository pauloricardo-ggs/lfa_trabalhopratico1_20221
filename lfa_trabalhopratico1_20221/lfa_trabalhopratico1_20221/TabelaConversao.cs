using System;
using System.Collections.Generic;

namespace lfa_trabalhopratico1_20221
{
    public class TabelaConversao
    {
        public Automato Automato { get; private set; }
        public Automato AutomatoConvertido { get; private set; }
        public string[,][] Tabela { get; private set; }

        public TabelaConversao(Automato automato)
        {
            Automato = automato;
            AutomatoConvertido = new Automato();
            foreach (var alphabet in Automato.Alphabet) { AutomatoConvertido.Alphabet.Add(alphabet); }
            foreach (var initial in Automato.Initial) { AutomatoConvertido.Initial.Add(initial); }
            foreach (var accepting in Automato.Accepting) { AutomatoConvertido.Accepting.Add(accepting); }
            foreach (var transition in Automato.Transitions) { AutomatoConvertido.Transitions.Add(transition); }
            foreach (var state in Automato.States) { AutomatoConvertido.States.Add(state); }
            Tabela = new string[Automato.States.Count, Automato.Alphabet.Count][];
        }

        public void CriarTabela()
        {
            var colunaCount = 0;

            foreach (var alphabet in Automato.Alphabet)
            {
                var linhaCount = 0;
                foreach(var state in Automato.States)
                {
                    for (int i = 0; i < Automato.Transitions.Count; i++)
                    {
                        if (Automato.TransitionsActualState[i] == state && Automato.TransitionsInput[i] == alphabet)
                        {
                            Tabela[linhaCount, colunaCount] = Automato.TransitionsFutureState[i];
                        }
                    }
                    linhaCount++;
                }
                colunaCount++;
            }
        }

        public void CriarEstadosNovos()
        {
            var states = new List<string>();
            for (int coluna = 0; coluna < Automato.Alphabet.Count; coluna++)
            {
                for (int linha = 0; linha < Automato.States.Count; linha++)
                {
                    if(Tabela[linha, coluna] != null && Tabela[linha, coluna].Length > 1)
                    {
                        var estadoNovo = string.Join("", Tabela[linha, coluna]);
                        Tabela[linha, coluna] = new string[] {estadoNovo};
                        states.Add(estadoNovo);
                    }
                }
            }

            foreach(var state in states)
            {
                if (!AutomatoConvertido.States.Contains(state))
                {
                    AutomatoConvertido.States.Add(state);
                }
            }
        }

        public void RecriarTabelaComEstadosNovos()
        {
            var tabela = Tabela;
            Tabela = new string[AutomatoConvertido.States.Count, AutomatoConvertido.Alphabet.Count][];

            for (int coluna = 0; coluna < (tabela.GetLength(1)); coluna++)
            {
                for (int linha = 0; linha < (tabela.GetLength(0)); linha++)
                {
                    Tabela[linha, coluna] = tabela[linha, coluna];
                }
            }
            Console.WriteLine();
            AutomatoConvertido.Imprimir();
        }

        public void PopularEstadosNovos()
        {
            for (int coluna = 0; coluna < AutomatoConvertido.Alphabet.Count; coluna++)
            {
                for (int linha = 0; linha < AutomatoConvertido.States.Count; linha++)
                {
                    
                }
            }
        }
    }
}
