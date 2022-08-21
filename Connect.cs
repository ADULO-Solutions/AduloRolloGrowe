using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace AduloRolloGrowe
{
#region Enumerations
    /// <summary>
    /// Http Verb 
    /// The Growe/Rolltex Restful API only needs only Post,
    /// but for further use all other html Verbs are implemented.
    /// </summary>
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    /// <summary>
    /// Authentification type
    /// </summary>
    public enum AuthenticationType
    {
        Basic,
        NTLM
    }
    /// <summary>
    /// The function type that Growe/Rolltex has implemented
    /// </summary>
    public enum FunctionType
    { 
         authentificate, 
         getLeistungen,
         createNewConfiguration,
         copyConfiguration,
         editConfiguration,
         readConfiguration,
         listConfigurations,
         validatePosition,
         createNewPosition,
         copyPosition,
         editPosition,
         readPosition
    }
#endregion
#region RestClient Class
    class RestClient
    {
#region Properties
        #region Main Properties
                public string EndPoint { get; set; }
                public HttpVerb HttpMethod { get; set; }
                public AuthenticationType AuthType { get; set; }
                public string UserName { get; set; }
                public string UserPassword { get; set; }
                public FunctionType Methode { get; set; }
                public string PostJSON { get; set; } 
                public string ApiToken { get; set; }
                public string Apiversion { get; set; }
                public string GetJson { get; set; }
            #endregion Main Properties
            #region Configuration
                public int Leistung_id { get; set; }
                public int Param_panzer_id { get; set; }
                public int Param_farbe__panzer_id { get; set; }
                public int Param_endleiste_id { get; set; }
                public int Param_panzer_aufhaengung_id { get; set; }
                public string Variante_id { get; set; }
            #endregion Configuration

            #region Position
                public int Menge { get; set; }
                public int Breite { get; set; }
                public int Hoehe { get; set; }
        #endregion Position
        #region list
            public Dictionary<string, In.variante> Variants { get; set; }
            public Dictionary<string, In.leistung> Leistung { get; set; }
        #endregion
        #endregion Properties
        #region Constructor
        public RestClient()
        {
            EndPoint = string.Empty;
        }
#endregion Constructor
        /// <summary>
        /// The function does the authentification and get back the API token
        /// </summary>
        /// <returns></returns>
        public bool Authentificate()
        {
            MakeRequest();
            return true; 
        }

        /// <summary>
        /// Makes the request to the Restful API
        /// </summary>
        /// <returns></returns>
        public bool MakeRequest()
        {
            string strResponseValue = string.Empty;
            ////////////Build the header////////////////////
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPoint + Methode);
            request.Method = HttpMethod.ToString();
            request.ContentType = "application/json";
            String authHeader = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(UserName + ":" + UserPassword));
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0";
            request.Headers.Add("Authorization", "Basic " + authHeader);
            request.Headers.Add("User-Agent", "RolloGrowe");
            ////////////Build the header////////////////////
            PostString();
            ////////////Post////////////////////
            if (request.Method == "POST")
            { 
                using (StreamWriter swJSONPayload = new StreamWriter(request.GetRequestStream()))
                { 
                    swJSONPayload.Write(PostJSON);
                    swJSONPayload.Close();
                }
            }
            ////////////Post////////////////////
            ////////////Response////////////////////
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                //Proecess the response stream... (could be JSON, XML or HTML etc..._
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            ////////////Response////////////////////
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new StringConverter());
            switch (Methode)
            {
                case FunctionType.authentificate:
                    In.Authentification? authentification =
                    JsonSerializer.Deserialize<In.Authentification>(strResponseValue);
                    ApiToken = authentification?.apitoken;
                    break;
                case FunctionType.getLeistungen:
                    In.GetLeistung leistungObject = JsonSerializer.Deserialize<In.GetLeistung>(strResponseValue, serializeOptions);
                    Leistung = leistungObject.leistung;
                    break;
                case FunctionType.createNewConfiguration:
                    In.CreateCopyConfiguration? createNewConfiguration =
                    JsonSerializer.Deserialize<In.CreateCopyConfiguration>(strResponseValue);
                    break;
                case FunctionType.copyConfiguration:
                    In.CreateCopyConfiguration? copyConfiguration =
                    JsonSerializer.Deserialize<In.CreateCopyConfiguration>(strResponseValue);
                    break;
                case FunctionType.editConfiguration:
                    In.CommonBoolean? editConfiguration =
                    JsonSerializer.Deserialize<In.CommonBoolean>(strResponseValue);
                    break;
                case FunctionType.listConfigurations:
                    In.GetlistConfigurations listObject = JsonSerializer.Deserialize<In.GetlistConfigurations>(strResponseValue, serializeOptions);
                    List<In.GetlistConfigurations> configurations = new List<In.GetlistConfigurations>();
                    Variants = listObject.variante;
                    break;
                case FunctionType.createNewPosition:

                    break;
                case FunctionType.copyPosition:
                    break;
                case FunctionType.editPosition:

                    break;
                case FunctionType.readPosition:
                    break;
                case FunctionType.validatePosition:
                    break;
            }

            GetJson = strResponseValue;
            return true;
        }//End of makeRequest
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void PostString()
        {
            switch (Methode)
            {

                case FunctionType.authentificate:
                    PostJSON = "";
                    break;
                case FunctionType.getLeistungen:
                    var getLeistung = new Out.commonRequest
                    {
                        apitoken = ApiToken,
                    };
                    PostJSON = JsonSerializer.Serialize<Out.commonRequest>(getLeistung);
                    break;
                case FunctionType.createNewConfiguration:
                    var createNewConfiguration = new Out.CreateNewConfiguration
                    {
                        apitoken = ApiToken,
                    };
                    PostJSON = JsonSerializer.Serialize<Out.CreateNewConfiguration>(createNewConfiguration);
                    break;
                case FunctionType.copyConfiguration:
                    var copyConfiguration = new Out.CommonRequestForPosition
                    {
                        apitoken = ApiToken,
                        variante_id = Variante_id,
                    };
                    PostJSON = JsonSerializer.Serialize<Out.CommonRequestForPosition>(copyConfiguration);
                    break;
                case FunctionType.editConfiguration:
                    var editConfiguration = new Out.EditConfiguration
                    {
                        apitoken = ApiToken,
                        variante_id = Variante_id,
                        param_panzer_id = Param_panzer_id,
                        param_endleiste_id = Param_endleiste_id,
                        param_farbe__panzer_id = Param_farbe__panzer_id,
                        param_panzer_aufhaengung_id = Param_panzer_aufhaengung_id
                    };
                    PostJSON = JsonSerializer.Serialize<Out.EditConfiguration>(editConfiguration);
                    break;
                case FunctionType.readConfiguration:
                    var readConfiguration = new Out.CommonRequestForPosition
                    {
                        apitoken = ApiToken,
                        variante_id = Variante_id,
                    };
                    PostJSON = JsonSerializer.Serialize<Out.CommonRequestForPosition>(readConfiguration);
                    break;
                case FunctionType.listConfigurations:
                    var getKonfigurations = new Out.commonRequest
                    {
                        apitoken = ApiToken,
                    };
                    PostJSON = JsonSerializer.Serialize<Out.commonRequest>(getKonfigurations);
                    break;
                case FunctionType.createNewPosition:

                    break;
                case FunctionType.copyPosition:
                    break;
                case FunctionType.editPosition:

                    break;
                case FunctionType.readPosition:
                    break;
                case FunctionType.validatePosition:
                    break;

            }

        }
        /// <summary>
        /// 
        /// </summary>
        public class StringConverter : System.Text.Json.Serialization.JsonConverter<string>
        {
            public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {

                if (reader.TokenType == JsonTokenType.Number)
                {
                    var stringValue = reader.GetInt32();
                    return stringValue.ToString();
                }
                else if (reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetString();
                }

                throw new System.Text.Json.JsonException();
            }

            public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
            }

        }
    }//End of Class
#endregion RestClient Class
}
