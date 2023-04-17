using Controllers;
using Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        #region ConnectDB
        var connection = new MongoClient("mongodb://localhost:27017");
        var database = connection.GetDatabase("ControleLivros");

        var shelfStored = database.GetCollection<Book>("Stored");
        //var shelfBorrowed = database.GetCollection<Book>("Borrowed");
        //var shelfReading = database.GetCollection<Book>("Reading");
        #endregion
        do
        {
            switch (Menu(0, shelfStored))
            {
                case 1:
                    Console.WriteLine(NewBook(shelfStored));
                    Thread.Sleep(1000);
                    break;
                case 2:
                    ListBooks(shelfStored);
                    Thread.Sleep(1000);
                    break;
                case 3:
                    Menu(10, shelfStored);
                    Thread.Sleep(1000);
                    break;
                case 4:
                    Thread.Sleep(1000);
                    break;
                case 5:
                    Console.WriteLine("Hasta la vista, baby!");
                    Thread.Sleep(1000);
                    System.Environment.Exit(0);
                    break;

                default:
                    break;
            }
        } while (true);
    }

    public static int Menu(int m, IMongoCollection<Book> s)
    {
        string op = string.Empty;
        switch (m)
        {
            default:
                Console.WriteLine(">>>>> CONTROLE DE LIVROS <<<<<<");
                Console.WriteLine("1 - Inserir Livro");
                Console.WriteLine("2 - Listar Livros");
                Console.WriteLine("3 - Editar Livro");
                Console.WriteLine("4 - Excluir Livro");
                Console.WriteLine("5 - Sair");

                return int.Parse(Console.ReadLine());

            case 10:
                string n = string.Empty;

                Console.WriteLine(">>>>> EDITOR DE LIVRO <<<<<<");
                Console.Write("Informe o nome do livro que deseja modificar: ");
                
                if(string.IsNullOrEmpty(n))
                    n = Console.ReadLine();

                var b = FindBook(n, s);
                if (b == null)
                    return 0;

                Console.WriteLine("1 - Alterar Título");
                Console.WriteLine("2 - Alterar ISBN");
                Console.WriteLine("3 - Alterar Editora");
                Console.WriteLine("4 - Alterar Autor(es)");
                Console.WriteLine("5 - Voltar");
                Console.Write("Escolha uma opção: ");
                op = Console.ReadLine();
                if (int.Parse(op) == 5)
                    return 0;
                else
                {
                    switch (op)
                    {
                        case "1":
                            Console.Write("Informe o novo título: ");
                            Console.WriteLine(new BookController().UpdateBookByTitle(b, s).ToString());
                            break;
                        case "2":
                            Console.Write("Informe o novo ISBN: ");
                            Console.WriteLine(new BookController().UpdateBookByISBN(b, s).ToString());
                            break;
                        case "3":
                            Console.Write("Informe a nova editora: ");
                            Console.WriteLine(new BookController().UpdateBookByPublisher(b, s).ToString());
                            break;
                        case "4":
                            Console.Write("Informe o novo título: ");
                            Console.WriteLine(new BookController().UpdateBookByAuthors(b, s).ToString());
                            break;

                        default:
                            return 0;
                    }
                }
                return 0;
        }
    }
    public static Book NewBook(IMongoCollection<Book> shelf)
    {
        string t = string.Empty;
        string i = string.Empty;
        string e = string.Empty;
        string n = string.Empty;
        string s = string.Empty;

        Console.WriteLine("Informe o título: ");
        t = Console.ReadLine();

        Console.WriteLine("Informe o ISBN: ");
        i = Console.ReadLine();

        Console.WriteLine("Informe o editora: ");
        e = Console.ReadLine(); ;

        List<Author> al = new List<Author>();
        char a = '\0';
        do
        {

            Console.WriteLine("Informe o nome do autor: ");
            n = Console.ReadLine();
            Console.WriteLine("Informe o sobrenome do autor: ");
            s = Console.ReadLine();

            al.Add(new AuthorController().CreateAuthor(n, s));

            Console.WriteLine("Inserir outro autor?(Sim\\Não): ");
            try
            {
                a = char.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        } while (a != 'n');

        var book = new BookController().CreateBook(t, i, e, al);
        new BookController().InsertBook(book, shelf);

        Console.WriteLine("Livro inserido com sucesso!");

        return book;
    }
    public static Book FindBook(string t, IMongoCollection<Book> s)
    {
        var b = new BookController().SelectBook(t, s);
        
        if (b == null)
            Console.WriteLine("Livro não encontrado");

        return b!;
    }
    public static void ListBooks(IMongoCollection<Book> s)
    {
        new BookController().ListBooks(s);
    }
}