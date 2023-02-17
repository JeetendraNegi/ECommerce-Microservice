using TechTalk.SpecFlow;

namespace SpecFlowAcceptanceTest.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        int number1 = 0;
        int number2 = 0;
        int add = 0;

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            number1 = number;
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            number2 = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
           add = number1 + number2;
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            result.Equals(add);
        }
    }
}