using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_01
{

    /// <summary>
    /// Организация хранения и генерации данных
    /// </summary>
    class Repository
    {
        /// <summary>
        /// База данных имён
        /// </summary>
        static readonly string[] firstNames;

        /// <summary>
        /// База данных фамилий
        /// </summary>
        static readonly string[] lastNames;

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        static Random randomize;

        /// <summary>
        /// Статический конструктор, в котором "хранятся"
        /// данные о именах и фамилиях баз данных firstNames и lastNames
        /// </summary>
        static Repository()
        {
            randomize = new Random(); // Размещение в памяти генератора случайных чисел

            // Размещение имен в базе данных firstNames
            firstNames = new string[] {
                "Агата",
                "Агнес",
                "Аделаида",
                "Аделина",
                "Алдона",
                "Алима",
                "Аманда",
            };

            // Размещение фамилий в базе данных lastNames
            lastNames = new string[]
            {
                "Иванова",
                "Петрова",
                "Васильева",
                "Кузнецова",
                "Ковалёва",
                "Попова",
                "Пономарёва",
                "Дьячкова",
                "Коновалова",
                "Соколова",
                "Лебедева",
                "Соловьёва",
                "Козлова",
                "Волкова",
                "Зайцева",
                "Ершова",
                "Карпова",
                "Щукина",
                "Виноградова",
                "Цветкова",
                "Калинина"
            };
        }

        /// <summary>
        /// База данных работников, в которой хранятся 
        /// Имя, фамилия, возраст и зарплаты каждого сотрудника
        /// </summary>
        public List<Worker> Workers { get; private set; }

        private List<Worker> _internalWorkers { get; set; }

        /// <summary>
        /// Применяет изменения при batch операциях <see cref="DeleteWorkersBatch(IEnumerable{Worker})"/>
        /// </summary>
        public void Commit()
        {
            Workers = _internalWorkers;
        }

        /// <summary>
        /// Конструктор, заполняющий базу данных Workers
        /// </summary>
        /// <param name="Count">Количество сотрудников, которых нужно создать</param>
        public Repository(int Count)
        {
            this.Workers = new List<Worker>(); // Выделение памяти для хранения базы данных Workers
            _internalWorkers = new List<Worker>();

            for (int i = 0; i < Count; i++)    // Заполнение базы данных Workers. Выполняется Count раз
            {
                var worker = new Worker(
                        // выбираем случайное имя из базы данных имён
                        firstNames[Repository.randomize.Next(Repository.firstNames.Length)],

                        // выбираем случайное имя из базы данных фамилий
                        lastNames[Repository.randomize.Next(Repository.lastNames.Length)],

                        // Генерируем случайный возраст в диапазоне 19 лет - 60 лет
                        randomize.Next(19, 60),

                        // Генерируем случайную зарплату в диапазоне 10000руб - 80000руб
                        randomize.Next(10000, 80000)
                        );

                // Добавляем нового работника в базы данных Workers
                this.Workers.Add(worker);
                this._internalWorkers.Add(worker);
            }
        }

        /// <summary>
        /// Метод вывода базы данных Workers в консоль
        /// </summary>
        /// <param name="Text">Вспомогательный текст, который будет напечатан перед выводом базы</param>
        public void Print(string Text)
        {
            Console.WriteLine(Text);    // Печать в консоль вспомогательного текста

            // Изменяем цвет шрифта для печати в консоли на DarkBlue
            Console.ForegroundColor = ConsoleColor.DarkBlue; 

            // Выводим Заголовки колонок базы данных
            Console.WriteLine($"{"Имя",15} {"Фамилия",15} {"Возраст",10} {"Зарплата",15}");

            // Изменяем цвет шрифта для печати в консоли на Gray
            Console.ForegroundColor = ConsoleColor.Gray;

            
            foreach (var worker in this.Workers) //
            {                                    // Печатаем в консоль всех работников
                Console.WriteLine(worker);       //
            }                                    //

            Console.WriteLine($"Итого: {this.Workers.Count}\n");    // Сводный отчёт. Сколько работников распечатано
        }

        /// <summary>
        /// Метод, увольняющий работников с зарплатой больше заданной
        /// </summary>
        /// <param name="MaxSalary">Уровень зарплаты работника, которых нужно уволить</param>
        public void DeleteWorkerBySalary(int MaxSalary)
        {
            this.Workers.RemoveAll(e => e.Salary > MaxSalary);//Удаление работников чья зарплата больше заданной MaxSalary
        }

        public Repository DeleteWorkersBatch(IEnumerable<Worker> workers)
        {
            foreach (var worker in workers)
            {
                this._internalWorkers.Remove(worker);
            }

            return this;
        }

        /// <summary>
        /// Метод, увольняющий работников с заданным именем
        /// </summary>
        /// <param name="CurrentName">Имя работников, которых нужно уволить</param>
        public void DeleteWorkerByName(string CurrentName)
        {
            this.Workers.RemoveAll(e => e.FirstName == CurrentName);//Удаление работников чьё имя Удовлетворяет выбранному CurrentName
        }
    }
}
