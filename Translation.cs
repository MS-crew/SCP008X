using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCP008X
{
    public class Translation :ITranslation
    {
        [Description("The message to be displayed in the round summary. {0} = number of SCP-008 victims, {1} = total number of players who died.")]
        public string RoundEnd { get; set; } = "<voffset=-9m><color=yellow><b>SCP-008 Victims:</b> </color> <color=red> {0}/{1} </color></voffset>";

        [Description("The message displayed to a player when they become infected with SCP-008.")]
        public string InfectionAlert { get; set; } = "<color=yellow><b>SCP-008</b></color>\nYou've been infected! Use SCP-500 or a medkit to be cured!";

        [Description("The full CASSIE announcement message for the SCP-008 breach. Uses special syntax for pitch and timing.")]
        public string Announcement { get; set; } = "<size=0> PITCH_.2 .G4 .G4 PITCH_.9 ATTENTION ALL PITCH_.6 PERSONNEL .G2 PITCH_.8 JAM027_4 . PITCH_.15 .G4 .G4 PITCH9999</size><color=#d64542>Attention, <color=#f5e042>all personnel...<split><size=0> PITCH_.9 SCP 008 PITCH_.7 CONTEİMENT PITCH_.85 BREACH PITCH_.8 DAMAGED PITCH_.2 .G4 .G4 PITCH9999</size><color=#d67d42>SCP 008 <color=#f5e042>Conteiment breach <color=#d67d42>violidation. <split><size=0> PITCH_.8 THE FACILITY PITCH_.9 IS GOING TO  PITCH_.85 A QUARANTINE PITCH_.15 .G4 .G4 PITCH_9999</size><color=#d64542><color=#f5e042>Please all personel follow the protocols until . <color=#d67d42>THE MTF UNİT <color=#000000>arrives in the facility.";

        [Description("The CASSIE message to be announced when SCP-008 is considered recontained (e.g., all zombies are eliminated).")]
        public string ConteimentAnnoc { get; set; } = "SCP 0 0 8 containedsuccessfully.";

        [Description("The message displayed to a player when they successfully cure themselves of SCP-008.")]
        public string Cured { get; set; } = "You cured your self.";
    }
}
