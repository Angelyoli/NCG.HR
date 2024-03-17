namespace NCG.HR.Interface
{
    public interface IEntity
    {
        int Id { get; set; }
        string State { get; set; }
        string Status { get; set; }
        string Type { get; set; }
        string? CreatedById { get; set; }
        DateTime CreatedOn { get; set; }
        string? ModifyById { get; set; }
        DateTime ModifyOn { get; set; }
    }
}
