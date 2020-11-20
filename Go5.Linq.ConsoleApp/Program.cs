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

            //readingTxtFile.SorteerProvincienaamABC();
            //readingTxtFile.LijstStraatNamenVoorGemeente("Antwerpen");
            readingTxtFile.MeestVoorkomendStraatNaam();
        }
    }
}
