using DomainEvents.Src.Domain.Task.Events;
using DomainEvents.Src.Domain.Task.Services;
using DomainEvents.Src.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.Domain.Task.Entities
{
    public enum TicketStates
    {
        Opened = 1,
        ReadyForAssignment = 2,
        Assigned = 3,
        Closed = 4,
        Review = 5,
        Completed = 6
    }

    public enum TicketDifficultyLevels
    {
        Simple = 1,
        Easy = 2,
        Medium = 3,
        Hard = 4,
        Extreme = 5
    }

    public enum TicketPriorityLevels
    {
        Low = 1,
        Normal = 2,
        Important = 3,
        Critical = 4
    }

    public class Ticket: Entity, IAggregateRoot
    {
        public string Id { get; private set; }
        public string Subject { get; private set; }
        public string Description { get; private set; }
        public int CurrentState { get; private set; }
        public DateTime CurrentDate { get; private set; }
        public string OwnerId { get; private set; }

        /// <summary>
        /// // Taskı atayacağım çalışan Idsi
        /// </summary>
        public string AssignedTo { get; private set; }

        /// <summary>
        /// Ticket Open yapılırken zorunlu alanlar değil
        /// </summary>
        public int LevelOfDifficulty { get; private set; }

        /// <summary>
        /// Sadece Set Technical Detail için gerekli
        /// </summary>
        public int LevelOfPriority { get; private set; }



        public Ticket(string subject, string description, string ownerId)
        {
            Id = Guid.NewGuid().ToString();

            if(string.IsNullOrEmpty(ownerId))
            {
                throw new Exception("Ticket'ı oluşturan kişiyi girmelisiniz");
            }

            this.OwnerId = ownerId;
            SetSubject(subject);
            SetDescription(description);
        }

        private void SetSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new Exception("Ticket konusu boş geçilemez");
            }

            if(subject.Length > 50)
            {
                throw new Exception("Ticket konusu 50 karakterden fazla olmamalıdır");
            }

            Subject = subject.Trim();
        }


        private void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new Exception("Ticket açıklaması boş geçilemez");
            }

            if (description.Length > 500)
            {
                throw new Exception("Ticket açıklaması en fazla 50 karakter  olmamalıdır");
            }

            Description = description.Trim();
        }

        /// <summary>
        /// Taskın assign (atamak için kullanırız)
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="assignedTo"></param>
        public void Assign(string assignedTo, ITicketAssignmentDomainService dService)
        {
            if(string.IsNullOrEmpty(assignedTo))
            {
                throw new Exception("Atanacak çalışan göndermek zorunludur!");
            }

            dService.Assign(Id, assignedTo);

            AssignedTo = assignedTo;
            CurrentState = (int)TicketStates.Assigned;
            CurrentDate = DateTime.Now;

            
          

        }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="openedByEmailAddress"></param>
      /// <param name="openedByName"></param>
        public void OpenTicket(string openedByEmailAddress,string openedByName)
        {
            CurrentState = (int)TicketStates.Opened;
            CurrentDate = DateTime.Now;

            var @event = new TicketOpened(Subject, Description, Id, openedByEmailAddress, CurrentDate, openedByName);

            AddEvents(@event);

        }

      

        public void SetTechDetail(TicketPriorityLevels levelOfPriority,TicketDifficultyLevels levelOfDifficulty)
        {
            LevelOfDifficulty = (int)levelOfDifficulty;
            LevelOfPriority = (int)levelOfPriority;
            CurrentState = (int)TicketStates.ReadyForAssignment;
            CurrentDate = DateTime.Now;

            var @event = new TicketReadyForAssignment(Id, CurrentDate, TicketStates.ReadyForAssignment);

            AddEvents(@event);
        }


        public void OpenTicket(string openedByEmailAddress, string openedByName, string managerEmailAddress)
        {

            CurrentState = (int)TicketStates.Opened;
            CurrentDate = DateTime.Now;

            var @event = new TicketOpened(Subject, Description, Id, openedByEmailAddress, CurrentDate, openedByName,managerEmailAddress);


            AddEvents(@event);

        }




    }
}
