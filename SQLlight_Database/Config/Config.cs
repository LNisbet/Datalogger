using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SQLight_Database
{
    static internal class Config
    {
        #region Users File
        internal const string UsersPathName = "users.json";
        #endregion

        #region Tag Table
        internal const string TagsTableName = "Tags";

        static internal List<ColumnDescription> TagsTableDescription =
        [
            new ColumnDescription("Tags", "TEXT", "PRIMARY KEY")
        ];

        static internal readonly List<string> StandardTags = 
        [
            "UpperBody", 
            "LowerBody",
            "Core",
            "Fingers",
            "Endurance",
            "Strength",
            "Power",
            "Flexibility"
        ];
        #endregion

        #region Exercise Table
        internal const string ExercieseTableName = "Exercises";

        static internal List<ColumnDescription> ExerciseTableDescription =
        [
            new ColumnDescription("Name", "VARCHAR(32)", "PRIMARY KEY"),
            new ColumnDescription("Tags", "TEXT"),
            new ColumnDescription("Unit1", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Unit2", "VARCHAR(20)"),
            new ColumnDescription("Unit3", "VARCHAR(20)"),
            new ColumnDescription("Unit4", "VARCHAR(20)"),
            new ColumnDescription("Description", "TEXT")
        ];

        static internal readonly List<Exercise> StandardExercises =
        [
            new Exercise("Weight", [], Exercise.Units.Kg),
            new Exercise("Left Little Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand little finger in a drag position"),
            new Exercise("Left Little Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand little finger in a Half crimp position"),
            new Exercise("Left Ring Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand ring finger in a drag position"),
            new Exercise("Left Ring Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand ring finger in a Half crimp position"),
            new Exercise("Left Middle Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand middle finger in a drag position"),
            new Exercise("Left Middle Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand middle finger in a Half crimp position"),
            new Exercise("Left Index Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand index finger in a drag position"),
            new Exercise("Left Index Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand index finger in a half crimp position"),
            new Exercise("Right Little Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand little finger in a drag position"),
            new Exercise("Right Little Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand little finger in a half crimp position"),
            new Exercise("Right Ring Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand ring finger in a drag position"),
            new Exercise("Right Ring Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand ring finger in a half crimp position"),
            new Exercise("Right Middle Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand middle finger in a drag position"),
            new Exercise("Right Middle Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand middle finger in a half crimp position"),
            new Exercise("Right Index Finger Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand index finger in a drag position"),
            new Exercise("Right Index Finger Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand index finger in a half crimp position"),
            new Exercise("Left Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand in a half crimp position"),
            new Exercise("Left Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand in a drag position"),
            new Exercise("Left Full Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Left hand in a full crimp position"),
            new Exercise("Right Half Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand in a half crimp position"),
            new Exercise("Right Open Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand in a drag position"),
            new Exercise("Right Full Crimp", ["Fingers", "Strength"], Exercise.Units.Kg,null,null,null,"Right hand in a full crimp position"),
            new Exercise("Pullup", ["UpperBody", "Strength"], Exercise.Units.Kg,Exercise.Units.Reps,null,null,"Full ROM pullup")
        ];
        #endregion

        #region Logs Table
        internal const string LogsTableName = "ExerciseLogs";

        static internal List<ColumnDescription> LogTableDescription =
        [
            new ColumnDescription("Id", "INTEGER PRIMARY KEY"),
            new ColumnDescription("Date", "DATE", "NOT NULL"),
            new ColumnDescription("Exercise", "VARCHAR(20)", "NOT NULL"),
            new ColumnDescription("Value1", "FLOAT", "NOT NULL"),
            new ColumnDescription("Value2", "FLOAT"),
            new ColumnDescription("Value3", "FLOAT"),
            new ColumnDescription("Value4", "FLOAT"),
            new ColumnDescription("Note", "TEXT")
        ];
        #endregion
    }
}
