using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
    Создать приложение-напоминалку. Пользователь может создавать таймер,
    который будет запускаться раз в указанное
    время и предупреждать о каком-то деле, которое тоже задаёт пользователь.
     */

namespace TimerHW
{
    class Program
    {
        static void Main(string[] args)
        {
            UserEvent userEvent = new UserEvent();

            Console.WriteLine("Enter your event name");
            userEvent.EventName = Console.ReadLine();

            Console.WriteLine("Enter your event time. Example: 13/05/2018 19:39:59");

            try
            {
                userEvent.EventTime = Convert.ToDateTime(Console.ReadLine());
                if (userEvent.EventTime <= DateTime.Now)
                {
                    throw new FormatException("You can't select past or current time");
                }
            }
            catch (FormatException m)
            {
                Console.WriteLine("Incorrect input. " + m.Message);
                Console.ReadLine();
                return;
            }

            var test = userEvent.EventTime.Subtract(DateTime.Now).TotalMilliseconds;
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Reminder);
            // создаем таймер
            Timer timer = new Timer(tm, userEvent, (int)(userEvent.EventTime - DateTime.Now).TotalMilliseconds, Timeout.Infinite);

            Console.ReadLine();
        }

        static void Reminder(object userEvent)
        {
            Console.WriteLine("User, you have an event:  \"" + (userEvent as UserEvent).EventName + "\"");
        }
    }
}
