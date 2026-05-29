using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace WinFormsApp1
{
	internal class Ticket
	{
		public Ticket(int speedLimit, int actualSpeed, int previousVio) {
			this.speedLimit = speedLimit;
			this.actualSpeed = actualSpeed;
			this.previousVio = previousVio;
		}
		private int speedLimit;
		private int actualSpeed;
		private int previousVio;

		public double ticketCost;
        public double courtCost;
        public double totalCost;

        public void CalculateTicketCost()
		{
            int overAmount = (actualSpeed - speedLimit);
			double cost = overAmount * 10;
			this.ticketCost = cost;
        }

        public void CalculateTotalCost()
		{
			CalculateTicketCost();
            float cost = 57.50f;
			cost += (previousVio * 30);
			this.courtCost = cost;
			this.totalCost = cost + this.ticketCost;
		}

    }
}
