using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolgozat_211004
{
    public enum Markak
    {
        Toyota,
        BMW,
        Skoda,
        Suzuki,
        Fiat
    }

    class Auto
    {
        private double _altagFogyasztas;
        private int _tankMeret;
        private int _aktualisBenzin;
        private Markak _marka;
        private bool _matrica;
        private string _rendszam;

        public double AltagFogyasztas 
        { 
            get => _altagFogyasztas;
            set
            {
                if (value < 3)
                    throw new Exception("hiba: Túl kicsi az átlag fogyasztás!");
                if (value > 20)
                    throw new Exception("hiba: Túl nagy az átlag fogyasztás!");
                _altagFogyasztas = value;
            }
        }
        public int TankMeret 
        { 
            get => _tankMeret;
            set 
            {
                if (value < 20)
                    throw new Exception("hiba: Túl kicsi a tank mérete!");
                if (value > 100)
                    throw new Exception("hiba: Túl nagy a zank mérete!");
                _tankMeret = value;
            }
        }
        public int AktualisBenzin 
        { 
            get => _aktualisBenzin;
            set 
            {
                if (value < 0)
                    throw new Exception("hiba: Nem lehet 0 alatt a benzin mennyisége!");s
                if (value > _tankMeret)
                    throw new Exception("hiba: Nem lehet több benzin mint a tank mérete!");
                _aktualisBenzin = value;
            }
        }
        public Markak Marka { get => _marka; set => _marka = value; }
        public bool Matrica{ get => _matrica; set => _matrica = value; }
        public string Rendszam { get => _rendszam; set => _rendszam = value; }
    }

    class Program
    {
        static Random rnd = new Random();
        static List<Auto> autok = new List<Auto>();

        static void Main(string[] args)
        {
            var a = new Auto();
            Feltoltes();
            Kiiras();
            LegtobbKilometer();
            LeggyakoribbMarka();
            Console.ReadKey();
        }

        private static void Feltoltes()
        {
            for (int i = 0; i < 30; i++)
            {
                autok.Add(new Auto()
                {
                    AltagFogyasztas = rnd.Next(3,21),
                    TankMeret = rnd.Next(20,101),
                    AktualisBenzin = rnd.Next (20,autok[i].TankMeret),
                    Marka = (Markak)rnd.Next(Enum.GetNames(typeof(Markak)).Length),
                    Matrica = rnd.Next(1) == 1,
                    Rendszam = "igen",//RendszamLetrehozas(),
                });
            }
        }

        private static string RendszamLetrehozas()
        {
            var generalas = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                int ascii_index = rnd.Next(65, 91);
                char betu = Convert.ToChar(ascii_index);
                generalas.Append(betu);
            }
            generalas.Append('-');
            for (int i = 0; i < 3; i++)
            {
                int szam = rnd.Next(10);
                generalas.Append(szam);
            }
            string rendszam = Convert.ToString(generalas);
            return rendszam;
            
        }

        private static void Kiiras()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{6}",autok[i].AltagFogyasztas,autok[i].TankMeret,autok[i].AktualisBenzin,autok[i].Marka,autok[i].Matrica,autok[i].Rendszam);
            }
        }
        private static void LegtobbKilometer()
        {
            int index = 0;
            double max = 0;
            for (int i = 0; i < autok.Count; i++)
            {
                double aktmax = autok[i].AktualisBenzin / autok[i].AltagFogyasztas;
                if (max < aktmax)
                {
                    max = aktmax;
                    index = i;
                }
            }
            Console.WriteLine("{0} rendszámú autó képes a legtöbb kilómétert megtenni",autok[index].Rendszam);
        }

        private static void LeggyakoribbMarka()
        {
            var markak = new Dictionary<string, int>()
            {
                {"Toyota",0},
                {"BMW",0},
                {"Skoda",0},
                {"Suzuki",0},
                {"Fiat",0},
            };
            for (int i = 0; i < autok.Count; i++)
            {
                if (autok[i].Matrica == true)
                {
                    if (autok[i].Marka == Markak.Toyota)
                    {
                        markak["Toyota"] = +1;
                    }
                    if (autok[i].Marka == Markak.BMW)
                    {
                        markak["BMW"] = +1;
                    }
                    if (autok[i].Marka == Markak.Skoda)
                    {
                        markak["Skoda"] = +1;
                    }
                    if (autok[i].Marka == Markak.Suzuki)
                    {
                        markak["Suzuki"] = +1;
                    }
                    if (autok[i].Marka == Markak.Fiat)
                    {
                        markak["Fiat"] = +1;
                    }
                }
            }
            Console.WriteLine("{0} autóból van a legtöbb azok között, akik rendelkeznek matricával",markak.Values.Max());
        }
    }
}
