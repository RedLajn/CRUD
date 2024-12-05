using FunWithSQL.Model;
using FunWithSQL.Repo;

var newSongRepo = new SQLSongRepository();
Console.WriteLine("Co chcialbys zrobic? Wcisnij: \n (1)Dodaj piosenke \n (2)Usun piosenke \n (3)Zmien piosenke \n (4)Wyswietl wszystkie piosenki \n (5)Wyswietl piosenki z filtrem");

ConsoleKeyInfo a = Console.ReadKey();
int b;

while (!int.TryParse(a.KeyChar.ToString(), out b) || b < 1 || b > 5)
{
    Console.Clear();
    Console.WriteLine("Nieprawidlowy wybor. Wcisnij cyfre od 1 do 5: ");
    a = Console.ReadKey();
}

string title, album, artist = "";
int year;
bool validYear;
var song = new Song(0, "0", "0", 0, "0");
int id = 0;

Console.Clear();

switch (b)
{
    case 1:
        title = GetValidInput("Podaj tytul: ");
        album = GetValidInput("Podaj album: ");
        artist = GetValidInput("Podaj artyste: ");
        year = GetValidYearInput("Podaj rok wydania: ");

        id = newSongRepo.GetNewId();
        song = new Song(id + 1, title, album, year, artist);
        newSongRepo.AddSong(song);
        Console.WriteLine("Piosenka zostala dodana!");
        break;

    case 2: 
        title = GetValidInput("Podaj tytul: ");
        album = GetValidInput("Podaj album: ");
        artist = GetValidInput("Podaj artyste: ");
        year = GetValidYearInput("Podaj rok wydania: ");

        song = new Song(0, title, album, year, artist);
        newSongRepo.DeleteSong(song);
        Console.WriteLine("Piosenka zostala usunieta!");
        break;

    case 3: 
        Console.WriteLine("Podaj dane piosenki, ktora chcesz uaktualnic:");
        title = GetValidInput("Podaj tytul: ");
        album = GetValidInput("Podaj album: ");
        artist = GetValidInput("Podaj artyste: ");
        year = GetValidYearInput("Podaj rok wydania: ");
        song = new Song(0, title, album, year, artist);

        Console.WriteLine("Podaj nowe dane:");
        string newTitle = GetValidInput("Podaj nowy tytul: ");
        string newAlbum = GetValidInput("Podaj nowy album: ");
        string newArtist = GetValidInput("Podaj nowego artyste: ");
        int newYear = GetValidYearInput("Podaj nowy rok wydania: ");
        var newSong = new Song(0, newTitle, newAlbum, newYear, newArtist);

        newSongRepo.UpdateSong(song, newSong);
        Console.WriteLine("Piosenka zostala uaktualniona!");
        break;

    case 4: 
        var songs = newSongRepo.ReadAll();
        Console.WriteLine("Lista wszystkich piosenek:");
        foreach (var s in songs)
        {
            Console.WriteLine(s);
        }
        break;

    case 5: 
        Console.WriteLine("Wzgledem ktorego parametru: \n (1)Tytul \n (2)Album \n (3)Rok \n (4)Artysta ");
        ConsoleKeyInfo c = Console.ReadKey();
        int d;

        while (!int.TryParse(c.KeyChar.ToString(), out d) || d < 1 || d > 4)
        {
            Console.Clear();
            Console.WriteLine("Nieprawidlowy wybor. Wcisnij cyfre od 1 do 4: ");
            c = Console.ReadKey();
        }

        string param = d switch
        {
            1 => "Title",
            2 => "Album",
            3 => "Year",
            4 => "Artist",
            _ => "Title"
        };

        Console.Clear();
        string filter = GetValidInput("Podaj filtr: ");
        var filteredSongs = newSongRepo.ReadAllByFilter(param, filter);

        Console.WriteLine("Piosenki spelniajace kryteria:");
        foreach (var fs in filteredSongs)
        {
            Console.WriteLine(fs);
        }
        break;

    default:
        Console.WriteLine("Nieprawidlowy wybor.");
        break;
}

static string GetValidInput(string prompt)
{
    string input;
    do
    {
        Console.WriteLine(prompt);
        input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Nie moze byc puste. Sprobuj ponownie.");
        }
    } while (string.IsNullOrWhiteSpace(input));

    return input;
}

static int GetValidYearInput(string prompt)
{
    string input;
    int year;

    do
    {
        Console.WriteLine(prompt);
        input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Rok nie moze byc pusty. Sprobuj ponownie.");
            continue; 
        }

        if (!int.TryParse(input, out year))
        {
            Console.WriteLine("Niepoprawny rok. Wprowadz liczbe calkowita.");
        }
        else
        {
            return year; 
        }
    } while (true);
}