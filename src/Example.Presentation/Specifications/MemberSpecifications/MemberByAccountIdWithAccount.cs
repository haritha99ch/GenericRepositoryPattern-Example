namespace Example.Presentation.Specifications.MemberSpecifications;
public sealed class MemberByAccountIdWithAccount : Specification<Member>
{
    public MemberByAccountIdWithAccount(Guid accountId)
        : base(e => e.AccountId == accountId)
    {
        AddInclude(q => q.Include(m => m.Account));
    }
}
