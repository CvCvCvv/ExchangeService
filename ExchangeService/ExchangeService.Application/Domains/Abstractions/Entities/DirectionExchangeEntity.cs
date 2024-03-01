namespace ExchangeService.Application.Domains.Abstractions.Entities
{
    /// <summary>
    /// Сущность направления обмена
    /// </summary>
    public class DirectionExchangeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateStart { get; set; }
        public List<ExchangeEntity> Exchanges { get; set; } = new();
        public List<DirectionOperationEntity> DirectionOperations { get; set; } = new();
    }
}
