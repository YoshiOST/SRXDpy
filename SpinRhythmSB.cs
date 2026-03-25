using Newtonsoft.Json.Linq;
using System.Collections.Generic;
public class CPHInline
{
    public bool Execute()
    {
        // Get JSON string
        string json = args["data"].ToString();

        // Parse JSON
        JObject obj = JObject.Parse(json);

        var trackArgs = obj["args"];

        string artist = (string)trackArgs["artist"];
        string track = (string)trackArgs["track_name"];
        string difficulty = (string)trackArgs["difficulty"];

        // Send Twitch chat message
        CPH.SendMessage($"Now playing: {track} by {artist} ({difficulty})");
        CPH.SendYouTubeMessage($"Now playing: {track} by {artist} ({difficulty})");
        CPH.SendKickMessage($"Now playing: {track} by {artist} ({difficulty})");
        var trackCredits = new Dictionary<string, string>
        {
            { "Adventures", "Music provided by Monstercat: Hyper Potions & Subtact - Adventures https://youtube.com/c/monstercatinstinct" },
            { "Inject", "Music provided by Monstercat: Droptek - Inject https://youtube.com/c/monstercat" },
            { "New Game", "Music provided by Monstercat: Nitro Fun - New Game https://youtube.com/c/monstercat" },
            { "Checkpoint", "Music provided by Monstercat: Hyper Potions & Nitro Fun - Checkpoint https://youtube.com/c/monstercat" },
            { "Humanity", "Song: Max Brhon - Humanity [NCS Release] Music provided by NoCopyrightSounds Free Download/Stream: http://ncs.io/Humanity Watch: http://youtu.be/OJhqsUnKUWw" },
            { "BUBBLES", "Music provided by Monstercat: Tokyo Machine - BUBBLES https://youtube.com/c/monstercatinstinct" },
            { "Revenger", "Music provided by Monstercat: F.O.O.L - Revenger https://youtube.com/c/monstercat" },
            { "Showdown", "Music provided by Monstercat: F.O.O.L - Showdown https://youtube.com/c/monstercat" },
            { "Your Pain", "Music provided by Monstercat: Koven - Your Pain https://youtube.com/c/monstercat" },
            { "Voidwalkers", "Music provided by Monstercat: Chime & Au5 - Voidwalkers https://youtube.com/c/monstercat" },
            { "New Year", "Music provided by SRXD OST: Hyper Potions - New Year https://www.youtube.com/watch?v=C39dTelfx0w" },
            { "Mimic", "Music provided by Monstercat: Droptek - Mimic https://youtube.com/c/monstercat" },
            { "This Is It", "Music provided by Monstercat: Rogue - This Is It https://youtube.com/c/monstercat" },
            { "Platform 9", "Track: Oneeva - Platform 9 [NCS Release] Music provided by NoCopyrightSounds. Watch more NCS on YouTube: https://NCS.lnk.to/YouTubeAT Free Download / Stream: http://ncs.io/Platform9" },
            { "VOLT", "Track: Modern Revolt - VOLT [NCS Release] Music provided by NoCopyrightSounds. Watch more NCS on YouTube: https://NCS.lnk.to/YouTubeAT Free Download / Stream: https://ncs.io/VOLT" },
            { "Razor Sharp", "Music provided by Monstercat: Tristam & Pegboard Nerds - Razor Sharp https://youtube.com/c/monstercat" },
            { "Hot Pursuit", "Music provided by Monstercat: Tut Tut Child - Hot Pursuit https://youtube.com/c/monstercat" },
            { "Flight", "it's on monstercat but unlicensable, please do not enable vod audio on stream" },
            { "Rattlesnake", "Music provided by Monstercat: Rogue - Rattlesnake https://youtube.com/c/monstercat" },
            { "Final Boss", "Music provided by Monstercat: Nitro Fun - Final Boss https://youtube.com/c/monstercat" }
        };

        if (trackCredits.TryGetValue(track, out string creditMessage))
        {
            CPH.SendMessage(creditMessage);
            CPH.SendYouTubeMessage(creditMessage);
            CPH.SendKickMessage(creditMessage);
            CPH.SendTrovoMessage(creditMessage);
            CPH.SetGlobalVar("SRXDcredit", creditMessage + $" on ({difficulty}) difficulty", true);
        } else {
            CPH.SetGlobalVar("SRXDcredit", $"Now playing: {track} by {artist} ({difficulty})", true);
        }
        return true;
    }
}
