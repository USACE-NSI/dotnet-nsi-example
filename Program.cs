
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
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
    var data = System.Text.Json.JsonSerializer.Deserialize<FeatureCollection>(responseBody);
    Console.WriteLine($"Found {data.Features.Count} features.");
            foreach (var feature in data.Features)
            {
                Console.WriteLine($"ID: {feature.Properties.FdId} | Loc: {feature.Geometry.Coordinates[1]}, {feature.Geometry.Coordinates[0]}");
            }
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Request error: {e.Message}");
}

internal class FeatureCollection
{
        [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; }
}
internal class Geometry
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("coordinates")]
    public List<double> Coordinates { get; set; }
}
internal class Feature
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }

    [JsonPropertyName("properties")]
    public NSIFeature Properties { get; set; }
}

internal class NSIFeature
{
    [JsonPropertyName("fd_id")]
    public long FdId { get; set; }

    [JsonPropertyName("bid")]
    public string Bid { get; set; }

    [JsonPropertyName("occtype")]
    public string OccType { get; set; }

    [JsonPropertyName("st_damcat")]
    public string StDamCat { get; set; }

    [JsonPropertyName("bldgtype")]
    public string BldgType { get; set; }

    [JsonPropertyName("found_type")]
    public string FoundType { get; set; }

    [JsonPropertyName("cbfips")]
    public string CbFips { get; set; }

    [JsonPropertyName("pop2amu65")]
    public int Pop2Amu65 { get; set; }

    [JsonPropertyName("pop2amo65")]
    public int Pop2Amo65 { get; set; }

    [JsonPropertyName("pop2pmu65")]
    public int Pop2Pmu65 { get; set; }

    [JsonPropertyName("pop2pmo65")]
    public int Pop2Pmo65 { get; set; }

    [JsonPropertyName("sqft")]
    public double SqFt { get; set; }

    [JsonPropertyName("num_story")]
    public int NumStory { get; set; }

    [JsonPropertyName("ftprntid")]
    public string FtPrntId { get; set; }

    [JsonPropertyName("ftprntsrc")]
    public string FtPrntSrc { get; set; } // Set as string to handle nulls safely

    [JsonPropertyName("students")]
    public int Students { get; set; }

    [JsonPropertyName("found_ht")]
    public double FoundHt { get; set; }

    [JsonPropertyName("val_struct")]
    public double ValStruct { get; set; }

    [JsonPropertyName("val_cont")]
    public double ValCont { get; set; }

    [JsonPropertyName("val_vehic")]
    public double ValVehic { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("med_yr_blt")]
    public int MedYrBlt { get; set; }

    [JsonPropertyName("firmzone")]
    public string FirmZone { get; set; }

    [JsonPropertyName("o65disable")]
    public double O65Disable { get; set; }

    [JsonPropertyName("u65disable")]
    public double U65Disable { get; set; }

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("ground_elv")]
    public double GroundElv { get; set; }

    [JsonPropertyName("ground_elv_m")]
    public double GroundElvM { get; set; }
}