using NRL_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Http;

namespace NRL_WebApi.Controllers
{
    public class languageAPIController : ApiController
    {
        public List<language> Get()
        {
            List<language> languages = new List<language>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT  languageID, languageName FROM tbl_language";
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (rdr.Read())
                    {
                        language lang = new language();
                        lang.languageID = Convert.ToInt32(rdr["languageID"]);
                        lang.languageName = rdr["languageName"].ToString();
                        languages.Add(lang);
                    }
                    conn.Close();
                    return languages;
                }
            }
        }


    }
       
}
