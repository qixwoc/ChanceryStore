using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore
{

        /// <summary>
        /// Содержит данные о подключении
        /// </summary>
        public abstract class Connection 
        {
            /// <summary>
            /// Строка подключения к БД
            /// </summary>
            public static string ConnectionString { get; set; } = @" data source = (localdb)\MSSQLLocalDB;Initial Catalog = ChanceryStoreDB; Integrated Security = True;";
        

    
    }
    
}
