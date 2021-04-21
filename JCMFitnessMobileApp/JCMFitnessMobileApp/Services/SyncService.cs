using JCMFitnessMobileApp.LocalDB;
using JCMFitnessMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JCMFitnessMobileApp.Services
{
    public class SyncService
    {
        private readonly IFitnessService _fitnessService;
        private readonly ILocalDatabase _localDatabase;

        // I keep a record of the last sync time.
        private DateTimeOffset _lastSync = DateTimeOffset.MinValue;

        private IList<Workout> _rows = new List<Workout>();

        public SyncService(IFitnessService fitnessService, ILocalDatabase localDatabase)
        {
          
            _fitnessService = fitnessService;
            _localDatabase = localDatabase;
        }

        public IFitnessService FitnessService { get; }
        public ILocalDatabase LocalDatabase { get; }

        public async void Sync()
        {
            // All rows that have changed since last sync
            _rows = await _localDatabase.GetWorkouts();
            var changed = _rows.Where(x => x.LastUpdated >= _lastSync || (x.IsDeleted == true)).ToList();

            await _fitnessService.PushSyncWorkout(changed.Cast<Workout>().ToList());
/*

            // Pull sync is just getting all records that have changed since that date.
            foreach (var row in _fitnessService.PullSync(_lastSync))
                if (!_rows.Any(x => x.Id == row.Id)) // Does not exist, hence insert 
                    InsertRow(new ClientTableSchema(row));
                else if (row.Deleted.HasValue)
                    DeleteRow(row.Id);
                else
                    UpdateRow(new ClientTableSchema(row));

            _lastSync = DateTimeOffset.Now;*/
        }

    }
}
