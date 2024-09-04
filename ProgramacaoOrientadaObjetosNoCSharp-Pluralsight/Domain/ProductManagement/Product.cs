using ProgramacaoOrientadaObjetosNoCSharp_Pluralsight.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoOrientadaObjetosNoCSharp_Pluralsight.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        public int Id { get { return id; } set { id = value; } }


        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }


        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }


        private int maxItemInStock = 0;

        public UnityType UnityType { get; set; }


        public int AmountInStock { get; private set; }


        public bool IsBelowStockThreshold { get; private set; }
        public Price Price { get; set; }

        public Product(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public Product(int id) : this(id, string.Empty)
        {

        }

        public Product(int id, string name, string? description,Price price, UnityType unityType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnityType = unityType;

            maxItemInStock = maxAmountInStock;

            AtualizarEstoqueBaixo();
        }
        public void UsarProduto(int items)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;

                AtualizarEstoqueBaixo();

                Registro($"Quantia em estoque atualizada. Agora {AmountInStock} items no estoque.");
            }
            else
            {
                Registro($"Item em estoque insuficentes para {CriarRepresentacaoSimplesProduto()}. {AmountInStock} disponivel porem {items} solicitados.");
            }
        }

        public void AumentarEstoque()
        {
            AmountInStock++;
        }
        public void AumentarEstoque(int quantia)
        {
            int newStock = AmountInStock + quantia;
            if (newStock <= maxItemInStock)
            {
                AmountInStock += quantia;
            }
            else
            {
                AmountInStock = maxItemInStock;
                Registro($"{CriarRepresentacaoSimplesProduto} estoque lotado! {newStock - AmountInStock} item(s) não puderam ser armazenados.");
            }
            if (AmountInStock > 10)
            {
                IsBelowStockThreshold = false;
            }
        }

        private string CriarRepresentacaoSimplesProduto()
        {
            return $"Produto {Id} ({name})";
        }

        public string MostrarDetalhesShort()
        {
            return $"{Id}. {name} \n{AmountInStock} item em estoque.";
        }

        public string MostrarDetalhesFull()
        {
            StringBuilder sb = new();
            sb.Append($"{Id} {name} Preço:{Price} \n{description}\n{AmountInStock} item(s) no estoque.");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!!ESTOQUE BAIXO!!!");
            }

            return sb.ToString();
        }
    }
}
