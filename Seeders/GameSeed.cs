using Playmor_Asp.Data;
using Playmor_Asp.Models;

namespace Playmor_Asp.Seeders;

public class GameSeed
{
    private readonly DataContext dataContext;
    public GameSeed(DataContext context)
    {
        this.dataContext = context;
    }
    public void GameSeedDataContext()
    {
        if (!dataContext.Games.Any())
        {
            Console.WriteLine("Seeding Game Data");
            List<Game> games = [new Game()
            {
                Title = "Zenless Zone Zero",
                Description = "Zenless Zone Zero is a free-to-play action role-playing game developed and published by miHoYo (with publishing outside mainland China under Cognosphere, d/b/a HoYoverse). The game was released on Windows, iOS, Android and PlayStation 5 on July 4, 2024.",
                Details = "",
                Developer = ["miHoYo"],
                Publisher = ["miHoYo", "Cognosphere", "Nijigen Games", "Gamota"],
                Platforms = ["Windows", "iOS", "Android", "PlayStation 5"],
                Genres = ["Action role-playing", "hack and slash"],
                Modes = ["Single-player"],
                Cover = "https://en.wikipedia.org/wiki/Zenless_Zone_Zero#/media/File:Zenless_Zone_Zero_curved_box_logo.svg",
                Artwork = "https://asset.vg247.com/zenless-zone-zero_obqWbPP.jpg/BROK/thumbnail/1600x900/format/jpg/quality/80/zenless-zone-zero_obqWbPP.jpg",
                ReleaseDates =
                [
                    new ReleaseDate()
                    {
                        Platform = "Windows",
                        Date = DateTime.Parse("04/07/2024"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "iOS",
                        Date = DateTime.Parse("04/07/2024"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Android",
                        Date = DateTime.Parse("04/07/2024"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 5",
                        Date = DateTime.Parse("04/07/2024"),
                    }
                ],
                WebsiteLinks =
                [
                    new Website()
                    {
                        WebsiteName = "Other",
                        WebsiteLink = "https://zenless.hoyoverse.com/en-us/main"
                    }
                ]
            },
            new Game ()
            {
                Title = "Elden Ring",
                Description = "Elden Ring is a 2022 action role-playing game developed by FromSoftware. It was directed by Hidetaka Miyazaki with worldbuilding provided by American fantasy writer George R. R. Martin. It was published for PlayStation 4, PlayStation 5, Windows, Xbox One, and Xbox Series X/S on February 25 in Japan by FromSoftware and internationally by Bandai Namco Entertainment. Set in the Lands Between, players control a customizable player character on a quest to repair the Elden Ring and become the new Elden Lord.",
                Details = "",
                Developer = ["FromSoftware"],
                Publisher = ["Bandai Namco Entertainment", "FromSoftware"],
                Platforms = ["Windows", "PlayStation 4", "Xbox One", "Xbox Series X/S", "PlayStation 5"],
                Genres = ["Action role-playing"],
                Modes = ["Single-player", "Multiplayer"],
                Cover = "https://en.wikipedia.org/wiki/Elden_Ring#/media/File:Elden_Ring_Box_art.jpg",
                Artwork = "https://cdn.wccftech.com/wp-content/uploads/2021/06/ER_KEY-ART-scaled-e1623411764381-2048x1052.jpg",
                ReleaseDates =
                [
                    new ReleaseDate()
                    {
                        Platform = "Windows",
                        Date = DateTime.Parse("25/02/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 4",
                        Date = DateTime.Parse("25/02/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox One",
                        Date = DateTime.Parse("25/02/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox Series X/S",
                        Date = DateTime.Parse("25/02/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 5",
                        Date = DateTime.Parse("25/02/2022"),
                    }
                ],
                WebsiteLinks =
                [
                    new Website()
                    {
                        WebsiteName = "Steam",
                        WebsiteLink = "https://store.steampowered.com/app/1245620/ELDEN_RING/"
                    }
                ]
            },
            new Game ()
            {
                Title = "The Witcher 3: Wild Hunt",
                Description = "The Witcher 3: Wild Hunt is a 2015 action role-playing game developed and published by CD Projekt. It is the sequel to the 2011 game The Witcher 2: Assassins of Kings and the third game in The Witcher video game series, played in an open world with a third-person perspective. The games follow the Witcher series of fantasy novels written by Andrzej Sapkowski.",
                Details = "",
                Developer = ["CD Projekt Red"],
                Publisher = ["CD Projekt", "Sega"],
                Platforms = ["Windows", "PlayStation 4", "Xbox One", "Xbox Series X/S", "PlayStation 5", "Nintendo Switch"],
                Genres = ["Action role-playing"],
                Modes = ["Single-player"],
                Cover = "https://en.wikipedia.org/wiki/The_Witcher_3:_Wild_Hunt#/media/File:Witcher_3_cover_art.jpg",
                Artwork = "https://wallpaperaccess.com/full/147237.png",
                ReleaseDates =
                [
                    new ReleaseDate()
                    {
                        Platform = "Windows",
                        Date = DateTime.Parse("19/05/2015"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 4",
                        Date = DateTime.Parse("19/05/2015"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox One",
                        Date = DateTime.Parse("19/05/2015"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox Series X/S",
                        Date = DateTime.Parse("14/12/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 5",
                        Date = DateTime.Parse("14/12/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Nintendo Switch",
                        Date = DateTime.Parse("15/10/2019"),
                    }
                ],
                WebsiteLinks =
                [
                    new Website()
                    {
                        WebsiteName = "Steam",
                        WebsiteLink = "https://store.steampowered.com/app/292030/The_Witcher_3_Wild_Hunt/"
                    }
                ]
            },
            new Game ()
            {
                Title = "Outer Wilds",
                Description = "Outer Wilds is a 2019 action-adventure video game developed by Mobius Digital and published by Annapurna Interactive. The game follows the player character as they explore a planetary system stuck in a 22-minute time loop that resets after the sun goes supernova and destroys the system. Through repeated attempts they investigate the alien ruins of the Nomai to discover their history and the cause of the time loop.",
                Details = "",
                Developer = ["Mobius Digital"],
                Publisher = ["Annapurna Interactive"],
                Platforms = ["Windows", "PlayStation 4", "Xbox One", "Xbox Series X/S", "PlayStation 5", "Nintendo Switch"],
                Genres = ["Action-adventure"],
                Modes = ["Single-player"],
                Cover = "https://en.wikipedia.org/wiki/Outer_Wilds#/media/File:Outer_Wilds_Steam_artwork.jpg",
                Artwork = "https://cdn.wccftech.com/wp-content/uploads/2019/06/WCCFouterwilds1.jpg",
                ReleaseDates =
                [
                    new ReleaseDate()
                    {
                        Platform = "Windows",
                        Date = DateTime.Parse("28/05/2019"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 4",
                        Date = DateTime.Parse("15/10/2019"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox One",
                        Date = DateTime.Parse("28/05/2019"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox Series X/S",
                        Date = DateTime.Parse("15/09/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 5",
                        Date = DateTime.Parse("15/09/2022"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Nintendo Switch",
                        Date = DateTime.Parse("15/09/2022"),
                    }
                ],
                WebsiteLinks =
                [
                    new Website()
                    {
                        WebsiteName = "Steam",
                        WebsiteLink = "https://store.steampowered.com/app/753640/Outer_Wilds/"
                    }
                ]
            },
            new Game ()
            {
                Title = "Hollow Knight",
                Description = "Hollow Knight is a 2017 Metroidvania video game developed and published by independent developer Team Cherry. The player controls the Knight, an insectoid warrior exploring Hallownest, a fallen kingdom plagued by a supernatural disease. The game is set in diverse subterranean locations, featuring friendly and hostile insectoid characters and numerous bosses. Players have the opportunity to unlock abilities as they explore, along with pieces of lore and flavour text that are spread throughout the kingdom.",
                Details = "",
                Developer = ["Team Cherry"],
                Publisher = ["Team Cherry"],
                Platforms = ["Windows", "PlayStation 4", "Xbox One", "Linux", "macOS", "Nintendo Switch"],
                Genres = ["Metroidvania"],
                Modes = ["Single-player"],
                Cover = "https://en.wikipedia.org/wiki/Hollow_Knight#/media/File:Hollow_Knight_first_cover_art.webp",
                Artwork = "https://vignette.wikia.nocookie.net/hollowknight/images/7/79/Promo_04.png/revision/latest/scale-to-width-down/2000?cb=20181111043345",
                ReleaseDates =
                [
                    new ReleaseDate()
                    {
                        Platform = "Windows",
                        Date = DateTime.Parse("24/02/2017"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 4",
                        Date = DateTime.Parse("25/09/2018"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox One",
                        Date = DateTime.Parse("25/09/2018"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Linux",
                        Date = DateTime.Parse("11/04/2017"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "macOS",
                        Date = DateTime.Parse("11/04/2017"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Nintendo Switch",
                        Date = DateTime.Parse("12/06/2018"),
                    }
                ],
                WebsiteLinks =
                [
                    new Website()
                    {
                        WebsiteName = "Steam",
                        WebsiteLink = "https://store.steampowered.com/app/367520/Hollow_Knight/"
                    }
                ]
            },
            new Game ()
            {
                Title = "Disco Elysium",
                Description = "Disco Elysium is a 2019 role-playing video game developed and published by ZA/UM. Inspired by Infinity Engine-era games, particularly Planescape: Torment, the game was written and designed by a team led by Estonian novelist Robert Kurvitz and features an art style based on oil painting with music by the English band British Sea Power.",
                Details = "",
                Developer = ["ZA/UM"],
                Publisher = ["ZA/UM"],
                Platforms = ["Windows", "PlayStation 4", "PlayStation 5", "Xbox One", "Xbox Series X/S", "Stadia", "macOS", "Nintendo Switch"],
                Genres = ["Role-playing"],
                Modes = ["Single-player"],
                Cover = "https://en.wikipedia.org/wiki/Disco_Elysium#/media/File:Disco_Elysium_Poster.jpeg",
                Artwork = "https://sirus.b-cdn.net/wp-content/uploads/2021/08/Disco-Elysium-4-scaled.jpg",
                ReleaseDates =
                [
                    new ReleaseDate()
                    {
                        Platform = "Windows",
                        Date = DateTime.Parse("15 October 2019"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 4",
                        Date = DateTime.Parse("30 March 2021"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox One",
                        Date = DateTime.Parse("12 October 2021"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Xbox Series X",
                        Date = DateTime.Parse("12 October 2021"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 5",
                        Date = DateTime.Parse("30 March 2021"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Stadia",
                        Date = DateTime.Parse("30 March 2021"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "macOS",
                        Date = DateTime.Parse("27 April 2020"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Nintendo Switch",
                        Date = DateTime.Parse("12 October 2021"),
                    }
                ],
                WebsiteLinks =
                [
                    new Website()
                    {
                        WebsiteName = "Steam",
                        WebsiteLink = "https://store.steampowered.com/app/632470/Disco_Elysium__The_Final_Cut/"
                    }
                ]
            },
            new Game ()
            {
                Title = "Transistor",
                Description = "Transistor is an action role-playing video game developed and published by Supergiant Games. The game was released in May 2014 for Microsoft Windows and PlayStation 4, for OS X and Linux in October 2014, and iOS devices in June 2015. Transistor sold over one million copies across all platforms by December 2015.",
                Details = "",
                Developer = ["Supergiant Games"],
                Publisher = ["Supergiant Games"],
                Platforms = ["Windows", "PlayStation 4", "iOS", "Linux", "macOS", "Nintendo Switch"],
                Genres = ["Action role-playing", "turn-based strategy"],
                Modes = ["Single-player"],
                Cover = "https://en.wikipedia.org/wiki/Transistor_(video_game)#/media/File:Transistor_art.jpg",
                Artwork = "https://cdn1.epicgames.com/undefined/offer/Transistor_Primary_Promo-2580x1450-80b4204d1dbee38616e1484a262f1a94.jpg",
                ReleaseDates =
                [
                    new ReleaseDate()
                    {
                        Platform = "Windows",
                        Date = DateTime.Parse("May 20, 2014"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "PlayStation 4",
                        Date = DateTime.Parse("May 20, 2014"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "iOS",
                        Date = DateTime.Parse("June 11, 2015"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Linux",
                        Date = DateTime.Parse("October 30, 2014"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "macOS",
                        Date = DateTime.Parse("October 30, 2014"),
                    },
                    new ReleaseDate()
                    {
                        Platform = "Nintendo Switch",
                        Date = DateTime.Parse("November 1, 2018"),
                    }
                ],
                WebsiteLinks =
                [
                    new Website()
                    {
                        WebsiteName = "Steam",
                        WebsiteLink = "https://store.steampowered.com/app/237930/Transistor/"
                    }
                ]
            },
            ];
            dataContext.Games.AddRange(games);
            dataContext.SaveChanges();
        }
    }

}
