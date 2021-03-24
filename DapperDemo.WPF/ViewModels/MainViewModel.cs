using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using System.Collections.Generic;

namespace DapperDemo.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IBonusRepository _bonusRepo;

        public MainViewModel(IBonusRepository bonusRepo)
        {
            _bonusRepo = bonusRepo;

            List<Company> companies = _bonusRepo.GetAllCompanyWithEmployees();
        }
    }
}
