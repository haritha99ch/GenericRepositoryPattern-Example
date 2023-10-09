namespace Example.Presentation.Specifications.MemberSpecifications;
public sealed class MemberByIdWithAccount : Specification<Member>
{
    public MemberByIdWithAccount(Guid id) : base(e => e.Id == id)
    {
        AddInclude(q => q.Include(m => m.Account));
    }
}
