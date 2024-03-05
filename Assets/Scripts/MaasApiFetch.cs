using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MaasApiFetch : MonoBehaviour
{
    private const string ApiUrl = "https://cab.inta-csic.es/rems/wp-content/plugins/marsweather-widget/api.php";

    private const string invalid = "--";

    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(ApiUrl);
        yield return www.SendWebRequest();
        Debug.Log("Started!");
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching data: " + www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            CSICResponse csicResponse = JsonUtility.FromJson<CSICResponse>(jsonString);
            Debug.Log(csicResponse);
            for (int i = 0; i < csicResponse.soles.Length; i++)
            {
                Debug.Log($"{i} {Sol2FSol(csicResponse.soles[i])}");
                Debug.Log(csicResponse.soles[i]);
            }
        }
    }

    [System.Serializable]
    public class CSICResponse
    {
        public Dictionary<string, string> descriptions;
        public Sol[] soles;
    }


    [System.Serializable]
    public class Sol
    {
        public string id;
        public string terrestrial_date;
        public string sol;
        public string ls;
        public string season;
        public string min_temp;
        public string max_temp;
        public string pressure;
        public string pressure_string;
        public string abs_humidity;
        public string wind_speed;
        public string wind_direction;
        public string sunrise;
        public string sunset;
        public string atmo_opacity;
        public string local_uv_irradiance_index;
        public string min_gts_temp;
        public string max_gts_temp;
    }

    public class FSol
    {
        public ulong ID;
        public string Date;
        public ulong Sol;
        public long SolarLon;
        public ulong Month;
        public long TempMin;
        public long TempMax;
        public long Pressure;
        public string PressureStr;
        public string Humidity;
        public string WindSpeed;
        public string WindDirection;
        public string Sunrise;
        public string Sunset;
        public string AtmoOpacity;
        public string UVIndex;
        public long TempGroundMin;
        public long TempGroundMax;

        public override string ToString()
        {
            return $"FSol: {{\n  ID: {ID}\n  Date: {Date}\n  Sol: {Sol}\n  Month: {Month}\n  Temp: ({TempMin}, {TempMax})\n}}";
        }
    }

    private FSol Sol2FSol(Sol s)
    {
        FSol f = new FSol
        {
            AtmoOpacity = s.atmo_opacity,
            Date = s.terrestrial_date,
            Humidity = s.abs_humidity,
            PressureStr = s.pressure_string,
            UVIndex = s.local_uv_irradiance_index,
            WindDirection = s.wind_direction,
            WindSpeed = s.wind_speed,
            Sunrise = s.sunrise,
            Sunset = s.sunset
        };

        if (!ulong.TryParse(s.id, out f.ID))
        {
            Debug.LogError("ID parsing failed");
        }

        if (!long.TryParse(s.ls, out f.SolarLon))
        {
            Debug.LogError("Solar Lon parsing failed");
        }

        if (!ulong.TryParse(s.season.Substring(6), out f.Month))
        {
            Debug.LogError("Month parsing failed");
        }

        if (s.pressure != invalid)
        {
            if (!long.TryParse(s.pressure, out f.Pressure))
            {
                Debug.LogError("Pressure parsing failed");
            }
        }

        if (!ulong.TryParse(s.sol, out f.Sol))
        {
            Debug.LogError("Sol parsing failed");
        }

        if (s.pressure != invalid)
        {
            if (!long.TryParse(s.max_gts_temp, out f.TempGroundMax))
            {
                Debug.LogError("Temp Ground Max parsing failed");
            }
        }

        if (s.pressure != invalid)
        {
            if (!long.TryParse(s.min_gts_temp, out f.TempGroundMin))
            {
                Debug.LogError("Temp Ground Min parsing failed");
            }
        }

        if (s.pressure != invalid)
        {
            if (!long.TryParse(s.max_temp, out f.TempMax))
            {
                Debug.LogError("Temp Max parsing failed");
            }
        }

        if (s.pressure != invalid)
        {
            if (!long.TryParse(s.min_temp, out f.TempMin))
            {
                Debug.LogError("Temp Min parsing failed");
            }
        }

        return f;
    }
}
