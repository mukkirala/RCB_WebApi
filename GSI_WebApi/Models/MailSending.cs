using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for MailSending
/// </summary>
public class MailSending
{
   static SqlConnection conAMS = new SqlConnection(WebConfigurationManager.ConnectionStrings["VisitorConnectionString"].ConnectionString);
   static  SqlConnection myConnectionString = new SqlConnection(WebConfigurationManager.ConnectionStrings["ASSETManagementConnectionString"].ConnectionString);

    public MailSending()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetReportingMail(string custid)
    {
        string rmail = "";
        string query = "SELECT email FROM [vEmpDtlsAssetApp] where CustodianID = '" + custid + "'";
        SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            rmail = dt.Rows[0]["email"].ToString();
        }
        return rmail;       
    }
    public static string GetReportingName(string custid)
    {
        string name = "";
        string query = "SELECT CustodianName FROM [vEmpDtlsAssetApp] where CustodianID = '" + custid + "'";
        SqlDataAdapter da = new SqlDataAdapter(query, myConnectionString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            name = dt.Rows[0]["CustodianName"].ToString();
        }
        return name;
    }

    //mail with attachment
    public static Boolean sendMailAttach(String custodianid, String subject, String body)
    {
        string approvername = GetReportingName(custodianid);
        string approvermail = GetReportingMail(custodianid);

        SmtpClient client = new SmtpClient();
        MailMessage message = new MailMessage();
        string EmailHost = "nrlexprht02.nrl.com";
       // string Port = WebConfigurationManager.AppSettings.Get("EmailHost");
        client.Host = EmailHost;
        client.Port = 25;
     
        string EmailFromAddress = "assettrack@nrl.co.in";
        string EmailFromName = "NRL ASSET TRACKING SYSTEM";
        MailAddress fromAddress = new MailAddress(EmailFromAddress, EmailFromName);
        message.From = fromAddress;
        EmailFromAddress = "assettrack@nrl.co.in";
        string AppCredentialPassword = "asset@@20nrl";
        client.Credentials = new System.Net.NetworkCredential(EmailFromAddress, AppCredentialPassword);
        client.EnableSsl = true;

        // string appr = approvermail;
        string appr = "vishnu.vardhan103@gmail.com";

        foreach (var address in appr.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        {
            message.To.Add(address);
        }     
        message.Subject = subject;
        message.IsBodyHtml = true;        
        string msg1 = "<h3>Dear " + approvername + "</h3><br/>";
        string msg4 = body;
        string msg5 = "<br/><br/>" + " Thanks and Regards, <br/>" + "NRL Asset Track";
        message.Body = msg1 + "<br/>" + msg4 + " <br/>" + "<a href='http://10.10.20.156:8080'>" + " nrlproduction.com" + "</a>" + "<br/>" + "" + msg5;       
        try
        {
            client.Send(message);
            return true;
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            return false;
        }
    }

    public static Boolean sendMailBuy(String custodianid, String subject, String body)
    {
        string authname = GetReportingName(custodianid);
        string authmail = GetReportingMail(custodianid);

        SmtpClient client = new SmtpClient();
        MailMessage message = new MailMessage();
        string EmailHost = "nrlexprht02.nrl.com";
        // string Port = WebConfigurationManager.AppSettings.Get("EmailHost");
        client.Host = EmailHost;
        client.Port = 25;

        string EmailFromAddress = "assettrack@nrl.co.in";
        string EmailFromName = "NRL ASSET TRACKING SYSTEM";
        MailAddress fromAddress = new MailAddress(EmailFromAddress, EmailFromName);
        message.From = fromAddress;
        EmailFromAddress = "assettrack@nrl.co.in";
        string AppCredentialPassword = "asset@@20nrl";
        client.Credentials = new System.Net.NetworkCredential(EmailFromAddress, AppCredentialPassword);
        client.EnableSsl = true;
        //client.UseDefaultCredentials = true;

        string appr = authmail;
       // string appr = "puttasnehapriya@gmail.com;vishnu.vardhan103@gmail.com";
        foreach (var address in appr.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        {
            message.To.Add(address);
        }
        message.Subject = subject;
        message.IsBodyHtml = true;
        string msg1 = "<h3>Dear " + authname + "</h3><br/>";
        string msg4 = body;
        string msg5 = "<br/><br/>" + " Thanks and Regards, <br/>" + "NRL Asset Track";
        message.Body = msg1 + "<br/>" + msg4 + " <br/>" + "<a href='http://10.10.20.156:8080'>" + " nrlproduction.com" + "</a>" + "<br/>" + "" + msg5;
        try
        {
            client.Send(message);
            return true;
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            return false;
        }
    }

}