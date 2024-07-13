using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithWebTables
{
    public class TestDataGenerator
    {
        public static IEnumerable<TestCaseData> GenerateTestCaseDataFromCsv()
        {
            string binFolder = AppDomain.CurrentDomain.BaseDirectory;
            string csvFilePath = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

            if (!File.Exists(csvFilePath))
            {
                throw new FileNotFoundException("The CSV file was not found.", csvFilePath);
            }

            List<TestCaseData> testCaseDataList = new List<TestCaseData>();

            foreach (var line in File.ReadLines(csvFilePath))
            {
                var parts = line.Split(new[] { ',' }, 2); // Split line by the first comma
                if (parts.Length == 2)
                {
                    string name = parts[0].Trim();
                    string price = parts[1].Trim();
                    testCaseDataList.Add(new TestCaseData(name, price));
                }
            }

            return testCaseDataList;
        }
    }
}
