<h1 align="center">SCP-008X</h1>
<div align="center">
<a href="https://github.com/MS-crew/SCP008X/releases"><img src="https://img.shields.io/github/downloads/MS-crew/SCP008X/total?style=for-the-badge&logo=githubactions&label=Downloads" alt="GitHub Release Download"></a> <a href="https://github.com/MS-crew/SCP008X/releases"><img src="https://img.shields.io/badge/Build-1.2.0-brightgreen?style=for-the-badge&logo=gitbook" alt="GitHub Releases"></a>
<a href="https://github.com/MS-crew/SCP008X/blob/master/LICENSE">
<img src="https://img.shields.io/badge/Licence-GPL_3.0-blue?style=for-the-badge&logo=gitbook" alt="General Public License v3.0"></a>
<a href="https://github.com/ExMod-Team/EXILED"><img src="https://img.shields.io/badge/Exiled-9.0.0-greeen?style=for-the-badge&logo=gitbook" alt="GitHub Exiled"></a>
</div>

<div align="center">
Adds the SCP-008 (Zombie Plague) mechanic to your server with extensive customization options.</div>

## SCP-008X
This plugin introduces the SCP-008 zombie plague to your server, allowing you to control every aspect of it.

- **Configurable Infection:** Control the chance of players getting infected by zombie attacks and the probability of curing the infection with medkits.

- **Zombie Buffs:** Reward zombies with temporary health (AHP) for each kill and set the maximum limit they can accumulate.

- **Containment Breach Events:** Trigger server-wide CASSIE announcements and facility-wide blackouts during a containment breach event.

- **Customizable Alerts:** Fully customize all player-facing hints, infection alerts, and CASSIE announcements.

- **Area of Effect (AOE) Infection:** Enable an Area of Effect (AOE) infection ability for zombies, affecting nearby players.

- **Round-End Statistics:** Enable summary stats at the end of the round to display the number of SCP-008 victims.

## Installation
- Download the latest release of the plugin from the releases page on GitHub.

- Extract the contents of the downloaded file into your \AppData\Roaming\EXILED\Plugins directory.

- Configure the plugin settings according to your server's needs.

- Restart your server to apply the changes.

## Feedback and Issues
We are actively developing this plugin. We welcome any feedback, bug reports, or suggestions for improvement.

Report Issues: Issues Page

Contact: discerrahidenetim@gmail.com

Thank you for using our plugin and helping us improve it!

Default Config
```yml
is_enabled: true
debug: false
# The probability of zombies revived by 49 becoming infected
recall_zombie_scp008_chance: 0
virus:
# The percentage chance of a player getting infected when attacked by an SCP-008 zombie.
  chance: 25
  # The percentage chance of curing the SCP-008 infection when using a Medkit. SCP-500 will always be 100% effective.
  cure_chance: 20
  # The message displayed to a player when they become infected with SCP-008.
  infection_alert: 'You''ve been infected! Use SCP-500 or a medkit to be cured!'
  infection_damage_per_seconds: 4
  infect_items: true
breached:
# Whether CASSIE should announce the containment breach of SCP-008.
  cassie_announce: true
  # The full CASSIE announcement message for the SCP-008 breach. Uses special syntax for pitch and timing.
  announcement: '<size=0> PITCH_.2 .G4 .G4 PITCH_.9 ATTENTION ALL PITCH_.6 PERSONNEL .G2 PITCH_.8 JAM027_4 . PITCH_.15 .G4 .G4 PITCH9999</size><color=#d64542>Attention, <color=#f5e042>all personnel...<split><size=0> PITCH_.9 SCP 008 PITCH_.7 CONTEİMENT PITCH_.85 BREACH PITCH_.8 DAMAGED PITCH_.2 .G4 .G4 PITCH9999</size><color=#d67d42>SCP 008 <color=#f5e042>Conteiment breach <color=#d67d42>violidation. <split><size=0> PITCH_.8 THE FACILITY PITCH_.9 IS GOING TO  PITCH_.85 A QUARANTINE PITCH_.15 .G4 .G4 PITCH_9999</size><color=#d64542><color=#f5e042>Please all personel follow the protocols until . <color=#d67d42>THE MTF UNİT <color=#000000>arrives in the facility.'
  # The CASSIE message to be announced when SCP-008 is considered recontained (e.g., all zombies are eliminated).
  conteiment_annoc: 'SCP 0 0 8 containedsuccessfully.'
  # Whether to turn off the lights in the facility during the SCP-008 containment breach.
  turn_off_lights: true
scp008_buff:
# Whether to give SCP-008 a temporary health (AHP) buff upon killing a player.
  enabled: true
  # The amount of Artificial Health Points (AHP) a zombie gains after killing a player.
  gain_ahp: 10
  # The maximum amount of AHP a zombie can gain from kills.
  max_gain: 400
aoe_infection:
# Whether to enable the Area of Effect (AOE) infection ability for zombie dies.
  enabled: false
  # The percentage chance that a player within the AOE radius will become infected.
  chance: 50
round_summary:
# Whether to display SCP-008 related statistics in the round summary.
  summary_stats: false
  # The message to be displayed in the round summary. {0} = number of SCP-008 victims, {1} = total number of players who died.
  round_end: '<voffset=-9m><color=yellow><b>SCP-008 Victims:</b> </color> <color=red> {0}/{1} </color></voffset>'
scp008role:
  id: 30
  damage: 40
  role: Scp0492
  max_health: 600
  name: 'SCP-008'
  description: 'An instance of SCP-008 that spreads the infection with each hit.'
  custom_info: 'SCP-008'
  ignore_spawn_system: true
  keep_position_on_spawn: true
  keep_inventory_on_spawn: true
  spawn_properties:
    limit: 1
    dynamic_spawn_points: []
    static_spawn_points: []
    role_spawn_points: []
    room_spawn_points:
    - room: HczHid
      offset:
        x: 0
        y: 0
        z: 0
      chance: 100
    locker_spawn_points: []
  custom_abilities: []
  inventory: []
  ammo: {}
  removal_kills_player: true
  keep_role_on_death: false
  spawn_chance: 0
  keep_role_on_changing_role: false
  broadcast:
  # The broadcast content
    content: ''
    # The broadcast duration
    duration: 10
    # The broadcast type
    type: Normal
    # Indicates whether the broadcast should be shown
    show: true
  display_custom_item_messages: true
  scale:
    x: 1
    y: 1
    z: 1
  gravity: 
  custom_role_f_f_multiplier: {}
  console_message: 'You have spawned as a custom role!'
  ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
