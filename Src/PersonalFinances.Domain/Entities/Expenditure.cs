using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinances.Domain.Entities
{
    public class Expenditure : EntityBase
    {
        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public decimal Value { get; private set; }

        public int ExpenditureTypeId { get; private set; }

        public virtual ExpenditureType ExpenditureType { get; private set; }


        protected Expenditure() { }

        public Expenditure(string name, DateTime? date, int expenditureTypeId, decimal value) 
        {
            Name = name;
            ExpenditureTypeId = expenditureTypeId;
            Date = date ?? DateTime.Now;
            Value = value;
        }

        public void Update(string name, int expenditureTypeId, DateTime date, decimal value) 
        {
            Name = name;
            ExpenditureTypeId = expenditureTypeId;
            Date = date;
            Value = value;
        }
    }
}