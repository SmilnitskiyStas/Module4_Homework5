using Microsoft.EntityFrameworkCore;
using UseLazyLoading.Entities;
using UseLazyLoading.Models;

namespace UseLazyLoading
{
    internal class Starter
    {
        public static void Run()
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
            }
        }

        public static void RequestToDB()
        {
            // 1. Запрос, который объединяет 3 таблицы и обязательно включает LEFT JOIN.
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var employeeLeftJoin2 = applicationContext.Employees.GroupJoin(
                    applicationContext.Titles,
                    e => e.TitleId,
                    t => t.TitleId,
                    (e, t) => new
                    {
                        e,
                        t
                    })
                    .GroupJoin(
                    applicationContext.Offices,
                    e => e.e.OfficeId,
                    o => o.OfficeId,
                    (e, o) => new
                    {
                        e,
                        o
                    })
                    .SelectMany(
                    x => x.o.DefaultIfEmpty(),
                    (e, o) => new
                    {
                        FirstName = e.e.e.FirstName,
                        LastName = e.e.e.LastName,
                        Title = e.e.t.FirstOrDefault().Name,
                        Location = o.Location
                    });
            }

            // 2. Запрос, который возвращает разницу между HiredDate и сегодня в днях. Фильтрация должна быть выполнена на сервере.
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                DateTime dateTime = DateTime.Now;

                var employeeHiredDate = applicationContext.Employees.Select(x => (dateTime - x.HiredDate.Date).Days).ToList();
            }

            // 3. Запрос, который обновляет 2 сущности. Сделать в одной  транзакции.
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var employeeUpdate = applicationContext.Employees.Join(
                    applicationContext.Titles,
                    e => e.TitleId,
                    t => t.TitleId,
                    (e, t) => new
                    {
                        e,
                        t
                    });

                employeeUpdate.FirstOrDefault().e.FirstName = "MamaRika";
                employeeUpdate.FirstOrDefault().t.Name = "UPDATE TEXT";

                applicationContext.SaveChanges();
            }

            // 4. Запрос, который добавляет сущность Employee с Title и Office.
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var titles = applicationContext.Titles;

                var offices = applicationContext.Offices;

                Employee employee = new Employee
                {
                    FirstName = "Stas",
                    LastName = "Smilnitskiy",
                    HiredDate = DateTime.Now,
                    DateOfBirth = new DateTime(1996, 07, 31),
                    Title = titles.Where(t => t.Name == "UPDATE TEXT").FirstOrDefault(),
                    Office = offices.Where(o => o.Location == "Kherson").FirstOrDefault()
                };

                applicationContext.Employees.Add(employee);

                applicationContext.SaveChanges();
            }

            // 5. Запрос, который удаляет сущность Employee.
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                Employee employeeDelete = applicationContext.Employees.Where(e => e.EmployeeId == 7).FirstOrDefault();

                applicationContext.Employees.Remove(employeeDelete);

                applicationContext.SaveChanges();
            }

            // 6. Запрос, который группирует сотрудников по ролям и возвращает название роли (Title) если оно не содержит ‘a’.
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var employeeGropBy = applicationContext.Employees.Where(e => !EF.Functions.Like(e.Office.Title, "%a%")).GroupBy(e => e.Office.Title).Select(g => new
                {
                    g.Key,
                    Count = g.Count()
                });
            }
        }

        private static void SeedDb()
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                Title title1 = new Title { Name = "Hello" };
                Title title2 = new Title { Name = "World!" };
                Title title3 = new Title { Name = "Goodbye!" };
                Title title4 = new Title { Name = "Holly" };
                Title title5 = new Title { Name = "My Name" };

                applicationContext.Titles.Add(title1);
                applicationContext.Titles.Add(title2);
                applicationContext.Titles.Add(title3);
                applicationContext.Titles.Add(title4);
                applicationContext.Titles.Add(title5);

                Office office1 = new Office { Title = "Fabrica", Location = "Kherson" };
                Office office2 = new Office { Title = "Cilpo", Location = "Kherson" };
                Office office3 = new Office { Title = "ATB", Location = "Kiev" };
                Office office4 = new Office { Title = "SportMaster", Location = "Kiev" };
                Office office5 = new Office { Title = "Lviv chocolate", Location = "Lviv" };

                applicationContext.Offices.Add(office1);
                applicationContext.Offices.Add(office2);
                applicationContext.Offices.Add(office3);
                applicationContext.Offices.Add(office4);
                applicationContext.Offices.Add(office5);

                Employee employee1 = new Employee { FirstName = "Oleg", LastName = "Momba", HiredDate = new DateTime(2019, 12, 02), DateOfBirth = new DateTime(1996, 08, 03), Title = title2, Office = office1 };
                Employee employee2 = new Employee { FirstName = "Anna", LastName = "Motsio", HiredDate = new DateTime(2002, 08, 12), DateOfBirth = new DateTime(1974, 12, 12), Title = title3, Office = office5 };
                Employee employee3 = new Employee { FirstName = "Olga", LastName = "Shults", HiredDate = new DateTime(2016, 11, 09), DateOfBirth = new DateTime(1986, 05, 12), Title = title4, Office = office4 };
                Employee employee4 = new Employee { FirstName = "Sasha", LastName = "Turisto", HiredDate = new DateTime(2013, 04, 21), DateOfBirth = new DateTime(1990, 06, 21), Title = title1, Office = office3 };
                Employee employee5 = new Employee { FirstName = "Victoria", LastName = "Dovzenko", HiredDate = new DateTime(2001, 10, 14), DateOfBirth = new DateTime(1984, 11, 17), Title = title5, Office = office2 };

                applicationContext.Employees.Add(employee1);
                applicationContext.Employees.Add(employee2);
                applicationContext.Employees.Add(employee3);
                applicationContext.Employees.Add(employee4);
                applicationContext.Employees.Add(employee5);

                Client client1 = new Client { FirstName = "Stith", LastName = "Horor", Email = "stith@gmai.com", DateOfBirth = new DateTime(1987, 08, 20) };
                Client client2 = new Client { FirstName = "Anna", LastName = "Omny", Email = "anna@gmai.com", DateOfBirth = new DateTime(1985, 11, 12) };
                Client client3 = new Client { FirstName = "Bob", LastName = "Potato", Email = "bob@gmai.com", DateOfBirth = new DateTime(1991, 02, 25) };
                Client client4 = new Client { FirstName = "Romeo", LastName = "Likeky", Email = "romeo@gmai.com", DateOfBirth = new DateTime(1982, 09, 14) };
                Client client5 = new Client { FirstName = "Kate", LastName = "Morgest", Email = "kate@gmai.com", DateOfBirth = new DateTime(1993, 12, 19) };

                applicationContext.Clients.Add(client1);
                applicationContext.Clients.Add(client2);
                applicationContext.Clients.Add(client3);
                applicationContext.Clients.Add(client4);
                applicationContext.Clients.Add(client5);

                Project project1 = new Project { Name = "Create DB", Budget = 14.89m, StartedDate = new DateTime(2022, 04, 05), Client = client2 };
                Project project2 = new Project { Name = "Add data to DB", Budget = 1499.99m, StartedDate = new DateTime(2022, 12, 03), Client = client1 };
                Project project3 = new Project { Name = "Use Lazy Loading", Budget = 9.49m, StartedDate = new DateTime(2021, 07, 07), Client = client4 };
                Project project4 = new Project { Name = "Make inquiries using the Linq", Budget = 27.99m, StartedDate = new DateTime(2022, 09, 17), Client = client5 };
                Project project5 = new Project { Name = "Add migration and update DB", Budget = 899.99m, StartedDate = new DateTime(2023, 07, 05), Client = client2 };

                applicationContext.Projects.Add(project1);
                applicationContext.Projects.Add(project2);
                applicationContext.Projects.Add(project3);
                applicationContext.Projects.Add(project4);
                applicationContext.Projects.Add(project5);

                EmployeeProject employeeProject1 = new EmployeeProject { Rate = 350m, StartedDate = new DateTime(2019, 04, 12), Employee = employee1, Project = project1 };
                EmployeeProject employeeProject2 = new EmployeeProject { Rate = 879m, StartedDate = new DateTime(2019, 08, 22), Employee = employee2, Project = project2 };
                EmployeeProject employeeProject3 = new EmployeeProject { Rate = 999m, StartedDate = new DateTime(2018, 05, 02), Employee = employee4, Project = project3 };
                EmployeeProject employeeProject4 = new EmployeeProject { Rate = 478m, StartedDate = new DateTime(2020, 03, 18), Employee = employee2, Project = project4 };
                EmployeeProject employeeProject5 = new EmployeeProject { Rate = 1149m, StartedDate = new DateTime(2022, 07, 19), Employee = employee5, Project = project5 };

                applicationContext.EmployeeProjects.Add(employeeProject1);
                applicationContext.EmployeeProjects.Add(employeeProject2);
                applicationContext.EmployeeProjects.Add(employeeProject3);
                applicationContext.EmployeeProjects.Add(employeeProject4);
                applicationContext.EmployeeProjects.Add(employeeProject5);

                applicationContext.SaveChanges();
            }
        }
    }
}
