using ThiIsFine.Application.Repositories;
using ThiIsFine.Infrastructure.Data;
using ThiIsFine.Infrastructure.Repositories.UnitsOfWork.Base;

namespace ThiIsFine.Infrastructure.Repositories.UnitsOfWork
{
    public class ApplicationUnitOfWork(ApplicationDbContext dbContext,
            IImageRepository imageRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IPurchasesRepository purchasesRepository,
            IUsagesRepository usagesRepository)
        : UnitOfWork(dbContext), IApplicationUnitOfWork
    {
        public IImageRepository ImageRepository { get; } = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        public ISubscriptionsRepository SubscriptionsRepository { get; } = subscriptionsRepository ?? throw new ArgumentNullException(nameof(subscriptionsRepository));
        public IPurchasesRepository PurchasesRepository { get; } = purchasesRepository ?? throw new ArgumentNullException(nameof(purchasesRepository));
        public IUsagesRepository UsagesRepository { get; } = usagesRepository ?? throw new ArgumentNullException(nameof(usagesRepository));
    }
}
