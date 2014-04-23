using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class serverlist5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ServerList = ConfigurationManager.AppSettings["ServerName"].ToString() + "|" +
            ConfigurationManager.AppSettings["ServerIP"].ToString() + "|" +
            ConfigurationManager.AppSettings["ServerPort"].ToString() + "|" +
            ConfigurationManager.AppSettings["UnknownVar1"].ToString() + "|" +
            ConfigurationManager.AppSettings["UnknownVar2"].ToString() + "|" +
            ConfigurationManager.AppSettings["UnknownVar3"].ToString() + "|" +
            ConfigurationManager.AppSettings["UnknownVar4"].ToString() + "|" +
            ConfigurationManager.AppSettings["UnknownVar5"].ToString() + "|" +
            ConfigurationManager.AppSettings["UnknownVar6"].ToString() + "|" +
            ConfigurationManager.AppSettings["ServerInfo"].ToString() + "|";
        Response.Write(ServerList);
    }
}