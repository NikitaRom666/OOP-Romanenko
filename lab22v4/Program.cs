using System;
using System.Collections.Generic;

namespace Lab22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЧАСТИНА 1: Демонстрація порушення LSP ===");
            
            // Створюємо звичайний файл
            LSP_Violation.File myFile = new LSP_Violation.File("document.txt");
            LSP_Violation.FileHandler.SaveText(myFile, "Привіт, світ!"); // Працює коректно

            // Створюємо файл тільки для читання
            LSP_Violation.File readOnlyFile = new LSP_Violation.ReadOnlyFile("config.cfg");

            try
            {
                // Клієнтський код думає, що працює з File, і намагається записати дані.
                // Це призведе до краху програми (винятку).
                LSP_Violation.FileHandler.SaveText(readOnlyFile, "Зміна налаштувань");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ПОМИЛКА]: {ex.Message}");
                Console.WriteLine("Аналіз: Похідний клас ReadOnlyFile не зміг виконати контракт базового класу File.");
            }

            Console.WriteLine("\n--------------------------------------------------\n");

            Console.WriteLine("=== ЧАСТИНА 2: Альтернативне рішення (Correct LSP) ===");

            // Створюємо об'єкти нової ієрархії
            LSP_Fixed.ReadOnlyFile fixedReadOnly = new LSP_Fixed.ReadOnlyFile("system.sys");
            LSP_Fixed.EditableFile fixedEditable = new LSP_Fixed.EditableFile("notes.txt");

            // Демонстрація читання (доступно для обох)
            fixedReadOnly.Read();
            fixedEditable.Read();

            // Демонстрація запису через безпечний клієнтський метод
            LSP_Fixed.FileProcessor.AppendData(fixedEditable, "Нові важливі дані");

            // Наступний рядок навіть не скомпілюється, що захищає нас від помилок ще на етапі написання коду:
            // LSP_Fixed.FileProcessor.AppendData(fixedReadOnly, "Це не спрацює"); 
            
            Console.WriteLine("\nВиконання завершено успішно.");
            Console.ReadKey();
        }
    }
}


// ЧАСТИНА 1: ІЄРАРХІЯ З ПОРУШЕННЯМ LSP

namespace LSP_Violation
{
    // Базовий клас
    public class File
    {
        public string FilePath { get; set; }

        public File(string path)
        {
            FilePath = path;
        }

        public virtual void Read()
        {
            Console.WriteLine($"[File] Читання файлу: {FilePath}");
        }

        public virtual void Write(string text)
        {
            Console.WriteLine($"[File] Запис у файл {FilePath}: \"{text}\"");
        }
    }

    // Похідний клас, який ламає логіку
    public class ReadOnlyFile : File
    {
        public ReadOnlyFile(string path) : base(path) { }

        public override void Read()
        {
            Console.WriteLine($"[ReadOnly] Читання захищеного файлу: {FilePath}");
        }

        // ПОРУШЕННЯ LSP:
        // Ми змушені реалізувати цей метод, бо він є в базовому класі,
        // але логіка класу забороняє запис. Викидання винятку - це класичне порушення.
        public override void Write(string text)
        {
            throw new NotImplementedException("Неможливо записати в файл, який доступний тільки для читання!");
        }
    }

    // Клієнтський клас
    public class FileHandler
    {
        // Цей метод очікує, що БУДЬ-ЯКИЙ File підтримує запис.
        public static void SaveText(File file, string text)
        {
            Console.Write($"Спроба запису в {file.GetType().Name}... ");
            file.Write(text); // Тут станеться вибух, якщо передати ReadOnlyFile
        }
    }
}


// ЧАСТИНА 2: РЕФАКТОРИНГ (Виправлення через розділення інтерфейсів/ієрархії)

namespace LSP_Fixed
{
    // Абстракція для читання (спільна для всіх)
    public interface IReadable
    {
        void Read();
    }

    // Абстракція для запису (тільки для тих, хто це може)
    public interface IWritable
    {
        void Write(string text);
    }

    // Базовий клас (загальні метадані, якщо потрібно)
    public abstract class FileBase : IReadable
    {
        public string Filename { get; }
        public FileBase(string name) => Filename = name;

        public virtual void Read()
        {
            Console.WriteLine($"Reading {Filename}...");
        }
    }

    // Клас тільки для читання (реалізує тільки IReadable)
    public class ReadOnlyFile : FileBase
    {
        public ReadOnlyFile(string name) : base(name) { }
        
        // Write() тут просто фізично відсутній, помилка неможлива
    }

    // Клас для редагування (реалізує і IReadable, і IWritable)
    public class EditableFile : FileBase, IWritable
    {
        public EditableFile(string name) : base(name) { }

        public void Write(string text)
        {
            Console.WriteLine($"Writing to {Filename}: {text}");
        }
    }

    // Клієнтський клас
    public class FileProcessor
    {
        // Цей метод приймає ТІЛЬКИ ті об'єкти, які гарантовано реалізують IWritable
        public static void AppendData(IWritable writableFile, string data)
        {
            Console.WriteLine("Запис даних через безпечний інтерфейс...");
            writableFile.Write(data);
        }
    }
}