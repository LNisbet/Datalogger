using DataLogger.Views;
using SQLight_Database;
using SQLight_Database.Tables.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    public class FingerStatisticsStore
    {
        private readonly BasicStatisticsStore _basicStatisticsStore;

        internal ObservableCollection<FingerStatistics> ListOfFingerStatistics { get; private set; } = [];

        public FingerStatisticsStore(BasicStatisticsStore basicStatisticsStore)
        {
            _basicStatisticsStore = basicStatisticsStore;
        }

        internal FingerStatistics Get(FingerStatistics.Hand hand, DateOnly startDate, DateOnly endDate)
        {
            FingerStatistics stat = ListOfFingerStatistics
                .FirstOrDefault(st => st.SetHand == hand && st.StartDate == startDate && st.EndDate == endDate)
                ?? new FingerStatistics(_basicStatisticsStore, hand, startDate, endDate);

            if (!ListOfFingerStatistics.Contains(stat))
                ListOfFingerStatistics.Add(stat);

            return stat;
        }
    }
}
