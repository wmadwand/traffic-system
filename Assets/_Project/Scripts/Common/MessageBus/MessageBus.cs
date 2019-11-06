using Bluehorse.Core.Helper;

namespace Bluehorse.Game.Messages
{
    public sealed class MessageBus
    {
        public static readonly Message<float[]> OnAnalyzeSound = new Message<float[]>();
        public static readonly Message<float> OnAnalyzeTemperature = new Message<float>();

        public static readonly Message<bool> OnPitchDeviceSetActive = new Message<bool>();
        public static readonly Message<bool> OnVolumeDeviceSetActive = new Message<bool>();
        public static readonly Message<bool> OnTemperatureDeviceSetActive = new Message<bool>();

        public static readonly Message OnLessonStart = new Message();
        public static readonly Message OnStepFinished = new Message();
        public static readonly Message<bool> OnSpeechActive = new Message<bool>();
        
        public static readonly Message<LessonStep> OnStepRun = new Message<LessonStep>();

        public static readonly Message<string, float, object> SampleMessage03 = new Message<string, float, object>();


    }

    public class MessageArgs
    {
        public static readonly MessageArgs None = new MessageArgs();

        public override string ToString()
        {
            return "()";
        }
    }

    public sealed class PvpLeagueFinishedMessageArgs : MessageArgs
    {
        public PvpLeagueFinishedMessageArgs(string leagueId, int reward)
        {
            LeagueId = leagueId;
            Reward = reward;
        }

        public string LeagueId { get; }
        public int Reward { get; }
    }
    // MessageBus.PvpLeagueFinished.Send(new PvpLeagueFinishedMessageArgs(league.Id, league.Reward));

    public sealed class PlayerResourcesChangedMessageArgs : MessageArgs
    {
        public PlayerResourcesChangedMessageArgs(int fame, int gold, int strix, int flasks)
        {
            Fame = fame;
            Gold = gold;
            Strix = strix;
            Flasks = flasks;
        }

        public int Fame { get; }
        public int Gold { get; }
        public int Strix { get; }
        public int Flasks { get; }

        public override string ToString()
        {
            return $"{nameof(Fame)}: {Fame}, {nameof(Gold)}: {Gold}, {nameof(Strix)}: {Strix}, {nameof(Flasks)}: {Flasks}";
        }
    }
    // MessageBus.ResourcesChanged.Send(new PlayerResourcesChangedMessageArgs(fame, gold, strix, flasks));
}