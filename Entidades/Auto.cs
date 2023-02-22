using System;
using System.Text;

namespace Entidades
{
    public class Auto
    {
        private string color;
        protected int kms;
        protected string marca;
        protected string modelo;
        protected string patente;

        public Auto()
        {
        }

        public Auto(string color, int kms, string marca, string modelo, string patente)
        {
            this.color = color;
            this.kms = kms;
            this.marca = marca;
            this.modelo = modelo;
            this.patente = patente;
        }


        public int Kms { get => kms;}
        public string Marca { get => marca;}
        public string Modelo { get => modelo;}
        public string Patente { get => patente;}
        public string Color { get => color; set => color = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Marca: " + this.Marca + " - Modelo: " + this.Modelo + " - Kms: " + this.Kms + " - Color: " + this.Color + " - Patente: " + this.Patente);

            return sb.ToString();
        }
    }
}
