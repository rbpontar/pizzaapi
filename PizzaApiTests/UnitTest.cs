using System.Net.Http;
using PizzaApi;
using PizzaApi.Controllers;
using PizzaApi.Models;
using PizzaApi.Repositories;
using PizzaApi.Services;
using Xunit;




namespace PizzaApiTests
{

    public class UnitTest1
    {

        private ProdutoRepo produtoRepo;
        private ProdutoService pedidoService;
        private PedidoController pedidoController;
        private readonly HttpClient client;

        private readonly WebApplicationFactory<Startup> _factory;


        public UnitTest1() {

            var appFactory = new WebApplicationFactory<Startup>().CreateCliente();

        }

        private void Setup()
        {
            pedidoService = new ProdutoService();
            //pedidoController = new PedidoController();
        }


        //private async Task<ApiContext> GetDatabaseContext()
        //{

           

        //    var options = new DbContextOptionsBuilder<ApiContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var databaseContext = new ApiContext(options);

        //    databaseContext.Database.EnsureCreated();

        //    if (await databaseContext.Users.CountAsync() <= 0)
        //    {
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            databaseContext.Users.Add(new User()
        //            {
        //                Id = i,
        //                Email = $"testuser{i}@example.com",
        //                IsLocked = false,
        //                CreatedBy = "SYSTEM",
        //                CreatedDate = DateTime.UtcNow
        //            });
        //            await databaseContext.SaveChangesAsync();
        //        }
        //    }
        //    return databaseContext;
        //}


        [Fact]
        public void Test1()
        {
            Setup();
            AdicionarProdutos();
        }


        #region CanGetItems
        [Fact]
        public void Can_get_items()
        {
            using (var context = Fixture.CreateContext())
            {
                var controller = new ItemsController(context);

                var items = controller.Get().ToList();

                Assert.Equal(3, items.Count);
                Assert.Equal("ItemOne", items[0].Name);
                Assert.Equal("ItemThree", items[1].Name);
                Assert.Equal("ItemTwo", items[2].Name);
            }
        }
        #endregion

        private async void AdicionarProdutos()
        {
            await produtoRepo.Gravar(new Produto("3 Queijos", 50));
            await produtoRepo.Gravar(new Produto("Frango com requeijão", 59.99));
            await produtoRepo.Gravar(new Produto("Mussarela", 42.50));
            await produtoRepo.Gravar(new Produto("Calabresa", 42.50));
            await produtoRepo.Gravar(new Produto("Pepperoni", 55));
            await produtoRepo.Gravar(new Produto("Veggie", 59.99));
        }
    }
}
