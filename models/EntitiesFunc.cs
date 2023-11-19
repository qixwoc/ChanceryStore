using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore.models
{
    class EntitiesFunc
    {
        private SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
    }
}
