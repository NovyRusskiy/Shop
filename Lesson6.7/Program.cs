using System;
using System.Collections.Generic;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> firstClientProducts = new List<Product>()
            {
                new Product ("Water", 5),
                new Product ("Flowers", 15)
            };

            List<Product> secondClientProducts = new List<Product>()
            {
                new Product("Toy", 500),
                new Product("Whiskey", 1000),
                new Product("Headphones", 100)
            };


            List<Product> thirdClientProducts = new List<Product>()
            {
                new Product("TV", 1500),
                new Product("Potato", 10),
                new Product("Glass", 400),
                new Product("Keyboard", 100)
            };

            Queue<Client> clients = new Queue<Client>();
            clients.Enqueue(new Client(firstClientProducts));
            clients.Enqueue(new Client(secondClientProducts));
            clients.Enqueue(new Client(thirdClientProducts));

            Shop shop = new Shop(clients);

            while (clients.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Нажмите любую клавишу, чтобы обслужить клиента из очереди\n");
                Console.ReadKey();
                shop.ServeClient();
                shop.ShowInfo();
                Console.ReadKey();
            }
        }
    }

    class Client
    {
        private int _money;
        private List<Product> _products;
        private static Random _random = new Random();


        public Client(List<Product> products)
        {
            _money = _random.Next(200, 2000);
            _products = products;
        }

        public bool CheckEnoughMoney(int sum)
        {
            return _money >= sum;
        }

        public void DeleteRandomProduct()
        {
            int maxValue = _products.Count;
            int indexForDelete = _random.Next(0, maxValue);
            _products.RemoveAt(indexForDelete);
        }

        public int GetPurchaseSum()
        {
            int sum = 0;

            foreach (Product product in _products)
            {
                sum += product.Cost;
            }
            return sum;
        }

        public void ShowReceipt(int sum)
        {
            Console.WriteLine("Товарный чек:");

            foreach (Product product in _products)
            {
                product.ShowInfo();
            }

            Console.WriteLine($"\nСумма покупки: {sum}$");
        }
    }

    class Product
    {
        public string Title { get; private set; }

        public int Cost { get; private set; }

        public Product(string title, int cost)
        {
            Title = title;
            Cost = cost;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Title} - {Cost}$");
        }
    }

    class Shop
    {
        private Queue<Client> _clients;

        public int Profit { get; private set; }

        public Shop(Queue<Client> clients)
        {
            Profit = 0;
            _clients = clients;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"\n\nДоход кассы за сегодня составляет: {Profit}$");
        }

        public int ServeClient()
        {
            Client client = _clients.Peek();
            bool isComplete = false;
            int sum = 0;

            while (isComplete == false)
            {
                sum = client.GetPurchaseSum();

                if (client.CheckEnoughMoney(sum))
                {
                    isComplete = true;
                }
                else
                {
                    client.DeleteRandomProduct();
                }
            }

            client.ShowReceipt(sum);
            _clients.Dequeue();
            Profit += sum;
            return sum;
        }
    }
}
