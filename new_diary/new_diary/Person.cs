namespace new_diary
{
    public class Person
    {
        
            public string Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

        public Person(string id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
