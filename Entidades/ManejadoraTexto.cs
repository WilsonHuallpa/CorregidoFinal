using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ManejadoraTexto
    {
        public static bool EscribirArchivo(List<Auto> lista)
        {
            bool retorno = true;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\autos.log";

            try
            {
                if (File.Exists(path))
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                        sw.WriteLine("Color: " + lista.Last().Color);
                        foreach (Auto auto in lista)
                        {
                            if (lista.Last().Color == auto.Color)
                            {
                                sw.WriteLine(auto.ToString());
                            }
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path, false))
                    {
                        sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                        sw.WriteLine("Color: " + lista.Last().Color);
                        foreach (Auto auto in lista)
                        {
                            if (lista.Last().Color == auto.Color)
                            {
                                sw.WriteLine(auto.ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
