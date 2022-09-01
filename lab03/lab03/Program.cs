using Entities;
using Repository;

namespace lab03
{
    internal class Program
    {
        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }
        //библиотека классов entities - модели для работы с бд (представляют таблицы бд)
        //repositpry - репозиторий для работы с каждой моделью (таблицей бд)
        //repositorycontext - контекст бд (указываются строка подключения к бд, таблицы бд)
        static async Task MainAsync()
        {
            RepositoryContext context = new RepositoryContext();
            using RepositoryManager repository = new RepositoryManager(context);

            await Initialization(repository);
            await SelectAll(repository);
            await DeleteItem(repository);
            await UpdateEmployeeAge(repository);


        }
        //заполняем таблицы (коламнда add)
        static async Task Initialization(RepositoryManager repository)
        {
            repository.PosititonRepository.CreatePosition(
                new Position 
                { 
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 
                    Name = "Manager" 
                });
            repository.PosititonRepository.CreatePosition(
                new Position() 
                { 
                    Name = "Seller" 
                });
            await repository.SaveAsync();

            repository.ItemRepository.CreateItem(
                new Item 
                { 
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 
                    Name = "Table", 
                    Price = 9.34M 
                });
            repository.ItemRepository.CreateItem(
                new Item 
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                    Name = "Sofa", 
                    Price = 15.2M 
                });
            repository.ItemRepository.CreateItem(
                new Item 
                {
                    Name = "Coat", 
                    Price = 2.33M 
                });
            await repository.SaveAsync();

            repository.OrganizationRepository.CreateOrganization(
                new Organization 
                {
                    Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    Name = "LookTo", 
                    Country = "Belarus" 
                });
            repository.OrganizationRepository.CreateOrganization(
                new Organization 
                { 
                    Name = "Look2", 
                    Country = "Russia" 
                });
            await repository.SaveAsync();

            repository.EmployeeRepository.CreateEmployee(
                new Employee
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    FirstName = "Mike",
                    LastName = "Shinoda",
                    Age = 23,
                    PositionId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                });
            await repository.SaveAsync();

            repository.OrderRepository.CreateOrder(
                new Order
                {
                    Amount = 23,
                    EmployeeId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    ItemId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                    OrganizationId = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811")
                });
            repository.OrderRepository.CreateOrder(
                new Order
                {
                    Amount = 23,
                    EmployeeId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    ItemId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    OrganizationId = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811")
                });
            await repository.SaveAsync();
        }
        //выборка из бд
        static async Task SelectAll(RepositoryManager repository)
        {
            //select
            var employees = await repository.EmployeeRepository.GetAllEmployeesAsync();
            var items = await repository.ItemRepository.GetAllItemsAsync();

            Console.WriteLine("EMPLOYEES");
            foreach(var employee in employees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine("ITEMS:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            //select .. order by
            items = await repository.ItemRepository.GetAllItemsOrderedByPriceAscAsync();
            Console.WriteLine("ITEMS Ordered by price ASC:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

        }
        //удаление (delete)
        static async Task DeleteItem(RepositoryManager repository)
        {
            var item = (await repository.ItemRepository.GetAllItemsAsync()).LastOrDefault();
            repository.ItemRepository.DeleteItem(item);
            await repository.SaveAsync();

            var items = await repository.ItemRepository.GetAllItemsAsync();
            Console.WriteLine("items after delete the last one");
            foreach (var item1 in items)
            {
                Console.WriteLine(item1);
            }
        }
        //обновление строки (update)
        static async Task UpdateEmployeeAge(RepositoryManager repository)
        {
            Console.WriteLine("before update");
            var employee = await repository.EmployeeRepository.GetEmployeeAsync(new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));
            Console.WriteLine(employee);
            employee.Age = 25;
            await repository.SaveAsync();

            Console.WriteLine("after update");
            employee = await repository.EmployeeRepository.GetEmployeeAsync(new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));
            Console.WriteLine(employee);
        }
    }
}