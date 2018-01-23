using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MyChatApp
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = tbxName.Text;
            string email = TbxEmail.Text;

            
            int userid = CheckIfUserExists(name);

            if (userid > 0)
            {
                lblSaved.Text = "Welcome back... Redirecting to chat page";
            }
            else
            {
                SaveUserToDb(name, email);
                lblSaved.Text = "User saved! Redirecting to chat page...";
            }

            Response.AddHeader("REFRESH", "3;URL=message.html");
        }

        public int CheckIfUserExists(string name)
        {
            int chatUserID = 0;
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString);

            var command = new SqlCommand("SELECT * FROM chatUser WHERE username = @username", conn);
            command.Parameters.AddWithValue("@username", name);

            conn.Open();
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                chatUserID = (int)(dr["ChatUserId"]);
            }
            conn.Close();

            return chatUserID;
        }

        private void SaveUserToDb(string name, string email)
        {
            try
            {
                
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString))
                using (var command = new SqlCommand("InsertUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    command.Parameters.AddWithValue("@userName", name);
                    command.Parameters.AddWithValue("@userEmail", email);
                    command.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);
                    conn.Open();
                    command.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            
        }
    }
}
