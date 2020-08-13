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
        static void Main(string[] args)
        {
            // Создание базы данных из 20 сотрудников
            Repository repository = new Repository(50);

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
