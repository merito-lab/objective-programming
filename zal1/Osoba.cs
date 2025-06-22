using System;

namespace zal_1
{
    public class Osoba
    {
        private string imię = string.Empty;
        private DateTime? dataUrodzenia = null;
        private DateTime? dataŚmierci = null;

        public Osoba(string imięNazwisko)
        {
            ImięNazwisko = imięNazwisko;
        }

        public string Imię
        {
            get => imię;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Imię nie może być puste!");
                imię = value;
            }
        }

        public string Nazwisko { get; set; } = string.Empty;

        public DateTime? DataUrodzenia 
        { 
            get => dataUrodzenia;
            set 
            {
                if (value.HasValue && dataŚmierci.HasValue && value > dataŚmierci)
                    throw new ArgumentException("Data urodzenia nie może być późniejsza niż data śmierci!");
                dataUrodzenia = value;
            }
        }

        public DateTime? DataŚmierci 
        { 
            get => dataŚmierci;
            set 
            {
                if (value.HasValue && dataUrodzenia.HasValue && value < dataUrodzenia)
                    throw new ArgumentException("Data śmierci nie może być wcześniejsza niż data urodzenia!");
                dataŚmierci = value;
            }
        }

        public string ImięNazwisko
        {
            get => $"{Imię} {Nazwisko}".Trim();
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Imię i nazwisko nie mogą być puste!");

                string[] parts = value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                if (parts.Length == 0)
                    throw new ArgumentException("Imię i nazwisko nie mogą być puste!");
                
                if (parts.Length == 1)
                {
                    Imię = parts[0];
                    Nazwisko = string.Empty;
                }
                else
                {
                    Imię = parts[0];
                    Nazwisko = parts[parts.Length - 1];
                }
            }
        }

        public TimeSpan? Wiek
        {
            get
            {
                if (DataUrodzenia == null)
                    return null;

                DateTime dataKońcowa = DataŚmierci ?? DateTime.Now;
                
                return dataKońcowa - DataUrodzenia.Value;
            }
        }

        public override string ToString()
        {
            string info = ImięNazwisko;
            
            if (DataUrodzenia.HasValue)
            {
                info += $" (ur. {DataUrodzenia.Value:yyyy-MM-dd}";
                
                if (DataŚmierci.HasValue)
                    info += $" - zm. {DataŚmierci.Value:yyyy-MM-dd})";
                else
                    info += ")";
            }
            
            if (Wiek.HasValue)
            {
                int lata = (int)(Wiek.Value.TotalDays / 365.25);
                info += $" - {lata} lat";
            }
            
            return info;
        }
    }
} 