using EzhaBy.Business.Tags;
using EzhaBy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.UnitTests.CQTests
{
    [TestClass]
    public class TagsTests
    {
        private DataContext context;

        public TagsTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Db")
                .Options;

            context = new DataContext(options);
        }

        [TestMethod]
        public void CanCreateTagAsync()
        {
            var tagName = "tag";

            var handler = new CreateTag.Handler(context);
            var command = new CreateTag.Command(tagName);

            var cancellationToken = new CancellationToken();

            _ = handler.Handle(command, cancellationToken);

            var expectedResult = context.Tags.FirstOrDefaultAsync(tag => tag.TagName == tagName);

            Assert.IsNotNull(expectedResult);
        }
    }
}
