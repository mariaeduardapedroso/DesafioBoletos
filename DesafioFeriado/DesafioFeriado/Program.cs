using System;
using System.Text.RegularExpressions;
using DesafioFeriado;
class MetodoParaDescobrirSeDiaEFeriado
{
    static void Main(string[] args)
    {
        var TesteDataFormato = "[0-9]{2}[/][0-9]{2}[/][0-9]{4}";
        string DataAdquiridaString;

        Console.WriteLine("----BEM VINDO AO GERADOR DE RECIBOS ME DIGA A DATA DO PRIMEIRO BOLETO----");
        Console.WriteLine("Digite a data com dd/mm/aa");
        DataAdquiridaString = Console.ReadLine();
        GracinhaParaMostrarOResultado();

        Match resultado1 = Regex.Match(DataAdquiridaString, TesteDataFormato);
            
        if (resultado1.Success)
        {
            List<VerSeADataEFeriado> ListaRecibos = new List<VerSeADataEFeriado>();
            GeradorDeRecibos(DataAdquiridaString, ListaRecibos);

            Console.WriteLine();
            Console.WriteLine("BOLETO");
            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine(ListaRecibos[i].GetData());
            }

            Console.WriteLine("PRESSIONE ENTER PARA SAIR");
            Console.Read();
            GracinhaParaEncerrarPrograma();
            return;

        }
        Console.WriteLine(DataAdquiridaString + " É UMA DATA ILEGIVEL");
        return;

    }


    private static void GeradorDeRecibos(string DataAdquiridaString, List<VerSeADataEFeriado> ListaRecibos)
    {
        VerSeADataEFeriado DataAdquirida = new VerSeADataEFeriado(DataAdquiridaString);
        var saida = true;

        for (int i = 0; i < 12; i++)
        {
            ListaRecibos.Add(new VerSeADataEFeriado(DataAdquirida.GetData().AddMonths(i)));
        }

        for (int i = 0; i < 12; i++)
        {
            saida = false;

            while (!saida)
            {
                if (ListaRecibos[i].CompararSeEFeriadoFixo() || 
                   ListaRecibos[i].CompararSeEFeriadoNaoFixo() ||
                    ListaRecibos[i].CompararFinalDeSemana())//TODO: MELHORAR PROCESSAMENTO?
                {
                    ListaRecibos[i].AcrescentarUmDia();
                    saida =false;
                }
                else {
                    saida = true;
                }
            }
        }
    }

    private static void GracinhaParaMostrarOResultado()
    {
        Console.Clear();
        Console.WriteLine("UM SEGUNDO ESTAMOS GERANDO");
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("UM SEGUNDO ESTAMOS GERANDO.");
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("UM SEGUNDO ESTAMOS GERANDO..");
        Thread.Sleep(1000);
        Console.Clear();
        Console.WriteLine("UM SEGUNDO ESTAMOS GERANDO...");
        Thread.Sleep(1000);
        Console.Clear();
    }

    private static void GracinhaParaEncerrarPrograma()
    {
        for (int contador = 5; contador >= 0; contador--)
        {
            Console.Clear();
            Console.WriteLine("ENCERRANDO O PROGRAMA EM " + contador);
            Thread.Sleep(1000);
        }
        Console.Clear();
        Console.WriteLine("OBRIGADA POR USAR");
    }
}

