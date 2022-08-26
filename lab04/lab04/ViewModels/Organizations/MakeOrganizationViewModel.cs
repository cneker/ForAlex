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
    public class MakeOrganizationViewModel : ViewModelBase
    {
        private RepositoryManager _repository;

        public MakeOrganizationViewModel()
        {
            _repository = new RepositoryManager(new RepositoryContext());
            AddOrganization = new CommandBase(OnAdd);
            SelectedOrganization = new Organization();
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

        public CommandBase AddOrganization { get; set; }

        private async void OnAdd(object obj)
        {
            _repository.OrganizationRepository.CreateOrganization(SelectedOrganization);
            await _repository.SaveAsync();
        }
    }
}
