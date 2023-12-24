using System;
using System.Collections.Generic;

class Person : ICloneable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public object Clone()
    {
        return new Person(FirstName, LastName, Age);
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, Возраст: {Age}";
    }
}

class ObjectManager
{
    private List<Person> persons;

    public ObjectManager()
    {
        persons = new List<Person>();
    }

    // Метод для добавления массива объектов
    public void OpenArray(params Person[] people)
    {
        persons.AddRange(people);
    }

    // Метод для сортировки массива по заданным полям и направлениям
    public void Sort(string sortBy, string sortOrder)
    {
        switch (sortBy)
        {
            case "FirstName":
                persons.Sort((p1, p2) => sortOrder == "Ascending" ? p1.FirstName.CompareTo(p2.FirstName) : p2.FirstName.CompareTo(p1.FirstName));
                break;
            case "LastName":
                persons.Sort((p1, p2) => sortOrder == "Ascending" ? p1.LastName.CompareTo(p2.LastName) : p2.LastName.CompareTo(p1.LastName));
                break;
            case "Age":
                persons.Sort((p1, p2) => sortOrder == "Ascending" ? p1.Age.CompareTo(p2.Age) : p2.Age.CompareTo(p1.Age));
                break;
            default:
                Console.WriteLine("Некорректное поле для сортировки");
                break;
        }
    }

    // Метод для клонирования массива объектов
    public List<Person> CloneArray()
    {
        List<Person> clonedArray = new List<Person>();
        foreach (var person in persons)
        {
            clonedArray.Add((Person)person.Clone());
        }
        return clonedArray;
    }

    // Метод для фильтрации массива объектов с использованием предиката
    public List<Person> FilterArray(Predicate<Person> filter)
    {
        return persons.FindAll(filter);
    }

    // Метод для добавления нового объекта
    public void AddPerson(Person person)
    {
        persons.Add(person);
    }

    // Метод для удаления объекта
    public void DeletePerson(Person person)
    {
        persons.Remove(person);
    }

    // Метод для обновления объекта
    public void UpdatePerson(Person oldPerson, Person newPerson)
    {
        int index = persons.IndexOf(oldPerson);
        if (index != -1)
        {
            persons[index] = newPerson;
        }
        else
        {
            Console.WriteLine("Объект не найден");
        }
    }

    // Метод для отображения массива объектов
    public void DisplayArray()
    {
        foreach (var person in persons)
        {
            Console.WriteLine(person);
        }
    }
}

class Program
{
    static void Main()
    {
        ObjectManager manager = new ObjectManager();

        manager.OpenArray(
            new Person("Иван", "Иванов", 25),
            new Person("Юлия", "Сидорова", 30),
            new Person("Олег", "Петров", 22)
        );

        Console.WriteLine("Исходный массив:");
        manager.DisplayArray();

        Console.WriteLine("\nОтсортированный массив по имени (по возрастанию):");
        manager.Sort("FirstName", "Ascending");
        manager.DisplayArray();

        Console.WriteLine("\nКлонированный массив:");
        List<Person> clonedArray = manager.CloneArray();
        foreach (var person in clonedArray)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\nОтфильтрованный массив (возраст > 25):");
        List<Person> filteredArray = manager.FilterArray(p => p.Age > 25);
        foreach (var person in filteredArray)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\nДобавление нового объекта:");
        manager.AddPerson(new Person("Алиса", "Попович", 28));
        manager.DisplayArray();

        Console.WriteLine("\nОбновление объекта:");
        manager.UpdatePerson(new Person("Иван", "Иванов", 25), new Person("Иван", "Иванов", 26));
        manager.DisplayArray();

        Console.WriteLine("\nУдаление объекта:");
        manager.DeletePerson(new Person("Юлия", "Сидорова", 30));
        manager.DisplayArray();
    }
}
