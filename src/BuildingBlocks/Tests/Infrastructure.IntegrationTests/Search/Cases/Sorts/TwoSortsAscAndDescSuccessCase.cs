using System.Collections.Generic;
using System.Linq;
using HelpLine.BuildingBlocks.Application.Search;
using HelpLine.BuildingBlocks.Infrastructure.IntegrationTests.Search.SeedWork;

namespace HelpLine.BuildingBlocks.Infrastructure.IntegrationTests.Search.Cases.Sorts
{
    public class TwoSortsAscAndDescSuccessCase : SortCaseBase
    {
        public override IEnumerable<TestModel> ExpectedResult =>
            Data.OrderBy(x => x.Bool).ThenByDescending(x => x.Id);

        public override IEnumerable<Sort> Sorts => new[]
        {
            new Sort(true, nameof(TestModel.Bool)),
            new Sort(false, nameof(TestModel.Id))
        };
    }
}
