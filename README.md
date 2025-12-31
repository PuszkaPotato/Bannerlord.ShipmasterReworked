# Bannerlord Shipmaster Reworked

A mod for Mount & Blade II: Bannerlord for the DLC War Sails that changes the behaviour of experience gain for Shipmaster skill.

## Features

- Experience gain for Shipmaster skill from sailing now has a 3x multiplier if the number of ships in the party equals the maximum amount of ships the player party can have, by default it's 3 ships, with perks it can raise to 5.
- Experience gain for Shipmaster skill from sailing now has additional 2x multiplier for travelling in storms.
- Experience gain for Shipmaster skill from ramming in naval battles, it depends on the ramming quality as well as the ramming damage.

## Requirements
- Mount & Blade II: Bannerlord with War Sails DLC installed.
- MCM (Mod Configuration Menu) for in-game configuration options.
- Harmony library.

## Ramming Experience Calculation

Shipmaster experience gained from ramming is calculated using the following formula:

``XP = BaseXP × DamagePercent × (1 + RamQuality × QualityFactor)``
Where:
- **BaseXP**: The base experience points for ramming (default is 80 XP).
- **DamagePercent**: The percentage of damage dealt to the target ship (0 to 100). For expeceptional rams, this value can exceed 100% slightly, so hard limit I set is 150%.
- **RamQuality**: A multiplier based on the quality of the ram, this value is taken from the game's existing ram quality system.
- **QualityFactor**: How much ram quality influences experience gain.

Example Calculation:
If a player rams an enemy ship with a good ram quality, they can expect damage percentage of around 105%, with ram quality of 5.

This gives us:

`` XP = 80 x 1.05 x (1 + 5 x 0.15) = 80 x 1.05 x 1.75 = 147 XP ``

That value is then rounded to the nearest integer, and then multiplied by the learning rate of the skill, before finally giving the final experience points to the player.


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

## License
This mod is released under the MIT License. See the [LICENSE](https://github.com/PuszkaPotato/Bannerlord.ShipmasterReworked/blob/master/LICENSE.txt) file for more details.

## Credits
- Developed by puszkapotato

## Support
If you encounter any issues or have suggestions for improvements, please open an issue on the [GitHub repository](https://github.com/puszkapotato/bannerlord.ShipmasterReworked/issues).

## Extra Links

- [Nexus Mods](https://www.nexusmods.com/mountandblade2bannerlord/mods/9570)
- Discord: (I don't have a server yet, if you need to contact me on Discord, please send me a message: puszkapotato)
