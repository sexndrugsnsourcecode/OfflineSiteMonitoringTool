using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class CheckToolShouldBeRun
    {
        private IRepository _repository;
        private DateTime _lastBusinessDay;

        public CheckToolShouldBeRun(IRepository repository, DateTime lastBusinessDay)
        {
            _repository = repository;
            _lastBusinessDay = lastBusinessDay;

            checkDataHasBeenUpdatedSinceLastBusinessDay();

            if (hasDataBeenUpdatedSinceLastBusinessDay == true)
                shouldToolBeRun = true;
            else
                shouldToolBeRun = false;
        }

        private bool hasDataBeenUpdatedSinceLastBusinessDay;

        private void checkDataHasBeenUpdatedSinceLastBusinessDay()
        {
            hasDataBeenUpdatedSinceLastBusinessDay = _repository.HasDataBeenUpdatedSinceLastBusinessDay(_lastBusinessDay);
        }

        private bool shouldToolBeRun = false;

        public bool ShouldToolBeRun { get { return shouldToolBeRun; } }
    }
}
