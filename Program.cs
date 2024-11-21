using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Generics_Activity
{
    public class DictionaryRepository<TKey, TProduct> where TKey : IComparable<TKey>
    {
        public Dictionary<TKey, TProduct> _dictionary;

        public DictionaryRepository()
        {
            _dictionary = new Dictionary<TKey, TProduct>();

        }

        public void Add(TKey id, TProduct value)
        {


            if (_dictionary.ContainsKey(id))
            {
                throw new ArgumentException("An item with this key already exists.");
            }
            _dictionary[id] = value;

        }
        public TProduct Get(TKey id)
        {
            if (!_dictionary.TryGetValue(id, out TProduct value))
            {
                throw new KeyNotFoundException("The specified key does not exist.");
            }
            return value;
        }
        public void Update(TKey id, TProduct newValue)
        {
            if (!_dictionary.ContainsKey(id))
            {
                throw new KeyNotFoundException("The Entered Key does not exist.");
            }
            _dictionary[id] = newValue;
        }
        public void Delete(TKey id)
        {
            if (!_dictionary.ContainsKey(id))
            {
                throw new KeyNotFoundException("The entered key does not exist.");
            }
            _dictionary.Remove(id);
        }
        public void DisplayAllItems()
        {
            foreach (var item in _dictionary)
            {
                Console.WriteLine($"Key: {item.Key} Value: {item.Value}");
            }
        }
    }

    public class Product
    {
        public int ProductId
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }
        public override string ToString()
        {
            return $"Product Id {ProductId} Product Name: {ProductName}";
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {

            DictionaryRepository<int, Product> dictionaryRepository = new DictionaryRepository<int, Product>();
            bool running = true;
            while (running)
            {
                Console.WriteLine("CRUD Operation (Procducts)" +
                                   "\n1. Add Product" +
                                   "\n2. Retrieve/ Display Product by its Key" +
                                   "\n3, Update Product" +
                                   "\n4. Delete Product" +
                                   "\n5. Display All Products" +
                                   "\n6. Exit");
                Console.Write("Enter 1 - 6: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        Console.Write("Enter product Id: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Poduct: ");
                        string product = Console.ReadLine();
                        var productRepository = new Product
                        {
                            ProductId = id,
                            ProductName = product
                        };
                        try
                        {
                            dictionaryRepository.Add(id, productRepository);
                            Console.WriteLine("Poduct added successfully.");

                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine();

                        break;
                    case 2:
                        Console.Write("Enter the Key id you want to retrieve: ");
                        int retrieveId = int.Parse(Console.ReadLine());
                        try
                        {
                            Product product1 = dictionaryRepository.Get(retrieveId);
                            Console.WriteLine($"Key ID: {retrieveId}" +
                                               $"\nProduct Name: {product1}");
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.Write("Enter Key ID you want to Update: ");
                        if (int.TryParse(Console.ReadLine(), out int keyId) && dictionaryRepository._dictionary.TryGetValue(keyId, out Product items))
                        {
                            Console.WriteLine($"Current ID: {items.ProductId}");
                            Console.Write("Enter a new id (leave a blank to keep current): ");
                            string newId = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newId))
                            {
                                int number = int.Parse(newId);
                                items.ProductId = number;
                            }
                            Console.WriteLine($"Current ID: {items.ProductName}");
                            Console.Write("Enter a new Name (leave a blank to keep current): ");
                            string newName = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newName))
                            {

                                items.ProductName = newName;
                            }
                            Console.WriteLine("Updated Successfully");
                        }
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.Write("Enter Key you want to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int keyID) && dictionaryRepository._dictionary.ContainsKey(keyID))
                        {
                            dictionaryRepository._dictionary.Remove(keyID);
                            Console.WriteLine("Product deleted Successfully");
                        }
                        else
                        {
                            Console.WriteLine("ID not found.");
                        }
                        Console.WriteLine();
                        break;
                    case 5:
                        dictionaryRepository.DisplayAllItems();
                        Console.WriteLine();
                        break;
                    case 6:
                        running = false;
                        break;
                }

            }
        }
    }
}