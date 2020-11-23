using System;

using Go5.Linq.ConsoleApp.ReadTxt;

namespace Go5.Linq.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadingTxtFile readingTxtFile = new ReadingTxtFile();
            readingTxtFile.StreamRead();

            //readingTxtFile.SorteerProvincienaamABC();                                              // 1
            //readingTxtFile.LijstStraatNamenVoorGemeente("Antwerpen");                              // 2
            readingTxtFile.MeestVoorkomendStraatNaam();                                            // 3
            //readingTxtFile.GemeenschappelijkeStratenVanTweeGemeenten("Aartselaar", "Antwerpen");   // 5
            //readingTxtFile.StraatNamenVanOpgegevenGemeenteEnkel("Antwerpen");                      // 6
            //readingTxtFile.GemeenteHoogsteAantalStraatnamen();                                     // 7
            //readingTxtFile.LangsteStraatnaam();                                                    // 8
            //readingTxtFile.LangsteStraatMetGMenPR();                                               // 9
        }
    }
}
