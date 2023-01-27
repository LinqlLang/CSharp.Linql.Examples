using Linql.Client;
using Linql.Core;
using Linql.Core.Test;
using Linql.Test.Files;
using System.Reflection;
using System.Text.Json;

namespace Linql.Server.Test
{
    public class ApplicationTest : TestFileTests
    {
        public IQueryable<DataModel> Data { get; set; }

        public LinqlCompiler Compiler { get; set; }

        [OneTimeSetUp]
        public override async Task Setup()
        {
            await base.Setup();
            List<DataModel> dataList = new List<DataModel>();
            dataList.Where(r => true).ToList();
            foreach (int index in Enumerable.Range(1, 100))
            {
                DataModel data = new CompiledDataModel(index, true);
                dataList.Add(data);
            }

            Data = dataList.AsQueryable();

            List<Assembly> assemblies = new List<Assembly>()
            {
                typeof(Boolean).Assembly,
                typeof(Enumerable).Assembly,
                typeof(Queryable).Assembly
            };
            this.Compiler = new LinqlCompiler(assemblies);


        }

        [Test]
        public void WhereFalse()
        {
            string json = this.TestLoader.TestFiles["SimpleBooleanFalse"];
            LinqlSearch? search = JsonSerializer.Deserialize<LinqlSearch>(json);

            Assert.DoesNotThrow(() =>
            {
                IEnumerable<DataModel> data = this.Compiler.Execute<IEnumerable<DataModel>>(search, this.Data);

                Assert.That(data.Count(), Is.EqualTo(0));
            });
        }

        [Test]
        public void WhereFalseQueryable()
        {
            string json = this.TestLoader.TestFiles["SimpleBooleanFalse"];
            LinqlSearch? search = JsonSerializer.Deserialize<LinqlSearch>(json);

            Assert.DoesNotThrow(() =>
            {
                IQueryable<DataModel> data = this.Compiler.Execute<IQueryable<DataModel>>(search, this.Data);

                Assert.That(data.Count(), Is.EqualTo(0));
            });
        }

        [Test]
        public async Task WhereFalseLinqlSearch()
        {
            string json = this.TestLoader.TestFiles["SimpleBooleanFalse"];
            LinqlSearch? search = JsonSerializer.Deserialize<LinqlSearch>(json);

            LinqlSearch<DataModel> data = new LinqlSearch<DataModel>();

            IQueryable<DataModel> result = this.Compiler.Execute<IQueryable<DataModel>>(search, data);
            string test = await result.ToJsonAsync();
            this.TestLoader.Compare("SimpleBooleanFalse", test);
        }

        [Test]
        public async Task SimpleBooleanPropertyInception()
        {
            string json = this.TestLoader.TestFiles["SimpleBooleanProperty"];
            LinqlSearch? search = JsonSerializer.Deserialize<LinqlSearch>(json);

            LinqlSearch<DataModel> data = new LinqlSearch<DataModel>();

            IQueryable<DataModel> result = this.Compiler.Execute<IQueryable<DataModel>>(search, data);
            string test = await result.ToJsonAsync();
            this.TestLoader.Compare("SimpleBooleanProperty", test);
        }

        [Test]
        public async Task SimpleBooleanPropertyData()
        {
            string json = this.TestLoader.TestFiles["SimpleBooleanProperty"];
            LinqlSearch? search = JsonSerializer.Deserialize<LinqlSearch>(json);

            IQueryable<DataModel> data = this.Data;

            IQueryable<DataModel> result = this.Compiler.Execute<IQueryable<DataModel>>(search, data);
            List<DataModel> compiledResult = result.ToList();
            Assert.That(result.Count(), Is.EqualTo(this.Data.Count() / 2));

        }
    }
}