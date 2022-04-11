using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Dal.AdoNet
{
    public class DbConnection
    {
        protected string AllStoreConnection { get { return "Data Source=PROG22; Initial Catalog=ALLSTORE; Integrated Security=SSPI;"; } private set { } }
        //protected string AllStoreConnection { get { return "Data Source=SQL6010.site4now.net;Initial Catalog=DB_A65D8A_AllStoreDb;User Id=DB_A65D8A_AllStoreDb_admin;Password=Ultraaslan1905"; } private set { } }
    }
}
