using KRSDataCollector.DataModel;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace KRSDataCollector;

class Program
{
    public static readonly string[] krsNumbers = ["49482", "471297", "103407", "113253", "219771", "319410", "321925", "55796", "407119", "445582", "58632", "301254", "273709", "272206", "199646", "17327", "207809", "25654", "80782", "149547", "105005", "140376", "140418", "127014", "186306", "264367", "260553", "253834", "162071", "277209", "270554", "237076", "479216", "85602", "490344", "628769", "589727", "682895", "637266", "262005", "11077", "680768", "679726", "71021", "211056", "35578", "282959", "165902", "34905", "382105", "766053", "275473", "273921", "275888", "282719", "141705", "515806", "475767", "272892", "85102", "89677", "276774", "259453", "47274", "509507", "121714", "101573", "74955", "136167", "18014", "220667", "162397", "279434", "275997", "24076", "270254", "508537", "434698", "293881", "183997", "458037", "265273", "346821", "100407", "32798", "272563", "263608", "267760", "85102", "195438", "262981", "163621", "270677", "371925", "227377", "277001", "658039", "404245", "260927", "524845", "520949", "152373", "565702", "643460", "499945", "259698", "38891", "276930", "659315", "560823", "560607", "560825", "672805", "70453", "184676", "84080", "169555", "240388", "264275", "398779", "592964", "272199", "169558"];
    static void Main(string[] args)
    {
        List<Dzial> Odpisy = new List<Dzial>();
        /*  foreach (string number in krsNumbers)
          {*/
        using (var client = new HttpClient())
        {
            var endpoint = new Uri($"https://api-krs.ms.gov.pl/api/krs/OdpisAktualny/49482?rejestr=P&format=json");
            var result = client.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;
            Root? data = JsonSerializer.Deserialize<Root>(json);
            Odpisy.Add(data.odpis.dane.dzial1);
        }
        //}
        StringBuilder sb = new StringBuilder();
        List<PropertyInfo> properties =
        [
            .. typeof(DanePodmiotu).GetProperties(),
            .. typeof(Siedziba).GetProperties(),
            .. typeof(Adres).GetProperties(),
        ];

        sb.AppendLine(string.Join(",", properties.Select(o => o.Name)));
        foreach (Dzial odpis in Odpisy)
        {
            sb.Append(string.Join(",", odpis.siedzibaIAdres.siedziba.GetType().GetProperties().Select(o => o.GetValue(o.Name))));
            /*            sb.Append(danePodmiotu.nazwa);
                        sb.Append(",");
                        sb.Append(odpis.dane.dzial1.siedzibaIAdres.siedziba.wojewodztwo);
                        sb.Append(",");
                        sb.Append(odpis.dane.dzial1.siedzibaIAdres.siedziba.powiat);
                        sb.Append(",");
                        sb.Append(odpis.dane.dzial1.siedzibaIAdres.siedziba.gmina);
                        sb.Append(",");
                        sb.Append(odpis.dane.dzial1.siedzibaIAdres.adres.ulica);
                        sb.Append(",");
                        sb.Append(odpis.dane.dzial1.siedzibaIAdres.adres.nrDomu);
                        sb.Append(",");
                        sb.Append(odpis.dane.dzial1.siedzibaIAdres.adres.miejscowosc);
                        sb.Append(",");
                        sb.AppendLine(odpis.dane.dzial1.siedzibaIAdres.adres.kodPocztowy);*/
        }
        //  File.WriteAllText("C:\\Users\\adam1\\source\\repos\\Results\\result.csv", sb.ToString());
        Console.WriteLine(sb.ToString());
    }
}
