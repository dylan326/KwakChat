using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Data.SqlClient;
using System.Configuration;

namespace MyChatApp
{
    public class myHub
    {

        public class ChatHub : Hub
        {

            public void Send(string name, string message)
            {
                if (CheckIfUserIsAllowed(name))
                {
                    // call the broadcastMessage method to update clents.
                    Clients.All.broadcastMessage(name, message);
                    //save name(converted to id) and message to chatlog table
                    SaveMessage(name, message);
                }
                else
                {
                    //call the client side error message to refresh page
                    Clients.All.broadcastError(name, message);


                }

            }

            //save the message and name to db,  input userID not name.
            public void SaveMessage(string name, string message)
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


                
                var command1 = new SqlCommand("insert into chatLog(chatUserID, userMessage, messageTime) values(@chatUserID, @userMessage, @messageTime)", conn);

                command1.Parameters.AddWithValue("@chatUserID", chatUserID);
                command1.Parameters.AddWithValue("@userMessage", message);
                command1.Parameters.AddWithValue("@messageTime", DateTime.UtcNow);

                conn.Open();
                command1.ExecuteNonQuery();
                conn.Close();
            }

            //bool function to verify user can chat
            public bool CheckIfUserIsAllowed(string name)
            {
                try
                {
                    string namereply = "";
                    var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString);

                    var command = new SqlCommand("SELECT * FROM chatUser WHERE userName = @userName", conn);

                    command.Parameters.AddWithValue("@userName", name);

                    conn.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        namereply = dr["userName"].ToString();
                    }

                    if (string.IsNullOrEmpty(namereply))
                    {
                        return false;
                        
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (SqlException ex)
                {

                    throw ex;
                }


            }



        }
    }
}
