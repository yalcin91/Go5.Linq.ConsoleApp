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

        #region 1) Geef lijst met de provincienamen, alfabetisch gesorteerd.
        /// <summary>
        /// Geeft Provincie namen terug
        /// </summary>
        /// <returns>Provincie</returns>
        public IEnumerable<string> SorteerProvincienaamABC()
        {
            var v = _Adres.Values.OrderBy(x => x.Provincie).Select(x => x.Provincie).Distinct();
            foreach(var a in v)
            {
                Console.WriteLine(a);
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
            var v = _Adres.Values.Where(x => x.Gemeente == gemeente).Select(x=> x.Provincie);
            foreach(var a in v)
            {
                Console.WriteLine(a);
            }
            return v;
        }
        #endregion

        #region 3) Selecteer de straatnaam die het meest keren voorkomt en druk voor elk voorkomen de pr, gm, stn. Sortering op basis van provincie en gemeente.
        public IEnumerable<string> MeestVoorkomendStraatNaam()
        {
            var straatNaam = _Adres.Values.ToLookup(x=> x.Straat);
            var straatNaamMax = straatNaam.Max(x=> x.Count());
            var straatNaamMaxWaar = straatNaam.Where(x=> x.Count() == straatNaamMax).Select(x => x.Key).ToList();
            var adressen = _Adres.Values.Where(x => x.Straat == straatNaamMaxWaar.ToString()).Select(x => x.Provincie).ToList();
            foreach (var a in straatNaamMaxWaar)
            {
                adressen = _Adres.Values.Where(x => x.Straat == a).Select(x =>  x.Provincie + " " + x.Gemeente +
                                                                          " " + x.Straat ).ToList();
            }
            foreach(var a in adressen)
            {
                Console.WriteLine(a);
            }

             return adressen;
        }
        #endregion

    }
}
