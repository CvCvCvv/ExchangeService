﻿using ExchangeService.Application.Domains.Responses.Exchanges;
using MediatR;

namespace ExchangeService.Application.Domains.Requests.Exchanges;

public class CreateExchangeRequest : IRequest<CreateExchangeResponse>
{
    public Guid DirectionExchangeId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string NameExecutor { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public bool Closed { get; set; }
}
