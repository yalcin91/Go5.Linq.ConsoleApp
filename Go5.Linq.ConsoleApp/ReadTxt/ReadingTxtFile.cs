using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Go5.Linq.ConsoleApp.ReadTxt
{
    public class ReadingTxtFile
    {
        public string ToReadTxt = @"D:\Users\Yalcin\Desktop\HBO5\Programmeren Gevorderd\Taak - 9) Linq\Txt\adresInfo.txt";
        private List<string> _Provincie = new List<string>();
        private List<string> _Gemeente = new List<string>();
        private List<string> _Straat = new List<string>();

        private Dictionary<int, Data> _Adres = new Dictionary<int, Data>();
        string[] parts;
        int count = 0;
        public int Id { get; set; }
        public void StreamRead()
        {
            string text = File.ReadAllText(ToReadTxt);
            text = text.Replace('\n', ',');
            text = text.Replace('\r', ' ');
            text = text.Replace(" ", string.Empty);
            parts = text.Split(',');
            foreach (string s in parts)
            {
                Data data = new Data();
                if (count == 0) _Provincie.Add(s);
                if (count == 1) _Gemeente.Add(s);
                if (count == 2) _Straat.Add(s);
                count++;
                if (count == 3)
                {
                    data.Provincie = _Provincie[Id];
                    data.Gemeente = _Gemeente[Id];
                    data.Straat = _Straat[Id];
                    _Adres.Add(Id, data);
                    Id++;
                    count = 0;
                }
            }
        }
        //***************************************************************************************************
        //***************************************************************************************************
        //***************************************************************************************************

        #region 1) Geef lijst met de provincienamen, alfabetisch gesorteerd.
        /// <summary>
        /// Geeft Provincie namen terug
        /// </summary>
        /// <returns>Provincie</returns>
        public IEnumerable<string> SorteerProvincienaamABC()
        {
            var v = _Adres.Values.OrderBy(x => x.Provincie).Select(x => x.Provincie).Distinct();
            foreach (var a in v)
            {
                //Console.WriteLine(a);
            }
            return v;
        }
        #endregion

        #region 2) Geef lijst van straatnamen voor opgegeven gemeente.
        /// <summary>
        /// Geeft de lijst straten voor gewenste gemeente.
        /// </summary>
        /// <param name="gemeente">Geef de gewenste gemeente in.</param>
        /// <returns>Straten</returns>
        public IEnumerable<string> LijstStraatNamenVoorGemeente(string gemeente)
        {
            var v = _Adres.Values.Where(x => x.Gemeente == gemeente).Select(x => x.Straat);
            foreach (var a in v)
            {
                //Console.WriteLine(a);
            }
            Console.WriteLine(count);
            return v;
        }
        #endregion

        #region 3) Selecteer de straatnaam die het meest keren voorkomt en druk voor elk voorkomen de pr, gm, strn. + sorteer.
        /// <summary>
        /// Geeft de lijst van meest voorkomend straat met pr, gm, strn
        /// en sorteer op basis van provincie en gemeente
        /// </summary>
        /// <returns></returns>
        public List<Data> MeestVoorkomendStraatNaam()
        {
            var adres = _Adres.Values.GroupBy(x => x.Straat).OrderByDescending(x=> x.Count()).First().
                OrderBy(x=> x.Provincie).ThenBy(x=> x.Gemeente).ToList();
            foreach (var a in adres)
            {
                Console.WriteLine(a.Provincie + " " + a.Gemeente + " " + a.Straat);
            }
            return adres;
        }
        #endregion

        #region 5) functie die voor 2 opgegeven gemeenten de gemeenschappelijke lijst van straatnamen
        /// <summary>
        /// Geeft de gemeenschappelijke straten terug
        /// </summary>
        /// <param name="gemeente1">Vul de eertse gemeente in</param>
        /// <param name="gemeente2">Vul de tweede gemeente in</param>
        /// <returns></returns>
        public IEnumerable<string> GemeenschappelijkeStratenVanTweeGemeenten(string gemeente1, string gemeente2)
        {
            var gem1 = _Adres.Values.Where(x => x.Gemeente == gemeente1).Select(x => x.Straat).ToList();
            var gem2 = _Adres.Values.Where(x => x.Gemeente == gemeente2).Select(x => x.Straat).ToList();
            var gelijk = gem1.Intersect(gem2);
            foreach (var a in gelijk)
            {
                Console.WriteLine(a);
            }
            return gelijk;
        }
        #endregion

        #region 6)  functie die de straatnamen weergeeft die enkel voorkomen in de opgegeven gemeente, maar niet in andere.
        /// <summary>
        /// Straatnamen die enkel voorkomen in geselecteerde gemeente
        /// maar die niet voorkomen in een lijst van andere gemeenten.
        /// </summary>
        /// <param name="gemeente">Vul de gewenste gemeente in</param>
        /// <returns></returns>
        public IEnumerable<string> StraatNamenVanOpgegevenGemeenteEnkel(string gemeente)
        {
            var nietGelijk = _Adres.Values.ToLookup(x => x.Straat).Where(x => x.Count() == 1).Select(x => x.First()).
                Where(x => x.Gemeente == gemeente).Select(x => x.Straat).ToList();
            foreach (var a in nietGelijk)
            {
                Console.WriteLine(a);
            }
            return nietGelijk;
        }
        #endregion

        #region 7) Maak een functie die de gemeente weergeeft met het hoogste aantal straatnamen.
        /// <summary>
        /// Dit is een functie die de gemeente weergeeft met het hoogste aantal straatnamen.
        /// </summary>
        /// <returns></returns>
        public string GemeenteHoogsteAantalStraatnamen()
        {
            var gemeente = _Adres.Values.GroupBy(x => x.Gemeente).Select(x => new { x.Key, n = x.Count() }).
                OrderByDescending(x => x.n).First();

            Console.WriteLine(gemeente);
            return gemeente.Key;
        }
        #endregion

        #region 8) Geef de langste straatnaam weer
        /// <summary>
        /// Dit geeft de langste straatnaam weer
        /// </summary>
        /// <returns></returns>
        public string LangsteStraatnaam()
        {
            var straat = _Adres.Values.Max(x => x.Straat.Count());
            var naam = _Adres.Values.Select(x => x.Straat).Where(x => x.Count() == straat);
            Console.WriteLine(naam.First());
            return straat.ToString();
        }
        #endregion

        #region 9)  Geeft de naast de langste straatnaam ook de gemeente en provincie weer.
        /// <summary>
        /// Geeft de langste straatnaam met de gemeente en provincie weer.
        /// </summary>
        /// <returns></returns>
        public string LangsteStraatMetGMenPR()
        {
            var straat = _Adres.Values.Max(x => x.Straat.Count());
            var naamStraat = _Adres.Values.Select(x => x.Straat).Where(x => x.Count() == straat).First();
            var adres = _Adres.Values.Where(x => x.Straat == naamStraat).
                Select(x => x.Provincie + " " + x.Gemeente + " " + x.Straat).First();
            Console.WriteLine(adres);
            return adres;
        }
        #endregion
    }
}