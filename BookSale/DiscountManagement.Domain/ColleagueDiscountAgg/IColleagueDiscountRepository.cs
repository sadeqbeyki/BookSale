using AppFramework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg;

public interface IColleagueDiscountRepository : IBaseRepository<long, ColleagueDiscount>
{
    EditColleagueDiscount GetDetails(long id);
    List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
}
