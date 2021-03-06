﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Supprimez les marques de commentaire dans la ligne suivante si vous utilisez des composants conçus 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public List<string> getInfos(string parametreName)
    {
        List<string> tmpList = new List<string>();

        tmpList.Add(this.User.Identity.Name);
        tmpList.Add(this.SoapVersion.ToString());
        tmpList.Add("Hello " + parametreName + " !");

        return tmpList;
    }
}
