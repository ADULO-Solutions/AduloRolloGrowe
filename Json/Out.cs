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
    }
}
