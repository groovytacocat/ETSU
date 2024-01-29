/**
*--------------------------------------------------------------------
* File name: Program.cs
* Project name: Playlist Manager
* Solution name: Playlist Manager
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/26/2024
* -------------------------------------------------------------------
*/

using MyDLL;

namespace PlaylistManager;

class Program
{
    //BEGIN MAIN
    static void Main(string[] args)
    {
        
        int userChoice;

        List<Album> discography = new List<Album>();
        List<Playlist> userPlaylists = new List<Playlist>();

        Console.WriteLine("Welcome to Playlist Manager");

        
        do
        {
            DisplayMenu();
            userChoice = HoovenLib.Validate<int>("Please select an option: ", "You made an invalid selection. ", 1, 7);

            switch(userChoice)
            {
                case 1:
                    int numAlbums = HoovenLib.Validate<int>("\nHow many albums would you like to add? ", "Please enter a positive integer. ", 1, 1000);

                    for(int i = 0; i < numAlbums; i++)
                    {
                        discography.Add(CreateAlbum());
                    }

                    break;
                case 2:
                    if (discography.Count == 0)
                    {
                        Console.WriteLine("\nNo Albums to display. Consider adding some!");
                    }
                    else
                    {
                        foreach (Album a in discography)
                        {
                            Console.WriteLine($"\n{a.ToString()}");
                        }
                    }
                    break;
                case 3:
                    DisplaySongs(discography);
                    break;
                case 4:
                    if (discography.Count == 0)
                    {
                        Console.WriteLine("\nCannot make a playlist until you add some music");
                    }
                    else
                    {
                        userPlaylists.Add(CreatePlaylist(discography));
                    }
                    break;
                case 5:
                    if (userPlaylists.Count == 0)
                    {
                        Console.WriteLine("\nNo Playlists to display. Consider making one!");
                    }
                    else
                    {
                        foreach (Playlist p in userPlaylists)
                        {
                            Console.WriteLine(p.ToString());
                        }
                    }
                    break;
                case 6:
                    if(userPlaylists.Count == 0)
                    {
                        Console.WriteLine("\nYou must make a playlist before you can edit one");
                    }
                    else
                    {
                        int playChoice = HoovenLib.Validate<int>("\nWhich playlist would you like to edit: ", $"Please choose a number between 1 and {userPlaylists.Count}", 1, userPlaylists.Count);
                    }
                    break;
                case 7:
                    Console.WriteLine("Goodbye!");
                    break;
            }

        } while (userChoice != 7);
        
    } //END MAIN


    /*
        1. Add Album //DONE
        2. View Albums
            1. View Info on Album
        3. Add Song
        4. View All Songs
            1. View info on Song
        5. Create Playlist //DONE
        6. View Playlists
            1. Get Playlist Info
                i. Display Song Info
            2. Add Song
            3. Remove Song
            4. Shuffle Playlist
        7. Exit


    COMMON FUNCTIONALITIES?
        * View Info (Album, Song, Playlist)

                        for (int i = 0; i < playlist.TrackList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {playlist.TrackList[i]}");
                    }

                    int removeChoice = HoovenLib.Validate<int>("\nWhich song would you like to remove: ", $"Please enter a valid choice from 1 to {playlist.TrackList.Count} ", 1, playlist.TrackList.Count) - 1;

                    Console.WriteLine($"{playlist.TrackList[removeChoice]} has been removed!");

                    playlist.RemoveSong(playlist.TrackList[removeChoice]);
    */

