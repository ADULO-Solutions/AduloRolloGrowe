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
         isLeistungInConfiguration,
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
                public int PositionID { get; set; }
                public int Menge { get; set; }
                public int Breite { get; set; }
                public int Hoehe { get; set; }
        #endregion Position
        #region Dictionarys
            public Dictionary<string, In.variante> Variants { get; set; }
            public Dictionary<string, In.leistung> Leistung { get; set; }
            public Dictionary<string, In.CommonPosition> Position { get; set; }
        #endregion Dictionarys
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
                            GetJson = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GetJson = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            Response();
            return true;
        }//End of MakeRequest
        /// <summary>
        /// Processed the response from the Restful API
        /// </summary>
        private void Response()
        {
            ////////////Response////////////////////
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new StringConverter());
            switch (Methode)
            {
                case FunctionType.authentificate:
                    In.Authentification? authentification =
                    JsonSerializer.Deserialize<In.Authentification>(GetJson);
                    ApiToken = authentification?.apitoken;
                    break;
                case FunctionType.getLeistungen:
                    In.GetLeistung leistungObject = JsonSerializer.Deserialize<In.GetLeistung>(GetJson, serializeOptions);
                    Leistung = leistungObject.leistung;
                    break;
                case FunctionType.createNewConfiguration:
                    In.CreateCopyConfiguration? createNewConfiguration =
                    JsonSerializer.Deserialize<In.CreateCopyConfiguration>(GetJson);
                    break;
                case FunctionType.copyConfiguration:
                    In.CreateCopyConfiguration? copyConfiguration =
                    JsonSerializer.Deserialize<In.CreateCopyConfiguration>(GetJson);
                    break;
                case FunctionType.editConfiguration:
                    In.CommonBoolean? editConfiguration =
                    JsonSerializer.Deserialize<In.CommonBoolean>(GetJson);
                    break;
                case FunctionType.readConfiguration:

                    break;
                case FunctionType.isLeistungInConfiguration:

                    break;
                case FunctionType.listConfigurations:
                    In.GetlistConfigurations listObject = JsonSerializer.Deserialize<In.GetlistConfigurations>(GetJson, serializeOptions);
                    List<In.GetlistConfigurations> configurations = new List<In.GetlistConfigurations>();
                    Variants = listObject.variante;
                    break;
                case FunctionType.validatePosition:

                    break;
                case FunctionType.createNewPosition:

                    break;
                case FunctionType.copyPosition:
                    goto case FunctionType.createNewPosition;
                case FunctionType.editPosition:
                    goto case FunctionType.createNewPosition;

                case FunctionType.readPosition:
                    break;

            }
        }//End of Response

        /// <summary>
        /// Builds the post string for the given methode.
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

                case FunctionType.isLeistungInConfiguration:
                    var isLeistungInConfiguration = new Out.IsLeistungInConfiguration
                    {
                        apitoken = ApiToken,
                        variante_id = Variante_id
                    };
                    PostJSON = JsonSerializer.Serialize<Out.IsLeistungInConfiguration>(isLeistungInConfiguration);
                    break;
                case FunctionType.listConfigurations:
                    var getKonfigurations = new Out.commonRequest
                    {
                        apitoken = ApiToken,
                    };
                    PostJSON = JsonSerializer.Serialize<Out.commonRequest>(getKonfigurations);
                    break;
                case FunctionType.createNewPosition:
                    // TODO
                    var position = new Out.CreateNewPosition
                    {
                        apitoken = ApiToken,


                    };
                    break;
                case FunctionType.copyPosition:
                    var copyPosition = new Out.CopyReadPosition
                    {
                        apitoken = ApiToken,
                        position_id = PositionID
                    };
                    PostJSON = JsonSerializer.Serialize<Out.CopyReadPosition>(copyPosition);
                    break;
                case FunctionType.editPosition:
                    break;
                case FunctionType.readPosition:
                    goto case FunctionType.copyPosition;
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
