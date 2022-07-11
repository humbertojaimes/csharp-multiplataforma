using DTOs;

namespace SpeakersData;
public class SpeakersRepository
{

    public IEnumerable<Speaker> AllSpeakers() => new List<Speaker>()
        {
            new("SP01", "Jesús Gil", "https://stconference.blob.core.windows.net/photos/JesusGil.jpg"),
            new("SP02", "Luis Beltrán", "https://stconference.blob.core.windows.net/photos/LuisBeltran.jpg"),
            new("SP03", "Cristian Gonzalez", "https://stconference.blob.core.windows.net/photos/CristianGonzalez.jpg"),
            new("SP04", "Humberto Jaimes", "https://stconference.blob.core.windows.net/photos/HumbertoJaimes.jpg"),
        };

}

