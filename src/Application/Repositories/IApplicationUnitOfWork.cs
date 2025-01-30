using Application.Core.Repositories.UnitsOfWork.Base;

namespace ThiIsFine.Application.Repositories
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IImageRepository ImageRepository { get; }
        ISubscriptionsRepository SubscriptionsRepository { get; }
        IPurchasesRepository PurchasesRepository { get; }
        IUsagesRepository UsagesRepository { get; }
    }
}
