using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana07
{
    public class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();


        public static void Main(string[] args)
        {
            Console.WriteLine("<<<<<Funciones Linq Clasic>>>>>");

            // IntroToLinq();
            // DataSource();
            // Filtering();
            //Ordering();
            // Grouping();
            //Grouping2();
            Joining();

            Console.WriteLine("<<<<<Funciones Linq Lambda>>>>>");
             //IntroToLinqLambda();
             //DataSourceLambda();
             // FilteringLambda();
             //OrderingLambda();
             // GroupingLambda();
             // Grouping2Lambda();
            JoiningLambda();

         
            Console.Read();
        }

        //consultas

        static void IntroToLinq()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;

            foreach (int num in numQuery)
            {
                Console.WriteLine("{0,1}",num);
            }
        }

        static void DataSource()
        {
            var queryAllCustomer = from cust in context.clientes 
                                   select cust;

            foreach (var item in queryAllCustomer)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres" 
                                       select cust;

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

        }

        static void Ordering()
        {
            var queryLondonCustomers3 =    from cust in context.clientes
                                           where cust.Ciudad == "Londres"
                                           orderby cust.NombreCompañia ascending
                                           select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Grouping()
        {
            var queryCustomerByCity = from cust in context.clientes
                                      group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomerByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine( "     {0}",  customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                                      group cust by cust.Ciudad into custGroup
                                      where custGroup.Count() > 2
                                      orderby custGroup.Key
                                      select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new
                                 {
                                     customerName = cust.NombreCompañia,
                                     DistribucionName = dist.PaisDestinatario
                                 };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.customerName);
            }
        }

        //consultas lambda

        static void IntroToLinqLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var numQuery = numbers.Where(x => (x % 2) == 0);

            foreach (int num in numQuery)
            {
                Console.WriteLine(num);
            }
        }

        static void DataSourceLambda()
        {
            var queryAllCustomer = context.clientes;

            foreach (var item in queryAllCustomer)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(x => x.Ciudad == "Londres");

            foreach (var cliente in queryLondonCustomers)
            {
                Console.WriteLine(cliente.Ciudad);
            }
        }

        static void OrderingLambda()
        {
            var queryLondonCustomers3 = context.clientes.Where(x => x.Ciudad == "Londres").OrderBy(x => x.NombreCompañia);

            foreach (var cliente in queryLondonCustomers3)
            {
                Console.WriteLine(cliente.NombreCompañia);
            }
        }

        static void GroupingLambda()
        {
            var queryCustomerByCity = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var customerGroup in queryCustomerByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("     {0}",customer.NombreCompañia);
                }
            }

        }

        static void Grouping2Lambda()
        {
            var custQuery = context.clientes.GroupBy(x => x.Ciudad).Where(x => x.Count() > 2).OrderBy(x => x.Key);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes.Join(
                context.Pedidos,
                cli => cli.idCliente,
                dist => dist.IdCliente,
                (cli, dist) => new { customerName = cli.NombreCompañia, DistribucionName = dist.PaisDestinatario });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.customerName);
            }
        }







    }
}
