ValidateProductInfo test is using the predefined ProductData testcase data.
This is one way to use parametrization for your tests.

The other way is to generate that data from a file:
TestDataGenerator.cs is a class that have a method inside it which
extracts the data from a file and returns IEnumerable<TestCaseData>.
By doing this you call this method in your test:
[Test, TestCaseSource(typeof(TestDataGenerator), nameof(TestDataGenerator.GenerateTestCaseDataFromCsv))]
and this will be your parameters for the test.