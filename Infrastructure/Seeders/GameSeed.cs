using Playmor_Asp.Domain.Models;
using Playmor_Asp.Infrastructure.Data;

namespace Playmor_Asp.Infrastructure.Seeders;

public class GameSeed
{
    private readonly DataContext dataContext;
    public GameSeed(DataContext context)
    {
        dataContext = context;
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
                Details = "# Gameplay\r\n\r\n**Zenless Zone Zero** is an action role-playing game. The player assumes the role of a Proxy (the protagonist known as Wise or Belle), a character who helps others explore hostile Parallel universes in fiction | alternate dimensions called \r\nHollows. Over time, the Proxy will recruit new Party (role-playing games) | party members known as Agents to fight the Ethereal and other enemies. In addition to the Proxy, players use assistants known as Bangboos. The gameplay is split into two different modes:\r\na \"TV Mode\" and \"Combat Mode.\" TV Mode gameplay resembles a Video game graphics#2D|2D dungeon crawler game, with the Proxy roaming on tiles that resemble TVs to find treasure, solve puzzles, and fight Ethereals. If the Proxy steps on\r\nan enemy tile, the party is sent to \"Combat Mode,\" where they fight in a Video game graphics#3D|3D Hack and slash|hack and slash-like game mode. By combining the abilities of different Agents and Bangboos, players can deal greater damage and Combo (video games)|combos (known as Chain Attack) onto enemies. The game features an Timekeeping in games|in-game clock system, with the days split into morning, afternoon, evening, and midnight.\r\n\r\n# Setting\r\n\r\nThe game takes place in a post-apocalyptic futuristic haven known as New Eridu. Large Pocket universe|pocket dimensions called Hollows plague the world, engulfing the land and spawning hostile monsters called Ethereals, who have wreaked havoc on humanity. After the fall of Eridu, a group of survivors established a bastion against the invaders known as New Eridu. The group survived the oncoming onslaught by extracting a substance called Ether from the Hollows, which is used as a potent energy source.\r\nSeveral factions are present in the game. The Cunning Hares, formally known as Gentle House, is an “odd jobs” agency from which the player obtains their first four agents. Belobog Heavy Industries and Victoria Housing Co specialize in construction and housekeeping, respectively. Hollow Special Operations Section 6 is a frontline unit of Hollow Special Operations, which is based in the original Eridu location. The Criminal Investigation Special Response Team is a branch of New Eridu Public Security. Lastly, the Sons of Calydon is a biker gang.\r\nThe story follows Phaethon, two siblings who work as Proxies on Sixth Street in New Eridu. The siblings possess a special technology known as the Hollow Deep Dive System, which allows them to remotely monitor and navigate Agents within Hollows through the use of a Bangboo.\r\nDespite falling into the \"futuristic\" category, ''Zenless Zone Zero'' adopts a retro and analog aesthetic, which is explained by the notion that modern technology is susceptible to corruption from overexposure to Ether. Characters operate Flip phone|flip-phones or mobile phones reminiscent of models from the 2000s and 2010s. The two main protagonists reside in a video store (known as Random Play), where New Eridu citizens can rent VHS tapes|VHS tapes. Cathode-ray tube|CRT television sets reminiscent of those from the ear\r\n",
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
                        WebsiteName = Domain.Enums.WebsiteNames.Other,
                        WebsiteLink = "https://zenless.hoyoverse.com/en-us/main"
                    }
                ]
            },
            new Game ()
            {
                Title = "Elden Ring",
                Description = "Elden Ring is a 2022 action role-playing game developed by FromSoftware. It was directed by Hidetaka Miyazaki with worldbuilding provided by American fantasy writer George R. R. Martin. It was published for PlayStation 4, PlayStation 5, Windows, Xbox One, and Xbox Series X/S on February 25 in Japan by FromSoftware and internationally by Bandai Namco Entertainment. Set in the Lands Between, players control a customizable player character on a quest to repair the Elden Ring and become the new Elden Lord.",
                Details = "# Gameplay \\n\r\n\r\nElden Ring is an action role-playing game set in third-person perspective. It includes elements that are similar to those in other FromSoftware-developed games, such as the Dark Souls series, Bloodborne, and Sekiro: Shadows Die Twice. The game is set in an open world; players can freely explore the Lands Between and its six main areas, which include Limgrave—an area of grassy plains and ancient ruins—and Caelid, a reddish wasteland home to undead monsters. The open world is explored using the character's mount Torrent as the main mode of transportation, though players may use fast travel outside combat. Throughout the game, players encounter non-player characters (NPCs) and enemies, including demigods who rule each main area and serve as the game's main bosses. Aside from the main areas, Elden Ring has hidden dungeons, catacombs, tunnels, and caves where players can fight bosses and gather helpful items.\r\nAt the game's start, the player chooses a character class, which determines their starting spells, equipment, and attributes. Combat with enemies can be within melee, or from a distance using ranged weapons. Enemy attacks can be dodged or blocked using shields. Spells allow players to enhance their weapons, fight enemies from afar, and restore lost hit points. The player can memorize a limited amount of these spells, which can be cast using a staff or sacred seal item. Weapons can be improved using ashes of war, which are obtainable enchantments that grant weapons new capabilities. Ashes of war can be applied to or removed from weapons, and each Ash adds a weapon art, a special ability that can be used during combat. Aside from direct combat, stealth mechanics can be used to avoid enemies or allow the targeting of foes with critical hits while hidden. Staggering enemies and parrying their attacks can also create opportunities for critical hits.\r\nCheckpoints called sites of grace are located throughout the game; in these places, characters can increase the power of their attributes, change memorized spells, swap ashes of war, or walk to using fast travel. Upon death, players respawn at the last site of grace they interacted with. Alternatively, they may choose to respawn at certain locations highlighted by \"stakes of Marika\" provided they died nearby. To increase their attributes at sites of grace, the player must spend runes, an in-game currency that is acquired by defeating enemies. Runes can be used to buy items, and improve weapons and armor. Dying in Elden Ring causes the player to lose all collected runes at the location of death; if the player dies again before retrieving the runes, they will be lost forever.\r\nElden Ring contains crafting mechanics; the creation of items requires materials. Recipes, which are required for the crafting of items, can be found inside collectibles called cookbooks, which are scattered throughout the world. Materials can be collected by defeating enemies, exploring the game's world, or by trading with merchant NPCs. Crafted items include poison darts, exploding pots, and consumables that temporarily increase the player's combat strength. Similar to the Dark Souls games, the player can summon friendly NPCs called spirits to fight enemies. Summoning each type of spirit requires its equivalent Spirit Ash; different types of Spirit Ashes can be discovered as the player explores the game world. Spirits can only be summoned near structures called Rebirth Monuments, which are primarily found in large areas and inside boss fight arenas.\r\nElden Ring has a multiplayer system that allows players to be summoned for both cooperative and player-versus-player (PvP) play over the Internet. Cooperative play involves the placing of a summon sign on the ground, which causes the sign to become visible to online players who have used a corresponding item. If another player interacts with the sign, the player who placed the sign is summoned into their world. Cooperative players remain in the same world until the boss of the area is defeated or until a summoned player dies and is returned to their home world. In PvP combat, a summon sign is used to challenge another player to a duel, or the player can use additional items to invade the worlds of others. World hosts may use a \"taunter's tongue\" to increase the likelihood their world will be invaded by others and to decrease the time between invasions.\r\n\r\n# Synopsis \\n\r\n\r\n## Setting and characters \\n\r\n\r\nElden Ring takes place in the Lands Between, a realm blessed by entities called outer gods. Most prominent is the Greater Will, who created the Elden Ring – a collection of runes that govern physics. The Greater Will's emissary, the Two Fingers, made a woman named Marika the Elden Ring's vessel, ascending her to godhood. She then formed a dynasty called the Golden Order alongside a consort, the Elden Lord; two held this title: Godfrey, a barbarian who was later banished; and Radagon, Marika's male alter-ego. She also planted a massive, golden tree called the Erdtree that empowers the realm's inhabitants. Finally, Marika removed the Rune of Death from the Elden Ring and entrusted it to her bodyguard, Maliketh, causing the deceased not to pass on but to be reborn through the Erdtree instead. However, the Golden Order also persecuted certain people, such as the horned Omens and exiled humans called Tarnished.\r\nAfter her firstborn, Godwyn, was permanently killed using a stolen piece of the Rune of Death, Marika seemingly went mad and shattered the Elden Ring, prompting its true form, the Elden Beast, to imprison her inside the Erdtree. Afterward, Marika's offspring inherited shards of the Ring, sparking a war of succession. These demigods are: Morgott, an Omen loyal to the Golden Order despite facing discrimination; Mohg, Morgott's twin who wishes to form his own dynasty; Radahn, a warrior general who went feral after being afflicted by the Scarlet Rot disease; Rykard, a former praetor who merged with a giant serpent; Ranni, a witch seeking to overthrow the Greater Will who orchestrated Godwyn's death; Miquella, a charming mystic cursed with eternal youth; Malenia, Miquella's twin and the progenitor of Scarlet Rot; Godrick, a distant relative who grafts his subjects' body parts onto himself; and Rennala, Radagon's ex-wife and queen of the royal Carian family.\r\nThe exiled Tarnished were gifted power by an unknown source, resurrecting and granting them immortality, and were called back to the Lands Between to repair the Elden Ring and become the new Elden Lord. Those summoned include: Goldmask, a fundamentalist seeking to revitalize the Golden Order; Fia, a deathbed companion who is attempting to revive Godwyn; Dung Eater, a criminal who wants to strip everyone in the Lands Between of the Erdtree's grace; Sir Gideon Ofnir, a spymaster researching current events; and the player character, a Tarnished of no renown.\r\nShadow of the Erdtree introduces the Land of Shadow, where Marika originated. Her people were wiped out by the Hornsent, beings similar to Omens who ruled the Lands Between at the time. Spurred on by the Two Fingers, this prompted her to become a god using an artifact called the Gate of Divinity. Afterward, she magically concealed the Land of Shadow from the rest of the Lands Between before having another of her offspring, Messmer, massacre the Hornsent to avenge her people; later ostracizing him there, both to keep her origins hidden and to prevent him from becoming a future threat.\r\n\r\n# Plot \\n\r\n\r\nDuring their quest, the Tarnished meets a mysterious maiden named Melina, who offers her assistance in return for being brought to the Erdtree. After proving themselves by driving away an Omen named Margit, Melina brings them to the Roundtable Hold – a gathering place for Tarnished – where they meet the Two Fingers, who reveals that at least two of Elden Ring's shards must be collected and brought to the Erdtree to repair it.\r\nEventually, the Tarnished makes their way to the foot of the Erdtree, where they once again duel Margit – revealed to really be Morgott – and kills him this time. However, even when having the necessary shards, they find the path inside blocked by thorns. In response, Melina advises the Tarnished to seek out the Fell God's power, the Flame of Ruin, and ignite the Erdtree. Alternatively, they can also form a pact with another outer god, the corruptive Frenzied Flame. Should the Tarnished use the Flame of Ruin, Melina immolates herself as the kindling to set the Erdtree alight. No sacrifice is needed if the Frenzied Flame is used, but Melina abandons the Tarnished.\r\nAs the Erdtree burns, the Tarnished tracks down and kills Maliketh to obtain the Rune of Death, infusing it into the flames to burn away the thorns. On their way back to the Erdtree, however, they are confronted by Gideon, who has gone mad after learning the truth about the Greater Will, and Godfrey, who seeks to reclaim the Elden Lord title. After vanquishing them both, the Tarnished enters the Erdtree, where they fight and slay both Radagon and the Elden Beast. Afterward, the Tarnished gains access to Marika's corpse, containing the remnants of the Elden Ring, from which six endings are available depending on their choice of allies:\r\n\r\n    > The first four endings all have the Tarnished become Elden Lord and usher in a new age.\r\n    > The default ending; if the Tarnished allied with no one, the Lands Between are left as is, resulting in the Age of Fracture.\r\n    > If the Tarnished allied with Goldmask, they usher in the Age of Order by enforcing laws into the Elden Ring.\r\n    > If the Tarnished allied with Fia, they usher in the Age of the Duskborn by altering how death works in the Lands Between.\r\n    > If the Tarnished allied with Dung Eater, they unleash a Blessing of Despair, turning everyone in the Lands Between into Omens.\r\n    > If the Tarnished conspired with Ranni, she destroys the Elden Ring and ushers in the Age of Stars.\r\n    > If the Tarnished made a pact with the Frenzied Flame, they set the Lands Between ablaze and become a Lord of Frenzied Flame. Afterward, Melina vows vengeance against the Tarnished.\r\n\r\n## Shadow of the Erdtree \\n\r\n\r\nThe Tarnished follows the trail of Miquella, whom Mohg seemingly abducted. After slaying Mohg and Radahn, they discover that Miquella abandoned his body to pass into the Land of Shadow. Entering the realm through Miquella's corpse, the Tarnished continues tracking him by locating aspects of him throughout the land, which he abandoned to sever his ties to the Erdtree. Optionally, the Tarnished can also interact with Miquella's followers: Leda, one of Miquella's knights; Dane, a monk disillusioned with the Golden Order; Freyja, a soldier of Radahn's army; Hornsent, a Land of Shadow native; Thiollier, a worshipper of Miquella's female alter-ego, St. Trina; Sir Ansbach, a former follower of Mohg; and Moore, a creature born from Scarlet Rot.\r\nMiquella's trail leads the Tarnished to the Gate of Divinity, but they find it locked behind magical thorns, forcing them to confront and kill Messmer to burn them away using his power. However, upon nearing the Gate, they are confronted by Miquella's followers, who had turned on each other after his affection-compelling charm came undone, with the survivors dependent on the Tarnished's choices. Confronting Miquella, it is revealed that he had orchestrated his own abduction and used Mohg's corpse to revive Radahn as his consort in a divine ascension ritual. The Tarnished proceeds to kill both Miquella and Radahn, thwarting the mystic's ascendency to godhood and preventing his 'Age of Compassion' from coming to pass.",
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
                        WebsiteName = Domain.Enums.WebsiteNames.Steam,
                        WebsiteLink = "https://store.steampowered.com/app/1245620/ELDEN_RING/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.Xbox,
                        WebsiteLink = "https://www.xbox.com/en-US/games/elden-ring/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.Microsoft,
                        WebsiteLink = "https://www.microsoft.com/en-us/p/elden-ring/9nl9dv1sh9ls"
                    }
                ]
            },
            new Game ()
            {
                Title = "The Witcher 3: Wild Hunt",
                Description = "The Witcher 3: Wild Hunt is a 2015 action role-playing game developed and published by CD Projekt. It is the sequel to the 2011 game The Witcher 2: Assassins of Kings and the third game in The Witcher video game series, played in an open world with a third-person perspective. The games follow the Witcher series of fantasy novels written by Andrzej Sapkowski.",
                Details = "# Gameplay \\n\r\n\r\nThe Witcher 3: Wild Hunt is an action role-playing game with a third-person perspective. Players control Geralt of Rivia, a monster slayer known as a Witcher. Geralt walks, runs, rolls and dodges, and (for the first time in the series) jumps, climbs and swims. He has a variety of weapons, including bombs, a crossbow and two swords (one steel and one silver). The steel sword is used primarily to kill humans while the silver sword is more effective against creatures and monsters. Players can draw out, switch and sheathe their swords at will. There are two modes of melee attack; light attacks are fast but weak, and heavy attacks are slow but strong. Players can block and counter enemy attacks with their swords. Swords have limited endurance and require regular repair. In addition to physical attacks, Geralt has five magical signs at his disposal: Aard, Axii, Igni, Yrden and Quen. Aard prompts Geralt to unleash a telekinetic blast, Axii confuses enemies, Igni burns them, Yrden slows them down and Quen offers players a temporary, protective shield. The signs use stamina, and cannot be used indefinitely. Players can use mutagens to increase Geralt's magic power. Geralt loses health when attacked by enemies, although wearing armour can help reduce health loss. Health is restored with meditation or consumables, such as food and potions. Players occasionally control Ciri, Geralt's adoptive daughter who can teleport short distances. The game has responsive, advanced artificial intelligence (AI) and dynamic environments. The day-night cycle influences some monsters; a werewolf becomes powerful during the night of a full moon. Players can learn about their enemies and prepare for combat by reading the in-game bestiary. When they kill an enemy, they can loot its corpse for valuables. Geralt's witcher sense enables players to find objects of interest, including items that can be collected or scavenged. Items are stored in the inventory, which can be expanded by purchasing upgrades. Players can sell items to vendors or use them to craft potions and bombs. They can visit blacksmiths to craft new weapons and armorers to craft new armour with what they have gathered. The price of an item and the cost of crafting it depend on a region's local economy. Players earn experience points by completing quests. When a player earns enough experience, Geralt's level increases and the player receives ability points. These points may be used on four skill trees: combat, signs, alchemy and general. Combat upgrades enhance Geralt's attacks and unlock new fighting techniques; signs upgrades enable him to use magic more efficiently, and alchemy upgrades improve crafting abilities. General upgrades have a variety of functions, from raising Geralt's vitality to increasing crossbow damage. The game focuses on narrative and has a dialogue tree which allows players to choose how to respond to non-player characters. Geralt must make decisions which change the state of the world and lead to 36 possible endings, affecting the lives of in-game characters. He can have a romantic relationship with some of the game's female characters by completing certain quests. In addition to the main quests, books offer more information on the game's world. Players can begin side quests after visiting a town's noticeboard. These side missions include Witcher Contracts (elaborate missions requiring players to hunt monsters) and Treasure Hunt quests, which reward players with top-tier weapons or armour. The game's open world is divided into several regions. Geralt can explore each region on foot or by transportation, such as a boat. Roach, his horse, may be summoned at will. Players can kill enemies with their sword while riding Roach, but an enemy presence may frighten the horse and unseat Geralt. Points of interest may be found on the map, and players receive experience points after completing mini-missions in these regions. Players can discover Places of Power for additional ability points. Other activities include horse racing, boxing and card playing; the card-playing mechanic was later expanded into a standalone game, Gwent: The Witcher Card Game.\r\n\r\n# Synopsis \\n\r\n\r\n## Setting \\n\r\n\r\nThe game is set in the Continent, a fictional fantasy world based on Slavic paganism. It is surrounded by parallel dimensions and extra-dimensional worlds. Humans, elves, dwarves, monsters and other creatures co-exist on the Continent, but non-humans are often persecuted for their differences in the northern realms. The Continent is caught up in a war between the empire of Nilfgaard—led by Emperor Emhyr var Emreis (Charles Dance), who invaded the Northern Kingdoms—and Redania, ruled by King Radovid V.Several locations appear, including the free city of Novigrad,the Redanian city of Oxenfurt, the no man's land of Velen, the city of Vizima (former capital of the recently conquered Temeria), the Skellige islands (home to several Norse-Gaels Viking clans) and the witcher stronghold of Kaer Morhen.\r\nCharacters. The main character is the Witcher, Geralt of Rivia (Doug Cockle), a monster hunter trained since childhood in combat, tracking, alchemy and magic, and made stronger, faster and resistant to toxins by mutagens. He is aided by his lover, the powerful sorceress Yennefer of Vengerberg (Denise Gough), his former love interest Triss Merigold (Jaimi Barbakoff), the bard Dandelion (John Schwab), the dwarf warrior Zoltan Chivay (Alexander Morton), and Geralt's Witcher mentor Vesemir (William Roberts).Geralt is spurred into action by the reappearance of his and Yennefer's adopted daughter, Ciri (Jo Wyatt). Ciri is a Source, born with innate (and potentially vast) magical abilities; after the apparent death of her parents, she was trained as a witcher while Yennefer taught her magic. Ciri disappeared years before to escape the Wild Hunt, a group of spectral warriors led by the King of the Wild Hunt: the elf Eredin (Steven Hartley), from a parallel dimension.\r\n\r\n## Plot \\n\r\n\r\nGeralt and his mentor Vesemir arrive at the town of White Orchard after receiving a letter from Geralt's long-lost lover Yennefer. After defeating a griffin for the local Nilfgaardian garrison, Geralt accompanies Yennefer to the city of Vizima, where they meet with Emperor Emhyr. Emhyr orders Geralt to find Ciri, who is Emhyr's biological (and Geralt's adopted) daughter. Ciri is a Child of the Elder Blood, the last heir to an ancient Elven bloodline that grants her the power to manipulate time and space, and is being relentlessly stalked by the enigmatic Wild Hunt. Geralt learns of three places Ciri was recently seen: the war-ravaged swamp province of Velen, the free city-state of Novigrad, and the Skellige Isles.\r\nIn Velen, Geralt tracks Ciri to the fortress of The Bloody Baron, a warlord who recently took over the province. The Baron demands that Geralt find his missing wife and daughter in exchange for information about Ciri. Geralt learns that the Baron drove his own family away with his drunken rages; while his daughter fled to Oxenfurt, his wife Anna became a servant of the Crones, three malicious witches that watch over Velen. He also discovers that Ciri was briefly captured by the Crones, but escaped to the Baron's stronghold before continuing on to Novigrad.\r\nAt Novigrad, Geralt reunites with his former lover Triss Merigold, who has gone underground to escape persecution by the Church of the Eternal Fire. He learns that Ciri and his old friend Dandelion ran afoul of Novigrad's powerful crime bosses while seeking to break a curse related to a mysterious phylactery. With the help of Triss and several old acquaintances, Geralt rescues Dandelion, who tells him that Ciri teleported away to escape pursuit by guards.\r\nGeralt sails to Skellige and reunites with Yennefer, who is investigating a magical explosion near where Ciri was last seen. They discover that Ciri visited the island of Lofoten, but when the Wild Hunt attacked again, fled in a boat with an unidentified elf. When the boat returned to shore, its only occupant was Uma, a deformed creature Geralt previously saw living with the Bloody Baron. Deducing that Uma was the victim of the curse Ciri tried to lift in Novigrad, Geralt collects Uma in Velen and takes him to the nearly abandoned witcher school at Kaer Morhen. Working with Yennefer and his fellow witchers, Geralt breaks the curse and restores Uma's true identity: Avallac'h, Ciri's teacher and the elf seen with her on her travels. Avallac'h tells Geralt that he placed Ciri in an enchanted sleep on the Isle of Mists to keep her temporarily safe from the Wild Hunt.\r\nGeralt finds Ciri on the Isle of Mists and learns from her that Eredin, the leader of the Wild Hunt, wants her Elder Blood powers to save his homeworld from a catastrophe known as the White Frost. They return to Kaer Morhen and fortify it against the inevitable arrival of the Hunt. In the battle that ensues, Vesemir is killed, causing Ciri to unleash her uncontrolled power and temporarily send the Hunt into retreat.\r\nRealizing that the Hunt will never stop, Ciri and Geralt decide to fight Eredin at a time and place of their choosing. While Triss and Yennefer reform the Lodge of Sorceresses to aid in the fight, Geralt recovers the Sunstone, an artifact that can communicate between worlds. Using the Sunstone, Avallac'h lures Eredin to Skellige, where Geralt defeats him in combat. As he dies, Eredin tells Geralt that Avallac'h has betrayed him, and plans to use Ciri's power for his own ends.\r\nAs the White Frost begins to encroach on the Continent, Geralt and Yennefer pursue Avallac'h, but find Ciri alive and well. She tells Geralt that Avallac'h is not a traitor, and has only ever intended to fight the White Frost. Thinking back on her relationship with Geralt, Ciri finds the strength to stop the cataclysm; if Geralt patronized and protected her throughout the game, she dies in the attempt, but if he guided her to mature and make her own choices, she survives.\r\nThe player's choices can lead to several different endings. If Ciri survives after defeating the White Frost and Geralt took her to meet her father, she will become the Empress of Nilfgaard. If Ciri survives but did not meet the emperor, Geralt helps her fake her death, and she becomes a witcher. If Ciri is killed in her confrontation with the White Frost, Geralt retrieves her medallion from the last remaining Crone and mourns quietly as his hut is swarmed by monsters. The player's choices also determine whether Geralt ends up in a romantic relationship with Yennefer, Triss, or neither, and how much of the North is ultimately conquered by Nilfgaard. ",
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
                        WebsiteName = Domain.Enums.WebsiteNames.Steam,
                        WebsiteLink = "https://store.steampowered.com/app/292030/The_Witcher_3_Wild_Hunt/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.Xbox,
                        WebsiteLink = "https://www.xbox.com/en-us/games/store/The-Witcher-3-Wild-Hunt/BR765873CQJD"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.EpicGames,
                        WebsiteLink = "https://store.epicgames.com/en-US/p/the-witcher-3-wild-hunt"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.GOG,
                        WebsiteLink = "https://www.gog.com/en/game/the_witcher_3_wild_hunt"
                    }
                ]
            },
            new Game ()
            {
                Title = "Outer Wilds",
                Description = "Outer Wilds is a 2019 action-adventure video game developed by Mobius Digital and published by Annapurna Interactive. The game follows the player character as they explore a planetary system stuck in a 22-minute time loop that resets after the sun goes supernova and destroys the system. Through repeated attempts they investigate the alien ruins of the Nomai to discover their history and the cause of the time loop.",
                Details = "# Gameplay \\n\r\n\r\nOuter Wilds is an action-adventure video game set in a small planetary system in which the player character, an unnamed space explorer referred to as the Hatchling, explores and investigates its mysteries in a self-directed manner. Whenever the Hatchling dies, the game resets to the beginning; this happens regardless after 22 minutes of gameplay due to the sun going supernova. The player uses these repeated time loops to discover the secrets of the Nomai, an alien species that has left ruins scattered throughout the planetary system, including why the sun is exploding. A downloadable content expansion, Echoes of the Eye, adds additional locations and mysteries to the game.\r\nThe Hatchling moves around the game space by walking and jumping; if they are wearing their spacesuit they may also use its jetpack to propel themselves upwards. The spacesuit has a limited supply of fuel, which can be refilled at specific locations, and a limited supply of oxygen, which is refilled when the Hatchling is near trees.[4] If the player runs out of fuel, they can use oxygen as a propellant. Running out of oxygen, hitting an object or surface too hard, or being crushed will injure the Hatchling or damage their suit, killing them if too much injury is sustained. While wearing the spacesuit, an interface showing the remaining fuel and oxygen is shown. Damage to the Hatchling is shown by their suit icon turning red, with no explicit health amount given. The Hatchling also has a signalscope, which can be used to scan for the source of audio transmissions.\r\nThe Hatchling can also freely fly a small spacecraft throughout the planetary system. The spacecraft and the celestial bodies of the system follow an exaggerated system of Newtonian physics, causing the planets and other bodies to swiftly orbit the sun and exert their own variable gravity fields, and requiring the player to counteract their own momentum to slow down while flying.[1] Collisions can damage parts of the spacecraft and make them inoperable; too much damage can destroy it and kill the player, but can otherwise be repaired by exiting the spacecraft and interacting with the damaged component. Both the spacecraft and the spacesuit can launch a small probe to light up an area or take pictures.\r\nThe player character can only carry a single object at a time. Nothing is brought back with the Hatchling when the time loop resets, with the exception of the data on the spacecraft's computer, which displays the information and mysteries that the player has found so far, organized either by location or in a web of connections.[10] Locations evolve throughout the duration of the time loop, such as parts of a planet collapsing or sand flowing from one area to another, making some areas only accessible from specific points in the time loop. Other people throughout the planetary system can be communicated with in text-based dialogue trees, while Nomai writing, found in their ruins and presented as a branching tree of messages, can be read with a translator tool.\r\n\r\n# Plot \\n\r\n\r\n## Setting \\n\r\n\r\nOuter Wilds is set in a planetary system consisting of a sun orbited by a number of celestial bodies: the Hourglass Twins, a pair of planets orbiting each other with sand flowing from one to the other; Timber Hearth, a forested Earth-like planet that is the homeworld of the four-eyed Hearthian species; the Attlerock, a small rocky moon orbiting Timber Hearth; Brittle Hollow, a hollow planet that is collapsing into a black hole at its center and is orbited by Hollow's Lantern, a volcanic moon; Giant's Deep, a cloud-covered water planet containing several floating islands; and Dark Bramble, a shattered planet largely composed of a space-warping vine plant, inhabited by giant aggressive anglerfish. Each planet has a distinct visual identity, such as Timber Hearth resembling a campsite in the woods with browns and greens, while Giant's Deep has blue-green rocky beaches. Each planet has a distinct auditory identity as well, with a member of the Outer Wilds space exploration program playing the same song on a unique instrument, which can be listened to from anywhere in the solar system with the signalscope. Additionally, there is the Quantum Moon, which moves to orbit different planets when not observed; the Interloper, an icy comet; and space stations orbiting the sun and Giant's Deep left by the Nomai, a race that went extinct in the planetary system long before the game begins. Hearthians are a four-eyed species that resemble aquatic animals with legs, while the Nomai are a three-eyed, fur-covered species that wear robes and large masks.\r\n\r\n## Story \\n\r\n\r\nThe player takes the role of an unnamed Hearthian space explorer, referred to as the Hatchling by other Hearthians, preparing for their first space flight as part of Outer Wilds. They are to be the first to explore with a device that can translate written Nomai text; prior to departure, a Nomai statue in a museum turns towards them. The player discovers that whenever the Hatchling dies, a vision of that statue appears and they are sent back in time to the start of a game in a time loop. Additionally, the loop resets after 22 minutes regardless, as the sun abruptly goes supernova, destroying the system and killing the Hatchling.\r\nThe player, through repeated timeloops, explores the planetary system and the ruins that the Nomai left behind. They discover from their writings that the Nomai were a nomadic species that explored the universe in large independent vessels; one vessel discovered a signal older than the universe emanating from something orbiting the Hearthian sun. Upon warping to the system, the vessel became embedded in Dark Bramble, with some of the Nomai surviving in escape pods. No longer able to detect the signal, the Nomai built a civilization throughout the system in order to find the source, dubbed the \"Eye of the Universe\". They eventually discover that, with enough power, they can send objects or information backwards in time using a linked pair of black and white holes. This inspires them to build a probe cannon in orbit around Giant's Deep to fire the probe in a random direction to locate the Eye, and a station in orbit around the sun that would artificially induce a supernova, generating enough power to send the probe's data back in time 22 minutes. Together, this would allow the probe to be fired in as many times and directions as necessary to find the Eye, at which point the Nomai would shut down the sun station, ending the time loop. The sun station did not work, however, and before an alternate power source could be found, the Interloper entered the system. Upon reaching the sun and melting, a powerful wave of \"ghost matter\" spread throughout the system, killing all of the Nomai instantly.\r\nA long time later, after the animals of Timber Hearth evolved into the Hearthians, the sun goes supernova as part of the natural end of the universe, triggering the time loop repeatedly until the probe cannon finds the Eye at the beginning of the game and through the statue inducts the Hatchling into the loop. Armed with this knowledge, the player is able to replace the power source of the derelict Nomai vessel and input the coordinates of the Eye. Upon entering the Eye, the player encounters echoes of the other members of Outer Wilds and, optionally, a Nomai if they had met on the Quantum Moon, and as the universe ends the Eye creates a new universe in a Big Bang. The ending shows a similar planetary system with new life forms 14.3 billion years after its creation, with influences of the Hearthians and Nomai.\r\n\r\n## Echoes of the Eye \\n\r\n\r\nThe Echoes of the Eye expansion adds an exhibit to the museum at the beginning of the game, which shows off the deep space satellite used to generate the player's system map. The player discovers an object that eclipses the sun—a planet-sized rotating ship, hidden within a cloaking field. Within this ship, called the Stranger, the player finds an abandoned village by a circular river, containing heavily damaged slide reels that can be projected to tell the story of the Stranger's inhabitants.\r\nSimilar to the Nomai, the unnamed species that built the Stranger also came to the Hearthian system after discovering the Eye of the Universe's signals, forming a religion around it and ravaging their homeworld moon to build the Stranger. Upon reaching the system and discovering that the Eye heralded the end of the universe, they destroyed their monuments to the Eye and constructed a device to block its signal from other races. The inhabitants built a virtual reality of their homeworld, which could be entered using lantern-like devices and in which they could remain after death. The player discovers their corpses, still holding the lanterns, as well as one inhabitant, the Prisoner, who is locked away from the others both in reality and in the virtual world.\r\nThe player learns how to enter the simulation via the lanterns and discovers the active consciousnesses of the inhabitants, who are hostile to the player. The Prisoner's vault, however, cannot be entered. After discovering slide reels showing the limitations of the virtual reality system, the player uses glitches in the system to unlock the vault's three seals and open it. Communicating with the player via a telepathic projection staff, the Prisoner transmits a memory of their crime, which was to disable the signal blocker surrounding the Eye temporarily before they were imprisoned. The player uses the staff to explain to the Prisoner how their actions led the Nomai to discover the signal of the Eye and enter the system, setting the events of the game in motion. The Prisoner exits the vault and vanishes, leaving footprints leading into a nearby lake and their staff on the shore, which shows the player a vision of the Prisoner and player riding into the sunrise together on a raft. If the player travels to the Eye of the Universe after having met with the Prisoner, they will find an echo of the Prisoner alongside the other characters, and the ending scene has an influence from the Stranger's inhabitants.",
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
                        WebsiteName = Domain.Enums.WebsiteNames.Steam,
                        WebsiteLink = "https://store.steampowered.com/app/753640/Outer_Wilds/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.Xbox,
                        WebsiteLink = "https://www.xbox.com/en-us/games/store/Outer-Wilds/C596FKDKMQN7/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.EpicGames,
                        WebsiteLink = "https://store.epicgames.com/en-US/p/outerwilds"
                    }
                ]
            },
            new Game ()
            {
                Title = "Hollow Knight",
                Description = "Hollow Knight is a 2017 Metroidvania video game developed and published by independent developer Team Cherry. The player controls the Knight, an insectoid warrior exploring Hallownest, a fallen kingdom plagued by a supernatural disease. The game is set in diverse subterranean locations, featuring friendly and hostile insectoid characters and numerous bosses. Players have the opportunity to unlock abilities as they explore, along with pieces of lore and flavour text that are spread throughout the kingdom.",
                Details = "# Gameplay \\n\r\n\r\nHollow Knight is a 2D side-scrolling Metroidvania. The player controls a silent insectoid protagonist called \"the Knight\" who explores an underground fallen kingdom called Hallownest. The Knight can strike enemies with a sword-like weapon called a Nail and can learn spells that allow for long-range attacks. Defeated enemies drop the currency called Geo. The Knight starts with a limited number of hit points, which are represented by masks. Mask Shards can be collected throughout the game to increase the player's maximum number of masks. By striking enemies, the Knight gains Soul, which is stored in the Soul Vessel. If all masks are lost, the Knight dies and a Shade enemy appears where they died. The player loses all Geo and can hold a reduced amount of Soul. Players need to defeat the Shade enemy to recover lost Geo and carry the normal amount of Soul. The game continues from the last visited bench the character sat on, which are scattered throughout the game world and act as save points and places where the player can change their charms. Initially the player can only use Soul to \"Focus\" and regenerate masks, but as the game progresses, players unlock and collect several offensive spells which consume Soul. Additional Soul Vessels, used to hold more Soul, can be acquired throughout the game. Many areas feature more challenging enemies and bosses which the player may need to defeat in order to progress further. Defeating some bosses grants the player access to new abilities. Later in the game, players acquire the Dream Nail, a special item that can access the minds of Hallownest's creatures. Hitting most enemies with the Dream Nail deals no damage but gives the Knight extra Soul compared to hitting them with the regular Nail. It also enables the player to face more challenging versions of a few bosses and to break the seal to the final boss. If the player defeats the final boss, they are given access to a mode called \"Steel Soul\". In this mode, dying is permanent, i.e. if the Knight loses all of their masks, the save slot will be reset. During the game, the player encounters bug-themed non-player characters (NPCs) with whom they can interact. These characters provide information about the plot and lore, offer aid, and sell items or services. The player can upgrade the Knight's Nail to deal more damage or find Soul Vessels to carry more Soul. Players acquire items that provide movement abilities including an additional mid-air jump (Monarch Wings), adhering to and jumping off walls (Mantis Claw), a quick dash (Mothwing Cloak), and a speedy super dash (Crystal Heart). The player can learn other combat abilities, known as Nail Arts, and spells. To further customise the Knight, players can equip various charms, which can be found or purchased from NPCs. Some of their effects include improved combat abilities or skills, granting more masks with or without the ability to regenerate them with Soul, greater mobility, easier collecting of Geo or Soul, the ability to gain more Geo per enemy, and other transformations to the Knight. Equipping a charm takes up a certain number of limited slots, called notches. Hallownest consists of several large, inter-connected areas with unique themes. With its nonlinear gameplay Metroidvania design, Hollow Knight does not bind the player to one path nor require them to explore the whole world; there are places that can be missed when finishing the game, though there are obstacles that limit the player's access to various areas. The player may need to acquire a specific movement ability, skill, or item to progress further. To fast travel through the world, the player can use Stag Stations, terminals connected to a network of tunnels that are traversed via giant stag beetles; players can only travel to previously visited and unlocked stations. Other fast travel methods, such as trams, lifts, and the \"Dreamgate\", are encountered later.\r\nAs the player enters a new area, they do not have access to the map of their surroundings. They must find Cornifer, the cartographer, to buy a rough map. As the player explores an area, the map becomes more accurate and complete, although it is updated only when sitting on a bench. The player will need to buy specific items to complete maps, to see points of interest, and to place markers. The Knight's position on the map can only be seen if the player has the Wayward Compass charm equipped.\r\n\r\n# Plot \\n\r\n\r\nAt the outset, the Knight arrives in Dirtmouth, a quiet town that sits just above the remains of the kingdom of Hallownest. As the Knight ventures through the ruins, they learn that Hallownest was once a flourishing kingdom which fell after becoming overrun with \"The Infection\", a supernatural disease that can infect anyone who has free will. The Infection gives its subjects heightened strength but at the cost of their civility, causing madness and undeath. Hallownest's ruler, The Pale King, had previously attempted to lock away the Infection in the Temple of the Black Egg. Despite the temple's magical seals, the disease managed to escape and Hallownest fell into ruin. The Knight's mission is to find and kill three bugs called the Dreamers, who act as the living seals on the temple door. Once the seals have been removed, the Knight may confront the source of the Infection. This quest brings the Knight into conflict with Hornet, a warrior who tests their combat prowess in several battles.\r\nThrough dialogue with non-player characters, environmental imagery, and writings scattered throughout Hallownest, the Knight learns the origins of the Infection. In ancient times, a tribe of moths that lived in Hallownest worshipped the Radiance, a primordial being who could control the minds of other bugs. When the Pale King arrived at Hallownest from afar, he used his powers to give sapience and knowledge to the creatures of the realm. The moths soon joined the other bugs of Hallownest in worshipping the Pale King, draining the power of the Radiance as she was slowly forgotten. Beneath the notice of the Pale King, some worship of the Radiance continued, allowing her to remain alive inside the Dream Realm.[8]\r\nHallownest prospered until the Radiance began appearing in the dreams of its people, poisoning their minds with the Infection. In an attempt to contain the menace, the Pale King used an ancient power called Void to create the Vessels; creatures that could trap the Infection within their own bodies. Due to the Vessels being made from Void, they are considered to not have free will, thus allowing them to not be infected since they cannot dream. The Pale King chose a Vessel known as the Hollow Knight to trap the Radiance, leaving the rest locked in a pit called the Abyss. After the Hollow Knight was locked within the Temple of the Black Egg, the Radiance persisted within the Vessel, weakening the temple's seals and allowing the Infection to escape.\r\nThroughout the game, it is implied that the Knight was a Vessel who managed to escape the Abyss. They gradually defeat the Dreamers and their guardians, removing the seals on the door. Inside, they encounter and battle with the infected Hollow Knight. Depending on the player's actions, multiple endings can then be achieved. These endings include the Knight defeating the infected Hollow Knight and taking its place containing the Radiance, defeating the Hollow Knight with Hornet's assistance, or using the Void Heart item to directly fight and defeat the Radiance inside the Dream Realm.\r\n\r\n## The Grimm Troupe expansion \\n\r\n\r\nIn the second expansion to Hollow Knight, a \"Nightmare Lantern\" was added to the Howling Cliffs. After using the Dream Nail on a masked bug, the lantern summons a mysterious group of circus performers to Dirtmouth, who identify themselves as the Grimm Troupe. Their leader, Troupe Master Grimm, gives the Knight a quest to collect magic flames throughout Hallownest in order to take part in a \"twisted ritual\". He gives the player the Grimmchild charm, which absorbs the flames into itself, progressing the ritual and allowing the Grimmchild to attack the Knight's enemies. Eventually, the Knight must choose to either complete the ritual by fighting Grimm and his powerful Nightmare King form, or prevent the ritual and banish the Grimm Troupe with the help of Brumm, a traitorous troupe member.[9]\r\n\r\n## Godmaster expansion \\n\r\n\r\nMore content was added to Hollow Knight with the fourth and final expansion, Godmaster, in which the Knight can battle harder versions of all of the bosses through a series of challenges. The main hub of the expansion is known as Godhome. Within Godhome are five \"pantheons\", each being a \"boss rush\", containing a set of bosses that must all be defeated consecutively without dying. The final pantheon, the Pantheon of Hallownest, contains every boss in the game or alternate forms of original bosses. If the Knight completes the Pantheon of Hallownest, the Absolute Radiance, a more powerful version of the Radiance, appears, acting as the new final boss. Upon defeating it, two unique endings can be achieved, each involving the destruction of Godhome by a powerful Void entity. A new game mode called \"Godseeker\" was added too. ",
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
                        WebsiteName = Domain.Enums.WebsiteNames.Steam,
                        WebsiteLink = "https://store.steampowered.com/app/367520/Hollow_Knight/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.Xbox,
                        WebsiteLink = "https://www.xbox.com/en-us/games/store/Hollow-Knight-Voidheart-Edition/9MW9469V91LM/"
                    }
                ]
            },
            new Game ()
            {
                Title = "Disco Elysium",
                Description = "Disco Elysium is a 2019 role-playing video game developed and published by ZA/UM. Inspired by Infinity Engine-era games, particularly Planescape: Torment, the game was written and designed by a team led by Estonian novelist Robert Kurvitz and features an art style based on oil painting with music by the English band British Sea Power.",
                Details = "# Gameplay \\n\r\n\r\nDisco Elysium is a role-playing video game that features an open world and dialogue-heavy gameplay mechanics. The game is presented in an isometric perspective in which the player character is controlled. The player takes the role of a detective, who suffers from alcohol and drug-induced amnesia, on a murder case. The player can move the detective about the current screen to interact with non-player characters (NPC) and highlighted objects or move onto other screens. Early in the game they gain a partner, Kim Kitsuragi, another detective who acts as the protagonist's voice of professionalism and who offers advice or support in certain dialogue options.\r\nThe gameplay features no combat in the traditional sense; instead, it is handled through skill checks and dialogue trees. There are four primary abilities in the game: Intellect, Psyche, Physique, and Motorics. Each ability has six distinct secondary skills for a total of 24. The player improves these skills through skill points earned from levelling up. The choice of clothing that the player equips on the player-character can impart both positive and negative effects on certain skills. Upgrading these skills helps the player character pass skill checks, based on a random dice roll, but potentially result in negative effects and character quirks, discouraging minmaxing. For instance, a player character with high Drama may be able to detect and fabricate lies effectively, but may also become prone to hysterics and paranoia. Likewise, high Electrochemistry shields the player character from the negative effects of drugs and provides knowledge on them, but may also lead to substance abuse and other self-destructive, self-gratifying behaviours.\r\nDisco Elysium features a secondary inventory system known as the \"Thought Cabinet\". Thoughts are unlockable through conversations with other characters, as well as through internal dialogues within the mind of the player character himself. The player is then able to \"internalise\" a thought through a certain amount of in-game hours, which, once completed, grants the player character permanent benefits but also occasionally negative effects, a concept that ZA/UM compared to the trait system used in the Fallout series. A limited number of slots are available in the Thought Cabinet at the start, though more can be gained with experience levels. For example, an early possible option for the Thought Cabinet is the \"Hobocop\" thought, in which the character ponders the option of living on the streets to save money, which reduces the character's composure with other NPCs while the thought is internalised. When the character has completed the Hobocop thought, it allows them to find more junk on the streets that can be sold for money. The 24 skills also play into the dialogue trees, creating a situation where the player-character may have an internal debate with one aspect of their mind or body, creating the idea that the player is communicating with a fragmented persona. These internal conversations may provide suggestions or additional insight that can guide the player into actions or dialogue with the game's non-playable characters, depending on the skill points invested into the skill. For example, the Inland Empire, a subskill of the Psyche, is described by ZA/UM as a representation of the intensity of the soul, and may come into situations where the player-character may need to pass themselves off under a fake identity with the conviction behind that stance, should the player accept this suggestion when debating with Inland Empire.\r\n\r\n# Synopsis \\n\r\n\r\n## Setting \\n\r\n\r\nDisco Elysium takes place in the fantastic realist world of Elysium, developed by Kurvitz and his team in the years prior, which includes over six thousand years of history. The fiction has been constructed with attention to the theory of historical materialism, which posits that, even if the details were different, human history would play out in a similar way. The game takes place in the year '51 of the Current Century. Elysium is made of \"isolas\", masses of land and sea that are separated from each other by the Pale, an inscrutable, mist-like \"connective tissue\" in which the laws of reality break down. Prolonged exposure to the Pale can cause mental instability and eventually death, and traversing the Pale, which is typically done with aerostatics, is heavily regulated due to the danger. \r\nEvents in the game take place in the impoverished district of Martinaise within the city of Revachol on the isola of Insulinde, the \"New New World\". Forty-nine years before the events of the game, a wave of communist revolutions swept multiple countries; the Suzerainty of Revachol, a monarchy that up to that point had been Elysium's pre-eminent superpower, was overthrown and replaced by a commune. Six years later, the Commune of Revachol was toppled by an invading alliance of moralist-capitalist nations called \"the Coalition\". Revachol was designated a Special Administrative Region and remains firmly under Coalition control decades later. One of the few governmental responsibilities that the Coalition concedes to the people of Revachol is policing, which is carried out by the Revachol Citizens Militia (RCM), a voluntary citizens' brigade turned semi-professional police force.\r\n\r\n## Plot \\n\r\n\r\nThe player character wakes up in a trashed hostel room in Martinaise with a severe hangover and no memory of his own identity due to an ostensible extreme case of drug-induced amnesia. He meets Lieutenant Kim Kitsuragi, who informs him that they have been assigned to investigate the death of a hanged man in an empty lot behind the hostel. The victim's identity is unclear and initial analysis of the scene indicates that he was lynched by a group of people. The detectives explore the rest of the district, following up on leads while helping residents with a variety of tasks. In the course of the investigation, the player character learns that he is a decorated RCM detective, Lieutenant Harrier \"Harry\" Du Bois. Harry experienced an event several years ago that began a midlife crisis, and on the night he was assigned to the hanged man case, he finally snapped and embarked on a self-destructive three-day drinking spree around Martinaise. When the player goes to bed on the first night in-game, Harry has a nightmare where he discovers himself as the hanged man underneath a disco ball. When he talks to his own dead body, it tells him that everything is hopeless and he will inevitably fail to solve the case or put his life back together. Harry and Kim discover the hanged man killing is connected to an ongoing strike by the Martinaise dockworkers' union against the Wild Pines Group, a major logistics corporation. They interview union boss Evrart Claire and Wild Pines negotiator Joyce Messier. Joyce reveals that the hanged man was Colonel Ellis \"Lely\" Kortenaer, the commander of a squad of mercenaries sent by Wild Pines to break the strike. She warns that the rest of the mercenaries have gone rogue and will likely seek retribution for Lely's death. Harry and Kim discover that Lely was killed before the hanging, and the Hardie Boys, a group of dockworker vigilantes who act as the de facto peacekeepers of Martinaise, claim responsibility for the murder. They assert that Lely attempted to rape a hostel guest named Klaasje. When questioned, Klaasje reveals that Lely was shot in the mouth while the two were having consensual sex. Unable to figure out the origin of the bullet and fearful of the authorities due to her past as a corporate spy, Klaasje enlisted the help of a union sympathiser named Ruby, who staged Lely's hanging with the rest of the Hardie Boys. The detectives find Ruby hiding in an abandoned building, where she incapacitates them with a radio wave-based device normally used to aid in traversing the Pale. She claims that the cover-up was Klaasje's idea and has no idea who shot Lely. Harry manages to overcome the Pale device and contemplates arresting Ruby, but she believes Harry to be a corrupt cop and either escapes or kills herself, depending on the player's skills and choices. Returning to their hostel, the detectives intercede in a standoff between the rogue mercenaries and the Hardie Boys. A firefight breaks out and Harry is wounded, leaving him unconscious for several days. Depending on the player's actions, none, some, or all of the mercenaries may die, and Kim may also be hospitalized, in which case street urchin Cuno offers to take his place as Harry's partner. The detectives chase down their remaining leads and determine that the shot that killed Lely came from an old fortress on an isle just off of Martinaise's shoreline. The detectives explore the ruins and find the shooter, a former commissar of the Revachol communist army named Iosef Lilianovich Dros who survived the collapse of the Revacholian commune because he deserted his post. Iosef reveals that he shot Lely with his sniper rifle in a fit of anger and jealousy; his motivations were born out of his bitterness towards the capitalist system Lely represented, as well as sexual envy for Klaasje. The detectives arrest him for the murder. At this point, an insectoid cryptid known as the Insulindian Phasmid appears from the reeds, whose existence the player has the choice to investigate throughout the game. It is directly implied that the Phasmid indirectly set off the chain of events leading to the murder, as the psychoactive chemicals it exudes inadvertently affected the man's mind for years, stoking his fanaticism and resentment. Harry may have a psychic conversation with the Phasmid, who tells him that it is fearful of the notion of his unstable mind, but awed by his ability to continue existing. It also implies to Harry that the Pale is a consequence of human perception and self-reflection that threatens to consume the world. Before leaving, it comforts Harry, telling him to move on from the wreck of his life. Harry and his partner are confronted by his old squad upon their return to Martinaise. They reflect on Harry's actions during the game, particularly whether he solved the case and how he handled the mercenaries. Lieutenant Jean Vicquemare, Harry's usual partner, confirms that Harry's emotional breakdown was the result of his fiancée leaving him years ago. In the best possible outcome, the squad expresses hope that Harry's state will improve in the future, and invites him and either Kim or Cuno to join a special RCM unit. ",
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
                        WebsiteName = Domain.Enums.WebsiteNames.Steam,
                        WebsiteLink = "https://store.steampowered.com/app/632470/Disco_Elysium__The_Final_Cut/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.Xbox,
                        WebsiteLink = "https://www.xbox.com/en-us/games/store/Disco-Elysium-The-Final-Cut/9NTRS771L8HL"
                    },

                ]
            },
            new Game ()
            {
                Title = "Transistor",
                Description = "Transistor is an action role-playing video game developed and published by Supergiant Games. The game was released in May 2014 for Microsoft Windows and PlayStation 4, for OS X and Linux in October 2014, and iOS devices in June 2015. Transistor sold over one million copies across all platforms by December 2015.",
                Details = "# Gameplay \\n\r\n\r\nTransistor uses an isometric point of view. The player controls the main character, Red, as she travels through a series of locations, battling enemies known collectively as the Process in both real-time combat and a frozen planning mode referred to as \"Turn()\". Using Turn() drains the action bar, which refills after a short delay. Until it is full again, Red cannot use Turn(), or any other power (with a few exceptions). Red earns experience points after each battle, and may collect new powers (called Functions) from fallen victims of the Process.\r\nFunctions can be used in several ways. First, they can be equipped in one of four active skill slots. Second, they can be used as an upgrade to augment another equipped Function. Third, they can be equipped in a passive skill slot for a persistent effect during battle. For example, the Function Spark() may be used to fire an explosive attack, to split the effects of another Function, or to passively spawn Red's decoys. With a total of 16 Functions, there are many combinations, allowing for a great degree of customization to fit different play styles.\r\nRed can also collect and activate Limiters, which serve as optional debuffs during combat, but in turn increase experience gained. Both Functions and Limiters reveal lore from Transistor's world after being used for a certain amount of time.\r\n\r\n# Plot \\n\r\n\r\nRed, a popular singer in the city of Cloudbank, kneels by the body of a man who has been killed with a glowing greatsword—the Transistor. The attack took away her voice, sealing it inside the Transistor. The sword also absorbed the man's consciousness, and through it he is able to speak to Red, acting as her guide and the game's narrator. As Red escapes, she is attacked by an army of intelligent robots known as the Process. She also discovers several Cloudbank citizens who have been \"integrated\" by the Process and absorbs their trace data into the Transistor, expanding the weapon's functionality.\r\nRed makes her way to Cloudbank's Goldwalk district, where she learns that the Process is being controlled by the Camerata, a sinister group of high-ranking officials. The Camerata have been integrating individuals of influence through the Transistor; they were the ones who attacked Red, though the murder attempt was thwarted when the man stepped in front of the blow. At her former performance stage, Red confronts and defeats Sybil Reisz, a Camerata member in a corrupted Process-like form. She uses Sybil's knowledge of the Camerata to locate their leader—one of the administrators of Cloudbank, Grant Kendrell.\r\nAs Red enters the Highrise district, the Process becomes more aggressive, attacking the entire city indiscriminately. Asher Kendrell, another member of the Camerata, publicly reveals the Camerata's involvement and apologizes for their actions. It becomes apparent that the Camerata have lost control of the Process. After battling numerous Process enemies, including a massive creature referred to as the \"Spine\", Red reaches the hideout of the Camerata in Bracket Towers, only to find that Grant and Asher have committed suicide.\r\nResolving to track down the final member of the Camerata, Royce Bracket, and then escape the city, Red and her companion travel through Goldwalk, which has been \"Processed\" into a blocky, white facsimile of its original form. Royce approaches Red through a robotic proxy and offers to work together to stop the Process. He reveals that the Transistor, along with its docking point, the Cradle, are part of the apparatus used by the city's administrators to manipulate the landscape and environment of Cloudbank in order to satisfy the whims of the people. After fighting through the completely Processed district of Fairview, Red meets Royce and places the Transistor into the Cradle. This brings an end to the Process attack but also absorbs Red and Royce into the Transistor's virtual realm.\r\nRoyce informs Red that only one of them can return to Cloudbank and the two clash. After defeating Royce, Red is transported back into the city, where she begins rebuilding its Processed areas. Upon un-Processing the man's body and learning he cannot be restored from inside the Transistor, she sits down beside him and—despite his protests and pleadings otherwise—impales herself with the Transistor. In the closing credits sequence, it is shown that the man is reunited with Red within the virtual world of the Transistor. In a heartfelt embrace, she reveals that her voice has been restored. ",
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
                        WebsiteName = Domain.Enums.WebsiteNames.Steam,
                        WebsiteLink = "https://store.steampowered.com/app/237930/Transistor/"
                    },
                    new Website()
                    {
                        WebsiteName = Domain.Enums.WebsiteNames.EpicGames,
                        WebsiteLink = "https://store.epicgames.com/en-US/p/transistor/"
                    }
                ]
            },
            ];
            dataContext.Games.AddRange(games);
            dataContext.SaveChanges();
        }
    }

}
