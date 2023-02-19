using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrputt.Models
{
    public class StationData
    {
        public bool HasChangedOrIsNew { get; set; }
        public string Id { get; set; }

        public bool IsFavourite { get; set; }

        public string StationName { get; set; }

        public List<ElevatorInfo> Elevators { get; set; }

        public List<ElevatorInfo> Escalators { get; set; }

    }

    public class ElevatorInfo
    {
        public bool IsElevator { get; set; }
        public ElevatorWarningState ElevatorWarningState { get; set; }
        public string WarningType { get; set; }
        public string LocationText { get; set; }
    }

    public enum ElevatorWarningState
    {
        Unchanged = 0,
        WarningChanged = 1,
        Fixed = 2,
        New = 3
    }
}
