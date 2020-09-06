using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaguarda
{
    public class Produto
    {
        public string nomeProduto { get; set; }
        public double precoProduto { get; set; }
        public int saidaDiaria { get; set; }
        public double totalSaidaDiariaFin { get; set; }
        public int devolucoes { get; set; }
        public int saidaMenosDevolucoes { get; set; }
        public double valorTotalAtual { get; set; }
        public string cobrador { get; set; }
        public string rota { get; set; }
        public int codLote { get; set; }
        public int promissorias { get; set; }
        public int consignados { get; set; }
        public int valorTotalConsignado { get; set; }
        public double valorRecebido { get; set; }
        public double valorTotalRecebido { get; set; }
        public int produtosDevolvidos { get; set; }
        public int produtoTotalDevolvido { get; set; }
        public int vendas_CodLotes { get; set; }
        public string mes { get; set; }
        public DateTime dataSaida { get; set; }
        public double produtosPendentes { get; set; }
        public string ano { get; set; }
        public double receitas { get; set; }
        public double despesas { get; set; }
        public double faturamento { get; set; }
        public int codRegistro { get; set; }
        public int codApuracao { get; set; }
        public DateTime dataDE { get; set; }
        public DateTime dataATE { get; set; }

    }
}
