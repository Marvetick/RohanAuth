using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sendcode3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpRequest currentRequest = HttpContext.Current.Request;
        String id = Server.UrlEncode(Request.QueryString["id"]);
        String pw = Server.UrlEncode(Request.QueryString["pw"]);
        String ip = "127.0.0.1";//currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
        int ret = 0;
        //if (ip == null || ip.ToLower() == "unknown")
        //    ip = currentRequest.ServerVariables["REMOTE_ADDR"];
        //if (id == "")
        //    Response.Write("-99");

        try
        {
            string ConnString = ConfigurationManager.ConnectionStrings["RohanUser"].ConnectionString;
            SqlConnection SqlConn = new SqlConnection(ConnString);
            string query = "call [dbo].[ROHAN3_SendCode](@id,@pw,@ip,@ret)";
            SqlCommand myCommand = new SqlCommand(query);
            myCommand.Parameters.AddWithValue("id", id);
            myCommand.Parameters.AddWithValue("pw", pw);
            myCommand.Parameters.AddWithValue("ip", ip);
            myCommand.Parameters.Add("ret", SqlDbType.Int).Direction = ParameterDirection.Output;

            myCommand.Connection = SqlConn;
            SqlConn.Open();
            if (myCommand.ExecuteNonQuery() > 0)
            {
                //ret = int.Parse(myCommand.Parameters["ret"].Value.ToString());
                ret = -202;
            }
            myCommand.Connection.Close();
            SqlConn.Close();

            Response.Write(ret);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
}