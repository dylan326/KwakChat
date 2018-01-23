using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace MyChatApp
{
    public class Startup
    {
      public void Configuration(IAppBuilder app)
        {
            //configureAuth(app);
            app.MapSignalR();
        }



    }
}
