using System;
using System.Collections.Generic;

namespace zal_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Test klasy Macierz<T> ===\n");

            // Test 1: Macierz<int> 2x3
            Console.WriteLine("Test 1: Macierz<int> 2x3");
            var m1 = new Macierz<int>(2, 3);
            
            // Wypełnianie macierzy z nowym indeksatorem ValueTuple
            m1[(0, 0)] = 1; m1[(0, 1)] = 2; m1[(0, 2)] = 3;
            m1[(1, 0)] = 4; m1[(1, 1)] = 5; m1[(1, 2)] = 6;
            
            Console.WriteLine($"Wymiary: {m1.Wiersze}x{m1.Kolumny}");
            Console.WriteLine("Zawartość:");
            Console.WriteLine(m1);
            Console.WriteLine();

            // Test 2: Macierz<string> 2x2
            Console.WriteLine("Test 2: Macierz<string> 2x2");
            var m2 = new Macierz<string>(2, 2);
            m2[(0, 0)] = "A"; m2[(0, 1)] = "B";
            m2[(1, 0)] = "C"; m2[(1, 1)] = "D";
            
            Console.WriteLine("Zawartość:");
            Console.WriteLine(m2);
            Console.WriteLine();

            // Test 3: Porównywanie macierzy
            Console.WriteLine("Test 3: Porównywanie macierzy");
            var m3 = new Macierz<int>(2, 3);
            m3[(0, 0)] = 1; m3[(0, 1)] = 2; m3[(0, 2)] = 3;
            m3[(1, 0)] = 4; m3[(1, 1)] = 5; m3[(1, 2)] = 6;
            
            var m4 = new Macierz<int>(3, 2); // Różne wymiary
            
            Console.WriteLine($"m1 == m3: {m1 == m3}");
            Console.WriteLine($"m1 != m3: {m1 != m3}");
            Console.WriteLine($"m1.Equals(m3): {m1.Equals(m3)}");
            Console.WriteLine($"m1 == m4 (różne wymiary): {m1 == m4}");
            
            // Zmiana wartości
            m3[(1, 1)] = 999;
            Console.WriteLine($"m1 == m3 (po zmianie): {m1 == m3}");
            Console.WriteLine();

            // Test 4: Dostęp przez indeksator ValueTuple
            Console.WriteLine("Test 4: Dostęp przez indeksator");
            var m5 = new Macierz<int>(3, 3);
            
            // Wypełnianie kolejnych liczb
            int wartosc = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m5[(i, j)] = wartosc++;
                }
            }
            
            Console.WriteLine("Macierz 3x3:");
            Console.WriteLine(m5);
            Console.WriteLine($"Element (1,1): {m5[(1, 1)]}");
            Console.WriteLine();

            // Test 5: Walidacja błędów
            Console.WriteLine("Test 5: Walidacja błędów");
            try
            {
                var nieprawidlowa = new Macierz<int>(0, 5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ Wykryto błędny wymiar: {ex.Message}");
            }

            try
            {
                var m = new Macierz<int>(2, 2);
                var element = m[(5, 1)]; // Błędny indeks
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"✅ Wykryto błędny indeks: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz aby zakończyć...");
            Console.ReadKey();
        }
    }
} 