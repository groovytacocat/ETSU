/**
*--------------------------------------------------------------------
* File Name: Program.cs
* Project Name: Playlist Manager
* Solution Name: Playlist Manager
*--------------------------------------------------------------------
* Author’s Name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/30/2024
* -------------------------------------------------------------------
*/

using MyDLL;

namespace PlaylistManager;

class Program
{
    static void Main(string[] args)
    {

        int userChoice;
        List<Album> discography = new List<Album>();
        List<Playlist> userPlaylists = new List<Playlist>();
        List<Song> masterList = new List<Song>();

        Console.WriteLine("Welcome to Playlist Manager");

        userPlaylists.Add(new Playlist("test"));

        do
        {
            DisplayMenu();
            userChoice = HoovenLib.Validate<int>("Please select an option: ", "You made an invalid selection. ", 1, 8);

            switch(userChoice)
            {
                case 1:
                    int numAlbums = HoovenLib.Validate<int>("\nHow many albums would you like to add? ", "Please enter a positive integer. ", 1, 100);

                    for(int i = 0; i < numAlbums; i++)
                    {
                        discography.Add(CreateAlbum(masterList));
                    }
                    break;
                case 2:
                    if(discography.Count == 0)
                    {
                        Console.WriteLine("\nNo albums to display. Consider adding some!");
                    }
                    else
                    {
                        for(int i = 0;i < discography.Count;i++)
                        {
                            Console.WriteLine($"{i + 1}. {discography[i].AlbumName}");
                        }

                        int albumChoice = HoovenLib.Validate<int>("Which album would you like more info on: ", $"Please make a valid choice between 1 and {discography.Count} ", 1, discography.Count) - 1;

                        Console.WriteLine(discography[albumChoice].ToString());
                    }
                    break;
                case 3:
                    int numSongs = HoovenLib.Validate<int>("How many songs would you like to add: ", "Please enter a positive integer ", 1, 100);

                    for(int i = 0; i < numSongs; i++)
                    {
                        masterList.Add(CreateSong());
                    }
                    break;
                case 4:
                    if(masterList.Count == 0)
                    {
                        Console.WriteLine("\nNo songs to display. Consider adding some!");
                    }
                    else
                    {
                        DisplaySongs(masterList);
                        
                        int songChoice = HoovenLib.Validate<int>("Which song would you like more info on: ", $"Please enter a valid choice between 1 and {masterList.Count}", 1, masterList.Count) - 1;

                        Console.WriteLine(masterList[songChoice].ToString());
                    }
                    break;
                case 5:
                    int numPlist = HoovenLib.Validate<int>("How many playlists would you like to make: ", "Please enter a positive integer. ", 1, 100);

                    for(int i = 0; i < numPlist; i++)
                    {
                        //TODO REWORK CREATE PLAYLIST
                    }
                    break;
                case 6:
                    if(userPlaylists.Count == 0)
                    {
                        Console.WriteLine("\nNo playlists to display. Consider adding some!");
                    }
                    else
                    {
                        Console.WriteLine();
                        for (int i = 0; i < userPlaylists.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {userPlaylists[i].Name}");
                        }

                        int playChoice = HoovenLib.Validate<int>("Which playlist would you like more information on: ", $"Please enter a valid choice between 1 and {userPlaylists.Count}. ", 1, userPlaylists.Count) - 1;

                        Console.WriteLine(userPlaylists[playChoice].ToString());

                        if(HoovenLib.Repeat("Would you like information on any of these songs (Y/N)? ", "Please enter Y/y for Yes or N/n for No. "))
                        {
                            DisplaySongs(userPlaylists[playChoice].TrackList);

                            int songChoice = HoovenLib.Validate<int>("Which song would you like info about: ", "Please enter a choice between 1 and {userPlaylists[playChoice].TrackList.Count}. ", 1, userPlaylists[playChoice].TrackList.Count) - 1;

                            Console.WriteLine(userPlaylists[playChoice].GetSong(songChoice));
                        }

                        if(HoovenLib.Repeat("Would you like to shuffle this playlist (Y/N)? ", "Please enter Y/y for Yes or N/n for No. "))
                        {
                            userPlaylists[playChoice].Shuffle();
                            userPlaylists[playChoice].ToString();
                        }
                    }
                    break;
                case 7:
                    if (userPlaylists.Count == 0)
                    {
                        Console.WriteLine("\nNo playlists to edit. Consider adding some!");
                    }
                    else
                    {
                        do
                        {
                            Console.WriteLine();
                            for (int i = 0; i < userPlaylists.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {userPlaylists[i].Name}");
                            }

                            int playChoice = HoovenLib.Validate<int>("\nWhich playlist would you like to edit: ", $"Please enter a valid choice between 1 and {userPlaylists.Count}. ", 1, userPlaylists.Count) - 1;

                            int editChoice = HoovenLib.Validate<int>("\n1. Add Song(s)\n2. Remove Song(s)\nWhich would you like to do: ", "Please select 1 to Add or 2 to Remove. ", 1, 2);

                            if (editChoice == 1)
                            {
                                if (masterList.Count == 0)
                                {
                                    Console.WriteLine("\nNo songs available to add right now!");
                                }
                                else
                                {
                                    for (int i = 0; i < masterList.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {masterList[i].Name} - {masterList[i].Artist}");
                                    }

                                    int songToAdd = HoovenLib.Validate<int>("Which song would you like to add: ", $"Please enter a valid choice between 1 and {masterList.Count}. ", 1, masterList.Count) - 1;

                                    userPlaylists[playChoice].AddSong(masterList[songToAdd]);
                                }
                            }
                            else
                            {
                                if (userPlaylists[playChoice].TrackList.Count == 0)
                                {
                                    Console.WriteLine("\nCannot remove songs from an empty playlist!");
                                }
                                else
                                {
                                    for (int i = 0; i < masterList.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {masterList[i].Name} - {masterList[i].Artist}");
                                    }

                                    int songToRemove = HoovenLib.Validate<int>("Which song would you like to remove: ", $"Please enter a valid choice between 1 and {masterList.Count}. ", 1, masterList.Count) - 1;

                                    userPlaylists[playChoice].AddSong(masterList[songToRemove]);
                                }
                            }
                        } while (HoovenLib.Repeat("\nWould you like to continue editing (Y/N): ", "Please enter Y/y for Yes or N/n for No. "));
                    }
                    break;
                case 8:
                    Console.WriteLine("Goodbye!");
                    break;
            }

        } while (userChoice != 8);

    } //END MAIN
    /*
        1. Add Album                    DONE
        2. View Albums                  DONE
            1. View Info on Album       DONE
        3. Add Song                     DONE
        4. View All Songs               DONE
            1. View info on Song        DONE
        5. Create Playlist          
        6. View Playlists               DONE
            1. Get Playlist Info        DONE
                i. Shuffle Playlist     DONE
        7. Edit Playlist(s)
            1. Add Song(s)
            2. Remove Song(s)
        8. Exit                         DONE
     */
    /// <summary>
    /// Displays all <see cref="Song"/>s that are present in the <see cref="List{T}"/> of <see cref="Song"/>s provided
    /// </summary>
    /// <param name="allSongs">Collection of <see cref="Song"/>s containing all songs</param>
    public static void DisplaySongs(List<Song> allSongs)
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
                Console.WriteLine($"{i + 1}. {allSongs[i].Name} - {allSongs[i].Artist}");
            }
        }
    }//END DisplaySongs()

    /// <summary>
    /// Creates a <see cref="Song"/> by prompting user for necessary information. Similar to <see cref="CreateAlbum(List{Song})"/> and <see cref="CreatePlaylist(List{Album})"/>
    /// **This is for songs that do not have an album. 
    /// </summary>
    /// <param name="userSong"></param>
    /// <returns>a <see cref="Song"/> defined by the user without an Album attached</returns>
    public static Song CreateSong()
    {
        Genre genre;

        Console.Write("What is the name of the Song: ");
        string songName = Console.ReadLine();

        int songLength = HoovenLib.Validate<int>("What is the length of the song (in seconds): ", "Please enter a valid positive integer. ");

        Console.Write("What is the Artist/Band Name: ");
        string songArtist = Console.ReadLine();

        Console.Write("Enter the Genre (Rock/HipHop/Rap/Pop/Country/Other): ");
        string genreString = Console.ReadLine();

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
            default:
                Console.WriteLine("Your selected genre was not listed and will be set as 'Other'");
                genre = Genre.Other;
                break;
        }

        return new Song(songName, songLength, songArtist, genre);
    }//END CreateSong()

    /// <summary>
    /// Creates a <see cref="Playlist"/> by prompting the user for the necessary information, similar to <see cref="CreateAlbum(List{Song})"/>
    /// Requires passing in an <see cref="Album"/>
    /// </summary>
    /// <param name="albumCollections">Collection of <see cref="Album"/s to display all <see cref="Song"/>s to the user></param>
    /// <returns>a user created <see cref="Playlist"/></returns>
    public static Playlist CreatePlaylist(List<Album> albumCollections)
    {
        Console.Write("\nName your playlist: ");
        string name = Console.ReadLine();

        Playlist playlist = new Playlist(name);

        do
        {
            for(int i = 0; i < albumCollections.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {albumCollections[i]}");
            }

            int albumChoice = HoovenLib.Validate<int>("\nSelect an Album to add songs from: ", $"Please enter a positive integer between 1 and {albumCollections.Count}", 1, albumCollections.Count) - 1;

            int addChoice = HoovenLib.Validate<int>("Would you like to:\n1. Add All songs from this album to the playlist\n2. Add select songs\nChoice: ", "Invalid choice. ", 1, 2);

            if(addChoice == 1)
            {
                foreach(Song s in albumCollections[albumChoice].TrackList)
                {
                    if(!playlist.TrackList.Contains(s))
                    {
                        Console.WriteLine($"\n{s.Name} added to playlist!");
                        playlist.TrackList.Add(s);
                    }
                    else
                    {
                        Console.WriteLine($"\n{s.Name} is already in your playlist");
                    }
                }
            }
            else
            {
                Console.WriteLine();
                for (int i = 0; i < albumCollections[albumChoice].TrackList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {albumCollections[albumChoice].TrackList[i].Name}");
                }

                Console.Write("Which song would you like to add: ");
                int songChoice = HoovenLib.Validate<int>("Which song would you like to add: ", $"Please enter a positive integer between 1 and {albumCollections[albumChoice].TrackList.Count}\n", 1, albumCollections[albumChoice].TrackList.Count) - 1;

                Song songToAdd = albumCollections[albumChoice].TrackList[songChoice];

                if (playlist.TrackList.Contains(songToAdd))
                {
                    Console.WriteLine("That song is already in your playlist!");
                }
                else
                {
                    Console.WriteLine($"\n{songToAdd.Name} added to playlist!");
                    playlist.AddSong(songToAdd);
                }
            }
        } while (HoovenLib.Repeat("Would you like to add another? Y/N: ", "Please enter Y/y for Yes or N/n for No "));

        return playlist;
    }//END CreatePlaylist()

    /// <summary>
    /// Creates an <see cref="Album"/> by prompting the User for all necessary input, and will add the <see cref="Song"/>s to the Master list 
    /// </summary>
    /// <returns>The <see cref="Album"/> created by a user</returns>
    public static Album CreateAlbum(List<Song> masterList)
    {
        string albumName, bandName, genreString, memberName;
        int numSongs, numMembers, releaseDate;
        List<string> band = new List<string>();
        Genre genre;

        Console.Write("\nWhat is the Album Name: ");
        albumName = Console.ReadLine();

        Console.Write("What is the Band Name: ");
        bandName = Console.ReadLine();

        numMembers = HoovenLib.Validate<int>("How many band members are there: ", "Please enter a valid positive integer. ", 0, 100);

        for(int i = 0; i < numMembers; i++)
        {
            Console.Write("Enter Band Member Name: ");
            memberName = Console.ReadLine();

            band.Add(memberName);
        }

        releaseDate = HoovenLib.Validate<int>("What year was this album released: ", "Please enter a valid year (1889 - Present). ", 1889, 2024);

        Console.Write("Enter the Album Genre (Rock/HipHop/Rap/Pop/Country/Other): ");
        genreString = Console.ReadLine();

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
            default:
                Console.WriteLine("Your selected genre was not listed and will be set as 'Other'");
                genre = Genre.Other;
                break;
        }

        Album album = new Album(albumName, bandName, band, releaseDate);

        numSongs = HoovenLib.Validate<int>("\nHow many songs are you adding to this album: ", "Please enter a valid integer from 1 to 30", 1, 30);

        for (int i = 0; i < numSongs; i++)
        {
            string songName;
            int duration;

            Console.Write("\nEnter the song name: ");
            songName = Console.ReadLine();

            duration = HoovenLib.Validate<int>("Enter the song's duration (in seconds): ", "Please enter a valid integer from 1 to 600", 1, 600);

            Song s = new Song(songName, duration, album.BandName, album, genre);

            album.AddSong(s);
        }

        return album;
    } //END CreateAlbum()

    /// <summary>
    /// Displays a string representing Main Menu options.
    /// </summary>
    public static void DisplayMenu()
    {
        Console.WriteLine($"\n1. Add Album(s)\n2. View Album(s)\n3. Add Songs\n4. View Songs\n5. Create Playlist(s)\n6. View Playlist(s)\n7. Edit Playlist(s)\n8. Exit");
    }
}
