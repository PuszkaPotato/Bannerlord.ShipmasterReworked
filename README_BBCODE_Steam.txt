[h1]Bannerlord Shipmaster Reworked[/h1]

A mod for Mount & Blade II: Bannerlord for the DLC War Sails that changes the behaviour of experience gain for Shipmaster skill.

[h2]Features[/h2]
[list]
[*] Experience gain for Shipmaster skill from sailing now has a 3x multiplier if the number of ships in the party equals the maximum amount of ships the player party can have, by default it's 3 ships, with perks it can raise to 5.
[*] Experience gain for Shipmaster skill from sailing now has additional 2x multiplier for travelling in storms.
[*] Experience gain for Shipmaster skill from ramming in naval battles, it depends on the ramming quality as well as the ramming damage.
[*] Experience gain for Shipmaster skill from using ballistas in naval battles, it depends on the damage dealt to ships as well as agents (crew members) on ships.
[/list]
[h2]Requirements[/h2]
[list]
[*] Mount & Blade II: Bannerlord with War Sails DLC installed.
[*] MCM (Mod Configuration Menu) for in-game configuration options.
[*] Harmony library.
[/list]

[h2]Installation[/h2]
[olist]
[*] Download the latest version of the mod from the [url=https://github.com/puszkapotato/Bannerlord.ShipmasterReworked/releases]Releases[/url] page.
[*] Extract the downloaded ZIP file.
[*] Copy the extracted folder to your Bannerlord [b]Modules[/b] directory, typically located at:
[code]
   C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules
[/code]
[*] Enable the mod in the Bannerlord launcher before starting the game.
[/olist]
[h2]Compatibility[/h2]
This mod is compatible with the latest version of Mount & Blade II: Bannerlord and the War Sails DLC. It may not be compatible with other mods that alter the Shipmaster skill or naval combat mechanics.

[h2]Ramming Experience Calculation[/h2]

Shipmaster experience gained from ramming is calculated using the following formula:

[b]XP = BaseXP � DamagePercent � (1 + RamQuality � QualityFactor)[/b]
Where:
[list]
[*] [b]BaseXP[/b]: The base experience points for ramming (default is 80 XP).
[*] [b]DamagePercent[/b]: The percentage of damage dealt to the target ship (0 to 100). For exceptional rams, this value can exceed 100% slightly, so hard limit I set is 150%.
[*] [b]RamQuality[/b]: A multiplier based on the quality of the ram, this value is taken from the game's existing ram quality system.
[*] [b]QualityFactor[/b]: How much ram quality influences experience gain.
[/list]
Example Calculation: If a player rams an enemy ship with a good ram quality, they can expect damage percentage of around 105%, with ram quality of 5.

This gives us:

[b]XP = 80 x 1.05 x (1 + 5 x 0.15) = 80 x 1.05 x 1.75 = 147 XP[/b]

That value is then rounded to the nearest integer, and then multiplied by the learning rate of the skill, before finally giving the final experience points to the player.

[h2]Ballista Experience Calculation[/h2]

Shipmaster experience gained from using ballistas is divided between hitting the ship or an agent, where an agent is any crew member on the ship.

[h3]Hitting a Ship[/h3]
When hitting a ship with a ballista, the experience is calculated as follows:

First the code responsible for this:

[code]
// Convert damage to XP contribution
    float baseXp = damage * ConfigCache.BallistaDamageFactor;

    float distanceMultiplier = CalculateBallistaDistanceMultiplier(distanceMeters);

    // Safety clamp
    baseXp = MathF.Clamp(
        baseXp,
        ConfigCache.BallistaDamageXpMin,
        ConfigCache.BallistaDamageXpMax);

    float finalXp = baseXp * distanceMultiplier;
[/code]

Now translated to human language: [b]XP = DamageDealt � BallistaDamageFactor � DistanceMultiplier[/b]
Where:
[list]
[*] [b]DamageDealt[/b]: The amount of damage dealt to the ship by the ballista.
[*] [b]BallistaDamageFactor[/b]: A configurable factor that scales the experience gained from ballista damage.
[*] [b]DistanceMultiplier[/b]: A multiplier based on the distance from which the ballista shot was made. The further the distance (up to a certain limit), the higher the multiplier.
[*] [b]BallistaDamageXpMin[/b]: The minimum experience points that can be gained from ballista damage.
[*] [b]BallistaDamageXpMax[/b]: The maximum experience points that can be gained from ballista damage.
[/list]
Example Calculation: If a players deals 1800 damage to a ship from a distance of 200 meters with a BallistaDamageFactor of 0.05, BallistaDamageXpMin of 10, and BallistaDamageXpMax of 200: [b]XP = 1800 x 0.05 x 3.0 = 270 XP[/b]

That value is then rounded to the nearest integer, and then multiplied by the learning rate of the skill, before finally giving the final experience points to the player.

[h3]Hitting an Agent[/h3]
When hitting an agent (crew member) on a ship with a ballista, the experience is derived from what you gain towards the Engineering skill in vanilla game, multiplied by a configurable factor:

The calculation is as follows: [b]XP = EngineeringXP � BallistaAgentDamageFactor[/b]
Where:
[list]
[*] [b]EngineeringXP[/b]: The experience points gained towards the Engineering skill from hitting an agent with a ballista (from testing it seems it equals to the damage dealt).
[*] [b]BallistaAgentDamageFactor[/b]: A configurable factor that scales the experience gained from hitting agents with ballista.
[/list]
Example Calculation: If a player deals 150 damage to an agent on a ship with a BallistaAgentDamageFactor of 0.5: [b]XP = 150 x 0.5 = 75 XP[/b]

That value is then rounded to the nearest integer, and then multiplied by the learning rate of the skill, before finally giving the final experience points to the player.

[h3]Ballista Distance Multiplier[/h3]

Ballista distance multiplier is applied only for hits on ships and is using the distance between the firing ship and the hit location, so hitting the front of the ship is technically closer than hitting the back of the ship.

Distance multipliers are configurable but I believe default settings are balanced, this setting is also considered for advanced users, and has its own subcategory in MCM.


[h2]License[/h2]
This mod is released under the MIT License. See the [url=https://github.com/PuszkaPotato/Bannerlord.ShipmasterReworked/blob/master/LICENSE.txt]LICENSE[/url] file for more details.

[h2]Credits[/h2]
[list]
[*] Developed by puszkapotato
[/list]
[h2]Support[/h2]
If you encounter any issues or have suggestions for improvements, please open an issue on the [url=https://github.com/puszkapotato/bannerlord.ShipmasterReworked/issues]GitHub repository[/url].

[h2]Extra Links[/h2]
[list]
[*] [url=https://www.nexusmods.com/mountandblade2bannerlord/mods/9570]Nexus Mods[/url]
[*] Discord: (I don't have a server yet, if you need to contact me on Discord, please send me a message: puszkapotato)
[/list]