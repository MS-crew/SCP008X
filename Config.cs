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
    }

    public class Virus
    {
        [Description("The percentage chance of a player getting infected when attacked by an SCP-008 zombie.")]
        public int Chance { get; set; } = 25;

        [Description("The percentage chance of curing the SCP-008 infection when using a Medkit. SCP-500 will always be 100% effective.")]
        public int CureChance { get; set; } = 20;

        public float InfectionDamagePerSeconds { get; set; } = 4f;

        public bool InfectItems { get; set; } = true;
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

        [Description("Whether to turn off the lights in the facility during the SCP-008 containment breach.")]
        public bool TurnOffLights { get; set; } = true;
    }
}