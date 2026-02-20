
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// See https://aka.ms/new-console-template for more information


string HECNSIRedirectURL = "https://www.hec.usace.army.mil/fwlink/?linkid=1&type=string";

var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};
var client = new HttpClient(handler);
string NSIURL = "";
try 
{
    
    // Use GetStringAsync for a direct string download
    NSIURL = await client.GetStringAsync(HECNSIRedirectURL);
    
    Console.WriteLine("Data Downloaded Successfully:");
    Console.WriteLine(NSIURL);
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Request error: {e.Message}");
}
//string NSIURL = "https://nsi.sec.usace.army.mil/nsiapi/";
NSIURL += "structures?bbox=";
NSIURL += "-81.58418,30.25165,-81.58161,30.26939,-81.55898,30.26939,-81.55281,30.24998,-81.58418,30.25165";
System.Console.WriteLine("downloading data from " + NSIURL);

try 
{
    
    // Use GetStringAsync for a direct string download
    string responseBody = await client.GetStringAsync(NSIURL);
    
    Console.WriteLine("Data Downloaded Successfully:");
    Console.WriteLine(responseBody.Substring(0, Math.Min(responseBody.Length, 500)) + "...");
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Request error: {e.Message}");
}

