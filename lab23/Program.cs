using System;

namespace Lab23
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("1. Поганий приклад (все в кучі):");
            // Створюємо стару машину, яка все робить сама
            OldMachine bad = new OldMachine();
            bad.Work();

            Console.WriteLine("\n2. Нормальний приклад (розділили логіку):");

            // Створюємо деталі окремо
            IPrinter myPrinter = new LaserPrinter();
            IScanner myScanner = new Scanner();
            IFax myFax = new Fax();

            // Передаємо деталі в головний клас (DI)
            OfficeCenter center = new OfficeCenter(myPrinter, myScanner, myFax);
            
            Console.WriteLine("- Офісний центр:");
            center.PrintDoc("Звіт");
            center.ScanDoc();
            center.SendFax("102", "Ало");

            // Окремий принтер, якому не треба сканер (ISP)
            SimplePrinter home = new SimplePrinter(myPrinter);
            
            Console.WriteLine("\n- Домашній принтер:");
            home.PrintOnly("Курсова");

            Console.ReadKey();
        }
    }

    // Частина 1: Як не треба робити

    interface IAllInOne
    {
        void Print();
        void Scan();
        void Fax();
    }

    class PrintMod { public void Print() { Console.WriteLine("Old print..."); } }
    class ScanMod { public void Scan() { Console.WriteLine("Old scan..."); } }
    class FaxMod { public void Fax() { Console.WriteLine("Old fax..."); } }

    class OldMachine : IAllInOne
    {
        PrintMod p;
        ScanMod s;
        FaxMod f;

        public OldMachine()
        {
            // Жорстка прив'язка, не можна поміняти
            p = new PrintMod();
            s = new ScanMod();
            f = new FaxMod();
        }

        public void Print() { p.Print(); }
        public void Scan() { s.Scan(); }
        public void Fax() { f.Fax(); }

        public void Work()
        {
            Print();
            Scan();
            Fax();
        }
    }

    // Частина 2: Виправлення

    // Розбили інтерфейси
    public interface IPrinter
    {
        void Print(string text);
    }

    public interface IScanner
    {
        void Scan();
    }

    public interface IFax
    {
        void Fax(string num, string text);
    }

    // Реалізація
    public class LaserPrinter : IPrinter
    {
        public void Print(string text)
        {
            Console.WriteLine($"Друкуємо: {text}");
        }
    }

    public class Scanner : IScanner
    {
        public void Scan()
        {
            Console.WriteLine("Скануємо документ...");
        }
    }

    public class Fax : IFax
    {
        public void Fax(string num, string text)
        {
            Console.WriteLine($"Факс на {num}: {text}");
        }
    }

    // Офісна машина (бере все через конструктор)
    public class OfficeCenter
    {
        IPrinter printer;
        IScanner scanner;
        IFax fax;

        public OfficeCenter(IPrinter p, IScanner s, IFax f)
        {
            printer = p;
            scanner = s;
            fax = f;
        }

        public void PrintDoc(string t) => printer.Print(t);
        public void ScanDoc() => scanner.Scan();
        public void SendFax(string n, string t) => fax.Fax(n, t);
    }

    // Простий принтер (бере тільки принтер)
    public class SimplePrinter
    {
        IPrinter printer;

        public SimplePrinter(IPrinter p)
        {
            printer = p;
        }

        public void PrintOnly(string t)
        {
            printer.Print(t);
        }
    }
}