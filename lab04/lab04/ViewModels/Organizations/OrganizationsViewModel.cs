using Entities;
using lab04.Command;
using lab04.Views.Organizations;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace lab04.ViewModels.Organizations
{
    public class OrganizationsViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public OrganizationsViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            Organizations = new ObservableCollection<Organization>(_repository.OrganizationRepository.GetAllOrganizations(false));
            MakeOrganization = new CommandBase(OnAdd);
            UpdateOrganization = new CommandBase(OnUpdate);
            FilterOrganizationByCountry = new CommandBase(OnFilteringByCountry);
        }

        private Organization _selectedOrganization;
        public Organization SelectedOrganization
        {
            get => _selectedOrganization;
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Organization> _organizations;
        public ObservableCollection<Organization> Organizations
        {
            get => _organizations;
            set
            {
                _organizations = value;
                OnPropertyChanged();
            }
        }
        private string _filterCountry;
        public string FilterCountry
        {
            get => _filterCountry;
            set
            {
                _filterCountry = value;
                OnPropertyChanged();
            }
        }
        public CommandBase UpdateOrganization { get; set; }
        public CommandBase FilterOrganizationByCountry { get; set; }
        public CommandBase MakeOrganization { get; set; }

        private async void OnAdd(object obj)
        {
            var window = new MakeOrganization();
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Organizations = new ObservableCollection<Organization>(_repository.OrganizationRepository.GetAllOrganizations(false));
            }
        }

        private async void OnUpdate(object obj)
        {
            var window = new OrganizationInfo(SelectedOrganization);
            if (window.ShowDialog() == true)
            {
                Thread.Sleep(200);
                Organizations = new ObservableCollection<Organization>(_repository.OrganizationRepository.GetAllOrganizations(false));
            }
        }

        public void OnFilteringByCountry(object obj)
        {
            Organizations = new ObservableCollection<Organization>(
                _repository
                .OrganizationRepository
                .GetAllOrganizations(false)
                .Where(o => Regex.IsMatch(o.Country.ToLower(), @$"\w*{FilterCountry.ToLower()}\w*")));
        }
    }
}
