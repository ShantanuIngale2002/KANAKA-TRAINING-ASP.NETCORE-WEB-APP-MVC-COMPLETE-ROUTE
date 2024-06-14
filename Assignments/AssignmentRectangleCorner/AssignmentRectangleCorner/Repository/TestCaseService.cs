using AssignmentRectangleCorner.Data;
using AssignmentRectangleCorner.Model;
using AssignmentRectangleCorner.Repository.EFCore;
using AssignmentRectangleCorner.Repository.Interface;

namespace AssignmentRectangleCorner.Repository
{
    public class TestCaseService : ITestCaseRepository
    {
        TestCaseDbContext dbcontext;
        public TestCaseService(TestCaseDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public List<List<float>> SplitTestCaseIntoMatrix(string input, int row, int col)
        {
            var matrix = new List<List<float>>();

            var values = input.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length != row * col)
            {
                // Handle invalid input
                throw new ArgumentException("Invalid input. Input length does not match row and column dimensions.");
            }

            int index = 0;
            for (int i = 0; i < row; i++)
            {
                var rowList = new List<float>();
                for (int j = 0; j < col; j++)
                {
                    rowList.Add(float.Parse(values[index]));
                    index++;
                }
                matrix.Add(rowList);
            }

            return matrix;
        }



        public float CalculateResult(List<List<float>> matrix, int row, int col)
        {
            float maxValue = 0;
            for(int i=0; i<row-1; i++)
            {
                for(int j=0; j<col-1; j++)
                {
                    float currMin = new List<float>() { matrix[i][j], matrix[i + 1][j], matrix[i][j + 1], matrix[i + 1][j + 1] }.Min();
                    if(currMin > maxValue) { maxValue = currMin; }
                }
            }
            return maxValue;
        }



        public void SaveToDB(TestCaseModel model, float resultingAnswer)
        {
            string resultingTestCase = "["+model.Row.ToString()+"]" + "\n" + "[" + model.Column.ToString() + "]" + "\n" + model.Testcase;
            TestCase newTestCase = new()
            {
                testcase = resultingTestCase,
                answer = resultingAnswer
            };
            dbcontext.TestCases.Add(newTestCase);
            dbcontext.SaveChanges();
        }






        public float PerformTheOperation(TestCaseModel model)
        {
            List<List<float>> matrix = SplitTestCaseIntoMatrix(model.Testcase, model.Row, model.Column);
            float result = CalculateResult(matrix, model.Row, model.Column);

            SaveToDB(model, result);

            return result;

        }



        public List<TestCaseModel> GetAllTestCases()
        {
            var data = (from tcs in dbcontext.TestCases
                        select new TestCaseModel
                        {
                            testcaseid = tcs.testcaseid,
                            Testcase = tcs.testcase,
                            Answer = tcs.answer,
                        }).ToList();
            return data;
        }

    }
}
