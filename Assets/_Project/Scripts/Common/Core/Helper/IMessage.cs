using System;

namespace Bluehorse.Core.Helper
{
    public interface IMessage
    {
        event Action Receive;
        void Send();
    }

    public interface IMessage<T>
    {
        event Action<T> Receive;
        void Send(T arg);
    }

    public interface IMessage<T1, T2>
    {
        event Action<T1, T2> Receive;
        void Send(T1 arg1, T2 arg2);
    }

    public interface IMessage<T1, T2, T3>
    {
        event Action<T1, T2, T3> Receive;
        void Send(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IMessage<T1, T2, T3, T4>
    {
        event Action<T1, T2, T3, T4> Receive;
        void Send(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
}