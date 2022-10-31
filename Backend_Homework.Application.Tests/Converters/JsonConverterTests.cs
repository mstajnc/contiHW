using Backend_Homework.Application.BusinessObjects;
using Backend_Homework.Application.Converters.Json;

namespace Backend_Homework.Application.Tests.Converters
{
    public class JsonConverterTests
    {
        private readonly string ValidDocumentJson = "{\"Title\":\"nazev\",\"Text\":\"nahodnyText\"}";

        [Fact]
        public async Task ShouldConvertValidDocumentJson()
        {
            var converter = new JsonConverter();

            var result = await converter.ParseText<Document>(ValidDocumentJson);

            Assert.Equal("nazev", result.Title);
            Assert.Equal("nahodnyText", result.Text);
        }

        [Fact]
        public async Task ShouldConvertDocumentToValidDocumentJson()
        {
            var converter = new JsonConverter();
            var document = new Document()
            {
                Title = "nazev",
                Text = "nahodnyText"
            };

            var result = await converter.Convert(document);

            Assert.Equal(ValidDocumentJson, result);
        }
    }
}