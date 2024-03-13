namespace NCG.HR.Interface
{
    public interface IEntity
    {
        int Id { get; set; }
        string State { get; set; }
        string Status { get; set; }
        string Type { get; set; }
        int? CreatedById { get; set; }
        DateTime CreatedOn { get; set; }
        int? ModifyById { get; set; }
        DateTime ModifyOn { get; set; }
    }
}
