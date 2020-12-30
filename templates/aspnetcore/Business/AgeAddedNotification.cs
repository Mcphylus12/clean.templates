using MediatR;
using System;

namespace Business
{
    internal class AgeAddedNotification : INotification
    {
        private int id;

        public AgeAddedNotification(int id)
        {
            this.id = id;
        }

        public int Id => id;
    }
}