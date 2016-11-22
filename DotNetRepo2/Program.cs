using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DotNetRepo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo Git-Welt 3");

            Console.WriteLine("Erste Änderung...");

            Console.WriteLine("Erste Änderung vom Tablet aus...");

            var dbConnect = new DBConnect();
            //dbConnect.Delete();
            List<string>[] results = dbConnect.Select();
            foreach (var item in results[0])
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }

    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            //server = "localhost";
            //database = "connectcsharptomysql";
            //uid = "username";
            //password = "password";
            //string connectionString;
            //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            string connectionString = "server=127.0.0.1;uid=root;" +
                "database=test;";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert()
        {
            //            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";
            //            string query = "INSERT INTO gender (id, gender) VALUES('0', 'Pumpkin')";
            string query = "INSERT INTO gender (gender) VALUES('Pumpkin2')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SQL-Exception: {0}", ex.Message);
                }
                finally
                {
                    //close connection
                    this.CloseConnection();
                }


            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            //            string query = "DELETE FROM tableinfo WHERE name='John Smith'";
            string query = "DELETE FROM gender WHERE gender LIKE 'Pumpkin%'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM mytest";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["firstname"] + "");
                    list[2].Add(dataReader["lastname"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }


    class Tests
    {
        /// <summary>
        /// Tests the Auto Pattern
        /// </summary>
        public static void TestAuto()
        {
            AutoEinfach ae = new AutoEinfach();
            ae.StepOnIt();
            ae.StepOnIt();
            ae.StepOnIt();
            ae.StepOnIt();

            ae.Breeeeak();
            ae.Breeeeak();
            ae.Breeeeak();

            Console.WriteLine("-------------Flexibel--------------");

            AutoFlexibel af = new AutoFlexibel();
            af.StepOnIt();
            af.StepOnIt();
            Console.WriteLine("Stärkerer Motor");
            af.motor = new MotorStark();
            af.StepOnIt();
            af.StepOnIt();

            Console.WriteLine("KombiBlock");
            KombiBlockSuperStrong kbss = new KombiBlockSuperStrong();
            af.motor = kbss;
            af.bremse = kbss;

            af.StepOnIt();
            af.StepOnIt();
            af.Breeeeak();
        }

        /// <summary>
        /// Tests the Singleton Pattern
        /// </summary>
        public static void TestSingleton()
        {
            Singleton singleton = Singleton.getInstance();
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Get new Instance");
            singleton = Singleton.getInstance();
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Dispose");
            Singleton.Dispose();
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Get new Instance after disposal");
            singleton = Singleton.getInstance();
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Get BigSingleton (old object should not be replaced)");
            singleton = BigSingleton.getInstance();
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Get BigSingleton after disposal");
            Singleton.Dispose();
            singleton = BigSingleton.getInstance();
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
            Console.WriteLine("Singleton Data: {0}", singleton.Data);
        }
    }

    class AutoEinfach
    {
        private int Speed = 0;

        public AutoEinfach()
        {
            this.Speed = 0;
            this.CommentSpeed();
        }

        private void CommentSpeed()
        {
            Console.WriteLine("Ich fahre mit {0} km/h. ", this.Speed);
        }

        public int GetSpeed()
        {
            return this.Speed;
        }

        public void StepOnIt()
        {
            this.Speed++;
            this.CommentSpeed();
        }

        public void Breeeeak()
        {
            this.Speed--;
            this.CommentSpeed();
        }

    }

    class AutoFlexibel
    {
        private int Speed = 0;
        public IMotor motor { get; set; }
        public IBremse bremse { get; set; }

        public AutoFlexibel()
        {
            this.Speed = 0;
            this.CommentSpeed();
            this.motor = new MotorSchwach();
            this.bremse = new BremseSchwach();
        }

        private void CommentSpeed()
        {
            Console.WriteLine("Ich fahre mit {0} km/h. ", this.Speed);
        }

        public int GetSpeed()
        {
            return this.Speed;
        }

        public void StepOnIt()
        {
            this.Speed = motor.StepOnIt(this.Speed);
            this.CommentSpeed();
        }

        public void Breeeeak()
        {
            this.Speed = bremse.Breeeeak(this.Speed);
            this.CommentSpeed();
        }

    }

    interface IMotor
    {
        int StepOnIt(int currentSpeed);
    }

    interface IBremse
    {
        int Breeeeak(int currentSpeed);
    }

    class MotorSchwach : IMotor
    {

        public int StepOnIt(int currentSpeed)
        {
            return currentSpeed + 1;
        }
    }

    class MotorStark : IMotor
    {

        public int StepOnIt(int currentSpeed)
        {
            return currentSpeed + 2;
        }
    }

    class BremseSchwach : IBremse
    {
        public int Breeeeak(int currentSpeed)
        {
            return currentSpeed - 1;
        }
    }

    class BremseStark : IBremse
    {
        public int Breeeeak(int currentSpeed)
        {
            return currentSpeed - 2;
        }
    }

    class KombiBlockSuperStrong : IMotor, IBremse
    {
        public int Breeeeak(int currentSpeed)
        {
            return currentSpeed - 10;
        }

        public int StepOnIt(int currentSpeed)
        {
            return currentSpeed + 10;
        }
    }

    class Singleton
    {
        protected static Singleton instance = null;
        protected static Random random = null;

        protected int _Data;

        public int Data
        {
            get { return _Data; }
        }


        protected Singleton()
        {
            if (random == null)
            {
                random = new Random();
            }
            this._Data = random.Next(0, 1000);
        }

        public static Singleton getInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }

            return instance;
        }

        public static void Dispose()
        {
            instance = null;
        }

    }

    class BigSingleton : Singleton
    {
        private BigSingleton()
        {
            this._Data = random.Next(1001, 2000);
        }

        public static Singleton getInstance()
        {
            if (instance == null)
            {
                instance = new BigSingleton();
            }

            return instance;
        }

    }
}
