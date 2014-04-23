using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpRequest currentRequest = HttpContext.Current.Request;
        //
        //  INPUT VARIABLES  //
        //
        String id = Server.UrlEncode(Request.QueryString["id"]);
        String pw = Server.UrlEncode(Request.QueryString["pw"]);
        String ver = Server.UrlEncode(Request.QueryString["ver"]);
        String test = Server.UrlEncode(Request.QueryString["test"]);
        String code = Server.UrlEncode(Request.QueryString["code"]);
        String pcode = Server.UrlEncode(Request.QueryString["pcode"]);
        String ip = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null || ip.ToLower() == "unknown")
            ip = currentRequest.ServerVariables["REMOTE_ADDR"];
        String nation = Server.UrlEncode(Request.QueryString["nation"]);
        //
        //  OUTPUT VARIABLES  //
        //
        int user_id = 0;
        string sess_id = "";
        string run_ver = "";
        int bill_no = 0;
        int grade = 0;
        int ret = 0;
        //  //  //  //  //  //  //  //  //  // 
        if (id == "") { Response.Write("-1|-2|-1"); }
        if (nation == "") { Response.Write("-1|-3|-1"); }
        if (pcode == "") { Response.Write("-1|-5|-1"); }
        if (nation == "") { Response.Write("-1|-3|-1"); }

        try
        {
            string ConnString = ConfigurationManager.ConnectionStrings["RohanUser"].ConnectionString;
            SqlConnection SqlConn = new SqlConnection(ConnString);
            string query = "call [dbo].[ROHAN4_Login] (@id,@pw,@nation,@ver,@test,@ip,@code,@user_id,@sess_id,@run_ver,@bill_no,@grade,@ret)";
            SqlCommand myCommand = new SqlCommand(query);
            myCommand.Parameters.AddWithValue("id", id);
            myCommand.Parameters.AddWithValue("pw", pw);
            myCommand.Parameters.AddWithValue("nation", nation);
            myCommand.Parameters.AddWithValue("ver", ver);
            myCommand.Parameters.AddWithValue("test", test);
            myCommand.Parameters.AddWithValue("ip", ip);
            myCommand.Parameters.AddWithValue("code", code);
            myCommand.Parameters.Add("user_id", SqlDbType.Int).Direction = ParameterDirection.Output;
            myCommand.Parameters.Add("sess_id", SqlDbType.Char).Direction = ParameterDirection.Output;
            myCommand.Parameters.Add("run_ver", SqlDbType.VarChar).Direction = ParameterDirection.Output;
            myCommand.Parameters.Add("bill_no", SqlDbType.Int).Direction = ParameterDirection.Output;
            myCommand.Parameters.Add("grade", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            myCommand.Parameters.Add("ret", SqlDbType.Int).Direction = ParameterDirection.Output;

            myCommand.Connection = SqlConn;
            SqlConn.Open();

            if (myCommand.ExecuteNonQuery() > 0)
            {
                user_id = int.Parse(myCommand.Parameters["user_id"].Value.ToString());
                sess_id = myCommand.Parameters["sess_id"].Value.ToString();
                run_ver = myCommand.Parameters["run_ver"].Value.ToString();
                bill_no = int.Parse(myCommand.Parameters["bill_no"].Value.ToString());
                grade = int.Parse(myCommand.Parameters["grade"].Value.ToString());
                ret = int.Parse(myCommand.Parameters["ret"].Value.ToString());
            }
            myCommand.Connection.Close();
            SqlConn.Close();

            Response.Write(sess_id + "|" + user_id + "|" + run_ver + "|" + grade + "|" + code);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }

    }
}