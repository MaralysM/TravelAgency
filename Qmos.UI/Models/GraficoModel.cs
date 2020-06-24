using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyTimeSheet.Models
{
    public class GraficoModel
    {
        public GraficoModel()
        {
            Labels = new List<string>();
            DataSets = new List<DataSetsModel>();
        }

        public List<string> Labels { get; set; }
        public List<DataSetsModel> DataSets { get; set; }

    }
    public class DataSetsModel
    {
        public DataSetsModel()
        {
            ListaColoresBackgroundAll = new List<string> {
                "rgba(237,85,101,0.5)", //Rojo
                "rgba(28,132,198,0.5)", //Azul
                "rgba(26,179,148,0.5)", //Verde claro
                "rgba(248,172,89,0.5)", //Amarillo
                "rgba(35,198,200,0.5)", //Verde oscuro
                "rgba(237,237,237,0.5)", //Gris,
                "rgba(237,85,101,0.5)", //Rojo
                "rgba(28,132,198,0.5)", //Azul
                "rgba(26,179,148,0.5)", //Verde claro
                "rgba(248,172,89,0.5)", //Amarillo
                "rgba(35,198,200,0.5)", //Verde oscuro
                "rgba(237,237,237,0.5)",
                "rgba(237,85,101,0.5)", //Rojo
                "rgba(28,132,198,0.5)", //Azul
                "rgba(26,179,148,0.5)", //Verde claro
                "rgba(248,172,89,0.5)", //Amarillo
                "rgba(35,198,200,0.5)", //Verde oscuro
                "rgba(237,237,237,0.5)",
                "rgba(237,85,101,0.5)", //Rojo
                "rgba(28,132,198,0.5)", //Azul
                "rgba(26,179,148,0.5)", //Verde claro
                "rgba(248,172,89,0.5)", //Amarillo
                "rgba(35,198,200,0.5)", //Verde oscuro
                "rgba(237,237,237,0.5)", //Gris,
                "rgba(237,85,101,0.5)", //Rojo
                "rgba(28,132,198,0.5)", //Azul
                "rgba(26,179,148,0.5)", //Verde claro
                "rgba(248,172,89,0.5)", //Amarillo
                "rgba(35,198,200,0.5)", //Verde oscuro
                "rgba(237,237,237,0.5)",
                "rgba(237,85,101,0.5)", //Rojo
                "rgba(28,132,198,0.5)", //Azul
                "rgba(26,179,148,0.5)", //Verde claro
                "rgba(248,172,89,0.5)", //Amarillo
                "rgba(35,198,200,0.5)", //Verde oscuro
                "rgba(237,237,237,0.5)"//Gris
            };
            ListaColoresBordercolorAll = new List<string> {
                "#ED5565", //Rojo
                "#1C84C6", //Azul
                "#1AB394", //Verde claro
                "#F8AC59", //Amarillo
                "#23C6C8", //Verde oscuro
                "#EDEDED",  //Gris
                "#ED5565", //Rojo
                "#1C84C6", //Azul
                "#1AB394", //Verde claro
                "#F8AC59", //Amarillo
                "#23C6C8", //Verde oscuro
                "#EDEDED",
                "#ED5565", //Rojo
                "#1C84C6", //Azul
                "#1AB394", //Verde claro
                "#F8AC59", //Amarillo
                "#23C6C8", //Verde oscuro
                "#EDEDED",
                "#ED5565", //Rojo
                "#1C84C6", //Azul
                "#1AB394", //Verde claro
                "#F8AC59", //Amarillo
                "#23C6C8", //Verde oscuro
                "#EDEDED",  //Gris
                "#ED5565", //Rojo
                "#1C84C6", //Azul
                "#1AB394", //Verde claro
                "#F8AC59", //Amarillo
                "#23C6C8", //Verde oscuro
                "#EDEDED",
                "#ED5565", //Rojo
                "#1C84C6", //Azul
                "#1AB394", //Verde claro
                "#F8AC59", //Amarillo
                "#23C6C8", //Verde oscuro
                "#EDEDED"//Gris//Gris
            };
        }
        public string Label { get; set; }
        public string[] BackgroundColor { get; set; }
        public string[] BorderColor { get; set; }
        public int?[] DataInt { get; set; }
        public decimal?[] DataDecimal { get; set; }
        public double?[] DataDouble { get; set; }
        public List<string> ListaColoresBackgroundAll { get; set; }
        public List<string> ListaColoresBordercolorAll { get; set; }
        public string[] ListaColores { get; set; }
    }
}