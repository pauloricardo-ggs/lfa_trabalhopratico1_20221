using System.Collections.Generic;

namespace lfa_trabalhopratico1_20221
{
    public class TabelaConversao
    {
        public Automato Automato { get; private set; }
        public List<string> Linhas { get; private set; }
        public List<string> Colunas { get; private set; }
        public string[,][] Tabela { get; private set; }

        public TabelaConversao(Automato automato)
        {
            Automato = automato;
            Linhas = Automato.States;
            Colunas = Automato.Alphabet;
            Tabela = new string[Linhas.Count, Colunas.Count][];
        }

        public void Criar()
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


    }
}
