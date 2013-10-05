using System;
using System.Collections.Generic;

namespace MinimizeTheCostForCables
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            Dictionary<string, House> houses = new Dictionary<string, House>();
            PriorityQueue<Connection> connections = new PriorityQueue<Connection>();
            List<Queue<House>> housePostion = new List<Queue<House>>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(' ');

                var house1 = new House(input[0]);
                var house2 = new House(input[1]);
                var distance = int.Parse(input[2]);

                houses.AddSafe(house1.Name, house1);
                houses.AddSafe(house2.Name, house2);

                Connection connection = new Connection()
                    {
                        House1 = houses[house1.Name],
                        House2 = houses[house2.Name],
                        Distance = distance
                    };
                connections.Enqueue(connection);
                houses[house1.Name].Connections.Add(connection);
                houses[house2.Name].Connections.Add(connection);

            }

            int index = 0;
            while (connections.Count > 0)
            {
                var connection = connections.Dequeue();
                if (connection.House1.Id == int.MaxValue)
                {
                    var queue = new Queue<House>();
                    if (connection.House2.Id == int.MaxValue)
                    {
                        connection.House1.Id = index;
                        connection.House2.Id = index;

                        queue.Enqueue(connection.House1);
                        queue.Enqueue(connection.House2);

                        housePostion.Add(queue);
                       
                        index++;
                    }
                    else
                    {
                        connection.House1.Id = connection.House2.Id;
                        housePostion[connection.House2.Id].Enqueue(connection.House1);
                    }
                }
                else
                {
                    if (connection.House1.Id == connection.House2.Id)
                    {
                        connection.IsMinPath = false;
                        continue;
                    }
                    else
                    {
                        if (connection.House2.Id == int.MaxValue)
                        {
                            connection.House2.Id = connection.House1.Id;
                            housePostion[connection.House1.Id].Enqueue(connection.House2);
                        }
                        else
                        {
                            var queue = housePostion[connection.House2.Id];
                            while (queue.Count > 0)
                            {
                                var h = queue.Dequeue();
                                h.Id = connection.House1.Id;
                                housePostion[connection.House1.Id].Enqueue(h);
                            }
                        }
                    }
                }
            }
        }
    }
}
