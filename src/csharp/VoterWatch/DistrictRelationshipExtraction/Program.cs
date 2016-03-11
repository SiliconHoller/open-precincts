using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistrictRelationshipExtraction.jointEntropy;

namespace DistrictRelationshipExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TownshipParents townships = new TownshipParents();
                townships.findParents();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
