using OkumaUygulamasi.API.Models;

namespace OkumaUygulamasi.Tests;

public class QuestionTests
{
    [Fact]
    public void CorrectAnswer_ShouldMatch_RegardlessOfCaseSensitivity()
    {
        var question = new Question
        {
            Id = 1,
            CorrectAnswer = "Gül",
            Points = 10
        };

        string childsAnswer = "gÜl";
        bool isCorrect = string.Equals(question.CorrectAnswer, childsAnswer, StringComparison.CurrentCultureIgnoreCase);

        Assert.True(isCorrect);
    }
}
