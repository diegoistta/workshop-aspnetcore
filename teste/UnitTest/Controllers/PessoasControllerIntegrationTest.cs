
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAPIApplication;
using Xunit;

namespace UnitTest
{
    public class PessoasControllerIntegrationTest : BaseIntegrationTest
    {   
        private const string BaseUrl = "api/pessoas";
        public PessoasControllerIntegrationTest(BaseTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task DeveRetornarListaPessoasVazia()
        {
            var response = await Client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);

            Assert.Equal(data.Count, 0);
        }

        [Fact]
        public async Task DeveRetornarListaPessoas()
        {
           
           var pessoa = new Pessoa
           {
                Nome = "Fulano de Tal",
                Twitter = "fulano"
           };
           
           await TestDataContext.AddAsync(pessoa);
           await TestDataContext.SaveChangesAsync();
           
            var response = await Client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);

            Assert.Equal(data.Count, 1);
            Assert.Contains( data, x => x.Nome == pessoa.Nome);
        }
    }
}