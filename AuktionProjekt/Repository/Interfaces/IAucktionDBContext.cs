using Microsoft.Data.SqlClient;

namespace AuktionProjekt.Repository.Interfaces
{
    public interface IAucktionDBContext
    {
        SqlConnection GetConnection();
    }
}
