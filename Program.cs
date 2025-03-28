using Newtonsoft.Json;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

public class Personne
{
    public string Nom { get; set; }
    public int Age { get; set; }

    public string Hello()
    {
        string message = $"hello {Nom}, you are {Age}";
        return message;
    }
}

class Program
{
    static void Main(string[] args)
    {

        // Traitement de fichier JSON =============================================
        Personne personne = new Personne
        {
            Nom = "Marco",
            Age = 23
        };

        string json = JsonConvert.SerializeObject(personne, Formatting.Indented);
        Console.WriteLine(json);

        // Traitement d'image Locale ==============================================

        // Liste d'images à redimensionner
        List<string> imageFiles = new List<string>
        {
            "image1.jpg",
            "image2.jpg",
            "image3.jpg",
            "image4.jpg"
        };

        // Mesurer le temps de traitement
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Parallel.ForEach(imageFiles, imagePath =>
        {
            try
            {
                using (Image image = Image.Load(imagePath))
                {
                    // Redimensionner l'image à la moitié de sa taille
                    image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                    string outputPath = Path.Combine("output", Path.GetFileName(imagePath));
                    Directory.CreateDirectory("output");
                    image.Save(outputPath);
                    Console.WriteLine($"Image {Path.GetFileName(imagePath)} redimensionnée avec succès !");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du traitement de l'image {imagePath}: {ex.Message}");
            }
        });

        stopwatch.Stop();
        Console.WriteLine($"Les Images ont été redimensionnée avec succès en {stopwatch.ElapsedMilliseconds} ms");

    }
}
