using System;

namespace zal_2a
{
    public class Wektor
    {
        private double[] współrzędne;

        public Wektor(byte wymiar)
        {
            if (wymiar == 0)
                throw new ArgumentException("Wymiar wektora musi być większy od zera", nameof(wymiar));
            
            współrzędne = new double[wymiar];
        }

        public Wektor(params double[] współrzędne)
        {
            if (współrzędne == null)
                throw new ArgumentNullException(nameof(współrzędne));
            if (współrzędne.Length == 0)
                throw new ArgumentException("Wektor musi mieć co najmniej jeden wymiar", nameof(współrzędne));
            
            this.współrzędne = new double[współrzędne.Length];
            Array.Copy(współrzędne, this.współrzędne, współrzędne.Length);
        }

        public double Długość 
        { 
            get 
            { 
                return Math.Sqrt(IloczynSkalarny(this, this));
            } 
        }

        public byte Wymiar 
        { 
            get 
            { 
                return (byte)współrzędne.Length; 
            } 
        }

        public double this[byte index]
        {
            get
            {
                if (index >= współrzędne.Length)
                    throw new IndexOutOfRangeException($"Indeks {index} jest poza zakresem wektora o wymiarze {Wymiar}");
                return współrzędne[index];
            }
            set
            {
                if (index >= współrzędne.Length)
                    throw new IndexOutOfRangeException($"Indeks {index} jest poza zakresem wektora o wymiarze {Wymiar}");
                współrzędne[index] = value;
            }
        }

        public static double IloczynSkalarny(Wektor V, Wektor W)
        {
            if (V == null) throw new ArgumentNullException(nameof(V));
            if (W == null) throw new ArgumentNullException(nameof(W));
            
            if (V.Wymiar != W.Wymiar)
                return double.NaN;

            double suma = 0;
            for (byte i = 0; i < V.Wymiar; i++)
            {
                suma += V[i] * W[i];
            }
            return suma;
        }

        public static Wektor Suma(params Wektor[] wektory)
        {
            if (wektory == null)
                throw new ArgumentNullException(nameof(wektory));
            if (wektory.Length == 0)
                throw new ArgumentException("Brak wektorów do zsumowania", nameof(wektory));

            for (int i = 0; i < wektory.Length; i++)
            {
                if (wektory[i] == null)
                    throw new ArgumentNullException($"wektory[{i}]", "Jeden z wektorów jest null");
            }

            byte wymiar = wektory[0].Wymiar;
            
            foreach (var wektor in wektory)
            {
                if (wektor.Wymiar != wymiar)
                    throw new ArgumentException("Wszystkie wektory muszą mieć ten sam wymiar");
            }

            Wektor wynik = new Wektor(wymiar);
            
            for (byte i = 0; i < wymiar; i++)
            {
                double suma = 0;
                foreach (var wektor in wektory)
                {
                    suma += wektor[i];
                }
                wynik[i] = suma;
            }

            return wynik;
        }

        public static Wektor operator +(Wektor V, Wektor W)
        {
            if (V == null) throw new ArgumentNullException(nameof(V));
            if (W == null) throw new ArgumentNullException(nameof(W));
            if (V.Wymiar != W.Wymiar)
                throw new ArgumentException("Wektory muszą mieć ten sam wymiar");

            Wektor wynik = new Wektor(V.Wymiar);
            for (byte i = 0; i < V.Wymiar; i++)
            {
                wynik[i] = V[i] + W[i];
            }
            return wynik;
        }

        public static Wektor operator -(Wektor V, Wektor W)
        {
            if (V == null) throw new ArgumentNullException(nameof(V));
            if (W == null) throw new ArgumentNullException(nameof(W));
            if (V.Wymiar != W.Wymiar)
                throw new ArgumentException("Wektory muszą mieć ten sam wymiar");

            Wektor wynik = new Wektor(V.Wymiar);
            for (byte i = 0; i < V.Wymiar; i++)
            {
                wynik[i] = V[i] - W[i];
            }
            return wynik;
        }

        public static Wektor operator *(Wektor V, double skalar)
        {
            if (V == null) throw new ArgumentNullException(nameof(V));
            
            Wektor wynik = new Wektor(V.Wymiar);
            for (byte i = 0; i < V.Wymiar; i++)
            {
                wynik[i] = V[i] * skalar;
            }
            return wynik;
        }

        public static Wektor operator *(double skalar, Wektor V)
        {
            return V * skalar;
        }

        public static Wektor operator /(Wektor V, double skalar)
        {
            if (V == null) throw new ArgumentNullException(nameof(V));
            if (Math.Abs(skalar) < double.Epsilon)
                throw new DivideByZeroException("Nie można dzielić przez zero");

            Wektor wynik = new Wektor(V.Wymiar);
            for (byte i = 0; i < V.Wymiar; i++)
            {
                wynik[i] = V[i] / skalar;
            }
            return wynik;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", współrzędne)}]";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Wektor other)
            {
                if (this.Wymiar != other.Wymiar) return false;
                
                for (byte i = 0; i < Wymiar; i++)
                {
                    if (Math.Abs(this[i] - other[i]) > double.Epsilon)
                        return false;
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = Wymiar.GetHashCode();
            for (byte i = 0; i < Wymiar; i++)
            {
                hash = hash * 31 + współrzędne[i].GetHashCode();
            }
            return hash;
        }
    }
} 