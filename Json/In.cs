using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Text.Json.Serialization;
namespace AduloRolloGrowe
{
    public class In
    {
        #region Authentification
        /// <summary>
        /// Class for authentification
        /// </summary>
        public class Authentification
        {
            public int apiversion { get; set; }
            public String apitoken { get; set; }
            public String apitoken_valid { get; set; }
            public String user { get; set; }
            public bool result { get; set; }
            //{"apiversion":1,"apitoken":"VqYJyvdqJAV,QsSPLxKvT9Jd","apitoken_valid":"2022-08-09 14:08:53","user":"ilovrinovic@adulo.de","result":true}
        }
        #endregion Authentification
        #region Common True False
        public class CommonBoolean
        {
            public bool result { get; set; }

            //{
            //"result":true,
            //}
        }
        #endregion
        #region GetLeistung
        /// <summary>
        /// Class for GetLeistung
        /// </summary>
        public class GetLeistung
        {
            public int apiversion { get; set; }
            public String apitoken { get; set; }
            public String apitoken_valid { get; set; }
            public String user { get; set; }
            public bool result { get; set; }

            //public List<leistung> Leistung { get; set; }
            public Dictionary<string, leistung> leistung { get; set; }

            //{"apiversion":1,"apitoken":"uPuBkstpP.D2ceAd2!ru9LSi","apitoken_valid":"2022-08-10 15:13:24","user":"ilovrinovic@adulo.de",
            //"user_agent":"Mozilla\/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit\/537.36 (KHTML, like Gecko) Chrome\/104.0.0.0 Safari\/537.36",
            //"result":true,
            //"leistung":
            //{"4":{"leistung_id":"4","name":"EXTE","zusatztext":"Expert XT \/ Elite XT","anbaute":"1","aufsatz":"1","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/4.jpg"},
            //"5":{"leistung_id":"5","name":"PAKTO\u00ae","zusatztext":"Neubau-\/Aufsatzkasten","anbaute":"1","aufsatz":"1","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/5.jpg"},
            //"6":{"leistung_id":"6","name":"Insektenschutz","zusatztext":"f\u00fcr Fenster und T\u00fcren","anbaute":"0","aufsatz":"0","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/6.jpg"},
            //"7":{"leistung_id":"7","name":"Panzer","zusatztext":"lose","anbaute":"0","aufsatz":"0","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/7.jpg"},
            //"8":{"leistung_id":"8","name":"Vorbau","zusatztext":null,"anbaute":"1","aufsatz":"0","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/8.jpg"},
            //"9":{"leistung_id":"9","name":"Elite-Raffstore","zusatztext":null,"anbaute":"1","aufsatz":"1","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/9.jpg"},
            //"10":{"leistung_id":"10","name":"PAKTO\u00ae-Raffstore","zusatztext":null,"anbaute":"1","aufsatz":"1","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/10.jpg"},
            //"11":{"leistung_id":"11","name":"Vorbau-Raffstore","zusatztext":null,"anbaute":"1","aufsatz":"0","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/11.jpg"},
            //"12":{"leistung_id":"12","name":"Rolltor","zusatztext":null,"anbaute":"0","aufsatz":"0","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/12.jpg"},
            //"13":{"leistung_id":"13","name":"Vorbau-SUN-TEX","zusatztext":"Der textile Sonnenschutz","anbaute":"1","aufsatz":"0","bild":"https:\/\/gkm-test2.rollladen-growe.de\/img\/leistungen\/13.jpg"}}}
        }
        public class leistung
        {
            public leistung()
            { }
            public string leistung_id { get; set; }
            public string name { get; set; }
            public string zusatztext { get; set; }
            public string anbaute { get; set; }
            public string aufsatz { get; set; }
            public string bild { get; set; }
        }
        #endregion GetLeistung
        #region Maintenance
        /// <summary>
        /// Class for Maintenance
        /// </summary>
        public class Maintenance
        {

            public String maintenance { get; set; }

            //{
            //"maintenance":"02.05.2022 zwischen 12:00 Uhr und 13:00 Uhr"
            //}
        }
        #endregion Maintenance
        #region CreateCopyConfiguration
        /// <summary>
        /// CreateCopyConfiguration
        /// </summary>
        public class CreateCopyConfiguration
        {
            public bool result { get; set; }
            public int variante_id { get; set; }
            //{
            //"result":true,
            //"variante_id":"1234"
            //}
        }
        #endregion CreateCopyConfiguration
        #region GetlistConfigurations
        /// <summary>
        /// Class for GetlistConfigurations
        /// </summary>
        public class GetlistConfigurations
        {
            public int apiversion { get; set; }
            public String apitoken { get; set; }
            public String apitoken_valid { get; set; }
            public String user { get; set; }
            public bool result { get; set; }


            public Dictionary<string, variante> variante { get; set; }
            //{"apiversion":1,"apitoken":"_mH7va5VGJYmeU.;2M8xKurw","apitoken_valid":"2022-08-12 14:00:57","user":"ilovrinovic@adulo.de","user_agent":"Mozilla\/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit\/537.36 (KHTML, like Gecko) Chrome\/104.0.0.0 Safari\/537.36","result":true,
            //"variante":
            //{"10":{"variante_id":"10","name":"Exte-Test","leistung_id":"4"},
            //"11":{"variante_id":"11","name":"test","leistung_id":"4"},
            //"12":{"variante_id":"12","name":"Test Insekt","leistung_id":"4"},
            //"5":{"variante_id":"5","name":"Insektenschutz 1","leistung_id":"6"},
            //"6":{"variante_id":"6","name":"Insektenschutz mit Tuch","leistung_id":"6"},
            //"14":{"variante_id":"14","name":"3E-TEst","leistung_id":"7"},
            //"15":{"variante_id":"15","name":"gr\u00fcner Rundkasten","leistung_id":
            //"7"},"9":{"variante_id":"9","name":"sedcztf","leistung_id":"7"},"13":{"variante_id":
            //"13","name":"Test 17","leistung_id":"7"},"4":{"variante_id":"4","name":"Testpanzer2","leistung_id":"7"},
            //"7":{"variante_id":"7","name":"Testpanzer3_1","leistung_id":"7"},
            //"8":{"variante_id":"8","name":"Testvorbau","leistung_id":"8"},
            //"16":{"variante_id":"16","name":"Text 2","leistung_id":"8"}}}

        }
        public class variante
        {
            public variante()
            { }
            public string variante_id { get; set; }
            public string name { get; set; }
            public string leistung_id { get; set; }
        }
    }
    #endregion GetlistConfigurations




    #region ReadConfiguration 
    //    {
    //    "result":true,
    //    "variante":{
    //        "1234":{
    //            "variante_id":"1234",
    //            "name":"Testpanzer",
    //            "leistung_id":"7",
    //            "param_panzer_id":"1",
    //            "param_farbe__panzer_id":"537",
    //            "param_farbe__panzer_ral_id":"",
    //            "param_endleiste_id":"2",
    //            "param_farbe__endleiste_id":"598",
    //            "param_farbe__endleiste_ral_id":"",
    //            "param_endleiste_stopper_id":"5",
    //            "param_endleiste_stopper_groesse_id":"3",
    //            "param_farbe__stopper_id":"",
    //            "param_panzer_aufhaengung_id":"7"
    //        }
    //    }
    //}
    #endregion readConfiguration
}


