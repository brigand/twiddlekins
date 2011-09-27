using System;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Configuration;
using System.Text;

namespace robokins.Utility
{
    class Yodish
    {
        static Uri uri = new Uri("http://www.yodaspeak.co.uk/index.php");
//        const string formAction = "index.php";
//        const string inputName = "YodaMe";
//        const string responseName = "YodaSpeak";

        public static string[] Yodize(string YodaMe)
        {
            string[] YodaSpeak = { "Hmm... learning, I am.", "" };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Referer = "http://www.twdev.net/";
            string postsourcedata = "YodaMe=" + YodaMe;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postsourcedata.Length;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Gentoo Gnu/Linux)";
            Stream writeStream = request.GetRequestStream();
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(postsourcedata);
            writeStream.Write(bytes, 0, bytes.Length);
            writeStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8);
            string page = readStream.ReadToEnd();
            // Parse the html page to display only the Yodish.
            return YodaSpeak;
        }
    }
}

/*
Uri uri = new Uri(BABELFISHURL);
HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
request.Referer = BABELFISHREFERER;
// Encode all the sourcedata 

string postsourcedata;
postsourcedata = "lp=" + translationmode + 
    "&tt=urltext&intl=1&doit=done&urltext=" + 
HttpUtility.UrlEncode(sourcedata);
request.Method = "POST";
request.ContentType = "application/x-www-form-urlencoded";
request.ContentLength = postsourcedata.Length;
request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
Stream writeStream = request.GetRequestStream();
UTF8Encoding encoding = new UTF8Encoding();
byte[] bytes = encoding.GetBytes(postsourcedata);
writeStream.Write(bytes, 0, bytes.Length);
writeStream.Close();
HttpWebResponse response = (HttpWebResponse) request.GetResponse();
Stream responseStream = response.GetResponseStream();
StreamReader readStream = new StreamReader (responseStream, Encoding.UTF8);
string page = readStream.ReadToEnd();
*/
/*
// Set the 'Method' property of the 'Webrequest' to 'POST'.
myHttpWebRequest.Method = "POST";
Console.WriteLine ("\nPlease enter the data to be posted to the (http://www.contoso.com/codesnippets/next.asp) Uri :");

// Create a new string object to POST data to the Url.
string inputData = Console.ReadLine ();


string postData = "firstone=" + inputData;
ASCIIEncoding encoding = new ASCIIEncoding ();
byte[] byte1 = encoding.GetBytes (postData);

// Set the content type of the data being posted.
myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

// Set the content length of the string being posted.
myHttpWebRequest.ContentLength = byte1.Length;

Stream newStream = myHttpWebRequest.GetRequestStream ();

newStream.Write (byte1, 0, byte1.Length);
Console.WriteLine ("The value of 'ContentLength' property after sending the data is {0}", myHttpWebRequest.ContentLength);

// Close the Stream object.
newStream.Close (); 
 */
