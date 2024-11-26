using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class IntegrationTestsBase : IDisposable
    {
        protected readonly DbContextOptions<FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Infra.Context> _options;
        internal readonly SqlServerTestFixture _sqlserverTest;

        public IntegrationTestsBase()
        {
            // Do "global" initialization here; Called before every test method.
            _sqlserverTest = new SqlServerTestFixture(
                imageNameMssqlTools: "fdelima/fiap-pos-techchallenge-micro-servico-producao-gurpo-71-scripts-database:fase4-test",
                containerNameMssqlTools: "mssql-tools-producao-test",
                databaseContainerName: "sqlserver-db-producao-test", port: "1434");
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
            _sqlserverTest.Dispose();
        }
    }
}
