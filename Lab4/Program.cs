using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                CollectionType<int> A = new CollectionType<int>("Myname", "Myorganization");
                A.Add(1);
                A.Add(2);
                A.Add(-3);
                A.Add(4);
                A.Add(2);
                A.Add(-2);
                CollectionType<int> B= new CollectionType<int>("Myname", "Myorganization");
                /*B.Add(1);
                B.Add(2.1);
                B.Add(-3.32);
                B.Add(4.11);*/
                CollectionType<int> C = new CollectionType<int>("Myname", "Myorganization");
                C.Add("ff");
                C.Add("432334");
                C.Add("vfff");
                C.Add("eeeee");

                C++;
                C++;
                A.Delete(2);
                A.Delete(-3);

                B.Read();

                A.Show();                
                B.Show();
                C.Show();
               

            }
            catch(Exception e)
            {
                Console.WriteLine( e.Message);
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Finally");
            }

            Console.ReadKey();
        }
    }


    static class File<T>
    {
        static string path = @"C:\Users\User\Desktop\Lab OOP\Lab8.txt";
        static FileStream file;
        static StreamWriter writer;
        static StreamReader reader;

        static File()
        {
            file = new FileStream(path, FileMode.OpenOrCreate);
        }
      
        public static void Write(Stack<T> A)
        {
            
            using (writer = new StreamWriter(file))
            {

                object str = A.Pop();
                while (str != null)
                {
                    writer.WriteLine(str);
                    str = A.Pop();

                }

                
            }

        }
        public static void Read(Stack<T> A)
        {

            using (reader = new StreamReader(file))
            {
                object line;
                while ((line = reader.ReadLine()) != null)
                {
                    A.Push(line);
                }
            }

        }
    }


    public interface IInfo<T>
    {
        void Add(object t);
        void Delete(T dt);
        void Show();

    }

     
    class CollectionType<T> : IInfo<T>
    {
        public Stack<T> A;
        public CollectionType(string s1,string s2)
        {
             A = new Stack<T>(s1, s2);
        }      
        public void Add(object t)
        {
            if (t ==null) throw new Exception("Не может null");
            if (t is int && (int)t < -100) throw new Exception("Не может быть меньше -100");
                A.Push(t);
        }
        public void Show()
        {
            Console.WriteLine();
            object str = A.Pop();
            while (str != null)
            {
                Console.WriteLine(str);
                str = A.Pop();
            }
        }
        public void Delete(T dt)
        {
            A = A - dt;
        }
        public static CollectionType<T> operator ++(CollectionType<T> stack)
        {
            object obj = stack.A.Pop();
            stack.Add(obj);
            stack.Add(obj);
            return stack;
        }
        public void Write()
        {
            File<T>.Write(A);
        }
        public void Read()
        {
            File<T>.Read(A);
        }
    }


    public class Elem<T>
    {
        object data;
        Elem<T> next;
        public Elem(object i, Elem<T> j)
        {
            data = i;
            next = j;
        }
        public object Data
        {
            get { return data; }
        }
        public Elem<T> Next
        {
            get { return next; }

        }
    }
    public class Stack<T> 
    {       
        int kol;
        Elem<T> head;
        Owner owner;
        DATE date;
        public Elem<T> Head
        {
            get { return head; }
        }
        public int Kol
        {
            get { return kol; }
        }
        public string Name
        {
            get { return owner.name; }
        }
        public string Organization
        {
            get { return owner.organization; }
        }
        public int Id
        {
            get { return owner.id; }
        }
        public string Date
        {
            get { return date.thisDay.ToString("d"); }
        }

        Stack()
        {
            head = null;
            kol = 0;
        }
        public Stack(string name, string organization) : this()
        {
            owner = new Owner(name, organization);
            date = new DATE();
        }

         class Owner
        {
            public readonly int id;
            public readonly string name;
            public readonly string organization;

            public Owner(string name, string organization)
            {
                id = (name + organization).GetHashCode();
                this.name = name;
                this.organization = organization;
            }
        }
         class DATE
        {
            public readonly DateTime thisDay;
            public DATE()
            {
                thisDay = DateTime.Today;
            }
        }
        
        public void Push(object i)
        {
            head = new Elem<T>(i, head);
            kol++;
        }
        public object Pop()
        {
            if (kol == 0)
            {
                return null;
            }
            object ret = head.Data;
            kol--;
            head = head.Next;
            return ret;
        }       
        public static Stack<T> operator -(Stack<T> stack, T el)
        {

            Stack<T> InterimSteck = new Stack<T>();
            object obj;
            while (stack.Head != null)
            {
                obj = stack.Pop();
              if (obj.Equals(el)) continue;

                InterimSteck.Push(obj);
            }

            while (InterimSteck.Head != null)
            {
                stack.Push(InterimSteck.Pop());
            }
            return stack;
        }
     
    }
   

   

}

