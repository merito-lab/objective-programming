using System;
using System.Collections.Generic;
using System.Text;

namespace zal_3
{
    public class Macierz<T> : IEquatable<Macierz<T>> where T : IEquatable<T>
    {
        private readonly T[,] dane;
        private readonly int wiersze;
        private readonly int kolumny;

        public Macierz(int wiersze, int kolumny)
        {
            if (wiersze <= 0)
                throw new ArgumentException("Liczba wierszy musi być większa od zera", nameof(wiersze));
            if (kolumny <= 0)
                throw new ArgumentException("Liczba kolumn musi być większa od zera", nameof(kolumny));

            VerifyTIsComparable();

            this.wiersze = wiersze;
            this.kolumny = kolumny;
            dane = new T[wiersze, kolumny];
        }

        private static void VerifyTIsComparable()
        {
            Type typeT = typeof(T);
            Type equatableInterface = typeof(IEquatable<T>);
            
            if (!equatableInterface.IsAssignableFrom(typeT))
            {
                throw new InvalidOperationException(
                    $"Typ {typeT.Name} nie implementuje IEquatable<{typeT.Name}>. " +
                    "Macierz może przechowywać tylko typy porównywalne.");
            }

            var equalsMethod = typeT.GetMethod("Equals", new[] { typeT });
            if (equalsMethod == null)
            {
                throw new InvalidOperationException(
                    $"Typ {typeT.Name} nie ma metody Equals({typeT.Name}).");
            }

            Console.WriteLine($"✅ Weryfikacja zakończona: Typ {typeT.Name} jest equatable.");
        }

        public int Wiersze => wiersze;
        public int Kolumny => kolumny;

        public T this[(int wiersz, int kolumna) indeks]
        {
            get
            {
                var (wiersz, kolumna) = indeks;
                if (wiersz < 0 || wiersz >= wiersze)
                    throw new IndexOutOfRangeException($"Indeks wiersza {wiersz} jest poza zakresem [0, {wiersze - 1}]");
                if (kolumna < 0 || kolumna >= kolumny)
                    throw new IndexOutOfRangeException($"Indeks kolumny {kolumna} jest poza zakresem [0, {kolumny - 1}]");
                return dane[wiersz, kolumna];
            }
            set
            {
                var (wiersz, kolumna) = indeks;
                if (wiersz < 0 || wiersz >= wiersze)
                    throw new IndexOutOfRangeException($"Indeks wiersza {wiersz} jest poza zakresem [0, {wiersze - 1}]");
                if (kolumna < 0 || kolumna >= kolumny)
                    throw new IndexOutOfRangeException($"Indeks kolumny {kolumna} jest poza zakresem [0, {kolumny - 1}]");
                dane[wiersz, kolumna] = value;
            }
        }

        public static bool operator ==(Macierz<T> lewa, Macierz<T> prawa)
        {
            if (ReferenceEquals(lewa, prawa))
                return true;
            if (lewa is null || prawa is null)
                return false;

            if (lewa.wiersze != prawa.wiersze || lewa.kolumny != prawa.kolumny)
                return false;

            for (int i = 0; i < lewa.wiersze; i++)
            {
                for (int j = 0; j < lewa.kolumny; j++)
                {
                    if (!EqualityComparer<T>.Default.Equals(lewa.dane[i, j], prawa.dane[i, j]))
                        return false;
                }
            }
            return true;
        }

        public static bool operator !=(Macierz<T> lewa, Macierz<T> prawa)
        {
            return !(lewa == prawa);
        }

        public bool Equals(Macierz<T>? other)
        {
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            return obj is Macierz<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            int hash = HashCode.Combine(wiersze, kolumny);
            for (int i = 0; i < wiersze; i++)
            {
                for (int j = 0; j < kolumny; j++)
                {
                    hash = HashCode.Combine(hash, dane[i, j]);
                }
            }
            return hash;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < wiersze; i++)
            {
                sb.Append('[');
                for (int j = 0; j < kolumny; j++)
                {
                    sb.Append(dane[i, j]?.ToString() ?? "null");
                    if (j < kolumny - 1)
                        sb.Append(", ");
                }
                sb.Append(']');
                if (i < wiersze - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
} 