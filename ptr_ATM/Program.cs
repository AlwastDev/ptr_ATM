using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptr_ATM
{
    interface ISubject
    {
        string Request();
    }

    class Subject
    {
        public string Request()
        {
            return "Request Subject";
        }
    }

    class Proxy : ISubject // виртуальный заместитель
    {
        private Subject _subject;
       
        public string Request()
        {
            if (_subject == null)
            {
                Console.WriteLine("Subject inactive");
                _subject = new Subject();
            }
            Console.WriteLine("Subject active");
            return $"Proxy call {_subject.Request()}";
        }
        
    }

    class ProtectionProxy : ISubject // защищающий заместитель
    {
        private Subject _subject;
        private string _password = "4545";
        private long _score = 23132131231;
        private int _money = 5000;
        public string Authentication(string password)
        {
            if (_password != password)
            {
                return "Неверный пин-код!";
            }
            else
            {
                _subject = new Subject();
                return "Вы ввошли в систему!";
            }
        }
        public bool CheckNumberScore()
        {
            Console.WriteLine(_score);
            return true;
        }
        public bool CheckMoney()
        {
            Console.WriteLine(_money);
            return true;
        }
        public int RemoveMoney(int removemoney)
        {
            if (_money - removemoney >= 0)
            {
                int isCheck = 0;
                Console.WriteLine("Распечатать чек ? 1/0");
                isCheck = Convert.ToInt32(Console.ReadLine());
                if (isCheck == 0)
                {
                    return _money -= removemoney;
                }
                else if (isCheck == 1)
                {
                    RemoveCheck(removemoney);
                    return _money -= removemoney;
                }
                return _money -= removemoney;
            }
            else
            {
                Console.WriteLine("У вас недостаточно средств!");
            }
            return 0;
        }
        public int AddMoney(int addmoney)
        {
            int isCheck = 0;
            Console.WriteLine("Распечатать чек ? 1/0");
            isCheck = Convert.ToInt32(Console.ReadLine());
            if (isCheck == 0)
            {
                return _money += addmoney;
            }
            else if (isCheck == 1)
            {
                AddCheck(addmoney);
                return _money += addmoney;
            }
            return _money += addmoney;
        }
        public bool AddCheck(int money)
        {
            Console.WriteLine($"\n Вы внесли сумму -> {money}\n Текущая сумма -> {_money += money}\n Дата -> {DateTime.Now} ");
            return true;
        }
        public bool RemoveCheck(int money)
        {
            Console.WriteLine($"\n Вы сняли сумму -> {money}\n Текущая сумма -> {_money -= money}\n Дата -> {DateTime.Now} ");
            return true;
        }
        public string Request()
        {
            return $"Protection proxy: {(_subject == null ? "first authenticated" : $"call {_subject.Request()}")}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;
            ISubject subject = new Proxy();
            subject = new ProtectionProxy();
            Console.WriteLine((subject as ProtectionProxy).Authentication("4545"));
            Console.WriteLine("\n--------------------------------\n");
            
            while (a != 0)
            {
                
                Console.WriteLine("1. Проверка номера счета");
                Console.WriteLine("2. Проверка средств");
                Console.WriteLine("3. Внесение денег");
                Console.WriteLine("4. Снятие денег");
                a = Convert.ToInt32(Console.ReadLine());
                switch (a)
                {
                    case 1: (subject as ProtectionProxy).CheckNumberScore(); break;
                    case 2: (subject as ProtectionProxy).CheckMoney(); break;
                    case 3: int addmoney = 0; Console.WriteLine("Введите сумму ->"); addmoney = Convert.ToInt32(Console.ReadLine()); (subject as ProtectionProxy).AddMoney(addmoney); break;
                    case 4: int removemoney = 0; Console.WriteLine("Введите сумму ->"); removemoney = Convert.ToInt32(Console.ReadLine()); (subject as ProtectionProxy).RemoveMoney(removemoney); break;
                    default: Console.WriteLine("Введите число от 1 до 3"); break;
                }
                Console.ReadKey();
                Console.Clear();
            }
            

            Console.ReadKey();
        }
    }
}
