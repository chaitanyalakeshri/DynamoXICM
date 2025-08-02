namespace WSClass
{
    public class WSError
    {
        public string Message { get; set; }
    }

    public class WSDatabase
    {
        public string Path { get; set; }

        public string Guid { get; set; }
        public string ResultRoot { get; set; }
        public List<string> ListReadWriteRunFields { get; set; }
        public long RubyId { get; set; }

    }
    public class WSModelObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public long RubyId { get; set; }
        //public string DatabaseObjectRubyID { get; set; }
    }


}
