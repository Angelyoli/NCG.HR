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
        string Extension1 { get; set; }
        string Extension2 { get; set; }
        string Extension3 { get; set; }
        string Extension4 { get; set; }
        string Extension5 { get; set; }
    }
}
