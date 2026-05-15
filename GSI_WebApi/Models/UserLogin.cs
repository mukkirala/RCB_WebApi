using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net;
using System.DirectoryServices;
using System.Text;
using System.Collections;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.Caching;

namespace NRL_WebApi.Models
{
    public class UserLogin
    {
        public string CustodianID { get; set; }
        public string CustodianDepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string CustodianName { get; set; }
        public string Designation { get; set; }
        
        public string EmployeeID { get; set; }
        public string USR_ROLEID { get; set; }
        public string USR_LOGIN { get; set; }
       
        public string USR_PASSWORD { get; set; }


        public string logintype { get;  set; }

        public string AssetClassID { get; set; }

        public int RoleExist(string Cust, string Type)
        {
            SqlConnection conAMS = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
            int result = 0;
            string query = "Select * from RoleMaster where CustodianID='" + Cust + "' and  ROLE_NAME='" + Type + "' and  ROLE_STATUS='Active'";
            SqlDataAdapter da = new SqlDataAdapter(query, conAMS);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                result = 1;
            }
            else
            {

            }
            return result;
        }

        public object GetValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(key);
        }

        public bool ValueAdd(string key, object value, DateTimeOffset absExpiration)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, value, absExpiration);
        }

        public void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }

        public bool IsAuthenticated(string ldap, string usr, string pwd)
        {
          
            bool authenticated = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry(ldap, usr, pwd);
                object nativeObject = entry.NativeObject;
                authenticated = true;
                DirectorySearcher dsearch = new DirectorySearcher(entry);
                SearchResult sResultSet = null;
                dsearch.Filter = "(samaccountname=" + usr + ")";
                //dsearch.Filter = "(&(objectCategory=User)(objectClass=person))";
                //dsearch.Filter = "(cn=" + usr + ")";
                sResultSet = dsearch.FindOne();
                string CustodianID = GetProperty(sResultSet, "extensionAttribute4");
                // HttpContext.Current.Session["emp"] = CustodianID;
                ValueAdd("emp", CustodianID , DateTimeOffset.UtcNow.AddHours(1));

            }
            catch (DirectoryServicesCOMException cex)
            {
                System.Diagnostics.Debug.WriteLine(cex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return authenticated;

        }

        public static string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

    }
}