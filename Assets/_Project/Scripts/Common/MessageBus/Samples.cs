using Bluehorse.Game.Messages;
using UnityEngine;

public interface IMessageReceiver
{
    void OnEnable();
    void OnDisable();
}

public class Samples : MonoBehaviour, IMessageReceiver
{
    //
    //
    // https://github.com/dotnet-state-machine/stateless
    // https://github.com/mxjones/RedBus
    // https://github.com/franciscotufro/message-bus-pattern
    // https://startupfreakgame.com/2016/02/17/a-simple-message-bus-in-unity/
    // http://www.cyberforum.ru/blogs/529033/blog5507.html
    //
    //

    void IMessageReceiver.OnEnable()
    {
        //MessageBus.SampleMessage01.Receive += SampleMessage01_Receive;
    }

    private void SampleMessage01_Receive(string arg1, float arg2, object arg3)
    {
        throw new System.NotImplementedException();
    }

    void IMessageReceiver.OnDisable()
    {
        //MessageBus.SampleMessage01.Receive -= SampleMessage01_Receive;
    }
}

public class SamplePublisher
{
    public void OnFire()
    {
        //MessageBus.SampleMessage01.Send(null, 123, null);
    }
}