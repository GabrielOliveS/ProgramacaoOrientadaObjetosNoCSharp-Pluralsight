using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoOrientadaObjetosNoCSharp_Pluralsight.Domain.ProductManagement
{
    public partial class Product
    {
        private void DiminuirEstoque(int items, string razao)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            AtualizarEstoqueBaixo();

            Registro(razao);
        }
        private void AtualizarEstoqueBaixo()
        {
            if (AmountInStock < 10)
            {
                IsBelowStockThreshold = true;
            }
        }

        private void Registro(string message)
        {
            Console.WriteLine(message);
        }
    }
    
}
