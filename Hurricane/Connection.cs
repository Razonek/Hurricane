using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using System.Windows;


namespace Hurricane
{
    public class Connection
    {

        ASCIIEncoding Encoding = new ASCIIEncoding();
        
        

        public enum ConnectionType
        {
            Login,
            Status,
            Logout,
        }
           
        
        public string ServerInfo(ConnectionType Type)
        {

            try
            {
                string postData = "type=" + Type;
                byte[] Data = Encoding.GetBytes(postData);
                WebRequest Request = WebRequest.Create("http://xcoreproject.eu/hurricanehack.php");
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.ContentLength = Data.Length;

                Stream stream = Request.GetRequestStream();
                stream.Write(Data, 0, Data.Length);
                stream.Close();

                WebResponse response = Request.GetResponse();
                stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string callBack = sr.ReadToEnd();
                sr.Close();
                response.Close();

                return callBack;
            }

            catch(WebException ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }         
            
        } 


         
            


    }
}
