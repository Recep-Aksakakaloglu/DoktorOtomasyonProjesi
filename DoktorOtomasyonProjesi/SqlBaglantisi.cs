using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DoktorOtomasyonProjesi
{
    class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-OS1BPQI;Initial Catalog=DbDoktorOtomasyon;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
