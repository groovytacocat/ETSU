/*
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

        if(HoovenLib.Repeat("\nMr. Buchanan (or Grader) would you like some pre-existing data populated? (Y/N): ", "Please enter Y/y for yes or N/n for no. "))
        {
            Console.WriteLine("\nCreating some songs, albums, and playlists for you\n");
            MrBuchanan(masterList, discography, userPlaylists);
        }

        ///<summary>
        /// Do-While Loop that uses <see cref="DisplayMenu()"/> to show user the Main Menu
        /// Makes liberal use of <see cref="HoovenLib.Validate{T}"/> methods to prompt user for various input and performs validation to ensure
        /// that input is realistic and proper.
        ///
        /// Allows the user to make <see cref="Album"/>s, <see cref="Song"/>s, and <see cref="Playlist"/>s
        /// This will not allow a <see cref="Playlist"/> to be created until there is an <see cref="Album"/> or <see cref="Song"/>s that have been added
        ///
        /// If User wishes to get more information on any of the above it will display the item's relevant <see cref="ToString()"/> method.
        ///
        /// Additionally once a <see cref="Playlist"/> has been created allows user to view all <see cref="Playlist"/>s, and shuffle the order if desired
        /// </summary>
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
                        Console.WriteLine();
                        for(int i = 0;i < discography.Count;i++)
                        {
                            Console.WriteLine($"{i + 1}. {discography[i].Name}");
                        }

                        int albumChoice = HoovenLib.Validate<int>("\nWhich album would you like more info on: ", $"Please make a valid choice between 1 and {discography.Count} ", 1, discography.Count) - 1;

                        Console.WriteLine($"\n{discography[albumChoice].ToString()}");
                    }
                    break;
                case 3:
                    int numSongs = HoovenLib.Validate<int>("\nHow many songs would you like to add: ", "Please enter a positive integer ", 1, 100);

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
                        
                        int songChoice = HoovenLib.Validate<int>("\nWhich song would you like more info on: ", $"Please enter a valid choice between 1 and {masterList.Count}. ", 1, masterList.Count) - 1;

                        Console.WriteLine($"\n{masterList[songChoice].ToString()}");
                    }
                    break;
                case 5:
                    if(masterList.Count == 0)
                    {
                        Console.WriteLine("\nCannot make a playlist until you have some songs!");
                    }
                    else
                    {
                        userPlaylists.Add(CreatePlaylist(masterList));
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

                        int playChoice = HoovenLib.Validate<int>("\nWhich playlist would you like more information on: ", $"Please enter a valid choice between 1 and {userPlaylists.Count}. ", 1, userPlaylists.Count) - 1;

                        Console.WriteLine($"\n{userPlaylists[playChoice].ToString()}");

                        while (HoovenLib.Repeat("\nWould you like information on any of these songs (Y/N)? ", "Please enter Y/y for Yes or N/n for No. "))
                        {
                            DisplaySongs(userPlaylists[playChoice].TrackList);

                            int songChoice = HoovenLib.Validate<int>("Which song would you like info about: ", "Please enter a choice between 1 and {userPlaylists[playChoice].TrackList.Count}. ", 1, userPlaylists[playChoice].TrackList.Count) - 1;

                            Console.WriteLine($"\n{userPlaylists[playChoice].GetSong(songChoice)}");
                        }

                        while(HoovenLib.Repeat("\nWould you like to shuffle this playlist (Y/N)? ", "Please enter Y/y for Yes or N/n for No. "))
                        {
                            userPlaylists[playChoice].Shuffle();
                            Console.WriteLine($"\n{userPlaylists[playChoice].ToString()}");
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
                                    Console.WriteLine();
                                    for (int i = 0; i < masterList.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {masterList[i].Name} - {masterList[i].Artist}");
                                    }

                                    int songToAdd = HoovenLib.Validate<int>("\nWhich song would you like to add: ", $"Please enter a valid choice between 1 and {masterList.Count}. ", 1, masterList.Count) - 1;

                                    if (userPlaylists[playChoice].TrackList.Contains(masterList[songToAdd]))
                                    {
                                        Console.WriteLine($"\n{masterList[songToAdd].Name} already in playlist\n");
                                    }
                                    else
                                    {
                                        userPlaylists[playChoice].AddSong(masterList[songToAdd]);
                                        Console.WriteLine($"\n{masterList[songToAdd].Name} added to playlist!");
                                    }

                                    Console.WriteLine($"\n{userPlaylists[playChoice].ToString()}");
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
                                    Console.WriteLine();
                                    for (int i = 0; i < masterList.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {masterList[i].Name} - {masterList[i].Artist}");
                                    }

                                    int songToRemove = HoovenLib.Validate<int>("Which song would you like to remove: ", $"Please enter a valid choice between 1 and {masterList.Count}. ", 1, masterList.Count) - 1;

                                    userPlaylists[playChoice].RemoveSong(masterList[songToRemove]);

                                    Console.WriteLine($"\n{userPlaylists[playChoice].ToString()}");
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

    #region DisplaySongs()
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
    }
    #endregion

    #region CreateSong()
    /// <summary>
    /// Creates a <see cref="Song"/> by prompting user for necessary information. Similar to <see cref="CreateAlbum(List{Song})"/> and <see cref="CreatePlaylist(List{Album})"/>
    /// **This is for songs that do not have an album. 
    /// </summary>
    /// <param name="userSong"></param>
    /// <returns>a <see cref="Song"/> defined by the user without an Album attached</returns>
    public static Song CreateSong()
    {
        Genre genre;

        Console.Write("\nWhat is the name of the Song: ");
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
    }
    #endregion

    #region CreatePlaylist()
    /// <summary>
    /// Method to create a playlist by selecting individual songs, rather than from albums
    /// </summary>
    /// <param name="songCollection"><see cref="List{T}"/> of <see cref="Song"/>s</param>
    /// <returns>a <see cref="Playlist"/> with the <see cref="Song"/>s added by the user</returns>
    public static Playlist CreatePlaylist(List<Song> songCollection)
    {
        Console.Write("\nName your playlist: ");
        string name = Console.ReadLine();

        Playlist playlist = new Playlist(name);

        do
        {
            for (int i = 0; i < songCollection.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {songCollection[i].Name} - {songCollection[i].Artist}");
            }

            int songChoice = HoovenLib.Validate<int>("\nSelect a Song to add: ", $"Please enter a positive integer between 1 and {songCollection.Count}", 1, songCollection.Count) - 1;

            if (playlist.TrackList.Contains(songCollection[songChoice]))
            {
                Console.WriteLine($"\n{songCollection[songChoice].Name} already in playlist!");
            }
            else
            {
                Console.WriteLine($"\n{songCollection[songChoice].Name} added to playlist!");
                playlist.AddSong(songCollection[songChoice]);
            }
        } while (HoovenLib.Repeat("\nWould you like to add another? Y/N: ", "Please enter Y/y for Yes or N/n for No "));

        return playlist;
    }
    #endregion

    #region CreateAlbum()
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
            case "other":
                genre = Genre.Other;
                break;
            default:
                Console.WriteLine("Your selected genre was not listed and will be set as 'Other'");
                genre = Genre.Other;
                break;
        }

        Album album = new Album(albumName, bandName, band, releaseDate);

        numSongs = HoovenLib.Validate<int>("\nHow many songs are you adding to this album: ", "Please enter a valid integer from 1 to 30. ", 1, 30);

        for (int i = 0; i < numSongs; i++)
        {
            string songName;
            int duration;

            Console.Write("\nEnter the song name: ");
            songName = Console.ReadLine();

            duration = HoovenLib.Validate<int>("Enter the song's duration (in seconds): ", "Please enter a valid integer from 1 to 600. ", 1, 600);

            Song s = new Song(songName, duration, album.BandName, album, genre);

            masterList.Add(s);
            album.AddSong(s);
        }

        return album;
    }
    #endregion

    /// <summary>
    /// Displays a string representing Main Menu options.
    /// </summary>
    public static void DisplayMenu()
    {
        Console.WriteLine($"\n1. Add Album(s)\n2. View Album(s)\n3. Add Songs\n4. View Songs\n5. Create Playlist(s)\n6. View Playlist(s)\n7. Edit Playlist(s)\n8. Exit");
    }

    #region MrBuchanan
    /// <summary>
    /// Creates an Album representing Olivia Rodrigo's most recent album GUTS, then creates all the songs on that album and adds them in
    /// Creates Beethoven's Fur Elise as a song
    /// Creates a 'Demo' playlist containing a few songs from the GUTS album and Fur Elise
    /// </summary>
    /// <param name="allSongs"><see cref="List{T}"/> containing all <see cref="Song"/>s</param>
    /// <param name="allAlbums"><see cref="List{T}"/> containing all <see cref="Album"/>s</param>
    /// <param name="allPlays"><see cref="List{T}"/> containing all <see cref="Playlist"/>s</param>
    public static void MrBuchanan(List<Song> allSongs, List<Album> allAlbums, List<Playlist> allPlays)
    {
        Album testing = new Album("GUTS", "Olivia Rodrigo", new List<string>(), 2023);

        Song firstSong = new Song("vampire", 219, "Olivia Rodrigo", testing, Genre.Pop);
        Song secondSong = new Song("Lacy", 177, "Olivia Rodrigo", testing, Genre.Pop);
        Song thirdSong = new Song("Logical", 231, "Olivia Rodrigo", testing, Genre.Pop);
        Song fourthSong = new Song("All-American Bitch", 165, "Olivia Rodrigo", testing, Genre.Pop);
        Song fifthSong = new Song("Ballad of a Homeschooled Girl", 203, "Olivia Rodrigo", testing, Genre.Pop);
        Song sixthSong = new Song("Bad Idea Right?", 184, "Olivia Rodrigo", testing, Genre.Pop);
        Song seventhSong = new Song("Making the Bed", 198, "Olivia Rodrigo", testing, Genre.Pop);
        Song eigthSong = new Song("Get Him Back!", 211, "Olivia Rodrigo", testing, Genre.Pop);
        Song ninthSong = new Song("Love is embarrassing", 154, "Olivia Rodrigo", testing, Genre.Pop);
        Song tenthSong = new Song("The Grudge", 189, "Olivia Rodrigo", testing, Genre.Pop);
        Song eleventhSong = new Song("Pretty isn't Pretty", 199, "Olivia Rodrigo", testing, Genre.Pop);
        Song twelfthSong = new Song("Teenage Dream", 222, "Olivia Rodrigo", testing, Genre.Pop);
        Song lastSong = new Song("Fur Elise", 180, "Beethoven", Genre.Other);

        Playlist demo = new Playlist("Demo");

        testing.AddSong(firstSong);
        testing.AddSong(secondSong);
        testing.AddSong(thirdSong);
        testing.AddSong(fourthSong);
        testing.AddSong(fifthSong);
        testing.AddSong(sixthSong);
        testing.AddSong(seventhSong);
        testing.AddSong(eigthSong);
        testing.AddSong(ninthSong);
        testing.AddSong(tenthSong);
        testing.AddSong(eleventhSong);
        testing.AddSong(twelfthSong);

        foreach(Song s in testing.TrackList)
        {
            allSongs.Add(s);
        }

        allSongs.Add(lastSong);

        allAlbums.Add(testing);

        demo.AddSong(firstSong);
        demo.AddSong(thirdSong);
        demo.AddSong(lastSong);

        allPlays.Add(demo);
    }
    #endregion
}