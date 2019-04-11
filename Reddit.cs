using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Leaf.xNet;

namespace RedditDominator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var Request = new HttpRequest())
            {
                try
                {
                    // acc
                    string username = "";
                    string password = "";

                    // Getting Data

                    Request.ConnectTimeout = 15000;
                    Request.ReadWriteTimeout = 15000;
                    Request.KeepAlive = true;
                    Request.Referer = "https://www.reddit.com";
                    Request.AllowAutoRedirect = true;
                    Request.AcceptEncoding = "gzip, deflate, br";

                    Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0";
                    var Response = Request.Get("https://www.reddit.com/login");

                    // Posting Data

                    var token = new Regex("<input type=\"hidden\" name=\"csrf_token\" value=\"(.*?)\">", RegexOptions.Singleline).Match(Response.ToString()).ToString().Substring(46).TrimEnd('>', '"');
                    var cookies = Response.Cookies.GetCookieHeader("https://www.reddit.com/login");

                    Request.ConnectTimeout = 15000;
                    Request.ReadWriteTimeout = 15000;
                    Request.KeepAlive = true;
                    Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0";
                    Request.Referer = "https://www.reddit.com/login";
                    Request.AllowAutoRedirect = true;
                    Request.AddHeader("cookies", cookies);

                    string data = "csrf_token=" + token + "&otp=&password=" + password + "&dest=https%3A%2F%2Fwww.reddit.com&username=" + username;


                    var Posted = Request.Post("https://www.reddit.com/login", data, "application/x-www-form-urlencoded");


                    // Getting account logged in

                    var cooks = Response.Cookies.GetCookieHeader("https://www.reddit.com/");
                    Request.AddHeader("cookies", cooks);
                    var Responsee = Request.Get("https://www.reddit.com/user/"+username);

                    Console.WriteLine(Responsee);
                    Console.ReadLine();

                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                }

            }
        }
    }
}
