using System;

namespace zal_2a
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TESTOWANIE KLASY WEKTOR ===\n");

            // Test konstruktorów
            Console.WriteLine("1. KONSTRUKTORY:");
            Wektor v1 = new Wektor(3); // Wektor 3D wypełniony zerami
            Console.WriteLine($"v1 (wymiar 3, zera): {v1}");

            Wektor v2 = new Wektor(1, 2, 3); // Wektor z konkretnymi wartościami
            Console.WriteLine($"v2 (1, 2, 3): {v2}");

            Wektor v3 = new Wektor(4, 0, -1); // Inny wektor 3D
            Console.WriteLine($"v3 (4, 0, -1): {v3}");

            // Test właściwości
            Console.WriteLine("\n2. WŁAŚCIWOŚCI:");
            Console.WriteLine($"v2.Wymiar: {v2.Wymiar}");
            Console.WriteLine($"v2.Długość: {v2.Długość:F3}");

            // Test indeksatora
            Console.WriteLine("\n3. INDEKSATOR:");
            Console.WriteLine($"v2[0] = {v2[0]}, v2[1] = {v2[1]}, v2[2] = {v2[2]}");
            v1[0] = 5;
            v1[1] = -2;
            v1[2] = 1;
            Console.WriteLine($"v1 po modyfikacji: {v1}");

            // Test iloczynu skalarnego
            Console.WriteLine("\n4. ILOCZYN SKALARNY:");
            double iloczyn = Wektor.IloczynSkalarny(v2, v3);
            Console.WriteLine($"v2 · v3 = {iloczyn}");

            // Test różnych wymiarów
            Wektor v2d = new Wektor(1, 1);
            double iloczynNaN = Wektor.IloczynSkalarny(v2, v2d);
            Console.WriteLine($"v2 · v2d (różne wymiary) = {iloczynNaN}");

            // Test sumy wektorów
            Console.WriteLine("\n5. SUMA WEKTORÓW:");
            Wektor suma = Wektor.Suma(v1, v2, v3);
            Console.WriteLine($"Suma({v1}, {v2}, {v3}) = {suma}");

            // Test operatorów
            Console.WriteLine("\n6. OPERATORY:");
            
            // Dodawanie
            Wektor dodawanie = v2 + v3;
            Console.WriteLine($"v2 + v3 = {dodawanie}");

            // Odejmowanie
            Wektor odejmowanie = v2 - v3;
            Console.WriteLine($"v2 - v3 = {odejmowanie}");

            // Mnożenie przez skalar
            Wektor mnozenie1 = v2 * 2.5;
            Console.WriteLine($"v2 * 2.5 = {mnozenie1}");

            Wektor mnozenie2 = 0.5 * v2;
            Console.WriteLine($"0.5 * v2 = {mnozenie2}");

            // Dzielenie przez skalar
            Wektor dzielenie = v2 / 2;
            Console.WriteLine($"v2 / 2 = {dzielenie}");

            // Test z wektorami 2D
            Console.WriteLine("\n7. WEKTORY 2D:");
            Wektor a = new Wektor(3, 4);
            Wektor b = new Wektor(1, 2);
            Console.WriteLine($"a = {a}, długość = {a.Długość:F3}");
            Console.WriteLine($"b = {b}, długość = {b.Długość:F3}");
            Console.WriteLine($"a + b = {a + b}");
            Console.WriteLine($"a · b = {Wektor.IloczynSkalarny(a, b)}");

            // Test wektorów jednostkowych
            Console.WriteLine("\n8. WEKTORY JEDNOSTKOWE:");
            Wektor jednostkowyX = new Wektor(1, 0, 0);
            Wektor jednostkowyY = new Wektor(0, 1, 0);
            Wektor jednostkowyZ = new Wektor(0, 0, 1);
            
            Console.WriteLine($"e_x = {jednostkowyX}, długość = {jednostkowyX.Długość}");
            Console.WriteLine($"e_y = {jednostkowyY}, długość = {jednostkowyY.Długość}");
            Console.WriteLine($"e_z = {jednostkowyZ}, długość = {jednostkowyZ.Długość}");
            
            Console.WriteLine($"e_x · e_y = {Wektor.IloczynSkalarny(jednostkowyX, jednostkowyY)}");
            Console.WriteLine($"e_x · e_x = {Wektor.IloczynSkalarny(jednostkowyX, jednostkowyX)}");

            // walidacja i równość
            Console.WriteLine("\n9. TESTY WALIDACJI:");
            try
            {
                Wektor nieprawidlowy = new Wektor(0); // Powinno rzucić wyjątek
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ Poprawnie przechwycono błąd: {ex.Message}");
            }

            try
            {
                Wektor pusty = new Wektor(); // Powinno rzucić wyjątek
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ Poprawnie przechwycono błąd: {ex.Message}");
            }

            Console.WriteLine("\n10. TESTY RÓWNOŚCI:");
            Wektor v4 = new Wektor(1, 2, 3);
            Wektor v5 = new Wektor(1, 2, 3);
            Wektor v6 = new Wektor(1, 2, 4);
            
            Console.WriteLine($"v2 = {v2}");
            Console.WriteLine($"v4 = {v4}");
            Console.WriteLine($"v5 = {v5}");
            Console.WriteLine($"v6 = {v6}");
            Console.WriteLine($"v2.Equals(v4): {v2.Equals(v4)}");
            Console.WriteLine($"v4.Equals(v5): {v4.Equals(v5)}");
            Console.WriteLine($"v4.Equals(v6): {v4.Equals(v6)}");

            Console.WriteLine("\n=== KONIEC TESTÓW ===");
        }
    }
}
