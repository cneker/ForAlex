using Entities;
using lab04.Command;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04.ViewModels.Organizations
{
    public class OrganizationInfoViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public OrganizationInfoViewModel(Organization organization)
        {
            _repository = new RepositoryManager(new RepositoryContext());
            UpdateOrganization = new CommandBase(OnUpdate);
            DeleteOrganization = new CommandBase(OnDelete);
            SelectedOrganization = organization;
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

        public CommandBase DeleteOrganization { get; set; }
        public CommandBase UpdateOrganization { get; set; }

        private async void OnUpdate(object obj)
        {
            _repository.OrganizationRepository.Update(SelectedOrganization);
            await _repository.SaveAsync();
        }

        private async void OnDelete(object obj)
        {
            _repository.OrganizationRepository.DeleteOrganization(SelectedOrganization);
            await _repository.SaveAsync();
        }
    }
}
