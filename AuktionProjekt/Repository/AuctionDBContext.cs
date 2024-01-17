using AuktionProjekt.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace AuktionProjekt.Repository
{
    public class AuctionDBContext : IAucktionDBContext
    {
        private readonly string? _DBContext;

        public AuctionDBContext(IConfiguration configuration)
        {
            _DBContext = configuration.GetConnectionString("AuctionDbConnection");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._DBContext);
        }
    }
}
