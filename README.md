# Bannerlord Shipmaster Reworked

A mod for Mount & Blade II: Bannerlord for the DLC War Sails that changes the behaviour of experience gain for Shipmaster skill.

## Features

- Experience gain for Shipmaster skill from sailing now has a 3x multiplier if the number of ships in the party equals the maximum amount of ships the player party can have, by default it's 3 ships, with perks it can raise to 5.
- Experience gain for Shipmaster skill from sailing now has additional 2x multiplier for travelling in storms.
- Experience gain for Shipmaster skill from ramming in naval battles, it depends on the ramming quality as well as the ramming damage.
- Experience gain for Shipmaster skill from using ballistas in naval battles, it depends on the damage dealt to ships as well as agents (crew members) on ships.

## Requirements
- Mount & Blade II: Bannerlord with War Sails DLC installed.
- MCM (Mod Configuration Menu) for in-game configuration options.
- Harmony library.


## Installation
1. Download the latest version of the mod from the [Releases](https://github.com/puszkapotato/Bannerlord.ShipmasterReworked/releases) page.
2. Extract the downloaded ZIP file.
3. Copy the extracted folder to your Bannerlord `Modules` directory, typically located at:
   ```
   C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules
   ```
4. Enable the mod in the Bannerlord launcher before starting the game.

## Compatibility
This mod is compatible with the latest version of Mount & Blade II: Bannerlord and the War Sails DLC. It may not be compatible with other mods that alter the Shipmaster skill or naval combat mechanics.

## Ramming Experience Calculation

Shipmaster experience gained from ramming is calculated using the following formula:

``XP = BaseXP × DamagePercent × (1 + RamQuality × QualityFactor)``
Where:
- **BaseXP**: The base experience points for ramming (default is 80 XP).
- **DamagePercent**: The percentage of damage dealt to the target ship (0 to 100). For exceptional rams, this value can exceed 100% slightly, so hard limit I set is 150%.
- **RamQuality**: A multiplier based on the quality of the ram, this value is taken from the game's existing ram quality system.
- **QualityFactor**: How much ram quality influences experience gain.

Example Calculation:
If a player rams an enemy ship with a good ram quality, they can expect damage percentage of around 105%, with ram quality of 5.

This gives us:

`` XP = 80 x 1.05 x (1 + 5 x 0.15) = 80 x 1.05 x 1.75 = 147 XP ``

That value is then rounded to the nearest integer, and then multiplied by the learning rate of the skill, before finally giving the final experience points to the player.

## Ballista Experience Calculation

Shipmaster experience gained from using ballistas is divided between hitting the ship or an agent, where an agent is any crew member on the ship.

### Hitting a Ship
When hitting a ship with a ballista, the experience is calculated as follows:

First the code responsible for this:

```
// Convert damage to XP contribution
    float baseXp = damage * ConfigCache.BallistaDamageFactor;

    float distanceMultiplier = CalculateBallistaDistanceMultiplier(distanceMeters);

    // Safety clamp
    baseXp = MathF.Clamp(
        baseXp,
        ConfigCache.BallistaDamageXpMin,
        ConfigCache.BallistaDamageXpMax);

    float finalXp = baseXp * distanceMultiplier;
```

Now translated to human language:
``XP = DamageDealt × BallistaDamageFactor × DistanceMultiplier``
Where:
- **DamageDealt**: The amount of damage dealt to the ship by the ballista.
- **BallistaDamageFactor**: A configurable factor that scales the experience gained from ballista damage.
- **DistanceMultiplier**: A multiplier based on the distance from which the ballista shot was made. The further the distance (up to a certain limit), the higher the multiplier.
- **BallistaDamageXpMin**: The minimum experience points that can be gained from ballista damage.
- **BallistaDamageXpMax**: The maximum experience points that can be gained from ballista damage.

Example Calculation:
If a players deals 1800 damage to a ship from a distance of 200 meters with a BallistaDamageFactor of 0.05, BallistaDamageXpMin of 10, and BallistaDamageXpMax of 200:
`` XP = 1800 x 0.05 x 3.0 = 270 XP ``

That value is then rounded to the nearest integer, and then multiplied by the learning rate of the skill, before finally giving the final experience points to the player.

### Hitting an Agent
When hitting an agent (crew member) on a ship with a ballista, the experience is derived from what you gain towards the Engineering skill in vanilla game, multiplied by a configurable factor:

The calculation is as follows:
``XP = EngineeringXP × BallistaAgentDamageFactor``
Where:
- **EngineeringXP**: The experience points gained towards the Engineering skill from hitting an agent with a ballista (from testing it seems it equals to the damage dealt).
- **BallistaAgentDamageFactor**: A configurable factor that scales the experience gained from hitting agents with ballista.

Example Calculation:
If a player deals 150 damage to an agent on a ship with a BallistaAgentDamageFactor of 0.5:
`` XP = 150 x 0.5 = 75 XP ``

That value is then rounded to the nearest integer, and then multiplied by the learning rate of the skill, before finally giving the final experience points to the player.

### Ballista Distance Multiplier

Ballista distance multiplier is applied only for hits on ships and is using the distance between the firing ship and the hit location, so hitting the front of the ship is technically closer than hitting the back of the ship.

Distance multipliers are configurable but I believe default settings are balanced, this setting is also considered for advanced users, and has its own subcategory in MCM.


## License
This mod is released under the MIT License. See the [LICENSE](https://github.com/PuszkaPotato/Bannerlord.ShipmasterReworked/blob/master/LICENSE.txt) file for more details.

## Credits
- Developed by puszkapotato

## Support
If you encounter any issues or have suggestions for improvements, please open an issue on the [GitHub repository](https://github.com/puszkapotato/bannerlord.ShipmasterReworked/issues).

## Extra Links

- [Nexus Mods](https://www.nexusmods.com/mountandblade2bannerlord/mods/9570)
- [Discord - Vee Workshop](https://discord.gg/YcJQkkSuau)
