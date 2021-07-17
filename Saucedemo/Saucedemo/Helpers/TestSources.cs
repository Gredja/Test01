using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Saucedemo.Helpers
{
    public class TestSources
    {
        public static IEnumerable<TestCaseData> AddCredentialsAndSortType()
        {
            yield return new TestCaseData("standard_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.AtoZ).Value);
            yield return new TestCaseData("problem_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.AtoZ).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.AtoZ).Value);

            yield return new TestCaseData("standard_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.ZtoA).Value);
            yield return new TestCaseData("problem_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.ZtoA).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.ZtoA).Value);

            yield return new TestCaseData("standard_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.LowToHigh).Value);
            yield return new TestCaseData("problem_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.LowToHigh).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.LowToHigh).Value);

            yield return new TestCaseData("standard_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.HighToLow).Value);
            yield return new TestCaseData("problem_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.HighToLow).Value);
            yield return new TestCaseData("performance_glitch_user", "secret_sauce",
                Helpers.SortType.First(x => x.Key == Helpers.Sort.HighToLow).Value);
        }

        public static IEnumerable<TestCaseData> AddProductDescription()
        {
            for (var i = 1; i <= 6; i++)
            {
                yield return new TestCaseData(i);
            }
        }

        public static IEnumerable<TestCaseData> AddCredentials()
        {
            yield return new TestCaseData("standard_user", "secret_sauce");
            yield return new TestCaseData("problem_user", "secret_sauce");
            yield return new TestCaseData("performance_glitch_user", "secret_sauce");
        }
    }
}