    public static void DisplaySongs(List<Album> allSongs)
    {
        Console.WriteLine();
        if (allSongs.Count == 0)
        {
            Console.WriteLine("\nNo Songs to display");
        }
        else
        {
            for (int i = 0; i < allSongs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allSongs[i].AlbumName}");
                foreach (Song s in allSongs[i].TrackList)
                {
                    Console.WriteLine($"\t-{s.name}");
                }
            }
        }
    }


    public static Playlist CreatePlaylist(List<Album> songCollection)
    {
        Console.Write("\nName your playlist: ");
        string name = Console.ReadLine();

        Playlist playlist = new Playlist(name);

        do
        {
            DisplaySongs(songCollection);

            int albumChoice = HoovenLib.Validate<int>("\nSelect an Album to add songs from: ", $"Please enter a positive integer between 1 and {songCollection.Count}", 1, songCollection.Count) - 1;

            int addChoice = HoovenLib.Validate<int>("Would you like to:\n1. Add All songs from this album to the playlist\n2. Add select songs\nChoice: ", "Invalid choice. ", 1, 2);

            if(addChoice == 1)
            {
                foreach(Song s in songCollection[albumChoice].TrackList)
                {
                    if(!playlist.TrackList.Contains(s))
                    {
                        Console.WriteLine($"\n{s.name} added to playlist!");
                        playlist.TrackList.Add(s);
                    }
                    else
                    {
                        Console.WriteLine($"\n{s.name} is already in your playlist");
                    }
                }
            }
            else
            {
                Console.WriteLine();
                for (int i = 0; i < songCollection[albumChoice].TrackList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {songCollection[albumChoice].TrackList[i].name}");
                }

                Console.Write("Which song would you like to add: ");
                int songChoice = HoovenLib.Validate<int>("Which song would you like to add: ", $"Please enter a positive integer between 1 and {songCollection[albumChoice].TrackList.Count}\n", 1, songCollection[albumChoice].TrackList.Count) - 1;

                Song songToAdd = songCollection[albumChoice].TrackList[songChoice];

                if (playlist.TrackList.Contains(songToAdd))
                {
                    Console.WriteLine("That song is already in your playlist!");
                }
                else
                {
                    Console.WriteLine($"\n{songToAdd.name} added to playlist!");
                    playlist.AddSong(songToAdd);
                }
            }
        } while (HoovenLib.Repeat("Would you like to add another? Y/N: ", "Please enter Y/y for Yes or N/n for No "));

        return playlist;
    }

    public static Album CreateAlbum()
    {
        string genreString, memberName;
        int numSongs, numMembers;
        List<string> band = new List<string>();
        Genre genre = Genre.Pop;
        List<string> genres = new List<string> { "rock", "hiphop", "pop", "rap", "country" };

        Album album = new Album();

        Console.Write("\nWhat is the Album Name: ");
        album.AlbumName = Console.ReadLine();

        Console.Write("What is the Band Name: ");
        album.bandName = Console.ReadLine();

        numMembers = HoovenLib.Validate<int>("How many band members are there", "Please enter a valid positive integer. ", 1, 100);

        for(int i = 0; i < numMembers; i++)
        {
            Console.Write("Enter Band Member Name: ");
            memberName = Console.ReadLine();

            band.Add(memberName);
        }

        album.releaseDate = HoovenLib.Validate<int>("What year was this album released: ", "Please enter a valid year (1889 - Present). ", 1889, 2024);

        do
        {
            Console.Write("Enter the Album Genre (Rock/HipHop/Rap/Pop/Country): ");
            genreString = Console.ReadLine();

            if(!genres.Contains(genreString.ToLower()))
            {
                Console.WriteLine("You made an invalid selection.");
            }
        } while (!genres.Contains(genreString.ToLower()));

        switch (genreString.ToLower())
        {
            case "rock":
                genre = Genre.Rock;
                break;
            case "hiphop":
                genre = Genre.HipHop;
                break;
            case "rap":
                genre = Genre.Rap;
                break;
            case "pop":
                genre = Genre.Pop;
                break;
            case "country":
                genre = Genre.Country;
                break;
        }

        numSongs = HoovenLib.Validate<int>("\nHow many songs are you adding to this album: ", "Please enter a valid integer from 1 to 30", 1, 30);

        for (int i = 0; i < numSongs; i++)
        {
            string songName;
            int duration;

            Console.Write("\nEnter the song name: ");
            songName = Console.ReadLine();

            duration = HoovenLib.Validate<int>("Enter the song's duration (in seconds): ", "Please enter a valid integer from 1 to 600", 1, 600);

            Song s = new Song(songName, duration, album.bandName, album, genre);

            album.AddSong(s);
        }

        return album;
    }

    public static void DisplayMenu()
    {
        Console.WriteLine($"\n1. Add Album(s)\n2. View Album(s)\n3. View Songs\n4. Create Playlist\n5. View Playlists\n6. Edit Playlist(s)\n7. Exit");
    }
}

