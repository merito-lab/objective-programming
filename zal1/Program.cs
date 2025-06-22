using System;

namespace zal_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Test klasy Osoba ===");
            Console.WriteLine();

            try
            {
                // Test 1: Podstawowe tworzenie osoby
                Console.WriteLine("Test 1: Tworzenie osoby 'Jan Kowalski'");
                var osoba1 = new Osoba("Jan Kowalski");
                Console.WriteLine($"Imię: '{osoba1.Imię}'");
                Console.WriteLine($"Nazwisko: '{osoba1.Nazwisko}'");
                Console.WriteLine($"ImięNazwisko: '{osoba1.ImięNazwisko}'");
                Console.WriteLine($"ToString(): {osoba1}");
                Console.WriteLine();

                // Test 2: Jednoczłonowe imię
                Console.WriteLine("Test 2: Tworzenie osoby 'Madonna'");
                var osoba2 = new Osoba("Madonna");
                Console.WriteLine($"Imię: '{osoba2.Imię}'");
                Console.WriteLine($"Nazwisko: '{osoba2.Nazwisko}'");
                Console.WriteLine($"ImięNazwisko: '{osoba2.ImięNazwisko}'");
                Console.WriteLine();

                // Test 3: Wieloczłonowe imię i nazwisko
                Console.WriteLine("Test 3: Tworzenie osoby 'Jan Maria Kowalski Nowak'");
                var osoba3 = new Osoba("Jan Maria Kowalski Nowak");
                Console.WriteLine($"Imię: '{osoba3.Imię}' (pierwsza składowa)");
                Console.WriteLine($"Nazwisko: '{osoba3.Nazwisko}' (ostatnia składowa)");
                Console.WriteLine($"ImięNazwisko: '{osoba3.ImięNazwisko}'");
                Console.WriteLine();

                // Test 4: Ustawianie dat urodzenia i śmierci
                Console.WriteLine("Test 4: Osoba z datami życia");
                var osoba4 = new Osoba("Adam Mickiewicz");
                osoba4.DataUrodzenia = new DateTime(1798, 12, 24);
                osoba4.DataŚmierci = new DateTime(1855, 11, 26);
                
                Console.WriteLine($"Data urodzenia: {osoba4.DataUrodzenia:yyyy-MM-dd}");
                Console.WriteLine($"Data śmierci: {osoba4.DataŚmierci:yyyy-MM-dd}");
                Console.WriteLine($"Wiek w momencie śmierci: {osoba4.Wiek?.TotalDays / 365.25:F1} lat");
                Console.WriteLine($"ToString(): {osoba4}");
                Console.WriteLine();

                // Test 5: Osoba żyjąca
                Console.WriteLine("Test 5: Osoba żyjąca");
                var osoba5 = new Osoba("Anna Nowak");
                osoba5.DataUrodzenia = new DateTime(1990, 5, 15);
                
                Console.WriteLine($"Data urodzenia: {osoba5.DataUrodzenia:yyyy-MM-dd}");
                Console.WriteLine($"Aktualny wiek: {osoba5.Wiek?.TotalDays / 365.25:F1} lat");
                Console.WriteLine($"ToString(): {osoba5}");
                Console.WriteLine();

                // Test 6: Testowanie walidacji dat
                Console.WriteLine("Test 6: Testowanie walidacji dat");
                TestWalidacjiDat();
                Console.WriteLine();

                // Test 7: Testowanie walidacji tekstów
                Console.WriteLine("Test 7: Testowanie walidacji tekstu");
                TestWalidacji();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("Naciśnij dowolny klawisz aby zakończyć...");
            Console.ReadKey();
        }

        static void TestWalidacjiDat()
        {
            Console.WriteLine("Testowanie walidacji dat:");

            // Test 1: Data śmierci wcześniejsza niż urodzenia (ustawianie śmierci po urodzeniu)
            try
            {
                var osoba = new Osoba("Jan Kowalski");
                osoba.DataUrodzenia = new DateTime(2000, 1, 1);
                osoba.DataŚmierci = new DateTime(1999, 1, 1); // Błędna data
                Console.WriteLine("❌ Nie wykryto błędnej daty śmierci!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ Wykryto błędną datę śmierci: {ex.Message}");
            }

            // Test 2: Data urodzenia późniejsza niż śmierci (ustawianie urodzenia po śmierci)
            try
            {
                var osoba = new Osoba("Anna Nowak");
                osoba.DataŚmierci = new DateTime(1999, 1, 1);
                osoba.DataUrodzenia = new DateTime(2000, 1, 1); // Błędna data
                Console.WriteLine("❌ Nie wykryto błędnej daty urodzenia!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ Wykryto błędną datę urodzenia: {ex.Message}");
            }

            // Test 3: Poprawne daty
            try
            {
                var osoba = new Osoba("Piotr Wiśniewski");
                osoba.DataUrodzenia = new DateTime(1950, 5, 15);
                osoba.DataŚmierci = new DateTime(2020, 10, 30);
                Console.WriteLine($"✅ Poprawne daty: {osoba}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Błąd przy poprawnych datach: {ex.Message}");
            }

            // Test 4: Ta sama data urodzenia i śmierci (graniczny przypadek)
            try
            {
                var osoba = new Osoba("Test Test");
                var data = new DateTime(2000, 1, 1);
                osoba.DataUrodzenia = data;
                osoba.DataŚmierci = data;
                Console.WriteLine($"✅ Ta sama data urodzenia i śmierci: {osoba}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Błąd przy tej samej dacie: {ex.Message}");
            }
        }

        static void TestWalidacji()
        {
            Console.WriteLine("Testowanie walidacji:");

            // Test pustego imienia
            try
            {
                var osoba = new Osoba("Jan Kowalski");
                osoba.Imię = ""; // To powinno rzucić wyjątek
                Console.WriteLine("❌ Nie wykryto pustego imienia!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ Wykryto pusté imię: {ex.Message}");
            }

            // Test pustego ImięNazwisko
            try
            {
                var osoba = new Osoba("   "); // To powinno rzucić wyjątek
                Console.WriteLine("❌ Nie wykryto pustego ImięNazwisko!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✅ Wykryto pusté ImięNazwisko: {ex.Message}");
            }

            // Test osoby bez daty urodzenia
            var osobaBezDaty = new Osoba("Jan Kowalski");
            Console.WriteLine($"✅ Wiek osoby bez daty urodzenia: {osobaBezDaty.Wiek?.ToString() ?? "null"}");
        }
    }
}
