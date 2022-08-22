using System;
using System.Collections.Generic;
using System.Text;

namespace AduloRolloGrowe
{
    class Out
    {
        /// <summary>
        /// getLeistung
        /// </summary>
        public class commonRequest
        {
            public String apitoken { get; set; }
        }

        public class CommonRequestForPosition
        {
            public String apitoken { get; set; }
            public String variante_id { get; set; }
        }

        public class CreateNewConfiguration
        {
            public String apitoken { get; set; }
            public int leistung_id { get; set; }
            public int param_panzer_id { get; set; }
            public int param_farbe__panzer_id { get; set; }
            public int param_endleiste_id { get; set; }
            public int param_panzer_aufhaengung_id { get; set; }
        }
        public class EditConfiguration
        {
            public String apitoken { get; set; }
            public string variante_id { get; set; }
            public int param_panzer_id { get; set; }
            public int param_farbe__panzer_id { get; set; }
            public int param_endleiste_id { get; set; }
            public int param_panzer_aufhaengung_id { get; set; }
        }
        public class IsLeistungInConfiguration
        {
            public String apitoken { get; set; }
            public string variante_id { get; set; }
            public int leistung_id { get; set; }
        }

        #region Position
        public class CreateNewPosition
        {
            public CreateNewPosition()
            { }
            public string apitoken { get; set; }
            public int leistung_id { get; set; }
            public int menge { get; set; }
            public int breite { get; set; }
            public int hoehe { get; set; }
            public int param_panzer_id { get; set; }
            public int param_farbe__panzer_id { get; set; }
            public int param_endleiste_id { get; set; }
            public int param_panzer_aufhaengung_id { get; set; }
            //{
            //    "apitoken":"<token>",
            //    "leistung_id":"7",
            //    "menge":"1",
            //    "breite":"1000",
            //    "hoehe":"1000",
            //    "param_panzer_id":"4",
            //    "param_farbe__panzer_id":"536",
            //    "param_endleiste_id":"16",
            //    "param_panzer_aufhaengung_id":"1"
            //}

        }
        public class CopyReadPosition
        {
            public CopyReadPosition()
            { }
            public string apitoken { get; set; }
            public int position_id { get; set; }
            //    {
            //    "apitoken":"<token>",
            //    "position_id":"1234"
            //     }
        }

        #endregion Position
    }
}
