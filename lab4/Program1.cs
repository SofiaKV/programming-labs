using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicLibrary
{
    public abstract class Composition
    {
        public string Title { get; set; }
        public string Composer { get; set; }
        public TimeSpan Duration { get; set; }
        public string Style { get; set; }

        protected Composition(string title, string composer, TimeSpan duration, string style)
        {
            Title = title;
            Composer = composer;
            Duration = duration;
            Style = style;
        }
    }

    public class Song : Composition
    {
        public string Artist { get; set; }
        public string Genre { get; set; }

        public Song(string title, string composer, TimeSpan duration, string style, string artist, string genre)
            : base(title, composer, duration, style)
        {
            Artist = artist;
            Genre = genre;
        }
    }

    public class Symphony : Composition
    {
        public int MovementCount { get; set; }

        public Symphony(string title, string composer, TimeSpan duration, string style, int movementCount)
            : base(title, composer, duration, style)
        {
            MovementCount = movementCount;
        }
    }

    public class Opera : Composition
    {
        public string Language { get; set; }

        public Opera(string title, string composer, TimeSpan duration, string style, string language)
            : base(title, composer, duration, style)
        {
            Language = language;
        }
    }


    public class DiskCollection
    {
        public List<Composition> Compositions { get; set; } = new List<Composition>();

        public void AddComposition(Composition composition)
        {
            Compositions.Add(composition);
        }

        public TimeSpan CalculateTotalDuration()
        {
            return new TimeSpan(Compositions.Sum(c => c.Duration.Ticks));
        }

        public List<Composition> RearrangeByStyle()
        {
            return Compositions.OrderBy(c => c.Style).ToList();
        }

        public List<Composition> FindCompositionsByDurationRangeAndSort(TimeSpan minDuration, TimeSpan maxDuration)
        {
            return Compositions.Where(c => c.Duration >= minDuration && c.Duration <= maxDuration)
                .OrderBy(c => c.Duration)
                .ToList();
        }

        public void WriteToDisk()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            File.WriteAllText("file.json", JsonSerializer.Serialize(Compositions, options));
            Console.WriteLine("\nКолекція успішно записана на диск.");
        }
    }


    class Program
    {
        static void Main()
        {
            var diskCollection = new DiskCollection();
            diskCollection.AddComposition(new Song("Yesterday", "Lennon McCartney", new TimeSpan(0, 2, 5), "Pop", "The Beatles", "Rock"));
            diskCollection.AddComposition(new Symphony("Symphony No. 9", "Beethoven", new TimeSpan(1, 5, 0), "Classical", 4));
            diskCollection.AddComposition(new Opera("The Magic Flute", "Mozart", new TimeSpan(2, 30, 0), "Classical", "German"));
            diskCollection.WriteToDisk();

            Console.WriteLine($"\nТривалість колекції: {diskCollection.CalculateTotalDuration()}");

            Console.WriteLine("\nКомпозиції, упорядковані за стилем:");
            foreach (var comp in diskCollection.RearrangeByStyle())
            {
                Console.WriteLine($"{comp.Style}: '{comp.Title}' by {comp.Composer}, Duration: {comp.Duration}");
            }

            var minDuration = new TimeSpan(0, 1, 0); 
            var maxDuration = new TimeSpan(0, 5, 0); 
            Console.WriteLine($"\nКомпозиції з довжиною від {minDuration} до {maxDuration} (відсорт):");
            foreach (var comp in diskCollection.FindCompositionsByDurationRangeAndSort(minDuration, maxDuration))
            {
                Console.WriteLine($"Знайдено: '{comp.Title}' by {comp.Composer}, Duration: {comp.Duration}");
            }

        }
    }
}
