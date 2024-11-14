using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataLogger.Models
{
    internal class FingerStatistics
    {
        internal Hand SetHand { get;}
        internal BasicStatistics LittelFingerOpen {  get;}
        internal BasicStatistics LittelFingerHalf { get;}

        internal BasicStatistics RingFingerOpen { get;}
        internal BasicStatistics RingFingerHalf { get;}

        internal BasicStatistics MiddleFingerOpen { get;}
        internal BasicStatistics MiddleFingerHalf { get;}

        internal BasicStatistics IndexFingerOpen { get;}
        internal BasicStatistics IndexFingerHalf { get;}

        internal FingerStatistics(BasicStatisticsList basicStatisticsList, Hand hand, DateOnly startDate, DateOnly endDate) 
        {
            SetHand = hand;
            LittelFingerOpen = basicStatisticsList.GetStatisticsForExercise($"{hand} Little Finger Open Crimp", startDate, endDate);
            LittelFingerHalf = basicStatisticsList.GetStatisticsForExercise($"{hand} Little Finger Half Crimp", startDate, endDate);

            RingFingerOpen = basicStatisticsList.GetStatisticsForExercise($"{hand} Ring Finger Open Crimp", startDate, endDate);
            RingFingerHalf = basicStatisticsList.GetStatisticsForExercise($"{hand} Ring Finger Half Crimp", startDate, endDate);

            MiddleFingerOpen = basicStatisticsList.GetStatisticsForExercise($"{hand} Middle Finger Open Crimp", startDate, endDate);
            MiddleFingerHalf = basicStatisticsList.GetStatisticsForExercise($"{hand} Middle Finger Half Crimp", startDate, endDate);

            IndexFingerOpen = basicStatisticsList.GetStatisticsForExercise($"{hand} Index Finger Open Crimp", startDate, endDate);
            IndexFingerHalf = basicStatisticsList.GetStatisticsForExercise($"{hand} Index Finger Half Crimp", startDate, endDate);
        }

        internal BasicStatistics SelectetBasicStatistic(Fingers fingers, Crimp crimp)
        {
            return crimp switch
            {
                Crimp.Half => fingers switch
                {
                    Fingers.Little => LittelFingerHalf,
                    Fingers.Ring => RingFingerHalf,
                    Fingers.Middle => MiddleFingerHalf,
                    Fingers.Index => IndexFingerHalf,
                    _ => throw new NotImplementedException()
                },
                Crimp.Open => fingers switch
                {
                    Fingers.Little => LittelFingerOpen,
                    Fingers.Ring => RingFingerOpen,
                    Fingers.Middle => MiddleFingerOpen,
                    Fingers.Index => IndexFingerOpen,
                    _ => throw new NotImplementedException()
                },
                _ => throw new NotImplementedException(),
            };
        }

        internal float? GetCrimpTotal(Crimp crimp, BasicStatistics.Options option)
        {
            return Enum.GetValues(typeof(Fingers))
                .Cast<Fingers>()
                .Sum(finger => SelectetBasicStatistic(finger, crimp).SelectetStatistic(option));
        }

        #region Enums
        internal enum Hand
        {
            Left,
            Right
        }

        internal enum Fingers
        {
            Little,
            Ring,
            Middle,
            Index
        }

        internal enum Crimp
        {
            Half,
            Open
        }
        #endregion
    }
}
