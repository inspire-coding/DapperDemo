using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DapperDemo.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IBonusRepository _bonusRepo;
        private readonly ObservableCollection<Company> _companies;


        public IEnumerable<Company> Companies => _companies;


        public HomeViewModel(IBonusRepository bonusRepo)
        {
            _bonusRepo = bonusRepo;

            _companies = new ObservableCollection<Company>();

            GetCompanies();
        }

        private void GetCompanies()
        {
            IEnumerable<Company> companies = _bonusRepo.GetAllCompanyWithEmployees();

            foreach (Company company in companies)
            {
                _companies.Add(company);
            }
        }

    }
}
