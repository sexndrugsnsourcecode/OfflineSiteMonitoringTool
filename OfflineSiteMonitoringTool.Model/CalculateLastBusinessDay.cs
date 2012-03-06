using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class CalculateLastBusinessDay
    {
        private IRepository _repository;
        private DateTime _currentDate;

        public CalculateLastBusinessDay(IRepository repository, DateTime currentDate)
        {
            _repository = repository;
            _currentDate = currentDate;

            setLastBusinessDay();
        }
        private DateTime lastBusinessDay;

        private void setLastBusinessDay()
        {
            DateTime currentDate = _currentDate;
            lastBusinessDay = _repository.GetLastBusinessDay(currentDate);
        }

        public DateTime GetLastBusinessDay { get { return lastBusinessDay; } }
    }
}
