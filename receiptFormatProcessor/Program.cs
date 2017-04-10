using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace receiptFormatProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputType = "txt";
            var CompanyName = "Brandon's Bikes";
            var subtotal = 1000;
            var ItemizedList = "1 x Bike 1 = 1000";
            var tax = subtotal * 0.07725;
            var total = subtotal + tax;
            //var templateFile = new StreamReader(@"c:\users\brandon\documents\visual studio 2017\Projects\receiptFormatProcessor\receiptFormatProcessor\BBHtmlTemplate.txt");
            var templateFile = new StreamReader(@"c:\users\brandon\documents\visual studio 2017\Projects\receiptFormatProcessor\receiptFormatProcessor\BBStringTemplate.txt");
            var receiptTemplate = new List<string>();

            var headerProcessing = templateFile.ReadLine();
            //process the header information that may be needed in the receipt processing.
            if (headerProcessing.Contains("{outputType="))
            {
                //one off to determine if the first string has a declared output type
                var headers = headerProcessing.Split(';');
                var lengthofInput = headers[0].IndexOf('}') - headers[0].IndexOf('=');
                outputType = headers[0].Substring(headers[0].IndexOf('=')+1, lengthofInput-1);

            }

            while (!templateFile.EndOfStream) {
                var input = templateFile.ReadLine();

                receiptTemplate.Add(input);
            }

            var outputFile = new StreamWriter(@"c:\users\brandon\documents\visual studio 2017\Projects\receiptFormatProcessor\receiptFormatProcessor\stringReceipt." + outputType);
            foreach (var line in receiptTemplate)
            {
                var outputLine = line.Replace("{companyName}", CompanyName);
                outputLine = outputLine.Replace("{ItemizedList}", ItemizedList);
                outputLine = outputLine.Replace("{subtotal}", subtotal.ToString());
                outputLine = outputLine.Replace("{tax}", tax.ToString());
                outputLine = outputLine.Replace("{total}", total.ToString());
                outputFile.WriteLine(outputLine);
            }
            //process the receipt template to look for keywords
            outputFile.Close();
        }
    }
}
