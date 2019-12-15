using System.Collections.Generic;

namespace SantanderBestStoriesAPI
{
    public static class Stories
    {
        public static string TopTwenty { get { return "_TopTwenty"; } }        
    }

#pragma warning disable IDE1006 // Naming Styles
    public class Story
    {
        public string by { get; set; }
        public int descendants { get; set; }
        public int id { get; set; }
        public List<int> kids { get; set; }
        public int score { get; set; }
        public int time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }
}
