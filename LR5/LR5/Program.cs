using System;
using System.Collections.Generic;

namespace LR5
{

    public class Program
    {
        public static void Main()
        {
            Wallet wallet = new Wallet();
            
            wallet.CreateCurrency();
            wallet.SwithCase();            
        }
    }

    public class Wallet
    {
        Bank bank = new Bank();
        MyPrinter myPrinter = new MyPrinter();

        public List<string> currencyList = new List<string>();
        public List<double> countList = new List<double>();
        public List<double> cursList = new List<double>();

        public void SwithCase()
        {
            Console.WriteLine("1. Внести валюту.\n2. Вывести валюту.\n3. Узнать баланс.\n4. Информация о кошельке.\n5. Перевести всё в одну валюту.");
            int s = Convert.ToInt32(Console.ReadLine());
            bool end = false;
            switch (s)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Введите название валюты:");
                        string currency = Console.ReadLine();
                        Console.WriteLine("Введите сумму ввода:");
                        double count = Convert.ToDouble(Console.ReadLine());
                        AddMoney(currency, count);
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("Введите название валюты:");
                        string currency = Console.ReadLine();
                        Console.WriteLine("Введите сумму вывода:");
                        double count = Convert.ToDouble(Console.ReadLine());
                        RemoveMoney(currency, count);
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        Console.WriteLine("Введите название валюты:");
                        string currency = Console.ReadLine();
                        GetMoney(currency);
                        break;
                    }
                case 4:
                    {
                        Console.Clear();
                        ToString();
                        break;
                    }
                case 5:
                    {
                        Console.Clear();
                        Console.WriteLine("Введите название валюты:");
                        string currency = Console.ReadLine();
                        GetTotalMoney(currency);
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        end = true;
                        break;
                    }
            }
            if (!end)
            {
                SwithCase();
            }
        }

        public void CreateCurrency()
        {
            currencyList.Add("RUB");
            currencyList.Add("USD");
            currencyList.Add("EUR");
            currencyList.Add("GBP");
            countList.Add(0);
            countList.Add(0);
            countList.Add(0);
            countList.Add(0);
            cursList.Add(1);
            cursList.Add(70);
            cursList.Add(80);
            cursList.Add(90);
        }

        public void AddMoney(string currency, double count)
        {
            if (count < 0)
            {
                Console.WriteLine("Введите положительное число!");
                return;
            }
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                countList[index] += count;
                myPrinter.Print("Add", currency, countList[index]);
            }
            else
            {
                Console.WriteLine("Данной валюты нет в списке, она будет добавлена.");
                currencyList.Add(currency);
                countList.Add(count);
                Console.WriteLine("Введите текущий курс указанной валюты:");
                cursList.Add(Convert.ToInt32(Console.ReadLine()));
                myPrinter.Print("Add", currency, countList.Count - 1);
            }
        }

        public void RemoveMoney(string currency, double count)
        {
            if (count < 0)
            {
                Console.WriteLine("Введите положительное число!");
                return;
            }
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                if (countList[index] >= count)
                {
                    countList[index] -= count;
                    myPrinter.Print("Remove", currency, countList[index]);
                }
                else
                    Console.WriteLine("Недостаточно средств!");
            }
            else
                Console.WriteLine("Данной валюты нет в списке!");
        }

        public void GetMoney(string currency)
        {
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                Console.WriteLine($"Валюта {currency} = {countList[index]} (курс относительно рубля = {cursList[index]})");
            }
            else
                Console.WriteLine($"Валюта {currency} = 0");
        }

        new public void ToString()
        {
            string result = "{";
            for (int i = 0; i < currencyList.Count; i++)
            {
                if (countList[i] > 0)
                {
                    result += ($" {currencyList[i]} = {countList[i]},");
                }
            }
            if (result[result.Length - 1] == ',')
            {
                result = result.Remove(result.Length - 1);
                result += " ";
            }
            result += "}";
            Console.WriteLine(result);
        }

        public double GetTotalMoney(string currency)
        {
            CursRandom(); // имитируются колебания курсов валют на рынке
            double result = 0;
            if (currencyList.Contains(currency))
            {
                int index = currencyList.FindIndex(x => x == currency);
                for (int i = 0; i < currencyList.Count; i++)
                {
                    result += bank.Convert(countList[i], cursList[i], cursList[index]); // перевести количество countList[i] ваюты с индексом i в валюту с индеком index
                }
                Console.WriteLine($"Общая сумма в {currency} = {result}");
            }
            else
                Console.WriteLine("Данной валюты нет в списке!");
            return result;
        }

        public void CursRandom()
        {
            for (int i = 1; i < cursList.Count; i++)
            {
                cursList[i] += bank.CursRandom(cursList[i]);
            }
        }
    }

    public class Bank
    {
        public double CursRandom(double curs) // имитируются колебания курсов валют на рынке
        {
            Random rnd = new Random();
            int rndCurs = rnd.Next(1, 10);
            if (rnd.Next(0, 1) == 1)
                rndCurs *= -1;
            return curs * rndCurs / 100;


        }

        public double Convert(double count, double currency1_curs, double currency2_curs)
        {
            double result = count * currency1_curs / currency2_curs;
            return Math.Round(result, 3);
        }
    }

    public class MyPrinter
    {
        public void Print(string operation, string currency, double amount)
        {
            Console.WriteLine($"{operation} {currency} = {amount}");
        }
    }
}