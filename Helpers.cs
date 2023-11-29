using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoIntegrador
{
    public static class Helpers 
    {
        public static string GeneraId() 
        {
            string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string nombre = "";

            int i = 0, index = 0;

            while (i <= 8)
            {
                Random random = new Random();
                index = random.Next(0, 36);
                nombre += alfabeto[index];
                i++;
            }
            return nombre;
        }
        public static void SerializarCuartel(Cuartel cuartel1)
        {
            try
            {
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                string rutaArchivoJson = Directory.GetCurrentDirectory() + "\\cuartel.json";

                // Serializa la lista de operadores a formato JSON
                string jsonCuartel = JsonConvert.SerializeObject(cuartel1, Formatting.Indented,
                                                                new JsonSerializerSettings
                                                                {
                                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                                });

                // Guarda el contenido JSON en el archivo especificado
                File.WriteAllText(rutaArchivoJson, jsonCuartel);
            }
            catch (Exception)
            {
                Console.WriteLine($"Error al intentar serializar Cuartel");
            }
        }

        public static Cuartel DeserializarCuartel() 
        {
            try
            {
                Cuartel cuartel1;
                string path;
                path = Directory.GetCurrentDirectory() + "\\cuartel.json";
                string jsonString = File.ReadAllText(path);

                cuartel1 = JsonConvert.DeserializeObject<Cuartel>(jsonString);

                return cuartel1;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;

        }

        public static void SerializarMapa(Terreno Mundo)
        {
            try
            {
                string rutaArchivoJson = Directory.GetCurrentDirectory() + "\\mundo.json";

                // Serializa la lista de operadores a formato JSON
                string jsonTerreno = JsonConvert.SerializeObject(Mundo);

                // Guarda el contenido JSON en el archivo especificado
                File.WriteAllText(rutaArchivoJson, jsonTerreno);
            }
            catch (Exception)
            {
                Console.WriteLine($"Error al intentar Deserializar Mapa");
            }
        }

        public static Terreno DeserializarMapa()
        {
            try
            {
                Terreno terreno;
                string path;
                path = Directory.GetCurrentDirectory() + "\\mundo.json";
                string jsonString = File.ReadAllText(path);

                terreno = JsonConvert.DeserializeObject<Terreno>(jsonString);

                return terreno;
            }
            catch (Exception)
            {
                Console.WriteLine($"Error al intentar Deserializar Cuartel");
            }
            return null;
        }

    }

}
