using System;
using System.Text.RegularExpressions;
using DesafioFeriado;
class MetodoParaDescobrirSeDiaEFeriado
{
    static void Main(string[] args)
    {
        var TesteDataFormato = "[0-9]{2}[/][0-9]{2}[/][0-9]{4}";
        string DataAdquiridaString;
        int Parcelas = 0;
        bool AdquirirValorParcelas;

        Console.WriteLine("----BEM VINDO AO GERADOR DE RECIBOS ME DIGA A DATA DO PRIMEIRO BOLETO----");
        Console.WriteLine("Digite a data com dd/mm/aaaa");
        DataAdquiridaString = Console.ReadLine();

        Console.WriteLine("Digite a quantidade de boletos");
        AdquirirValorParcelas = Int32.TryParse(Console.ReadLine(),out Parcelas);
        GracinhaParaMostrarOResultado();

        Match resultado1 = Regex.Match(DataAdquiridaString, TesteDataFormato);
            
        if (resultado1.Success && Parcelas > 0 && AdquirirValorParcelas)
        {
            List<VerSeADataEFeriado> ListaRecibos = new List<VerSeADataEFeriado>();
            GeradorDeRecibos(DataAdquiridaString, ListaRecibos,Parcelas);

            Console.WriteLine();
            Console.WriteLine($"BOLETO COM {Parcelas} PARCELAS GERADO COM SUCESSO!");
            for (int i = 0; i < Parcelas; i++)
            {
                Console.WriteLine($"Vencimento da Parcela {i+1} - " + ListaRecibos[i].GetData());
            }

            Console.WriteLine("PRESSIONE ENTER PARA SAIR");
            Console.Read();
            GracinhaParaEncerrarPrograma();
            return;

        }
        Console.WriteLine("INFELIZMENTE ALGUM DADO QUE ME FORNECEU ESTÁ INCORRETO, RECIBO NÃO GERADO");
        return;

    }


    private static void GeradorDeRecibos(string DataAdquiridaString, List<VerSeADataEFeriado> ListaRecibos,int qdeParcelas)
    {
        VerSeADataEFeriado DataAdquirida = new VerSeADataEFeriado(DataAdquiridaString);
        var saida = true;

        for (int i = 0; i < qdeParcelas; i++)
        {
            ListaRecibos.Add(new VerSeADataEFeriado(DataAdquirida.GetData().AddMonths(i)));
        }

        for (int i = 0; i < qdeParcelas; i++)
        {
            saida = false;

            while (!saida)
            {
                if (ListaRecibos[i].CompararSeEFeriadoFixo() ||  ListaRecibos[i].CompararSeEFeriadoNaoFixo() ||
                    ListaRecibos[i].CompararFinalDeSemana())//TODO: MELHORAR PROCESSAMENTO?
                {
                    ListaRecibos[i].AcrescentarUmDia();
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

