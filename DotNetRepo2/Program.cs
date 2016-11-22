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
