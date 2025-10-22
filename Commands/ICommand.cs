using System;

namespace CustomerManagement.Commands;

public interface ICommand
{
    void Execute();
}

public interface ICommand<TIinput>
{
    void Execute(TIinput input);
}

public interface ICommand<TIinput, TOutput>
{
    TOutput Execute(TIinput iinput);
}