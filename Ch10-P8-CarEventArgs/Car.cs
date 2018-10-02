using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10_P8_CarEventArgs
{
    class Car
    {
        // Internal state data.
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; } = 100;
        public string PetName { get; set; }
        // Is the car alive or dead?
        private bool carIsDead;
        
        // Class constructors.
        public Car() { }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }

        // 1) Define a delegate type.
        //public delegate void CarEngineHandler(string msgForCaller);
        public delegate void CarEngineHandler(object sender, CarEventArgs e);

        // This car can send these events.
        public event CarEngineHandler Exploded;
        public event CarEngineHandler AboutToBlow;

        // 2) Define a member variable of this delegate.
        private CarEngineHandler listOfHandlers;
        
        // 3) Add registration function for the caller.
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            //listOfHandlers = methodToCall;
            //listOfHandlers += methodToCall;

            if (listOfHandlers == null)
                listOfHandlers = methodToCall;
            else
                listOfHandlers = Delegate.Combine(listOfHandlers, methodToCall) as CarEngineHandler;
        }
        public void UnRegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers -= methodToCall;
        }

        // 4) Implement the Accelerate() method to invoke the delegate's
        // invocation list under the correct circumstances.
        public void Accelerate(int delta)
        {
            // If this car is "dead," send dead message.
            if (carIsDead)
            {
                Exploded?.Invoke(this , new CarEventArgs("Sorry, this car is dead..."));
            }
            else
            {
                CurrentSpeed += delta;
                // Almost dead?
                if (10 == MaxSpeed - CurrentSpeed  && AboutToBlow != null)
                {
                    AboutToBlow(this , new CarEventArgs("Careful buddy! Gonna blow!"));
                }
                // Still OK!
                if (CurrentSpeed >= MaxSpeed)
                    carIsDead = true;
                else
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
            }
        }
    }
}
