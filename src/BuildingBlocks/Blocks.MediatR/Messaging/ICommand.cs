using System;
using MediatR;

namespace Blocks.MediatR.Messaging;

public interface ICommand : IRequest, IBaseCommand;
public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand;

public interface IBaseCommand;
