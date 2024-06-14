using AssignmentRectangleCorner.Model;
using System.Security.Policy;

namespace AssignmentRectangleCorner.Repository.Interface
{
    public interface ITestCaseRepository
    {
        public float PerformTheOperation(TestCaseModel model);

        public List<TestCaseModel> GetAllTestCases();

    }
}
