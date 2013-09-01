using System;
using System.Collections.Generic;
using System.Linq;

namespace OwinIoCContainerSample
{
    public class Repository : IRepository
    {
        private static IEnumerable<string> __randomTexts = new List<string> 
        {
            "Yes! This is my favorite sound in the whole world",
            "Excellent. Don't hold back!",
            "Great job! It smells like roses in here now",
            "Sometimes you just have to let 'em rip",
            "Ahhh, that felt right",
            "Ooops, I'm not sure about that one",
            "Wow, someone has been practicing",
            "You might want to be a little careful",
            "What a stinker. The paint is coming off the walls"
        };

        public Repository()
        {
            Console.WriteLine("Foo constructor");
        }

        public string GetRandomText()
        {
            Console.WriteLine("Getting the random text");
            return __randomTexts.ElementAt(new Random().Next(__randomTexts.Count()));
        }

        public IEnumerable<string> GetTexts()
        {
            return __randomTexts;
        }

        public void Dispose()
        {
            Console.WriteLine("Foo dispose");
        }
    }
}
