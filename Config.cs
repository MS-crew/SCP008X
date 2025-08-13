using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCP008X
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        [Description("The probability of zombies revived by 49 becoming infected")]
        public int RecallZombieScp008Chance { get; set; } = 0;

        public Virus Virus { get; set; } = new();
        public Breached Breached { get; set; } = new();
        public Scp008Buff Scp008Buff { get; set; } = new();
        public AoeInfection AoeInfection { get; set; } = new();
        public RoundSummary RoundSummary { get; set; } = new();
        public Scp008Role Scp008role { get; set; } = new();
    }

    public class RoundSummary
    {
        [Description("Whether to display SCP-008 related statistics in the round summary.")]
        public bool SummaryStats { get; set; } = false; 

        [Description("The message to be displayed in the round summary. {0} = number of SCP-008 victims, {1} = total number of players who died.")]
        public string RoundEnd { get; set; } = "<voffset=-9m><color=yellow><b>SCP-008 Victims:</b> </color> <color=red> {0}/{1} </color></voffset>";
    }

    public class Virus
    {
        [Description("The percentage chance of a player getting infected when attacked by an SCP-008 zombie.")]
        public int Chance { get; set; } = 25;

        [Description("The percentage chance of curing the SCP-008 infection when using a Medkit. SCP-500 will always be 100% effective.")]
        public int CureChance { get; set; } = 20;

        [Description("The message displayed to a player when they become infected with SCP-008.")]
        public string InfectionAlert { get; set; } = "You've been infected! Use SCP-500 or a medkit to be cured!";
        public float InfectionDamagePerSeconds { get; set; } = 4f;
    }

    public class AoeInfection
    {
        [Description("Whether to enable the Area of Effect (AOE) infection ability for zombie dies.")]
        public bool Enabled { get; set; } = false;

        [Description("The percentage chance that a player within the AOE radius will become infected.")]
        public int Chance { get; set; } = 50;
    }

    public class Scp008Buff
    {
        [Description("Whether to give SCP-008 a temporary health (AHP) buff upon killing a player.")]
        public bool Enabled { get; set; } = true;

        [Description("The amount of Artificial Health Points (AHP) a zombie gains after killing a player.")]
        public int GainAhp { get; set; } = 10;

        [Description("The maximum amount of AHP a zombie can gain from kills.")]
        public int MaxGain { get; set; } = 400;
    }

    public class Breached
    {
        [Description("Whether CASSIE should announce the containment breach of SCP-008.")]
        public bool CassieAnnounce { get; set; } = true;

        [Description("The full CASSIE announcement message for the SCP-008 breach. Uses special syntax for pitch and timing.")]
        public string Announcement { get; set; } = "<size=0> PITCH_.2 .G4 .G4 PITCH_.9 ATTENTION ALL PITCH_.6 PERSONNEL .G2 PITCH_.8 JAM027_4 . PITCH_.15 .G4 .G4 PITCH9999</size><color=#d64542>Attention, <color=#f5e042>all personnel...<split><size=0> PITCH_.9 SCP 008 PITCH_.7 CONTEİMENT PITCH_.85 BREACH PITCH_.8 DAMAGED PITCH_.2 .G4 .G4 PITCH9999</size><color=#d67d42>SCP 008 <color=#f5e042>Conteiment breach <color=#d67d42>violidation. <split><size=0> PITCH_.8 THE FACILITY PITCH_.9 IS GOING TO  PITCH_.85 A QUARANTINE PITCH_.15 .G4 .G4 PITCH_9999</size><color=#d64542><color=#f5e042>Please all personel follow the protocols until . <color=#d67d42>THE MTF UNİT <color=#000000>arrives in the facility.";
        
        [Description("The CASSIE message to be announced when SCP-008 is considered recontained (e.g., all zombies are eliminated).")]
        public string ConteimentAnnoc { get; set; } = "SCP 0 0 8 containedsuccessfully.";

        [Description("Whether to turn off the lights in the facility during the SCP-008 containment breach.")]
        public bool TurnOffLights { get; set; } = true;
    }
}