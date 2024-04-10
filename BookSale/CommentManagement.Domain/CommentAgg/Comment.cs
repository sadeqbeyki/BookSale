using AppFramework.Domain;

namespace CommentManagement.Domain.CommentAgg;

public class Comment : EntityBase<long>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Website { get; private set; }
    public string Message { get; private set; }
    public bool IsConfirmed { get; private set; }
    public bool IsCanceled { get; private set; }
    public long OwnerRecordId { get; private set; }
    public int Type { get; private set; }
    public long ParentId { get; private set; }
    public Comment Parent { get; private set; }

    public Comment(string name, string email, string website, string message,
        long ownerRecordId, int type, long parentId)
    {
        Name = name;
        Email = email;
        Message = message;
        Website = website;
        OwnerRecordId = ownerRecordId;
        Type = type;
        ParentId = parentId;
    }

    public void Confirm()
    {
        IsConfirmed = true;
        IsCanceled = false;
    }
    public void Cancel()
    {
        IsCanceled = true;
    }
}
