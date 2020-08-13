using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Homework_01
{
    class Program
    {
        private static Random r = new Random();

        static void Main(string[] args)
        {
            //№1
            Repository repository = new Repository(r.Next(1, 20));
            repository.Print("База данных до преобразования");
            repository.DeleteWorkerBySalary(30000);
            repository.Print("База данных после первого преобразования");

            //№2
            repository = new Repository(40);
            repository.Print("База данных до преобразования");
            repository.Workers.Take(20).ToList().ForEach(x =>
            {
                repository.Workers.Remove(x);
            });
            repository.Print("База данных после первого преобразования");


            //№3
            repository = new Repository(50);

            // Печать в консоль всех сотрудников
            repository.Print("База данных до преобразования");

            var highSalaryWorkers = repository
                .Workers
                .Where(x => x.Salary >= 30000);

            repository
                .DeleteWorkersBatch(highSalaryWorkers)
                .Commit();

            // Печать в консоль сотрудников, которые не попали под увольнение
            repository.Print("База данных после первого преобразования");

            Console.ReadLine();
        }
    }
}
